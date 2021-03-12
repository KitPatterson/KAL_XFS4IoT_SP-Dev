// (C) KAL ATM Software GmbH, 2021

using XFS4IoTFramework.Common;

namespace CardReader
{
    public interface ICardReaderConnection : ICommonConnection
    {

        void InsertCardEvent();

        void MediaInsertedEvent();

        void MediaRemovedEvent();

        void InvalidTrackDataEvent(XFS4IoT.CardReader.Events.InvalidTrackDataEventPayload Payload);

        void InvalidMediaEvent();

        void TrackDetectedEvent(XFS4IoT.CardReader.Events.TrackDetectedEventPayload Payload);

        void RetainBinThresholdEvent(XFS4IoT.CardReader.Events.RetainBinThresholdEventPayload Payload);

        void MediaRetainedEvent();

        void MediaDetectedEvent(XFS4IoT.CardReader.Events.MediaDetectedEventPayload Payload);

        void EMVClessReadStatusEvent(XFS4IoT.CardReader.Events.EMVClessReadStatusEventPayload Payload);

        void CardActionEvent(XFS4IoT.CardReader.Events.CardActionEventPayload Payload);

        void PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEventPayload Payload);

        void DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEventPayload Payload);

    }
}
