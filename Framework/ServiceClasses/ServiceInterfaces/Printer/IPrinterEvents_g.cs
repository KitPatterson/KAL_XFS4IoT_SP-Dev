/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * IPrinterEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    public interface IPrinterEvents
    {

        void RetractBinThresholdEvent(XFS4IoT.Printer.Events.RetractBinThresholdEvent.PayloadData Payload);

        void MediaTakenEvent();

        void PaperThresholdEvent(XFS4IoT.Printer.Events.PaperThresholdEvent.PayloadData Payload);

        void TonerThresholdEvent(XFS4IoT.Printer.Events.TonerThresholdEvent.PayloadData Payload);

        void InkThresholdEvent(XFS4IoT.Printer.Events.InkThresholdEvent.PayloadData Payload);

        void MediaAutoRetractedEvent(XFS4IoT.Printer.Events.MediaAutoRetractedEvent.PayloadData Payload);

        void LampThresholdEvent(XFS4IoT.Printer.Events.LampThresholdEvent.PayloadData Payload);

        void MediaDetectedEvent(XFS4IoT.Printer.Events.MediaDetectedEvent.PayloadData Payload);

        void DefinitionLoadedEvent(XFS4IoT.Printer.Events.DefinitionLoadedEvent.PayloadData Payload);

        void MediaInsertedUnsolicitedEvent();

        void MediaPresentedUnsolicitedEvent(XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent.PayloadData Payload);

        void RetractBinStatusEvent(XFS4IoT.Printer.Events.RetractBinStatusEvent.PayloadData Payload);

    }
}
