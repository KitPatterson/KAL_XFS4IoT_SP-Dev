// (C) KAL ATM Software GmbH, 2021

using XFS4IoTFramework.Common;

namespace TextTerminal
{
    public interface ITextTerminalConnection : ICommonConnection
    {

        void FieldErrorEvent(XFS4IoT.TextTerminal.Events.FieldErrorEventPayload Payload);

        void FieldWarningEvent();

        void KeyEvent(XFS4IoT.TextTerminal.Events.KeyEventPayload Payload);

        void PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEventPayload Payload);

        void DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEventPayload Payload);

    }
}
