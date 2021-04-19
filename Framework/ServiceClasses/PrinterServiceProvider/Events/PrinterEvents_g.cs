/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrinterEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Printer
{
    internal class PrinterEvents : IPrinterEvents
    {
        protected readonly IConnection connection;
        protected readonly string requestId;

        public PrinterEvents(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public async Task RetractBinThresholdEvent(XFS4IoT.Printer.Events.RetractBinThresholdEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.RetractBinThresholdEvent(requestId, Payload));

        public async Task MediaTakenEvent() => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaTakenEvent(requestId));

        public async Task PaperThresholdEvent(XFS4IoT.Printer.Events.PaperThresholdEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.PaperThresholdEvent(requestId, Payload));

        public async Task TonerThresholdEvent(XFS4IoT.Printer.Events.TonerThresholdEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.TonerThresholdEvent(requestId, Payload));

        public async Task InkThresholdEvent(XFS4IoT.Printer.Events.InkThresholdEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.InkThresholdEvent(requestId, Payload));

        public async Task MediaAutoRetractedEvent(XFS4IoT.Printer.Events.MediaAutoRetractedEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaAutoRetractedEvent(requestId, Payload));

        public async Task LampThresholdEvent(XFS4IoT.Printer.Events.LampThresholdEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.LampThresholdEvent(requestId, Payload));

        public async Task MediaDetectedEvent(XFS4IoT.Printer.Events.MediaDetectedEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaDetectedEvent(requestId, Payload));

        public async Task DefinitionLoadedEvent(XFS4IoT.Printer.Events.DefinitionLoadedEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.DefinitionLoadedEvent(requestId, Payload));

        public async Task MediaInsertedUnsolicitedEvent() => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaInsertedUnsolicitedEvent(requestId));

        public async Task MediaPresentedUnsolicitedEvent(XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent(requestId, Payload));

        public async Task RetractBinStatusEvent(XFS4IoT.Printer.Events.RetractBinStatusEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Printer.Events.RetractBinStatusEvent(requestId, Payload));

    }
}
