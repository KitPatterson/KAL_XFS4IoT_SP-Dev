/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ICardReaderDevice.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System.Threading;
using System.Threading.Tasks;
using XFS4IoTServer;

// KAL specific implementation of cardreader. 
namespace XFS4IoTFramework.CardReader
{
    public interface ICardReaderDevice : IDevice
    {

        /// <summary>
        /// This command is used to retrieve the complete list of registration authority Interface Module (IFM) identifiers.The primary registration authority is EMVCo but other organizations are also supported for historical or localcountry requirements.New registration authorities may be added in the future so applications should be able to handle the return of new(as yet undefined) IFM identifiers.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.QueryIFMIdentifierCompletion.PayloadData> QueryIFMIdentifier(IQueryIFMIdentifierEvents events, 
                                                                                                         XFS4IoT.CardReader.Commands.QueryIFMIdentifierCommand.PayloadData payload, 
                                                                                                         CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve the supported payment system applications available within an intelligentcontactless card unit. The payment system application can either be identified by an AID or by the AID incombination with a Kernel Identifier. The Kernel Identifier has been introduced by the EMVCo specifications; seeReference [3].
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.EMVClessQueryApplicationsCompletion.PayloadData> EMVClessQueryApplications(IEMVClessQueryApplicationsEvents events, 
                                                                                                                       XFS4IoT.CardReader.Commands.EMVClessQueryApplicationsCommand.PayloadData payload, 
                                                                                                                       CancellationToken cancellation);

        /// <summary>
        /// This command is only applicable to motor driven card readers and latched dip card readers.For motorized card readers the default operation is that the card is driven to the exit slot from where the usercan remove it. The card remains in position for withdrawal until either it is taken or another command is issuedthat moves the card.For latched dip readers, this command causes the card to be unlatched (if not already unlatched), enablingremoval.After successful completion of this command, a [CardReader.MediaRemovedEvent](#cardreader.mediaremovedevent) isgenerated to inform the application when the card is taken.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.EjectCardCompletion.PayloadData> EjectCard(IEjectCardEvents events, 
                                                                                       XFS4IoT.CardReader.Commands.EjectCardCommand.PayloadData payload, 
                                                                                       CancellationToken cancellation);

