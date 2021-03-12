// (C) KAL ATM Software GmbH, 2021

using XFS4IoTFramework.Common;

namespace Printer
{
    public interface IPrinterConnection : ICommonConnection
    {

        void RetractBinThresholdEvent(XFS4IoT.Printer.Events.RetractBinThresholdEventPayload Payload);

        void MediaTakenEvent();

        void PaperThresholdEvent(XFS4IoT.Printer.Events.PaperThresholdEventPayload Payload);

        void TonerThresholdEvent(XFS4IoT.Printer.Events.TonerThresholdEventPayload Payload);

        void InkThresholdEvent(XFS4IoT.Printer.Events.InkThresholdEventPayload Payload);

        void MediaPresentedEvent(XFS4IoT.Printer.Events.MediaPresentedEventPayload Payload);

        void MediaAutoRetractedEvent(XFS4IoT.Printer.Events.MediaAutoRetractedEventPayload Payload);

        void NoMediaEvent(XFS4IoT.Printer.Events.NoMediaEventPayload Payload);

        void MediaInsertedEvent();

        void FieldErrorEvent(XFS4IoT.Printer.Events.FieldErrorEventPayload Payload);

        void FieldWarningEvent(XFS4IoT.Printer.Events.FieldWarningEventPayload Payload);

        void MediaRejectedEvent(XFS4IoT.Printer.Events.MediaRejectedEventPayload Payload);

        void LampThresholdEvent(XFS4IoT.Printer.Events.LampThresholdEventPayload Payload);

        void MediaDetectedEvent(XFS4IoT.Printer.Events.MediaDetectedEventPayload Payload);

        void DefinitionLoadedEvent(XFS4IoT.Printer.Events.DefinitionLoadedEventPayload Payload);

        void MediaInsertedUnsolicitedEvent();

        void MediaPresentedUnsolicitedEvent(XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEventPayload Payload);

        void RetractBinStatusEvent(XFS4IoT.Printer.Events.RetractBinStatusEventPayload Payload);

        void PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEventPayload Payload);

        void DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEventPayload Payload);

    }
}
