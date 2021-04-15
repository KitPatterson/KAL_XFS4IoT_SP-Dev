/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * IReadFormEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
\***********************************************************************************************/


using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    public interface IReadFormEvents : IPrinterEvents
    {

        void NoMediaEvent(XFS4IoT.Printer.Events.NoMediaEvent.PayloadData Payload);

        void MediaInsertedEvent();

        void FieldErrorEvent(XFS4IoT.Printer.Events.FieldErrorEvent.PayloadData Payload);

        void FieldWarningEvent(XFS4IoT.Printer.Events.FieldWarningEvent.PayloadData Payload);

        void MediaRejectedEvent(XFS4IoT.Printer.Events.MediaRejectedEvent.PayloadData Payload);

    }
}
