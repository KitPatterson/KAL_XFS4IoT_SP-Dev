// (C) KAL ATM Software GmbH, 2021

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.IO.Ports;
using CardReader;
using XFS4IoT;
using XFS4IoTFramework.CardReader;
using XFS4IoTFramework.Common;

namespace KAL.XFS4IoTSP.CardReader.HOTS
{
    public class V2BF : ICardReaderDevice
    {
        public V2BF(ILogger Logger)
        {
            Logger.IsNotNull($"Invalid parameter received in the {nameof(V2BF)} constructor. {nameof(Logger)}");
            this.Logger = Logger;

            string portName = ConfigurationManager.AppSettings.Get(Constants.ComPort);
            if (string.IsNullOrEmpty(portName))
            {
                portName = Default.ComPort;
            }

            serialComms = new SerialComms(portName, 19200, StopBits.One, Parity.None, 8, Handshake.None);

            if (!serialComms.Open())
            {
                Logger.Warning(Constants.DeviceClass, $"Failed to open comport. {portName}");
                return;
            }

            // Activated the P/F sensor to eject the card on power down
            serialComms.SendAndReceive("CN11").Wait(Default.Timeout);

            // Read the firmware version
            Task<byte[]> task = serialComms.SendAndReceive("CV0");
            if (task.Wait(Default.Timeout) &&
                task.IsCompleted &&
                task.Result.Length >= 6 &&
                task.Result.ToString().StartsWith("PV0"))
            {
                // Log the firmware version:
                for (int i = 0; i < task.Result.Length; i++)
                {
                    // Replace unprintable chars with spaces
                    if (task.Result[i] < 0x20 ||
                        task.Result[i] > 0x7F)
                    {
                        task.Result[i] = 0x20;
                    }
                }

                CapFirmwareVersion = task.Result.ToString()[5..];
            }

            // Send command to get bin counter from device
            task = serialComms.SendAndReceive("CQ0");
            if (task.Wait(Default.Timeout) ||
                !task.IsCompleted)
            {
                Logger.Warning(Constants.DeviceClass, $"Failed to process command CQ0.");
                return;
            }

            if (task.Result.ToString().StartsWith("PQ0") &&
                task.Result.Length >= 8)
            {
                // Check the status code
                int rc = ProcessStatusCode(task.Result[4..]);
                switch (rc)
                {
                    case 0:     // No card present
                    case 1:     // Card in jaws (Note: We don't need to read a card that is in jaws,
                    case 2: // A card is in the C/R
                    case 4: // A card is in Read Start Position of the MM Sensor
                    case 10: // IC Contact is pressed to the ICC
                    case 11: // ICC is in the Activation Status
                    case 20: // Transmission to the ICC is completed.
                        break;  // This is a good response; continue
                    default:
                        Logger.Warning(Constants.DeviceClass, $"Unexpected message received from the device. ${task.Result}");
                        return;
                }

                //Convert the string "031" into number 31
                CapturedCards = (uint)((task.Result[5] - '0') * 100 + (task.Result[6] - '0') * 10 + (task.Result[7] - '0'));

                if (CapturedCards >= CapMaxCapturedCards)
                {
                    RetainBinStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.RetainBinEnum.Full;
                }
                else if (CapturedCards >= (uint)(((3 * (long)CapMaxCapturedCards) / 4) + 0.5))
                {
                    RetainBinStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.RetainBinEnum.High;
                }
                else
                {
                    RetainBinStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.RetainBinEnum.Ok;
                }
            }
            else if (task.Result.ToString().StartsWith("NQ0") &&
                     task.Result.Length >= 5)
            {
                // Check the error code
                Logger.Warning(Constants.DeviceClass, $"Unexpected message received from the device. ${task.Result}");
                return;
            }
            else
            {
                Logger.Warning(Constants.DeviceClass, $"Unexpected message received from the device. ${task.Result}");
                DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                return;
            }

            // Find out we have a card reader that supports chip cards (and what tracks are supported)
            task = serialComms.SendAndReceive("CN0");
            if (task.Wait(Default.Timeout) ||
                task.Result.Length == 0)
            {
                Logger.Warning(Constants.DeviceClass, $"Failed to process command CN0.");
                return;
            }

            if (task.Result.ToString().StartsWith("PN0") &&
                task.Result.Length >= 5)
            {
                // Check the status code
                int rc = ProcessStatusCode(task.Result[4..]);
                switch (rc)
                {
                    case 0:     // No card present
                    case 1:     // Card in jaws (Note: We don't need to read a card that is in jaws,
                    case 2: // A card is in the C/R
                    case 4: // A card is in Read Start Position of the MM Sensor
                    case 10: // IC Contact is pressed to the ICC
                    case 11: // ICC is in the Activation Status
                    case 20: // Transmission to the ICC is completed.
                        break;  // This is a good response; continue
                    default:
                        Logger.Warning(Constants.DeviceClass, $"Unexpected message received from the device. ${task.Result}");
                        return;
                }

                if (task.Result.Length > 9)
                {
                    switch ((char)task.Result[5 + 2]) // track iso #1
                    {
                        case '0':
                            break;
                        case '1':
                        default:
                            CapReadTrack1 = true;
                            break;
                    }
                    switch ((char)task.Result[5 + 3]) // track iso #2
                    {
                        case '0':
                            break;
                        case '1':
                        default:
                            CapReadTrack2 = true;
                            break;
                    }
                    switch ((char)task.Result[5 + 4]) // track iso #3
                    {
                        case '0':
                            break;
                        case '1':
                            CapReadTrack3 = true;
                            break;
                    }
                    // JIS 2 not supported
                }

                //If some data, this card reader support smart cards
                if (task.Result.Length > 9 &&
                    task.Result[5 + 1] == '1') // Presence of IC Contact
                {
                    CapChipProtocol0 = true;
                    CapChipProtocol1 = true;

                    // No IFSD check
                    serialComms.SendAndReceive("CY0").Wait(Default.Timeout);

                    // Read the entry for ICC standard, default to EMV 3.1.1
                    serialComms.SendAndReceive("CY105").Wait(Default.Timeout);

                    // Set monitoring time for reception from IC card
                    serialComms.SendAndReceive("CY240").Wait(Default.Timeout);

                    // No IFSD check
                    serialComms.SendAndReceive("CY31").Wait(Default.Timeout);

                    // No TCK check
                    serialComms.SendAndReceive("CY41").Wait(Default.Timeout);
                }
            }
        }