        /// <summary>
        /// The card is removed from its present position (card inserted into device, card entering, unknown position) andstored in the retain bin; applicable to motor-driven card readers only. The ID card unit sends a[CardReader.RetainBinThresholdEvent](#cardreader.retainbinthresholdevent) if the storage capacity of the retainbin is reached. If the storage capacity has already been reached, and the command cannot be executed, an error isreturned and the card remains in its present position.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.RetainCardCompletion.PayloadData> RetainCard(IRetainCardEvents events, 
                                                                                         XFS4IoT.CardReader.Commands.RetainCardCommand.PayloadData payload, 
                                                                                         CancellationToken cancellation);

        /// <summary>
        /// This function resets the present value for number of cards retained to zero. The function is possible formotor-driven card readers only.The number of cards retained is controlled by the service and can be requested before resetting via[Common.Status](#common.status).
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.ResetCountCompletion.PayloadData> ResetCount(IResetCountEvents events, 
                                                                                         XFS4IoT.CardReader.Commands.ResetCountCommand.PayloadData payload, 
                                                                                         CancellationToken cancellation);

        /// <summary>
        /// This command is used for setting the DES key that is necessary for operating a CIM86 module. The command must beexecuted before the first read command is issued to the card reader.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.SetKeyCompletion.PayloadData> SetKey(ISetKeyEvents events, 
                                                                                 XFS4IoT.CardReader.Commands.SetKeyCommand.PayloadData payload, 
                                                                                 CancellationToken cancellation);

        /// <summary>
        /// For motor driven card readers, the card unit checks whether a card has been inserted. If so, all specified tracksare read immediately. If reading the chip is requested, the chip will be contacted and reset and the ATR (AnswerTo Reset) data will be read. When this command completes the chip will be in contacted position. This command canalso be used for an explicit cold reset of a previously contacted chip.This command should only be used for user cards and should not be used for permanently connected chips.If no card has been inserted, and for all other categories of card readers, the card unit waits for the period oftime specified in the call for a card to be either inserted or pulled through. The next step is trying to read alltracks specified.The [CardReader.InsertCardEvent](#cardreader.insertcardevent) will be generated when there is no card in the cardreader and the device is ready to accept a card. In addition to that, a security check via a security module(i.e. MM, CIM86) can be requested. If the security check fails however this should not stop valid data beingreturned. The response *securityFail* will be returned if the command specifies only security data to be read andthe security check could not be executed, in all other cases *ok* will be returned with the data field of theoutput parameter set to the relevant value including *hardwareError*.For non-motorized Card Readers which read track data on card exit, the *invalidData* error code is returned whena call to is made to read both track data and chip data.If the card unit is a latched dip unit then the device will latch the card when the chip card will be read, i.e.[chip](#cardreader.readrawdata.command.properties.chip) is specified (see below). The card will remain latcheduntil a call to [CardReader.EjectCard](#cardreader.ejectcard) is made.For contactless chip card readers a collision of two or more card signals may happen. In this case, if the deviceis not able to pick the strongest signal, *errorCardCollision* will be returned.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.ReadRawDataCompletion.PayloadData> ReadRawData(IReadRawDataEvents events, 
                                                                                           XFS4IoT.CardReader.Commands.ReadRawDataCommand.PayloadData payload, 
                                                                                           CancellationToken cancellation);

        /// <summary>
        /// For motor-driven card readers, the ID card unit checks whether a card has been inserted. If so, the data iswritten to the tracks.If no card has been inserted, and for all other categories of devices, the ID card unit waits for the applicationspecified [timeout](#cardreader.writerawdata.command.properties.timeout) for a card to be either inserted orpulled through. The next step is writing the data to the respective tracks.The [CardReader.InsertCardEvent](#cardreader.insertcardevent) event will be generated when there is no card in thecard reader and the device is ready to accept a card.The application must pass the magnetic stripe data in ASCII without any sentinels. The data will be converted bythe Service Provider (ref [CardReader.ReadRawData](#cardreader.readrawdata)). If the data passed in is too longthe invalidError error code will be returned.This procedure is followed by data verification.If power fails during a write the outcome of the operation will be vendor specific, there is no guarantee that thewrite will have succeeded.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.WriteRawDataCompletion.PayloadData> WriteRawData(IWriteRawDataEvents events, 
                                                                                             XFS4IoT.CardReader.Commands.WriteRawDataCommand.PayloadData payload, 
                                                                                             CancellationToken cancellation);

        /// <summary>
        /// This command is used to communicate with the chip. Transparent data is sent from the application to the chip andthe response of the chip is returned transparently to the application.The identification information e.g. ATR of the chip must be obtained before issuing this command. Theidentification information for a user card or the Memory Card Identification (when available) must initially beobtained using [CardReader.ReadRawData](#cardreader.readrawdata). The identification information for subsequentresets of a user card can be obtained using either *CardReader.ReadRawData*[CardReader.ChipPower](#cardreader.chippower). The ATR for permanent connected chips is always obtained through*CardReader.ChipPower*.For contactless chip card readers, applications need to specify which chip to contact with, as part of[chipData](#cardreader.chipio.command.properties.chipdata), if more than one chip has been detected and multipleidentification data has been returned by the *CardReader.ReadRawData* command.For contactless chip card readers a collision of two or more card signals may happen. In this case, if the deviceis not able to pick the strongest signal, the *cardCollision* error code will be returned.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.ChipIOCompletion.PayloadData> ChipIO(IChipIOEvents events, 
                                                                                 XFS4IoT.CardReader.Commands.ChipIOCommand.PayloadData payload, 
                                                                                 CancellationToken cancellation);

        /// <summary>
        /// This command is used by the application to perform a hardware reset which will attempt to return the card readerdevice to a known good state. This command does not over-ride a lock obtained by another application or servicehandle.If the device is a user ID card unit, the device will attempt to either retain, eject or will perform no action onany user cards found in the device as specified in the input parameter. It may not always be possible to retain oreject the items as specified because of hardware problems. If a user card is found inside the device the[CardReader.MediaInsertedEvent](#cardreader.mediainsertedevent) will inform the application where card wasactually moved to. If no action is specified the user card will not be moved even if this means that the devicecannot be recovered.If the device is a permanent chip card unit, this command will power-off the chip.For devices with parking station capability there will be one *MediaInsertedEvent* for each card found.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.ResetCompletion.PayloadData> Reset(IResetEvents events, 
                                                                               XFS4IoT.CardReader.Commands.ResetCommand.PayloadData payload, 
                                                                               CancellationToken cancellation);

        /// <summary>
        /// This command handles the power actions that can be done on the chip.For user chips, this command is only used after the chip has been contacted for the first time using the[CardReader.ReadRawData](#cardreader.readrawdata) command. For contactless user chips, this command may be used todeactivate the contactless card communication.For permanently connected chip cards, this command is the only way to control the chip power.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.ChipPowerCompletion.PayloadData> ChipPower(IChipPowerEvents events, 
                                                                                       XFS4IoT.CardReader.Commands.ChipPowerCommand.PayloadData payload, 
                                                                                       CancellationToken cancellation);

        /// <summary>
        /// This command is used to move a card that is present in the reader to a parking station. A parking station isdefined as an area in the ID card unit, which can be used to temporarily store the card while the device performsoperations on another card. This command is also used to move a card from the parking station to the read/write,chip I/O or transport position. When a card is moved from the parking station to the read/write, chip I/O ortransport position (*parkOut*), the read/write, chip I/O or transport position must not be occupied with anothercard, otherwise the error *cardPresent* will be returned.After moving a card to a parking station, another card can be inserted and read by calling, e.g.,[CardReader.ReadRawData](#cardreader.readrawdata) or [CardReader.ReadTrack](#cardreader.readtrack).Cards in parking stations will not be affected by any CardReader commands until they are removed from the parkingstation using this command, except for the [CardReader.Reset](#cardreader.reset) command, which will move thecards in the parking stations as specified in its input as part of the reset action if possible.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.ParkCardCompletion.PayloadData> ParkCard(IParkCardEvents events, 
                                                                                     XFS4IoT.CardReader.Commands.ParkCardCommand.PayloadData payload, 
                                                                                     CancellationToken cancellation);

        /// <summary>
        /// This command is used to configure an intelligent contactless card reader before performing a contactlesstransaction. This command sets terminal related data elements, the list of terminal acceptable applications withassociated application specific data and any encryption key data required for offline data authentication.This command should be used prior to[CardReader.EMVClessPerformTransaction](#cardreader.emvclessperformtransaction) if the command. It may be calledonce on application start up or when any of the configuration parameters require to be changed. The configurationset by this command is persistent.This command should be called with a complete list of acceptable payment system applications as any previousconfigurations will be replaced.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.EMVClessConfigureCompletion.PayloadData> EMVClessConfigure(IEMVClessConfigureEvents events, 
                                                                                                       XFS4IoT.CardReader.Commands.EMVClessConfigureCommand.PayloadData payload, 
                                                                                                       CancellationToken cancellation);

        /// <summary>
        /// This command is used to enable an intelligent contactless card reader. The transaction will start as soon as thecard tap is detected.Based on the configuration of the contactless chip card and the reader device, this command could return dataformatted either as magnetic stripe information or as a set of BER-TLV encoded EMV tags.This command supports magnetic stripe emulation cards and EMV-like contactless cards but cannot be used on storagecontactless cards. The latter must be managed using the [CardReader.ReadRawData](#cardreader.readrawdata) and[CardReader.ChipIO](#cardreader.chipio) commands.For specific payment system's card profiles an intelligent card reader could return a set of EMV tags along withmagnetic stripe formatted data. In this case, two contactless card data structures will be returned, onecontaining the magnetic stripe like data and one containing BER-TLV encoded tags.If no card has been tapped, the contactless chip card reader waits for the period of time specified in the commandcall for a card to be tapped.For intelligent contactless card readers, any in-built audio/visual feedback such as Beep/LEDs, need to becontrolled directly by the reader. These indications should be implemented based on the EMVCo and payment system'sspecifications.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.EMVClessPerformTransactionCompletion.PayloadData> EMVClessPerformTransaction(IEMVClessPerformTransactionEvents events, 
                                                                                                                         XFS4IoT.CardReader.Commands.EMVClessPerformTransactionCommand.PayloadData payload, 
                                                                                                                         CancellationToken cancellation);

        /// <summary>
        /// This command performs the post authorization processing on payment systems contactless cards.Before an online authorized transaction is considered complete, further chip processing may be requested by theissuer. This is only required when the authorization response includes issuer update data; either issuer scriptsor issuer authentication data.The command enables the contactless card reader and waits for the customer to re-tap their card.The contactless chip card reader waits for the period of time specified in the WFSExecute call for a card to betapped.
        /// </summary>
        Task<XFS4IoT.CardReader.Completions.EMVClessIssuerUpdateCompletion.PayloadData> EMVClessIssuerUpdate(IEMVClessIssuerUpdateEvents events, 
                                                                                                             XFS4IoT.CardReader.Commands.EMVClessIssuerUpdateCommand.PayloadData payload, 
                                                                                                             CancellationToken cancellation);

        /// <summary>
        /// Wait for CardTaken
        /// </summary>
        Task WaitForCardTaken(ICardReaderEvents connection,
                                           CancellationToken cancellation);
    }
}
