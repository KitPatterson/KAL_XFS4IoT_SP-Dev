// (C) KAL ATM Software GmbH, 2021

using XFS4IoT;
using XFS4IoTServer;

namespace Printer
{
    internal class PrinterConnection : IPrinterConnection
    {
        private readonly IConnection connection;
        private readonly string requestId;

        public PrinterConnection(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public void RetractBinThresholdEvent(XFS4IoT.Printer.Events.RetractBinThresholdEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.RetractBinThresholdEvent(requestId, Payload));

        public void MediaTakenEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaTakenEvent(requestId));

        public void PaperThresholdEvent(XFS4IoT.Printer.Events.PaperThresholdEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.PaperThresholdEvent(requestId, Payload));

        public void TonerThresholdEvent(XFS4IoT.Printer.Events.TonerThresholdEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.TonerThresholdEvent(requestId, Payload));

        public void InkThresholdEvent(XFS4IoT.Printer.Events.InkThresholdEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.InkThresholdEvent(requestId, Payload));

        public void MediaPresentedEvent(XFS4IoT.Printer.Events.MediaPresentedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaPresentedEvent(requestId, Payload));

        public void MediaAutoRetractedEvent(XFS4IoT.Printer.Events.MediaAutoRetractedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaAutoRetractedEvent(requestId, Payload));

        public void NoMediaEvent(XFS4IoT.Printer.Events.NoMediaEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.NoMediaEvent(requestId, Payload));

        public void MediaInsertedEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaInsertedEvent(requestId));

        public void FieldErrorEvent(XFS4IoT.Printer.Events.FieldErrorEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.FieldErrorEvent(requestId, Payload));

        public void FieldWarningEvent(XFS4IoT.Printer.Events.FieldWarningEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.FieldWarningEvent(requestId, Payload));

        public void MediaRejectedEvent(XFS4IoT.Printer.Events.MediaRejectedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaRejectedEvent(requestId, Payload));

        public void LampThresholdEvent(XFS4IoT.Printer.Events.LampThresholdEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.LampThresholdEvent(requestId, Payload));

        public void MediaDetectedEvent(XFS4IoT.Printer.Events.MediaDetectedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaDetectedEvent(requestId, Payload));

        public void DefinitionLoadedEvent(XFS4IoT.Printer.Events.DefinitionLoadedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.DefinitionLoadedEvent(requestId, Payload));

        public void MediaInsertedUnsolicitedEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaInsertedUnsolicitedEvent(requestId));

        public void MediaPresentedUnsolicitedEvent(XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent(requestId, Payload));

        public void RetractBinStatusEvent(XFS4IoT.Printer.Events.RetractBinStatusEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.RetractBinStatusEvent(requestId, Payload));

        public void PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.PowerSaveChangeEvent(requestId, Payload));

        public void DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.DevicePositionEvent(requestId, Payload));

        public void ServiceDetailEvent(XFS4IoT.Common.Events.ServiceDetailEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.ServiceDetailEvent(requestId, Payload));
    }
}