        public async Task<XFS4IoT.CardReader.Responses.ReadRawDataPayload> ReadRawData(ICardReaderConnection connection,
                                                                                       XFS4IoT.CardReader.Commands.ReadRawDataPayload payload,
                                                                                       CancellationToken cancellation)
        {
            DateTime startTime = DateTime.Now;

            bool ReAccept;
            do
            {
                ReAccept = false;
                // Check if we already have a card: if so, return right away
                PresentStatus cardPresentStatus = await CardPresent();

                if (cardPresentStatus != PresentStatus.Error)
                {
                    return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                               "CardPresent Error");
                }

                // Reset all track info from previous card
                if (cardPresentStatus == PresentStatus.NoCard ||
                    cardPresentStatus == PresentStatus.InJaws)
                {
                    string command = cardPresentStatus == PresentStatus.InJaws ? "C40" : "C:0";

                    int actualTimeout = payload.Timeout - (int)((startTime - DateTime.Now).TotalSeconds);

                    var task = serialComms.SendAndReceive(command);

                    if (actualTimeout <= 0 &&
                        await Task.WhenAny(task, Task.Delay(actualTimeout, CancellationToken.None)) != task)
                    {
                        // Disable reader
                        cardPresentStatus = await DisableReader();
                        if (cardPresentStatus == PresentStatus.NoCard)
                        {
                            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.TimeOut,
                                                                                       "Timeout");
                        }
                        else if (cardPresentStatus != PresentStatus.Present)
                        {
                            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.TimeOut,
                                                                                       "DisableReader Error");
                        }

                        connection.MediaInsertedEvent();
                        // read card tracks later
                    }

                    task.Result.IsNotNull($"Unexpected response received after accept command issued.");

