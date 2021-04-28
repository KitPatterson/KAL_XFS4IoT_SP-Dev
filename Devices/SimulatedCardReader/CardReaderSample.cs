﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using XFS4IoT;
using XFS4IoTFramework.CardReader;
using XFS4IoTFramework.Common;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using XFS4IoT.CardReader.Events;
using XFS4IoT.Completions;

namespace KAL.XFS4IoTSP.CardReader.Sample
{
    /// <summary>
    /// Sample CardReader device class to implement
    /// </summary>
    public class CardReaderSample : ICardReaderDevice, ICommonDevice
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Logger"></param>
        public CardReaderSample(ILogger Logger)
        {
            Logger.IsNotNull($"Invalid parameter received in the {nameof(CardReaderSample)} constructor. {nameof(Logger)}");
            this.Logger = Logger;
        }

        /// CARDREADER interface

        /// <summary>
        /// For motor driven card readers, the card unit checks whether a card has been inserted. 
        /// All specified tracks are read immediately if the device can read with the low level accept command and store read data in the device specific class and set data on the ReadCardData method call.
        /// If reading the chip is requested, the chip will be contacted and reset and the ATR (AnswerTo Reset) data will be read. 
        /// When this command completes the chip will be in contacted position. This command can also be used for an explicit cold reset of a previously contacted chip.
        /// This command should only be used for user cards and should not be used for permanently connected chips.
        /// If no card has been inserted, and for all other categories of card readers, the card unit waits for the period of time specified in the call for a card to be either inserted or pulled through.
        /// The InsertCardEvent will be generated when there is no card in the cardreader and the device is ready to accept a card.
        /// </summary>
        public async Task<AcceptCardToReadResult> AcceptCard(IReadRawDataEvents events,
                                                             AcceptCardToReadRequest acceptCardInfo,
                                                             CancellationToken cancellation)
        {
            await Task.Delay(2000, cancellation);
            await events.MediaInsertedEvent();

            MediaStatus = StatusCompletion.PayloadData.CardReaderClass.MediaEnum.Present;
            await Task.Delay(1000, cancellation);

            return new AcceptCardToReadResult(MessagePayload.CompletionCodeEnum.Success);
        }

