// (C) KAL ATM Software GmbH, 2021

using XFS4IoTFramework.Common;


namespace TextTerminal
{
    public interface ITextTerminalConnection : ICommonConnection
    {

        void FieldErrorEvent(XFS4IoT.TextTerminal.Events.FieldErrorEvent.PayloadData Payload);

        void FieldWarningEvent();

        void KeyEvent(XFS4IoT.TextTerminal.Events.KeyEvent.PayloadData Payload);

    }
}
