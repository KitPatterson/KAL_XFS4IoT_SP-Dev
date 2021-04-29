/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrinterServiceProvider.cs.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:07
\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;

using XFS4IoT;

namespace XFS4IoTServer
{
    public partial class PrinterServiceClass : ServiceProvider, IPrinterServiceClass
    {
        public PrinterServiceClass(EndpointDetails endPoint, string ServiceName, IEnumerable<XFSConstants.ServiceClass> serviceClasses, IDevice device, ILogger logger)
            : base(endPoint, ServiceName, serviceClasses, device, logger)
        {
        }
        public async Task RetractBinThresholdEvent(XFS4IoT.Printer.Events.RetractBinThresholdEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.RetractBinThresholdEvent(Payload));

        public async Task MediaTakenEvent()
            => await BroadcastEvent(new XFS4IoT.Printer.Events.MediaTakenEvent());

        public async Task PaperThresholdEvent(XFS4IoT.Printer.Events.PaperThresholdEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.PaperThresholdEvent(Payload));

        public async Task TonerThresholdEvent(XFS4IoT.Printer.Events.TonerThresholdEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.TonerThresholdEvent(Payload));

        public async Task InkThresholdEvent(XFS4IoT.Printer.Events.InkThresholdEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.InkThresholdEvent(Payload));

        public async Task MediaAutoRetractedEvent(XFS4IoT.Printer.Events.MediaAutoRetractedEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.MediaAutoRetractedEvent(Payload));

        public async Task LampThresholdEvent(XFS4IoT.Printer.Events.LampThresholdEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.LampThresholdEvent(Payload));

        public async Task MediaDetectedEvent(XFS4IoT.Printer.Events.MediaDetectedEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.MediaDetectedEvent(Payload));

        public async Task DefinitionLoadedEvent(XFS4IoT.Printer.Events.DefinitionLoadedEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.DefinitionLoadedEvent(Payload));

        public async Task MediaInsertedUnsolicitedEvent()
            => await BroadcastEvent(new XFS4IoT.Printer.Events.MediaInsertedUnsolicitedEvent());

        public async Task MediaPresentedUnsolicitedEvent(XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.MediaPresentedUnsolicitedEvent(Payload));

        public async Task RetractBinStatusEvent(XFS4IoT.Printer.Events.RetractBinStatusEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Printer.Events.RetractBinStatusEvent(Payload));

    }
}