        public Task<AcceptCardToWriteResult> AcceptCard(IWriteRawDataEvents events,
                                                        int timeout,
                                                        CancellationToken cancellation)
        {
            MediaStatus = StatusCompletion.PayloadData.CardReaderClass.MediaEnum.Present;

            return Task.FromResult(new AcceptCardToWriteResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// <summary>
        /// Read alltracks specified.
        /// All specified tracks are read immediately or 
        /// A security check via a security module(i.e. MM, CIM86) can be requested. If the security check fails however this should not stop valid data beingreturned.
        /// The response securityFail will be returned if the command specifies only security data to be read andthe security check could not be executed, in all other cases ok will be returned with the data field of theoutput parameter set to the relevant value including *hardwareError*.
        /// For non-motorized Card Readers which read track data on card exit, the invalidData error code is returned whena call to is made to read both track data and chip data.
        /// If the card unit is a latched dip unit then the device will latch the card when the chip card will be read, 
        /// The card will remain latched until a call to EjectCard is made.
        /// For contactless chip card readers a collision of two or more card signals may happen. 
        /// In this case, if the deviceis not able to pick the strongest signal, errorCardCollision will be returned.
        /// </summary>
        public Task<ReadCardDataResult> ReadCardData(IReadRawDataEvents events,
                                                     ReadCardDataRequest dataToRead)
        {
            MessagePayload.CompletionCodeEnum completionCode = MessagePayload.CompletionCodeEnum.InvalidData;

            Dictionary<ReadCardDataRequest.CardDataTypesEnum, ReadCardDataResult.CardData> readData = new();
            List<ReadCardDataResult.CardData> chipATR = new(); 
            
            if ((dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Track1) == ReadCardDataRequest.CardDataTypesEnum.Track1||
                (dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Track2) == ReadCardDataRequest.CardDataTypesEnum.Track2||
                (dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Track3) == ReadCardDataRequest.CardDataTypesEnum.Track3 ||
                (dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Chip) == ReadCardDataRequest.CardDataTypesEnum.Chip)
            {
                if ((dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Track1) == ReadCardDataRequest.CardDataTypesEnum.Track1)
                {
                    readData.Add(ReadCardDataRequest.CardDataTypesEnum.Track1, 
                                 new ReadCardDataResult.CardData(ReadCardDataResult.CardData.DataStatusEnum.Ok, 
                                 Encoding.UTF8.GetBytes("B1234567890123456^SMITH/JOHN.MR^020945852301200589800568000000").ToList()));
                }
                if ((dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Track2) == ReadCardDataRequest.CardDataTypesEnum.Track2)
                {
                    readData.Add(ReadCardDataRequest.CardDataTypesEnum.Track2,
                                 new ReadCardDataResult.CardData(ReadCardDataResult.CardData.DataStatusEnum.Ok,
                                 Encoding.UTF8.GetBytes("1234567890123456=0209458523012005898").ToList()));
                }
                if ((dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Track3) == ReadCardDataRequest.CardDataTypesEnum.Track3)
                {
                    readData.Add(ReadCardDataRequest.CardDataTypesEnum.Track3,
                                 new ReadCardDataResult.CardData(ReadCardDataResult.CardData.DataStatusEnum.Ok,
                                 Encoding.UTF8.GetBytes("011234567890123456==000667788903609640040000006200013010000020000098120209105123==00568000999999").ToList()));
                }
                if ((dataToRead.DataToRead & ReadCardDataRequest.CardDataTypesEnum.Chip) == ReadCardDataRequest.CardDataTypesEnum.Chip)
                {
                    chipATR.Add(new ReadCardDataResult.CardData(ReadCardDataResult.CardData.DataStatusEnum.Ok,
                                new List<byte>() { 0x3b, 0x2a, 0x00, 0x80, 0x65, 0xa2, 0x1, 0x2, 0x1, 0x31, 0x72, 0xd6, 0x43 }));
                }
                completionCode = MessagePayload.CompletionCodeEnum.Success;
            }
            
            return Task.FromResult(new ReadCardDataResult(completionCode,
                                                          readData,
                                                          chipATR));
        }

        /// <summary>
        /// The device is ready to accept a card.
        /// The application must pass the magnetic stripe data in ASCII without any sentinels. 
        /// If the data passed in is too long. The invalidError error code will be returned.
        /// This procedure is followed by data verification.
        /// If power fails during a write the outcome of the operation will be vendor specific, there is no guarantee that thewrite will have succeeded.
        /// </summary>
        public Task<WriteCardDataResult> WriteCardData(IWriteRawDataEvents events,
                                                       WriteCardDataRequest dataToWrite)
        {
            return Task.FromResult(new WriteCardDataResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// <summary>
        /// This command is used to retrieve the complete list of registration authority Interface Module (IFM) identifiers.
        /// The primary registration authority is EMVCo but other organizations are also supported for historical or localcountry requirements.
        /// New registration authorities may be added in the future so applications should be able to handle the return of new(as yet undefined) IFM identifiers.
        /// </summary>
        public Task<QueryIFMIdentifierResult> QueryIFMIdentifier()
        {
            return Task.FromResult((new QueryIFMIdentifierResult(MessagePayload.CompletionCodeEnum.Success, 
                                                                 new List<IFMIdentifierInfo>() 
                                                                 {
                                                                   new IFMIdentifierInfo(XFS4IoT.CardReader.Completions.QueryIFMIdentifierCompletion.PayloadData.IfmAuthorityEnum.Emv, new List<byte>() { 0x1, 0x2, 0x3, 0x4 } )
                                                                 })));
        }

        /// <summary>
        /// This command is used to retrieve the supported payment system applications available within an intelligentcontactless card unit. 
        /// The payment system application can either be identified by an AID or by the AID incombination with a Kernel Identifier. 
        /// The Kernel Identifier has been introduced by the EMVCo specifications; seeReference [3].
        /// </summary>
        public Task<QueryEMVApplicationResult> EMVContactlessQueryApplications()
        {
            List<EMVApplication> AIDList = new() 
            { 
                new EMVApplication(Encoding.UTF8.GetBytes("A0000000031010").ToList(), null),
                new EMVApplication(Encoding.UTF8.GetBytes("A0000000041010").ToList(), null)
            };
            return Task.FromResult(new QueryEMVApplicationResult(MessagePayload.CompletionCodeEnum.Success, AIDList));
        }

        /// <summary>
        /// This command is only applicable to motor driven card readers and latched dip card readers.
        /// For motorized card readers the default operation is that the card is driven to the exit slot from where the usercan remove it.
        /// The card remains in position for withdrawal until either it is taken or another command is issuedthat moves the card.
        /// For latched dip readers, this command causes the card to be unlatched (if not already unlatched), enablingremoval.
        /// After successful completion of this command, a CardReader.MediaRemovedEvent is generated to inform the application when the card is taken.
        /// </summary>
        public async Task<EjectCardResult> EjectCard(EjectCardRequest ejectCardInfo)
        {
            await Task.Delay(1000);

            MediaStatus = StatusCompletion.PayloadData.CardReaderClass.MediaEnum.Entering;

            return new EjectCardResult(MessagePayload.CompletionCodeEnum.Success);
        }

        /// <summary>
        /// This command is used to communicate with the chip.
        /// Transparent data is sent from the application to the chip andthe response of the chip is returned transparently to the application.
        /// The identification information e.g. ATR of the chip must be obtained before issuing this command. 
        /// The identification information for a user card or the Memory Card Identification (when available) must initially be obtained using CardReader.ReadRawData.
        /// The identification information for subsequentresets of a user card can be obtained using either CardReader.ReadRawDat or CardReader.ChipPower. 
        /// The ATR for permanent connected chips is always obtained through CardReader.ChipPower.
        /// For contactless chip card readers, applications need to specify which chip to contact with, as part of chipData, if more than one chip has been detected and multiple identification data has been returned by the CardReader.ReadRawData command.
        /// For contactless chip card readers a collision of two or more card signals may happen. 
        /// In this case, if the deviceis not able to pick the strongest signal, the cardCollision error code will be returned.
        /// </summary>
        public Task<ChipIOResult> ChipIO(ChipIORequest dataToSend)
        {
            List<byte> chipData = new() { 0x90, 0x00 };
            return Task.FromResult(new ChipIOResult(MessagePayload.CompletionCodeEnum.Success, chipData));
        }

        /// <summary>
        /// This command is used by the application to perform a hardware reset which will attempt to return the card readerdevice to a known good state.
        /// This command does not over-ride a lock obtained by another application or service handle.
        /// If the device is a user ID card unit, the device will attempt to either retain, eject or will perform no action onany user cards found in the device as specified in the input parameter. 
        /// It may not always be possible to retain oreject the items as specified because of hardware problems. 
        /// If a user card is found inside the device the CardReader.MediaInsertedEvent will inform the application where card wasactually moved to.
        /// If no action is specified the user card will not be moved even if this means that the devicecannot be recovered.
        /// If the device is a permanent chip card unit, this command will power-off the chip.For devices with parking station capability there will be one MediaInsertedEvent for each card found.
        /// </summary>
        public async Task<ResetDeviceResult> ResetDevice(IResetEvents events,
                                                         ResetDeviceRequest cardAction)
        {
            await Task.Delay(1000);
            MediaStatus = StatusCompletion.PayloadData.CardReaderClass.MediaEnum.NotPresent;

            return new ResetDeviceResult(MessagePayload.CompletionCodeEnum.Success);
        }

        /// <summary>
        /// This command handles the power actions that can be done on the chip.For user chips, this command is only used after the chip has been contacted for the first time using the[CardReader.ReadRawData](#cardreader.readrawdata) command. For contactless user chips, this command may be used todeactivate the contactless card communication.For permanently connected chip cards, this command is the only way to control the chip power.
        /// </summary>
        public Task<ChipPowerResult> ChipPower(IChipPowerEvents events,
                                               ChipPowerRequest action)
        {
            return Task.FromResult(new ChipPowerResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// <summary>
        /// This command is used to move a card that is present in the reader to a parking station.
        /// A parking station isdefined as an area in the ID card unit, which can be used to temporarily store the card while the device performs operations on another card. 
        /// This command is also used to move a card from the parking station to the read/write,chip I/O or transport position. 
        /// When a card is moved from the parking station to the read/write, chip I/O ortransport position parkOut, the read/write, chip I/O or transport position must not be occupied with anothercard, otherwise the error cardPresent will be returned.
        /// After moving a card to a parking station, another card can be inserted and read by calling, e.g.,CardReader.ReadRawData.
        /// Cards in parking stations will not be affected by any CardReader commands until they are removed from the parkingstation using this command, except for the CardReader.Reset command, which will move thecards in the parking stations as specified in its input as part of the reset action if possible.
        /// </summary>
        public Task<ParkCardResult> ParkCard(ParkCardRequest parkCardInfo)
        {
            return Task.FromResult(new ParkCardResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// <summary>
        /// This command is used to configure an intelligent contactless card reader before performing a contactlesstransaction.
        /// This command sets terminal related data elements, the list of terminal acceptable applications with associated application specific data and any encryption key data required for offline data authentication.
        /// This command should be used prior to CardReader.EMVClessPerformTransaction command. 
        /// It may be calledonce on application start up or when any of the configuration parameters require to be changed. 
        /// The configurationset by this command is persistent.This command should be called with a complete list of acceptable payment system applications as any previous configurations will be replaced.
        /// </summary>
        public Task<EMVContactlessConfigureResult> EMVContactlessConfigure(EMVContactlessConfigureRequest terminalConfig)
        {
            return Task.FromResult(new EMVContactlessConfigureResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// <summary>
        /// This command is used to enable an intelligent contactless card reader.
        /// The transaction will start as soon as thecard tap is detected.
        /// Based on the configuration of the contactless chip card and the reader device, this command could return dataformatted either as magnetic stripe information or as a set of BER-TLV encoded EMV tags.
        /// This command supports magnetic stripe emulation cards and EMV-like contactless cards but cannot be used on storagecontactless cards. 
        /// The latter must be managed using the CardReader.ReadRawData and CardReader.ChipIO commands.
        /// For specific payment system's card profiles an intelligent card reader could return a set of EMV tags along withmagnetic stripe formatted data. 
        /// In this case, two contactless card data structures will be returned, onecontaining the magnetic stripe like data and one containing BER-TLV encoded tags.
        /// If no card has been tapped, the contactless chip card reader waits for the period of time specified in the commandcall for a card to be tapped.
        /// For intelligent contactless card readers, any in-built audio/visual feedback such as Beep/LEDs, need to becontrolled directly by the reader. 
        /// These indications should be implemented based on the EMVCo and payment system'sspecifications.
        /// </summary>
        public async Task<EMVContactlessPerformTransactionResult> EMVContactlessPerformTransaction(IEMVClessPerformTransactionEvents events,
                                                                                                   EMVContactlessPerformTransactionRequest transactionData,
                                                                                                   CancellationToken cancellation)
        {
            await events.EMVClessReadStatusEvent(new EMVClessReadStatusEvent.PayloadData(100, EMVClessReadStatusEvent.PayloadData.StatusEnum.ReadyToRead, 0, EMVClessReadStatusEvent.PayloadData.ValueQualifierEnum.Amount));
            
            await Task.Delay(2000);

            EMVContactlessTransactionDataOutput txnOutput = new(EMVContactlessTransactionDataOutput.TxOutcomeEnum.Approve,
                                                                EMVContactlessTransactionDataOutput.CardholderActionEnum.None,
                                                                new List<byte>() { 0x9c, 0x1, 0x0, 0x9f, 0x26, 0x08, 0x47, 0x9c, 0x4f, 0x7e, 0xc8, 0x52, 0xd1, 0x6, 0x9f, 0x34, 0x03, 0x1e, 0x00, 0x00 },
                                                                new EMVContactlessTransactionDataOutput.EMVContactlessOutcome(EMVContactlessTransactionDataOutput.EMVContactlessOutcome.CvmEnum.OnlinePIN,
                                                                                                                              EMVContactlessTransactionDataOutput.EMVContactlessOutcome.AlternateInterfaceEnum.Contact,
                                                                                                                              false,
                                                                                                                              new EMVContactlessTransactionDataOutput.EMVContactlessOutcome.EMVContactlessUI(3,
                                                                                                                                                                                                             EMVContactlessTransactionDataOutput.EMVContactlessOutcome.EMVContactlessUI.StatusEnum.CardReadOk,
                                                                                                                                                                                                             0,
                                                                                                                                                                                                             EMVContactlessTransactionDataOutput.EMVContactlessOutcome.EMVContactlessUI.ValueQualifierEnum.NotApplicable,
                                                                                                                                                                                                             "0",
                                                                                                                                                                                                             "EUR",
                                                                                                                                                                                                             "EN"),
                                                                                                                              null,
                                                                                                                              0,
                                                                                                                              0,
                                                                                                                              new List<byte>() { }));

            return new EMVContactlessPerformTransactionResult(MessagePayload.CompletionCodeEnum.Success, 
                                                              new() 
                                                              { 
                                                                  { 
                                                                      EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Chip, 
                                                                      txnOutput 
                                                                  } 
                                                              });
        }

        /// <summary>
        /// This command performs the post authorization processing on payment systems contactless cards.
        /// Before an online authorized transaction is considered complete, further chip processing may be requested by theissuer. 
        /// This is only required when the authorization response includes issuer update data either issuer scriptsor issuer authentication data.
        /// The command enables the contactless card reader and waits for the customer to re-tap their card.
        /// The contactless chip card reader waits for the period of time specified in the command all for a card to be tapped.
        /// </summary>
        public async Task<EMVContactlessIssuerUpdateResult> EMVContactlessIssuerUpdate(IEMVClessIssuerUpdateEvents events,
                                                                                       EMVContactlessIssuerUpdateRequest transactionData,
                                                                                       CancellationToken cancellation)
        {
            await Task.Delay(1000);

            EMVContactlessTransactionDataOutput txnOutput = new (EMVContactlessTransactionDataOutput.TxOutcomeEnum.Approve,
                                                                 EMVContactlessTransactionDataOutput.CardholderActionEnum.None, 
                                                                 new List<byte>() { 0x9c, 0x1, 0x0, 0x9f, 0x26, 0x08, 0x47, 0x9c, 0x4f, 0x7e, 0xc8, 0x52, 0xd1, 0x6, 0x9f, 0x34, 0x03, 0x1e, 0x00, 0x00}, 
                                                                 new EMVContactlessTransactionDataOutput.EMVContactlessOutcome(EMVContactlessTransactionDataOutput.EMVContactlessOutcome.CvmEnum.OnlinePIN,
                                                                                                                               EMVContactlessTransactionDataOutput.EMVContactlessOutcome.AlternateInterfaceEnum.Contact, 
                                                                                                                               false, 
                                                                                                                               new EMVContactlessTransactionDataOutput.EMVContactlessOutcome.EMVContactlessUI(3,
                                                                                                                                                                                                              EMVContactlessTransactionDataOutput.EMVContactlessOutcome.EMVContactlessUI.StatusEnum.CardReadOk, 
                                                                                                                                                                                                              0,
                                                                                                                                                                                                              EMVContactlessTransactionDataOutput.EMVContactlessOutcome.EMVContactlessUI.ValueQualifierEnum.NotApplicable, 
                                                                                                                                                                                                              "0", 
                                                                                                                                                                                                              "EUR", 
                                                                                                                                                                                                              "EN"), 
                                                                                                                               null, 
                                                                                                                               0, 
                                                                                                                               0, 
                                                                                                                               new List<byte>() { }));

            return new EMVContactlessIssuerUpdateResult(MessagePayload.CompletionCodeEnum.Success, txnOutput);
        }

        /// <summary>
        /// The card is removed from its present position (card inserted into device, card entering, unknown position) and stored in the retain bin;
        /// applicable to motor-driven card readers only.
        /// The ID card unit sends a CardReader.RetainBinThresholdEvent if the storage capacity of the retainbin is reached.
        /// If the storage capacity has already been reached, and the command cannot be executed, an error isreturned and the card remains in its present position.
        /// </summary>
        public async Task<CaptureCardResult> CaptureCard(IRetainCardEvents events)
        {
            await Task.Delay(1000);
            await events.MediaRetainedEvent();

            return new CaptureCardResult(MessagePayload.CompletionCodeEnum.Success,
                                         CapturedCount++,
                                         XFS4IoT.CardReader.Completions.RetainCardCompletion.PayloadData.PositionEnum.Present);
        }

        /// <summary>
        /// This function resets the present value for number of cards retained to zero.
        /// The function is possible formotor-driven card readers only.
        /// The number of cards retained is controlled by the service.
        /// </summary>
        public Task<ResetCountResult> ResetBinCount()
        {
            CapturedCount = 0;
            return Task.FromResult(new ResetCountResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// <summary>
        /// This command is used for setting the DES key that is necessary for operating a CIM86 module.
        /// The command must beexecuted before the first read command is issued to the card reader.
        /// </summary>
        public Task<SetCIM86KeyResult> SetCIM86Key(SetCIM86KeyRequest keyInfo)
        {
            return Task.FromResult(new SetCIM86KeyResult(MessagePayload.CompletionCodeEnum.Success));
        }

        /// COMMON interface
        
        public Task<StatusCompletion.PayloadData> Status()
        {
            StatusCompletion.PayloadData.CommonClass common = new(
                StatusCompletion.PayloadData.CommonClass.DeviceEnum.Online,
                new List<string>(), 
                new StatusCompletion.PayloadData.CommonClass.GuideLightsClass(
                    StatusCompletion.PayloadData.CommonClass.GuideLightsClass.FlashRateEnum.Off,
                    StatusCompletion.PayloadData.CommonClass.GuideLightsClass.ColorEnum.Green,
                    StatusCompletion.PayloadData.CommonClass.GuideLightsClass.DirectionEnum.Off),
                StatusCompletion.PayloadData.CommonClass.DevicePositionEnum.Inposition,
                0,
                StatusCompletion.PayloadData.CommonClass.AntiFraudModuleEnum.Ok);

            StatusCompletion.PayloadData.CardReaderClass cardReader = new StatusCompletion.PayloadData.CardReaderClass(
                MediaStatus,
                StatusCompletion.PayloadData.CardReaderClass.RetainBinEnum.Ok,
                StatusCompletion.PayloadData.CardReaderClass.SecurityEnum.NotSupported,
                CapturedCount,
                StatusCompletion.PayloadData.CardReaderClass.ChipPowerEnum.PoweredOff,
                StatusCompletion.PayloadData.CardReaderClass.ChipModuleEnum.Ok,
                StatusCompletion.PayloadData.CardReaderClass.MagWriteModuleEnum.Ok,
                StatusCompletion.PayloadData.CardReaderClass.FrontImageModuleEnum.Ok,
                StatusCompletion.PayloadData.CardReaderClass.BackImageModuleEnum.Ok,
                new List<string>());

            return Task.FromResult(new StatusCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                    null,
                                                                    common,
                                                                    cardReader));
        }

        public Task<CapabilitiesCompletion.PayloadData> Capabilities()
        {
            CapabilitiesCompletion.PayloadData.CommonClass.GuideLightsClass guideLights = new(
                new CapabilitiesCompletion.PayloadData.CommonClass.GuideLightsClass.FlashRateClass(true, true, true, true),
                new CapabilitiesCompletion.PayloadData.CommonClass.GuideLightsClass.ColorClass(true, true, true, true, true, true, true),
                new CapabilitiesCompletion.PayloadData.CommonClass.GuideLightsClass.DirectionClass(false, false));

            CapabilitiesCompletion.PayloadData.CommonClass common = new(
                "1.0",
                new CapabilitiesCompletion.PayloadData.CommonClass.DeviceInformationClass(
                    "Simulator",
                    "123456-78900001",
                    "1.0",
                    "KAL simualtor",
                    new CapabilitiesCompletion.PayloadData.CommonClass.DeviceInformationClass.FirmwareClass(
                    "XFS4 SP",
                    "1.0",
                    "1.0"),
                    new CapabilitiesCompletion.PayloadData.CommonClass.DeviceInformationClass.SoftwareClass(
                    "XFS4 SP",
                    "1.0")),
                new CapabilitiesCompletion.PayloadData.CommonClass.VendorModeIformationClass(
                    true,
                    new List<string>() 
                    { 
                        "CardReader.ReadRawData",
                        "CardReader.EjectCard"
                    }),
                new List<string>(),
                guideLights,
                false,
                false,
                new List<string>(),
                false,
                false,
                false);

            CapabilitiesCompletion.PayloadData.CardReaderClass cardReader = new(
                CapabilitiesCompletion.PayloadData.CardReaderClass.TypeEnum.Motor,
                new CapabilitiesCompletion.PayloadData.CardReaderClass.ReadTracksClass(true, true, true, false, false, false, false, false, false, false),
                new CapabilitiesCompletion.PayloadData.CardReaderClass.WriteTracksClass(true, true, true, false, false, false),
                new CapabilitiesCompletion.PayloadData.CardReaderClass.ChipProtocolsClass(true, true, false, false, false, false, false),
                100,
                CapabilitiesCompletion.PayloadData.CardReaderClass.SecurityTypeEnum.NotSupported,
                CapabilitiesCompletion.PayloadData.CardReaderClass.PowerOnOptionEnum.NoAction,
                CapabilitiesCompletion.PayloadData.CardReaderClass.PowerOffOptionEnum.NoAction,
                false, false, 
                new CapabilitiesCompletion.PayloadData.CardReaderClass.WriteModeClass(false, true, false, true),
                new CapabilitiesCompletion.PayloadData.CardReaderClass.ChipPowerClass(false, true, true, true),
                new CapabilitiesCompletion.PayloadData.CardReaderClass.MemoryChipProtocolsClass(false, false),
                new CapabilitiesCompletion.PayloadData.CardReaderClass.EjectPositionClass(true, false),
                0);


            List<CapabilitiesCompletion.PayloadData.InterfacesClass> interfaces = new()
            {
                new CapabilitiesCompletion.PayloadData.InterfacesClass(
                    CapabilitiesCompletion.PayloadData.InterfacesClass.NameEnum.Common,
                    new List<string>()
                    { 
                        "Common.Status", 
                        "Common.Capabilities" 
                    },
                    new List<string>(),
                    1000,
                    new List<string>()),
                new CapabilitiesCompletion.PayloadData.InterfacesClass(
                    CapabilitiesCompletion.PayloadData.InterfacesClass.NameEnum.CardReader,
                    new List<string>
                    { 
                        "CardReader.ReadRawData", 
                        "CardReader.EjectCard", 
                        "CardReader.Reset", 
                        "CardReader.WriteRawData",
                        "CardReader.ChipIO",
                        "CardReader.ChipPower",
                        "CardReader.EMVClessConfigure",
                        "CardReader.EMVClessIssuerUpdate",
                        "CardReader.EMVClessPerformTransaction",
                        "CardReader.EMVClessQueryApplications",
                        "CardReader.ParkCard",
                        "CardReader.QueryIFMIdentifier",
                        "CardReader.ResetCount",
                        "CardReader.RetainCard",
                        "CardReader.SetKey"
                    },
                    new List<string>
                    {
                        "CardReader.MediaDetectedEvent",
                        "CardReader.MediaInsertedEvent",
                        "CardReader.MediaRemovedEvent",
                        "CardReader.MediaRetainedEvent",
                        "CardReader.InvalidMediaEvent",
                        "CardReader.EMVClessReadStatusEvent"
                    },
                    1000,
                    new List<string>())
            };

            return Task.FromResult(new CapabilitiesCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                          null,
                                                                          interfaces,
                                                                          common,
                                                                          cardReader));
        }

        public Task<PowerSaveControlCompletion.PayloadData> PowerSaveControl(PowerSaveControlCommand.PayloadData payload) => throw new System.NotImplementedException();
        public Task<SynchronizeCommandCompletion.PayloadData> SynchronizeCommand(SynchronizeCommandCommand.PayloadData payload) => throw new System.NotImplementedException();
        public Task<SetTransactionStateCompletion.PayloadData> SetTransactionState(SetTransactionStateCommand.PayloadData payload) => throw new System.NotImplementedException();
        public Task<GetTransactionStateCompletion.PayloadData> GetTransactionState() => throw new System.NotImplementedException();
        public Task<GetCommandRandomNumberHandler.GetCommandRandomNumberResult> GetCommandRandomNumber() => throw new System.NotImplementedException();


        /// Internal variables
        
        private StatusCompletion.PayloadData.CardReaderClass.MediaEnum MediaStatus = StatusCompletion.PayloadData.CardReaderClass.MediaEnum.NotPresent;
        // Retain bin count should be kept in persistent data
        private int CapturedCount = 0;

        private ILogger Logger { get; }
    }
}