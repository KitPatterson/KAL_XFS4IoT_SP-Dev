// (C) KAL ATM Software GmbH, 2021

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CardReader;
using XFS4IoT;
using XFS4IoTFramework.CardReader;
using XFS4IoTFramework.Common;

namespace KAL.XFS4IoTSP.CardReader.Simulator
{
    public class SimulatedCardReader : ICardReaderDevice
    {
        public SimulatedCardReader(ILogger Logger)
        {
            Logger.IsNotNull($"Invalid parameter received in the {nameof(SimulatedCardReader)} constructor. {nameof(Logger)}");
            this.Logger = Logger;
        }


        public async Task<XFS4IoT.CardReader.Responses.ReadRawDataPayload> ReadRawData(ICardReaderConnection connection, 
                                                                                       XFS4IoT.CardReader.Commands.ReadRawDataPayload payload,
                                                                                       CancellationToken cancellation)
        {
            await Task.Delay(2000, cancellation);
            connection.MediaInsertedEvent();

            await Task.Delay(1000, cancellation);

            List<XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass> DataClass = new List<XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass>();
            DataClass.Add(new XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass(XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum.Track1, XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum.Ok, "B1234567890123456^SMITH/JOHN.MR^020945852301200589800568000000"));
            DataClass.Add(new XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass(XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum.Track2, XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum.Ok, "1234567890123456=0209458523012005898"));
            DataClass.Add(new XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass(XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.DataSourceEnum.Chip, XFS4IoT.CardReader.Responses.ReadRawDataPayload.DataClass.StatusEnum.Ok, "011234567890123456==000667788903609640040000006200013010000020000098120209105123==00568000999999"));

            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Present;

            return new XFS4IoT.CardReader.Responses.ReadRawDataPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success,
                                                                       "ok",
                                                                       XFS4IoT.CardReader.Responses.ReadRawDataPayload.StatusEnum.Ok,
                                                                       DataClass);
        }


        public void Enable() { }

