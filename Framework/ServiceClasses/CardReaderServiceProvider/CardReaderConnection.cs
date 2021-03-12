// (C) KAL ATM Software GmbH, 2021

using XFS4IoT;
using XFS4IoTServer;

namespace CardReader
{
    internal class CardReaderConnection : ICardReaderConnection
    {
        private readonly IConnection connection;
        private readonly string requestId;

        public CardReaderConnection(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public void InsertCardEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InsertCardEvent(requestId));

        public void MediaInsertedEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaInsertedEvent(requestId));

        public void MediaRemovedEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaRemovedEvent(requestId));

        public void InvalidTrackDataEvent(XFS4IoT.CardReader.Events.InvalidTrackDataEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InvalidTrackDataEvent(requestId, Payload));

        public void InvalidMediaEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InvalidMediaEvent(requestId));

        public void TrackDetectedEvent(XFS4IoT.CardReader.Events.TrackDetectedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.TrackDetectedEvent(requestId, Payload));

        public void RetainBinThresholdEvent(XFS4IoT.CardReader.Events.RetainBinThresholdEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.RetainBinThresholdEvent(requestId, Payload));

        public void MediaRetainedEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaRetainedEvent(requestId));

        public void MediaDetectedEvent(XFS4IoT.CardReader.Events.MediaDetectedEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaDetectedEvent(requestId, Payload));

        public void EMVClessReadStatusEvent(XFS4IoT.CardReader.Events.EMVClessReadStatusEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.EMVClessReadStatusEvent(requestId, Payload));

        public void CardActionEvent(XFS4IoT.CardReader.Events.CardActionEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.CardActionEvent(requestId, Payload));

        public void PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.PowerSaveChangeEvent(requestId, Payload));

        public void DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.DevicePositionEvent(requestId, Payload));

        public void ServiceDetailEvent(XFS4IoT.Common.Events.ServiceDetailEventPayload Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.ServiceDetailEvent(requestId, Payload));
    }
}
