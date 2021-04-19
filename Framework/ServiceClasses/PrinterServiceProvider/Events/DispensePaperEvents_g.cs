/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * DispensePaperEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Printer
{
    internal class DispensePaperEvents : PrinterEvents, IDispensePaperEvents
    {

        public DispensePaperEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

        public async Task MediaPresentedEvent(XFS4IoT.Printer.Events.MediaPresentedEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaPresentedEvent(requestId, Payload));

    }
}
