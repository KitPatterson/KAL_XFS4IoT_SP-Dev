/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrintFormEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    internal class PrintFormEvents : PrinterEvents, IPrintFormEvents
    {

        public PrintFormEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

        public void MediaPresentedEvent(XFS4IoT.Printer.Events.MediaPresentedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaPresentedEvent(requestId, Payload));

        public void NoMediaEvent(XFS4IoT.Printer.Events.NoMediaEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.NoMediaEvent(requestId, Payload));

        public void MediaInsertedEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaInsertedEvent(requestId));

        public void FieldErrorEvent(XFS4IoT.Printer.Events.FieldErrorEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.FieldErrorEvent(requestId, Payload));

        public void FieldWarningEvent(XFS4IoT.Printer.Events.FieldWarningEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.FieldWarningEvent(requestId, Payload));

        public void MediaRejectedEvent(XFS4IoT.Printer.Events.MediaRejectedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaRejectedEvent(requestId, Payload));

    }
}
