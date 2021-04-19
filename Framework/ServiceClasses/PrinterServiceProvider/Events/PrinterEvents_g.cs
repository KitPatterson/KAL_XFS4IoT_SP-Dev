/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrinterEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    internal class PrinterEvents : IPrinterEvents
    {
        protected readonly IConnection connection;
        protected readonly string requestId;

        public PrinterEvents(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public void RetractBinThresholdEvent(XFS4IoT.Printer.Events.RetractBinThresholdEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.RetractBinThresholdEvent(requestId, Payload));

        public void MediaTakenEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaTakenEvent(requestId));

        public void PaperThresholdEvent(XFS4IoT.Printer.Events.PaperThresholdEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.PaperThresholdEvent(requestId, Payload));

        public void TonerThresholdEvent(XFS4IoT.Printer.Events.TonerThresholdEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.TonerThresholdEvent(requestId, Payload));

        public void InkThresholdEvent(XFS4IoT.Printer.Events.InkThresholdEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.InkThresholdEvent(requestId, Payload));

        public void MediaAutoRetractedEvent(XFS4IoT.Printer.Events.MediaAutoRetractedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaAutoRetractedEvent(requestId, Payload));

        public void LampThresholdEvent(XFS4IoT.Printer.Events.LampThresholdEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.LampThresholdEvent(requestId, Payload));

        public void MediaDetectedEvent(XFS4IoT.Printer.Events.MediaDetectedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaDetectedEvent(requestId, Payload));

        public void DefinitionLoadedEvent(XFS4IoT.Printer.Events.DefinitionLoadedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.DefinitionLoadedEvent(requestId, Payload));

        public void MediaInsertedUnsolicitedEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaInsertedUnsolicitedEvent(requestId));

        public void MediaPresentedUnsolicitedEvent(XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent(requestId, Payload));

        public void RetractBinStatusEvent(XFS4IoT.Printer.Events.RetractBinStatusEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.RetractBinStatusEvent(requestId, Payload));

    }
}
