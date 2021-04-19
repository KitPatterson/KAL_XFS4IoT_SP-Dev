/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * IMediaExtentsEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    public interface IMediaExtentsEvents : IPrinterEvents
    {

        void NoMediaEvent(XFS4IoT.Printer.Events.NoMediaEvent.PayloadData Payload);

        void MediaInsertedEvent();

        void MediaRejectedEvent(XFS4IoT.Printer.Events.MediaRejectedEvent.PayloadData Payload);

    }
}
