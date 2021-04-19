/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ReadFormEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    internal class ReadFormEvents : PrinterEvents, IReadFormEvents
    {

        public ReadFormEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

        public void NoMediaEvent(XFS4IoT.Printer.Events.NoMediaEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.NoMediaEvent(requestId, Payload));

        public void MediaInsertedEvent() => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaInsertedEvent(requestId));

        public void FieldErrorEvent(XFS4IoT.Printer.Events.FieldErrorEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.FieldErrorEvent(requestId, Payload));

        public void FieldWarningEvent(XFS4IoT.Printer.Events.FieldWarningEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.FieldWarningEvent(requestId, Payload));

        public void MediaRejectedEvent(XFS4IoT.Printer.Events.MediaRejectedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaRejectedEvent(requestId, Payload));

    }
}