                    if (task.Result.Length >= 3 &&
                        task.Result[0] == 'P' &&
                        task.Result[1] == command[1] &&
                        task.Result[2] == command[2])
                    {
                        // Check the status code
                        int rc = ProcessStatusCode(task.Result[4..]);
                        switch (rc)
                        {
                            case 0:     // No card present
                            case 1:     // Card in jaws
                                if (task.Result[1] == ':') // We have to poll untill the card has been 
                                                           // inserted
                                {
                                    actualTimeout = payload.Timeout - (int)((startTime - DateTime.Now).TotalSeconds);
                                    CardInsertedResult insertedResult = await WaitForCardInserted(actualTimeout);
                                    switch (insertedResult)
                                    {
                                        case CardInsertedResult.CardInserted:     // card has been inserted
                                            break;  // end case 0:  // card has been inserted

                                        case CardInsertedResult.Timeout:
                                            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.TimeOut,
                                                                                                       "Timeout");

                                        case CardInsertedResult.Cancelled:
                                            // Disable the reader before returning canceled code
                                            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Canceled,
                                                                                                      "Card Insertion Error");
                                        default:
                                            Logger.Warning(Constants.DeviceClass, $"Invalid response from device when monitoring a card to be inserted. {command}");
                                            DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                                            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                                                       "Card Insertion Error");
                                    }
                                }
                                break;
                            case 2: // A card is in the C/R
                            case 4: // A card is in Read Start Position of the MM Sensor
                            case 10: // IC Contact is pressed to the ICC
                            case 11: // ICC is in the Activation Status
                            case 20: // Transmission to the ICC is completed.
                                break;      // Good response, continue
                            default:
                                Logger.Warning(Constants.DeviceClass, $"Invalid response from device when trying to accept a card. {command}");
                                DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                                return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                                           "Accept Error");
                        }
                    }
                    else if (task.Result.Length >= 3 &&
                             task.Result[0] == 'N' &&
                             task.Result[1] == command[1] &&
                             task.Result[2] == command[2])
                    {
                        // Check the error code
                        int rc = await ProcessErrorCode(task.Result[4..]);
                        switch (rc)
                        {
                            case 10:    // Card jam
                            case 15:    // Card jam on re-intaken
                            case 16:    // Card jam at the rear-end
                                Logger.Warning(Constants.DeviceClass, $"Media jam during card entry. {command}");
                                DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                                MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Jammed;
                                return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                                           "Card jammed");
                            case 20:    // Too long card
                            case 21:    // Too short card
                                return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                                           rc == 20 ? "Card too long" : "Card too short",
                                                                                           rc == 20 ? XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.ErrorCardTooLong : XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.ErrorCardTooShort);
                            case 60:    // Card taken out when re-intake, this happens for example
                                        // when the card is in front of the shutter before the accept command
                                        // is dispatched; in this case the shutter opes briefly and if the card is not
                                        // pushed in immediately, closes again and the device returns error 60
                                if ((int)((DateTime.Now - startTime).TotalSeconds) > payload.Timeout)
                                {
                                    return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.TimeOut,
                                                                                               "Timeout");
                                }
                                // If there is still time left re-accept the card by jumping to the start of this function,
                                // else return timeout
                                ReAccept = true;
                                break;

                            default:
                                Logger.Warning(Constants.DeviceClass, $"Invalid response from device when trying to accept a card. {command}");
                                DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                                return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                                           "Accept Error");
                        }
                    }
                    else
                    {
                        Logger.Warning(Constants.DeviceClass, $"Invalid response from device when trying to accept a card. {command}");
                        DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                        return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError,
                                                                                   "Accept Error");
                    }
                }
            } while (ReAccept);

            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Present;

            connection.MediaInsertedEvent();

            // now card is present and read tracks
            List<Track> readTracks = new List<Track>();
            if ((bool)payload.Track1)
                readTracks.Add(Track.Track1);
            if ((bool)payload.Track2)
                readTracks.Add(Track.Track2);
            if ((bool)payload.Track3)
                readTracks.Add(Track.Track3);

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum completionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success;
            List<XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass> DataClass = new List<XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass>();

            foreach (Track track in readTracks)
            {
                ReadTrackResult result = await ReadTrack(track);
                XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum xfsTrack = XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum.Track1;

                if (track == Track.Track2)
                    xfsTrack = XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum.Track2;
                else if (track == Track.Track3)
                    xfsTrack = XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum.Track3;

                if (result.HardwareError)
                    completionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError;

                DataClass.Add(new XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass(xfsTrack, result.MediaStatus, result.Base64Data));
            }
            
            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(completionCode,
                                                                       "",
                                                                       XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.ErrorInvalidMedia,
                                                                       DataClass);
        }


        public void Enable() { }

        public Task<XFS4IoT.CardReader.Responses.FormListPayload> FormList(ICardReaderConnection connection,
                                                                           XFS4IoT.CardReader.Commands.FormListPayload payload,
                                                                           CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.QueryFormPayload> QueryForm(ICardReaderConnection connection,
                                                                             XFS4IoT.CardReader.Commands.QueryFormPayload payload,
                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.QueryIFMIdentifierPayload> QueryIFMIdentifier(ICardReaderConnection connection,
                                                                                               XFS4IoT.CardReader.Commands.QueryIFMIdentifierPayload payload,
                                                                                               CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.EMVClessQueryApplicationsPayload> EMVClessQueryApplications(ICardReaderConnection connection,
                                                                                                             XFS4IoT.CardReader.Commands.EMVClessQueryApplicationsPayload payload,
                                                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.ReadTrackPayload> ReadTrack(ICardReaderConnection connection,
                                                                             XFS4IoT.CardReader.Commands.ReadTrackPayload payload,
                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.WriteTrackPayload> WriteTrack(ICardReaderConnection connection,
                                                                               XFS4IoT.CardReader.Commands.WriteTrackPayload payload,
                                                                               CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public async Task<XFS4IoT.CardReader.Responses.EjectCardPayload> EjectCard(ICardReaderConnection connection,
                                                                                   XFS4IoT.CardReader.Commands.EjectCardPayload payload,
                                                                                   CancellationToken cancellation)
        {
            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum CompletionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success;
            // Find out if there is a card inside the device
            // If the card is not we are done, all other cases we try to eject
            PresentStatus presentStatus = await CardPresent();
            if (presentStatus == PresentStatus.NoCard)
            {
                // Should me MediaNotPresent error to the eject response
            }
            else
            {
                presentStatus = await Eject();
                if (presentStatus != PresentStatus.NoCard)
                    CompletionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError;
            }
            return new XFS4IoT.CardReader.Responses.EjectCardPayload(CompletionCode,
                                                                     presentStatus == PresentStatus.NoCard ? "ok" : "Error");
        }

        public Task<XFS4IoT.CardReader.Responses.RetainCardPayload> RetainCard(ICardReaderConnection connection,
                                                                               XFS4IoT.CardReader.Commands.RetainCardPayload payload,
                                                                               CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.ResetCountPayload> ResetCount(ICardReaderConnection connection,
                                                                               XFS4IoT.CardReader.Commands.ResetCountPayload payload,
                                                                               CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.SetKeyPayload> SetKey(ICardReaderConnection connection,
                                                                       XFS4IoT.CardReader.Commands.SetKeyPayload payload,
                                                                       CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.WriteRawDataPayload> WriteRawData(ICardReaderConnection connection,
                                                                                   XFS4IoT.CardReader.Commands.WriteRawDataPayload payload,
                                                                                   CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.ChipIOPayload> ChipIO(ICardReaderConnection connection,
                                                                       XFS4IoT.CardReader.Commands.ChipIOPayload payload,
                                                                       CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public async Task<XFS4IoT.CardReader.Responses.ResetPayload> Reset(ICardReaderConnection connection,
                                                                     XFS4IoT.CardReader.Commands.ResetPayload payload,
                                                                     CancellationToken cancellation)
        {
            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum completionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success;
            switch (payload.ResetIn)
            {
                case XFS4IoT.CardReader.Commands.ResetPayload.ResetInEnum.Eject:
                    {
                        bool result = await InitialiseDevice(false, true);
                        if (!result)
                            completionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError;
                    }
                    break;
                case XFS4IoT.CardReader.Commands.ResetPayload.ResetInEnum.NoAction:
                    {
                        bool result = await InitialiseDevice(false, false);
                        if (!result)
                            completionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError;
                    }
                    break;
                case XFS4IoT.CardReader.Commands.ResetPayload.ResetInEnum.Retain:
                    {
                        bool result = await InitialiseDevice(true, false);
                        if (!result)
                            completionCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.HardwareError;
                    }
                    break;
                default:
                    return new XFS4IoT.CardReader.Responses.ResetPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData,
                                                                         payload.ResetIn.ToString());
            }

            return new XFS4IoT.CardReader.Responses.ResetPayload(completionCode,
                                                                 "");
        }

        public Task<XFS4IoT.CardReader.Responses.ChipPowerPayload> ChipPower(ICardReaderConnection connection,
                                                                             XFS4IoT.CardReader.Commands.ChipPowerPayload payload,
                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.ParseDataPayload> ParseData(ICardReaderConnection connection,
                                                                             XFS4IoT.CardReader.Commands.ParseDataPayload payload,
                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.ParkCardPayload> ParkCard(ICardReaderConnection connection,
                                                                           XFS4IoT.CardReader.Commands.ParkCardPayload payload,
                                                                           CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.EMVClessConfigurePayload> EMVClessConfigure(ICardReaderConnection connection,
                                                                                             XFS4IoT.CardReader.Commands.EMVClessConfigurePayload payload,
                                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.EMVClessPerformTransactionPayload> EMVClessPerformTransaction(ICardReaderConnection connection,
                                                                                                               XFS4IoT.CardReader.Commands.EMVClessPerformTransactionPayload payload,
                                                                                                               CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.CardReader.Responses.EMVClessIssuerUpdatePayload> EMVClessIssuerUpdate(ICardReaderConnection connection,
                                                                                                   XFS4IoT.CardReader.Commands.EMVClessIssuerUpdatePayload payload,
                                                                                                   CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.Common.Responses.StatusPayload> Status(ICommonConnection connection,
                                                                   XFS4IoT.Common.Commands.StatusPayload payload,
                                                                   CancellationToken cancellation)
        {
            XFS4IoT.Common.Responses.StatusPayload.CommonClass common = new XFS4IoT.Common.Responses.StatusPayload.CommonClass(
            DeviceStatus,
            new List<string>(),
            new XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass(
                XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass.FlashRateEnum.Off,
                XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass.ColorEnum.Green,
                XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass.DirectionEnum.Off),
            XFS4IoT.Common.Responses.StatusPayload.CommonClass.DevicePositionEnum.Inposition,
            (int)CapturedCards,
            XFS4IoT.Common.Responses.StatusPayload.CommonClass.AntiFraudModuleEnum.NotSupp);

            XFS4IoT.Common.Responses.StatusPayload.CardReaderClass cardReader = new XFS4IoT.Common.Responses.StatusPayload.CardReaderClass(
                MediaStatus,
                RetainBinStatus,
                XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.SecurityEnum.NotSupported,
                0,
                XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.ChipPowerEnum.PoweredOff);

            return Task.FromResult(new XFS4IoT.Common.Responses.StatusPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success,
                                                                              "ok",
                                                                              common,
                                                                              cardReader));
        }

        public Task<XFS4IoT.Common.Responses.CapabilitiesPayload> Capabilities(ICommonConnection connection, 
                                                                               XFS4IoT.Common.Commands.CapabilitiesPayload payload, 
                                                                               CancellationToken cancellation)
        {
            XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.GuideLightsClass guideLights = new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.GuideLightsClass(
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.GuideLightsClass.FlashRateClass(true, true, true, true),
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.GuideLightsClass.ColorClass(true, true, true, true, true, true, true),
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.GuideLightsClass.DirectionClass(false, false));

            XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass common = new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass(
                "1.0",
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.DeviceInformationClass(
                    "3S4YR-V2BF-01JS",
                    "",
                    "1.0",
                    "Motor Driven Card Reader",
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.DeviceInformationClass.FirmwareClass(
                    "H",
                    CapFirmwareVersion,
                    "1.0"),
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.DeviceInformationClass.SoftwareClass(
                    "KAL V2BF XFS4",
                    "1.0",
                    "1.0")),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.VendorModeIformationClass(
                    false,
                    new List<string>()),
                new List<string>(),
                guideLights,
                false,
                false,
                new List<string>(),
                false,
                false,
                false);

            XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass cardReader = new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass(
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.TypeEnum.Motor,
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.ReadTracksClass(CapReadTrack1, CapReadTrack2, CapReadTrack3, false, false, false, false, false, false, false),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.WriteTracksClass(false, false, false, false, false, false),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.ChipProtocolsClass(CapChipProtocol0, CapChipProtocol1, false, false, false, false, false),
                CapMaxCapturedCards,
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.SecurityTypeEnum.NotSupported,
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOnOptionEnum.NoAction,
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum.Eject);


            List<XFS4IoT.Common.Responses.CapabilitiesPayload.InterfacesClass> interfaces = new List<XFS4IoT.Common.Responses.CapabilitiesPayload.InterfacesClass>
            {
                new XFS4IoT.Common.Responses.CapabilitiesPayload.InterfacesClass(
                    XFS4IoT.Common.Responses.CapabilitiesPayload.InterfacesClass.NameEnum.Common,
                    new List<string>(){ "Status", "Capabilities" },
                    new List<string>(),
                    1000,
                    new List<string>()),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.InterfacesClass(
                    XFS4IoT.Common.Responses.CapabilitiesPayload.InterfacesClass.NameEnum.CardReader,
                    new List<string>{ "ReadRawData", "EjectCard", "Reset" },
                    new List<string>{ "MediaDetectedEvent", "MediaInsertedEvent", "MediaRemovedEvent", "MediaRetainedEvent", "InvalidMediaEvent" },
                    1000,
                    new List<string>())
            };

            return Task.FromResult(new XFS4IoT.Common.Responses.CapabilitiesPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success,
                                                                    "ok",
                                                                    interfaces,
                                                                    common,
                                                                    cardReader));
        }

        public Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> SetGuidanceLight(ICommonConnection connection,
                                                                                       XFS4IoT.Common.Commands.SetGuidanceLightPayload payload,
                                                                                       CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> PowerSaveControl(ICommonConnection connection,
                                                                                       XFS4IoT.Common.Commands.PowerSaveControlPayload payload,
                                                                                       CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> SynchronizeCommand(ICommonConnection connection,
                                                                                           XFS4IoT.Common.Commands.SynchronizeCommandPayload payload,
                                                                                           CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> SetTransactionState(ICommonConnection connection,
                                                                                             XFS4IoT.Common.Commands.SetTransactionStatePayload payload,
                                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> GetTransactionState(ICommonConnection connection,
                                                                                             XFS4IoT.Common.Commands.GetTransactionStatePayload payload,
                                                                                             CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public async Task WaitForCardTaken(ICardReaderConnection connection, CancellationToken cancellation)
        {
            for (; ; )
            {
                var task = CardPresent();
                if (await Task.WhenAny(task, Task.Delay(-1, cancellation)) != task)
                {
                    // it's cancelled
                    return;
                }

                if (task.Result == PresentStatus.NoCard)
                {
                    MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;
                    connection.MediaRemovedEvent();
                    return;       // Card has been taken
                }

                if (task.Result == PresentStatus.Present ||
                    task.Result == PresentStatus.InJaws)
                {
                    continue;
                }
                else
                {
                    DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                    return;
                }
            }
        }


        /*****************************************************************************\
            ProcessStatusCode
            Extract and return the two byte status codes at the start of the
            passed response.
            Returns -1 if status invalid
        \*****************************************************************************/
        private int ProcessStatusCode(byte[] StatusResponse)
        {
            Contracts.Assert(StatusResponse.Length >= 2, $"The status code must be greater than length 2 bytes.");

            int Status;

            if (StatusResponse[0] < '0' || StatusResponse[0] > '9' ||
                StatusResponse[1] < '0' || StatusResponse[1] > '9')
                return -1;

            Status = 10 * (StatusResponse[0] - '0') + (StatusResponse[1] - '0');

            // We got status information this means the device is online
            DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.Online;

            UpdatePresentStatus = false;

            switch (Status)
            {
                case 0: // No card is in C/R
                    MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;
                    break;
                case 1: // A card is in the Takeout Position
                    MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Entering;
                    break;
                case 2: // A card is in the C/R
                case 4: // A card is in Read Start Position of the MM Sensor
                case 10: // IC Contact is pressed to the ICC
                case 11: // ICC is in the Activation Status
                case 20: // Transmission to the ICC is completed.
                    MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Present;
                    UpdatePresentStatus = true;
                    break;
                case 21: // Continuous receiving Status from the ICC
                case 22: // Continuous sending Status to the ICC
                case 23: // Ends the Completion of ICC Transmission by forcedly interruption
                case 30: // In Down loading
                case 31: // Normal Completion of Down loading, Status of Initial Reset Waiting
                default:
                    DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                    UpdatePresentStatus = true;
                    break;
            }

            switch (Status)
            {
                case 0: // No card is in C/R
                case 1: // A card is in the Takeout Position
                case 2: // A card is in the C/R
                case 4: // A card is in Read Start Position of the MM Sensor
                    CardInChipContactPosition = false;
                    break;

                default:
                    // If we are not sure we assume that we are contaceted so that 
                    // the relevant commands for disconnecting the chip are called
                    CardInChipContactPosition = true;
                    break;
            }

            return Status;
        }

        /*****************************************************************************\
            ProcessErrorCode
            Extract and return the two byte error codes at the start of the
            passed response.
            Returns -1 if status invalid
        \*****************************************************************************/
        private async Task<int> ProcessErrorCode(byte[] ErrorResponse)
        {
            int Error = -1;

            if ((ErrorResponse[0] < '0' || ErrorResponse[0] > '9') ||
                ((ErrorResponse[1] < '0' || ErrorResponse[1] > '9') && ErrorResponse[1] != 'A'))
                return Error;

            if (ErrorResponse[0] == '4' && ErrorResponse[1] == 'A')
            {
                Error = 465; // 100 * 4 + '0';
            }
            else if (ErrorResponse[0] == '8' && ErrorResponse[1] == 'A')
            {
                Error = 865; // 100 * 8 + '0';
            }
            else
                Error = 10 * (ErrorResponse[0] - '0') + (ErrorResponse[1] - '0');

            if (Error == 40 ||
                Error == 41 ||
                Error == 42 ||
                Error == 43 ||
                Error == 44 ||
                Error == 45 ||
                Error == 46 ||
                Error == 49)
            {
                Logger.Warning(Constants.DeviceClass, $"Read error/warning {Error} returned from card reader device, this may be due to a invalid card.");
            }
            else
            {
                string message = string.Empty;

                if (Error == 465)
                    message = $"Error/warning 4A returned from card reader device.";
                else if (Error == 865)
                    message = $"Error/warning A returned from card reader device.";
                else
                    message = $"Error/warning {Error} returned from card reader device.";

                Logger.Warning(Constants.DeviceClass, message);
            }

            // We got status information this means the device is online, 
            // but we might have an hardware error
            DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.Online;


            // We need to initialise the device after an error (exclude error 20)
            if (Error != 00 &&
                Error != 01 &&
                Error != 02 &&
                Error != 03 &&
                Error != 20 &&
                Error != 21 &&
                Error != 50 &&
                Error != 51 &&
                Error != 52 &&
                Error != 53 &&
                Error != 54 &&
                Error != 55 &&
                Error != 56)
            {
                await InitialiseDevice(false, false);
            }

            return Error;
        }

        /************************************************************************\
            InitialiseDevice

	        Initialises the device
	
        \************************************************************************/
        private async Task<bool> InitialiseDevice(bool CaptureCard, bool EjectCard)
        {
            Contracts.Assert(!(CaptureCard && EjectCard), $"Invalid parameter passed in {nameof(InitialiseDevice)}");

            // Hold card in standby - Eject card on power failure
            string command = "C022B";
            if (CaptureCard)
            {
                // Capture now - Capture card on power failure
                command = "C012B";
            }
            else if (EjectCard)
            {
                // Eject now - Eject card on power failure
                command = "C002B";
            }

            // Overwrite the reset action with the one configured via the registry
            switch (PowerOffOption)
            {
                case XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum.NoAction:
                    command += 'A';
                    break;

                case XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum.Retain:
                    command += 'C';
                    break;

                case XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum.Eject:
                default:
                    command += 'B';
                    break;
            }

            int retries = 2;
            while (retries-- > 0)
            {
                // Do we need this???
                //if (!doNotClearLine)
                //    ClearLine(WaitCallbackProc);

                var task = serialComms.SendAndReceive(command);

                if (task == await Task.WhenAny(task, Task.Delay(10000, CancellationToken.None)))
                {
                    // Got response: continue to process it
                    // Reset should return "P01" or "P06" if everythings OK
                    if (task.Result.Length >= 5 &&
                        task.Result.ToString().StartsWith("P") &&
                        command[1] == task.Result[1] &&
                        command[2] == task.Result[2])
                    {
                        DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.Online;

                        if (CaptureCard ||
                            EjectCard)
                        {
                            // Check the status code
                            int rc = ProcessStatusCode(task.Result[4..]);
                            switch (rc)
                            {
                                case 0:     // No card present
                                case 1:     // Card in jaws
                                case 2:     // A card is in the C/R
                                case 4:     // A card is in Read Start Position of the MM Sensor
                                case 10:    // IC Contact is pressed to the ICC
                                case 11:    // ICC is in the Activation Status
                                case 20:    // Transmission to the ICC is completed.
                                    break;
                                default:
                                    Logger.Warning(Constants.DeviceClass, $"Unexpected response - retry. {command}");
                                    continue;
                            }
                        }
                        return true;
                    }
                    else if (task.Result.Length >= 5 &&
                             task.Result.ToString().StartsWith("N") &&
                             task.Result[1] == command[1] &&
                             task.Result[2] == command[2])
                    {
                        // special behaviour for error code 10
                        if (task.Result.ToString()[3..].StartsWith("10"))
                        {
                            Logger.Warning(Constants.DeviceClass, "Card jammed (error response 10), do not retry to initialise the device.");
                            return false;
                        }
                        else if (task.Result.ToString()[3..].StartsWith("18") ||
                                 task.Result.ToString()[3..].StartsWith("19"))
                        {
                            // no ClearLine on N..18 or N..19
                            //doNotClearLine = true;
                        }
                        else
                        {
                            //doNotClearLine = false;
                        }
                    }
                    else
                    {
                        Logger.Warning(Constants.DeviceClass, $"Unexpected response - retry. {command}");
                        //doNotClearLine = false;
                        // carry on to retry
                    }
                }
                else
                {
                    // Timeout
                    DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                    MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Jammed;
                    Logger.Warning(Constants.DeviceClass, $"Timeout on RESET command: card reader not detected, the card must be jammed in the reader.");
                }
                retries--;
            }

            Logger.Warning(Constants.DeviceClass, "Retries exhausted when trying to RESET card reader.");
            return false;
        }

        /*****************************************************************************\
            CardPresent
            Sends status command to determine whether a card is present in the
            reader.
            Returns 0 if no card present, 
                    1 if card present and ready to read, 
                    2 if card is probably in the jaws (can't tell this for sure)
                    -1 if current command has been cancelled, 
                    -2 on hardware error.
        \*****************************************************************************/
        private enum PresentStatus
        {
            NoCard,
            Present,
            InJaws,
            Error,
        }
        private async Task<PresentStatus> CardPresent()
        {
            var task = serialComms.SendAndReceive("C15\0");
            if (task != await Task.WhenAny(task, Task.Delay(Default.Timeout, CancellationToken.None)))
            {
                MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Unknown;
                return PresentStatus.Error;
            }

            if (task.Result.Length >= 3 &&
                task.Result.ToString().StartsWith("P15"))
            {
                // Check the status code
                int rc = ProcessStatusCode(task.Result[4..]);
                switch (rc)
                {
                    case 0:     // No card present
                        MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;
                        return PresentStatus.NoCard;
                    case 1:     // Card in jaws 
                        MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Entering;
                        return PresentStatus.InJaws;
                    case 2:     // Card present
                    case 4:     // A card is in Read Start Position of the MM Sensor
                    case 10:    // IC Contact is pressed to the ICC
                    case 11:    // ICC is in the Activation Status
                    case 20:    // Transmission to the ICC is completed.
                        MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Present;
                        return PresentStatus.Present;
                    default:
                        Logger.Warning(Constants.DeviceClass, $"Invalid response from device following a CARDPRESENT command. ${task.Result}");
                        MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Unknown;
                        return PresentStatus.Error;
                }
            }

            // Card too long or too short - return card present.
            if (task.Result.ToString().StartsWith("N15"))
            {
                // Check the error code
                int rc = await ProcessErrorCode(task.Result[4..]);
                switch (rc)
                {
                    case 20:    // Too long card
                    case 21:    // Too short card
                        MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Present;
                        return PresentStatus.Present;
                    default:
                        break;
                }
            }

            if (task.Result.ToString().StartsWith("N15"))
            {
                // Check the error code
                await ProcessErrorCode(task.Result[4..]);
            }

            Logger.Warning(Constants.DeviceClass, $"Unable to obtain card present status.");
            DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Unknown;

            return PresentStatus.Error;
        }

        /*****************************************************************************\
            DisableReader
            This routine disables the card reader so cards cannot be inserted.
            The routine returns 
                0 if successfully disabled with no card in, or card in jaws 
                1 if disabled but card was entered
                -1 on hardware error
		        -2 on jam
        \*****************************************************************************/
        private async Task<PresentStatus> DisableReader()
        {
            PresentStatus presentStatus = await CardPresent();

            if (presentStatus == PresentStatus.Present)
            {
                return PresentStatus.Present;
            }

            int Retries;
            const int MaxRetries = 3;
            string command = "C: 1";
            for (Retries = 0; Retries < MaxRetries; Retries++)
            {
                // Send the command
                var task = serialComms.SendAndReceive(command);
                if (task != await Task.WhenAny(task, Task.Delay(Default.Timeout, CancellationToken.None)))
                {
                    MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Unknown;
                    return PresentStatus.Error;
                }
                if (task.Result.ToString().StartsWith("P") &&
                    task.Result.Length >= 2 &&
                    task.Result[1] == command[1] &&
                    task.Result[2] == command[2])
                {
                    // Check the status code
                    int rc = ProcessStatusCode(task.Result[4..]);
                    switch (rc)
                    {
                        case 0:     // No card present
                            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;
                            return PresentStatus.NoCard;

                        case 1:     // Card in jaws - double check the status
                            Thread.Sleep(300);
                            presentStatus = await CardPresent();
                            
                            if (presentStatus == PresentStatus.NoCard)
                            {
                                // No card in reader, we are done
                                return presentStatus;
                            }

                            if (Retries < MaxRetries)
                            {
                                await Eject();
                                Thread.Sleep(1000);
                            }
                            break;

                        case 2: // A card is in the C/R
                        case 4: // A card is in Read Start Position of the MM Sensor
                        case 10: // IC Contact is pressed to the ICC
                        case 11: // ICC is in the Activation Status
                        case 20: // Transmission to the ICC is completed.
                            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Present;
                            return PresentStatus.Present;

                        default:
                            Logger.Warning(Constants.DeviceClass, $"Invalid response from device following a CARDPRESENT command. ${task.Result}");
                            return PresentStatus.Error;
                    }
                }
                else if (task.Result.ToString().StartsWith("N") &&
                         task.Result.Length >= 2 &&
                         task.Result[1] == command[1] &&
                         task.Result[2] == command[2])
                {
                    // Check the error code
                    await ProcessErrorCode(task.Result[4..]);
                    break;
                }
                else
                {
                    Logger.Warning(Constants.DeviceClass, $"Invalid response from device following a C: 1 command. ${task.Result}");
                    break;
                }
            }

            DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Unknown;

            return PresentStatus.Error;
        }

        /*****************************************************************************\
            Eject
            Move the card to the exit jaws and return: not don't wait
            for card to be taken.
            On a dip or swipe or motorised with no card, return WFS_ERR_IDC_NOMEDIA
        \*****************************************************************************/
        private async Task<PresentStatus> Eject()
        {
            // Find out if there is a card inside the device
            // If the card is not we are done, all other cases we try to eject
            PresentStatus presentStatus = await CardPresent();
            if (presentStatus == PresentStatus.NoCard)
            {
                return presentStatus;
            }

            // To get consistent behavior werther a initialsie is required or not
            // we always eject the card with the initialise command.
            bool success = await InitialiseDevice(false, true);
            if (!success)
            {
                DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                return PresentStatus.Error;

            }

            return PresentStatus.NoCard;
        }

        /*****************************************************************************\
            WaitForCardInserted
            Wait for the card to be inserted.
            Possible return codes are 
                 0   -   card inserted
		         1   -   timeout
		         2   -   canceled
		         -1  -   hardware error
		         -2  -   jam
        \*****************************************************************************/
        private enum CardInsertedResult
        {
            CardInserted,
            Timeout,
            Cancelled,
            Error,
            Jammed,
        }
        private async Task<CardInsertedResult> WaitForCardInserted(int Timeout)
        {
            // Normally when this routine is called, the media is in the jaws
            // we don't get any unsolic from the reader when its inserted
            // so we have to actively check to see when its inserted.
            // While checking the card, we must also be aware that new commands
            // can arrive from the WOSA side
            CardInsertedResult insertedResult = CardInsertedResult.Timeout;

            for (; ; )
            {
                var task = CardPresent();
                if (await Task.WhenAny(task, Task.Delay(Timeout, CancellationToken.None)) != task)
                {
                    break;
                }
                else if (task.IsCanceled)
                {
                    insertedResult = CardInsertedResult.CardInserted;
                    break;
                }

                if (task.Result == PresentStatus.Present)
                {
                    insertedResult = CardInsertedResult.CardInserted;
                    break;
                }
                else if (task.Result == PresentStatus.NoCard ||
                         task.Result == PresentStatus.InJaws)
                {
                    Thread.Sleep(500);
                    continue;
                }
                else
                {
                    insertedResult = CardInsertedResult.Error;
                    break;
                }
            }

            // Disable reader if it's cancelled
            PresentStatus presentStatus = await DisableReader();

            if (insertedResult != CardInsertedResult.CardInserted)
            {
                switch (presentStatus)
                {
                    case PresentStatus.NoCard: // no need to over right result
                        break;
                    case PresentStatus.Present:
                    case PresentStatus.InJaws:
                        insertedResult = CardInsertedResult.CardInserted;
                        break;
                    default:
                        insertedResult = CardInsertedResult.Error;
                        break;
                }
            }

            return insertedResult;
        }

        /*****************************************************************************\
            ReadTrack
            This routine returns the data for the given track.  This may involve
            sending a command to the device and may involve just returning
            data which has already been read.  The data of the track, with 
            terminator sentinels removed should be written into TrackData (null-
            terminated).
            Return WFS_SUCCESS if successful or an error code.
            Some possible errors: 
                 WFS_ERR_IDC_INVALIDDATA if the card track is invalid
                     In this case, write a status code into Status to indicate
                     the failure.  The choices are WFS_IDC_DATAINVALID, 
                     WFS_IDC_DATATOOSHORT, WFS_IDC_DATATOOLONG and
                     WFS_IDC_DATAMISSING (this value may be sent in an
                     execute event). If the HRESULT is not INVALIDDATA
                     then Status must be set to WFS_CMD_IDC_READ_TRACK -
                     that's to check you've read this comment carefully.
                 WFS_ERR_HARDWARE_ERROR if the device is knacked
                 WFS_ERR_IDC_MEDIAJAM if the card is jammed
                 any other that seems appropriate.
        \*****************************************************************************/
        private enum Track
        {
            Track1,
            Track2,
            Track3,
        }

        private class ReadTrackResult
        {
            public ReadTrackResult(string Base64Data, 
                                   byte[] Data, 
                                   XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum MediaStatus,
                                   XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum Status,
                                   bool HardwareError)
            {
                this.Base64Data = Base64Data;
                this.Data = Data;
                this.MediaStatus = MediaStatus;
                this.Status = Status;
                this.HardwareError = true;
            }

            public ReadTrackResult(XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum MediaStatus,
                                   XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum Status,
                                   bool HardwareError)
            {
                this.Base64Data = string.Empty;
                this.Data = Array.Empty<byte>();
                this.MediaStatus = MediaStatus;
                this.Status = Status;
                this.HardwareError = HardwareError;
            }

            public ReadTrackResult()
            {
                this.Base64Data = string.Empty;
                this.Data = Array.Empty<byte>();
                this.MediaStatus = XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum.ErrorDataMissing;
                this.Status = XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.ErrorInvalidMedia;
                this.HardwareError = true;
            }

            public string Base64Data { get; private set; }
            public byte[] Data { get; private set; }
            public XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum MediaStatus { get; private set; }
            public XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum Status { get; private set; }
            public bool HardwareError { get; private set; }
        }

        private async Task<ReadTrackResult> ReadTrack(Track TrackNo)
        {
            Dictionary<Track, string> readTrackCommands = new Dictionary<Track, string>()
            {
                { Track.Track1, "C61\0" },
                { Track.Track2, "C62\0" },
                { Track.Track3, "C63\0" }
            };

            byte[] response = await serialComms.SendAndReceive(readTrackCommands[TrackNo]);

            // Response should be "RRRSSDDD...DDD\n" 
            // where RRR = eg. "P61", SS = eg. "00" , DDD...DDD = Track Data
            if (response is null ||
                response.Length == 0)
            {
                return new ReadTrackResult();
            }

            if (response.ToString().StartsWith("P6") &&
                response.Length >= 5)
            {
                // Check the status code
                int rc = ProcessStatusCode(response[4..]);
                switch (rc)
                {
                    case 0:     // No card present
                    case 1:     // Card in jaws (Note: We don't need to read a card that is in jaws,
                                //                     the accept function should handle this case)
                        return new ReadTrackResult();

                    case 2: // A card is in the C/R
                    case 4: // A card is in Read Start Position of the MM Sensor
                    case 10: // IC Contact is pressed to the ICC
                    case 11: // ICC is in the Activation Status
                    case 20: // Transmission to the ICC is completed.
                        break;  // This is a good response; continue

                    default:
                        Logger.Warning(Constants.DeviceClass, $"Invalid response from the device following a read command. ${response}");
                        return new ReadTrackResult();
                }
            }
            else if (response.ToString().StartsWith("N6"))
            {
                // Check the error code
                int rc = await ProcessErrorCode(response[4..]);
                switch (rc)
                {
                    case 20:    // Too long card
                    case 21:    // Too short card
                        return new ReadTrackResult();

                    case 40:    // Read error (SS error)
                    case 41:    // Read error (ES error)
                    case 42:    // Read error (VRC error)
                    case 43:    // Read error (LRC error)
                    case 44:    // Read error (No encode)
                    case 45:    // Read error (No data)
                    case 46:    // Read error (Jitter error), but still has the track data, maybe in wrong length or incorrect contents
                    case 49:    // Read track setting error
                                //Can't determine the exact reason for this error
                        return new ReadTrackResult(XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum.ErrorDataInvalid,
                                                   XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.ErrorInvalidMedia,
                                                   false);

                    case 465:    // Transport during reading or card held by user (try a second time to read the card)
                        return await ReadTrack(TrackNo);

                    default:
                        Logger.Warning(Constants.DeviceClass, $"Invalid response from the device following a read command. ${response}");
                        DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                        return new ReadTrackResult();
                }
            }
            else
            {
                Logger.Warning(Constants.DeviceClass, $"Invalid response from the device following a read command. ${response}");
                DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.HardwareError;
                return new ReadTrackResult();
            }


            // Everything looks hunky dory.  Copy the data read, to our internal
            // store & to the output param    
            byte[] rawData = response[6..];
            string trackData = Convert.ToBase64String(rawData);

            return new ReadTrackResult(trackData, 
                                       rawData, 
                                       XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum.Ok,
                                       XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.Ok, 
                                       false);
        }

        /// <summary>
        /// Serial port communication class 
        /// </summary>
        private readonly SerialComms serialComms = null;
        
        /// <summary>
        /// Internal flag for chip IO
        /// </summary>
        private bool CardInChipContactPosition = true;

        /// <summary>
        /// Internal flag to update preset status or not
        /// </summary>
        private bool UpdatePresentStatus = true;

        /// <summary>
        /// Logging system
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Internal variables
        /// </summary>
        private XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;
        private XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum DeviceStatus = XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.NoDevice;
        private readonly XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.RetainBinEnum RetainBinStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.RetainBinEnum.Ok;
        private readonly XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum PowerOffOption = XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum.Eject;
        private readonly string CapFirmwareVersion = string.Empty;
        private uint CapturedCards = 0;
        private readonly uint CapMaxCapturedCards = 50;
        private readonly bool CapReadTrack1 = false;
        private readonly bool CapReadTrack2 = false;
        private readonly bool CapReadTrack3 = false;
        private readonly bool CapChipProtocol0 = false;
        private readonly bool CapChipProtocol1 = false;

        /// <summary>
        /// Set of default values
        /// </summary>
        private static class Default
        {
            /// <summary>
            /// Default value of com port
            /// </summary>
            public static readonly string ComPort = "COM1";

            /// <summary>
            /// Timeout on waiting response
            /// </summary>
            public static readonly int Timeout = 5000;
        }

        /// <summary>
        /// Constants
        /// </summary>
        private static class Constants
        {
            public const string ComPort = "ComPort";
            public const string DeviceClass = "DevClass";
        }
    }
}