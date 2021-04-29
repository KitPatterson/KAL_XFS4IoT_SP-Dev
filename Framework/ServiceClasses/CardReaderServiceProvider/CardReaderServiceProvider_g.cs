/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * CardReaderServiceProvider.cs.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:04
\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;

using XFS4IoT;

namespace XFS4IoTServer
{
    public partial class CardReaderServiceClass : ServiceProvider, ICardReaderServiceClass
    {
        public CardReaderServiceClass(EndpointDetails endPoint, string ServiceName, IEnumerable<XFSConstants.ServiceClass> serviceClasses, IDevice device, ILogger logger)
            : base(endPoint, ServiceName, serviceClasses, device, logger)
        {
        }
        public async Task MediaRemovedEvent()
            => await BroadcastEvent(new XFS4IoT.CardReader.Events.MediaRemovedEvent());

        public async Task RetainBinThresholdEvent(XFS4IoT.CardReader.Events.RetainBinThresholdEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.CardReader.Events.RetainBinThresholdEvent(Payload));

        public async Task CardActionEvent(XFS4IoT.CardReader.Events.CardActionEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.CardReader.Events.CardActionEvent(Payload));

    }
}