        public Task<XFS4IoT.CardReader.Responses.FormListPayload> FormList(ICardReaderConnection connection,
                                                                           XFS4IoT.CardReader.Commands.FormListPayload payload, 
                                                                           CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.QueryFormPayload> QueryForm(ICardReaderConnection connection, 
                                                                             XFS4IoT.CardReader.Commands.QueryFormPayload payload, 
                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.QueryIFMIdentifierPayload> QueryIFMIdentifier(ICardReaderConnection connection, 
                                                                                               XFS4IoT.CardReader.Commands.QueryIFMIdentifierPayload payload,
                                                                                               CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.EMVClessQueryApplicationsPayload> EMVClessQueryApplications(ICardReaderConnection connection, 
                                                                                                             XFS4IoT.CardReader.Commands.EMVClessQueryApplicationsPayload payload,
                                                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ReadTrackPayload> ReadTrack(ICardReaderConnection connection, 
                                                                             XFS4IoT.CardReader.Commands.ReadTrackPayload payload,
                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.WriteTrackPayload> WriteTrack(ICardReaderConnection connection, 
                                                                               XFS4IoT.CardReader.Commands.WriteTrackPayload payload,
                                                                               CancellationToken cancellation) => throw new System.NotImplementedException();
        public async Task<XFS4IoT.CardReader.Responses.EjectCardPayload> EjectCard(ICardReaderConnection connection,
                                                                                   XFS4IoT.CardReader.Commands.EjectCardPayload payload,
                                                                                   CancellationToken cancellation)
        {
            await Task.Delay(1000, cancellation);

            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.Entering;

            return new XFS4IoT.CardReader.Responses.EjectCardPayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success,
                                                                     "ok");
        }

        public Task<XFS4IoT.CardReader.Responses.RetainCardPayload> RetainCard(ICardReaderConnection connection,
                                                                               XFS4IoT.CardReader.Commands.RetainCardPayload payload,
                                                                               CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ResetCountPayload> ResetCount(ICardReaderConnection connection, 
                                                                               XFS4IoT.CardReader.Commands.ResetCountPayload payload,
                                                                               CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.SetKeyPayload> SetKey(ICardReaderConnection connection,
                                                                       XFS4IoT.CardReader.Commands.SetKeyPayload payload,
                                                                       CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.WriteRawDataPayload> WriteRawData(ICardReaderConnection connection,
                                                                                   XFS4IoT.CardReader.Commands.WriteRawDataPayload payload,
                                                                                   CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ChipIOPayload> ChipIO(ICardReaderConnection connection,
                                                                       XFS4IoT.CardReader.Commands.ChipIOPayload payload,
                                                                       CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ResetPayload> Reset(ICardReaderConnection connection,
                                                                     XFS4IoT.CardReader.Commands.ResetPayload payload,
                                                                     CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ChipPowerPayload> ChipPower(ICardReaderConnection connection,
                                                                             XFS4IoT.CardReader.Commands.ChipPowerPayload payload,
                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ParseDataPayload> ParseData(ICardReaderConnection connection,
                                                                             XFS4IoT.CardReader.Commands.ParseDataPayload payload,
                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.ParkCardPayload> ParkCard(ICardReaderConnection connection,
                                                                           XFS4IoT.CardReader.Commands.ParkCardPayload payload,
                                                                           CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.EMVClessConfigurePayload> EMVClessConfigure(ICardReaderConnection connection,
                                                                                             XFS4IoT.CardReader.Commands.EMVClessConfigurePayload payload,
                                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.EMVClessPerformTransactionPayload> EMVClessPerformTransaction(ICardReaderConnection connection,
                                                                                                               XFS4IoT.CardReader.Commands.EMVClessPerformTransactionPayload payload,
                                                                                                               CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.CardReader.Responses.EMVClessIssuerUpdatePayload> EMVClessIssuerUpdate(ICardReaderConnection connection,
                                                                                                   XFS4IoT.CardReader.Commands.EMVClessIssuerUpdatePayload payload,
                                                                                                   CancellationToken cancellation) => throw new System.NotImplementedException();
        

        
        public Task<XFS4IoT.Common.Responses.StatusPayload> Status(ICommonConnection connection,
                                                                   XFS4IoT.Common.Commands.StatusPayload payload,
                                                                   CancellationToken cancellation)
        {
            XFS4IoT.Common.Responses.StatusPayload.CommonClass common = new XFS4IoT.Common.Responses.StatusPayload.CommonClass(
                XFS4IoT.Common.Responses.StatusPayload.CommonClass.DeviceEnum.Online,
                new List<string>(), 
                new XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass(
                    XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass.FlashRateEnum.Off,
                    XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass.ColorEnum.Green, 
                    XFS4IoT.Common.Responses.StatusPayload.CommonClass.GuideLightsClass.DirectionEnum.Off),
                XFS4IoT.Common.Responses.StatusPayload.CommonClass.DevicePositionEnum.Inposition,
                0,
                XFS4IoT.Common.Responses.StatusPayload.CommonClass.AntiFraudModuleEnum.Ok);

            XFS4IoT.Common.Responses.StatusPayload.CardReaderClass cardReader = new XFS4IoT.Common.Responses.StatusPayload.CardReaderClass(
                MediaStatus,
                XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.RetainBinEnum.Ok,
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
                    "Simulator",
                    "123456-78900001",
                    "1.0",
                    "KAL simualtor",
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.DeviceInformationClass.FirmwareClass(
                    "XFS4 SP",
                    "1.0",
                    "1.0"),
                    new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.DeviceInformationClass.SoftwareClass(
                    "XFS4 SP",
                    "1.0",
                    "1.0")),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CommonClass.VendorModeIformationClass(
                    true,
                    new List<string>() { "ReadRawData", "EjectCard" }),
                new List<string>() { "MediaInsertedEvent", "MediaRemovedEvent" },
                guideLights,
                false,
                false,
                new List<string>(),
                false,
                false,
                false);

            XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass cardReader = new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass(
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.TypeEnum.Motor,
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.ReadTracksClass(true, true, true, false, false, false, false, false, false, false),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.WriteTracksClass(true, true, true, false, false, false),
                new XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.ChipProtocolsClass(true, true, false, false, false, false, false),
                100,
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.SecurityTypeEnum.NotSupported,
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOnOptionEnum.NoAction,
                XFS4IoT.Common.Responses.CapabilitiesPayload.CardReaderClass.PowerOffOptionEnum.NoAction);


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
                                                                                       CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> PowerSaveControl(ICommonConnection connection,
                                                                                       XFS4IoT.Common.Commands.PowerSaveControlPayload payload,
                                                                                       CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> SynchronizeCommand(ICommonConnection connection,
                                                                                           XFS4IoT.Common.Commands.SynchronizeCommandPayload payload,
                                                                                           CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> SetTransactionState(ICommonConnection connection,
                                                                                             XFS4IoT.Common.Commands.SetTransactionStatePayload payload,
                                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();
        public Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> GetTransactionState(ICommonConnection connection,
                                                                                             XFS4IoT.Common.Commands.GetTransactionStatePayload payload,
                                                                                             CancellationToken cancellation) => throw new System.NotImplementedException();

        public async Task WaitForCardTaken(ICardReaderConnection connection, CancellationToken cancellation)
        {
            await Task.Delay(1000, cancellation);

            MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;

            connection.MediaRemovedEvent();
        }

        public ILogger Logger { get; }


        private XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum MediaStatus = XFS4IoT.Common.Responses.StatusPayload.CardReaderClass.MediaEnum.NotPresent;
    }
}