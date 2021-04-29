/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CommonServiceProvider.cs.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:04
\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;

using XFS4IoT;

namespace XFS4IoTServer
{
    public partial class CommonServiceClass : ServiceProvider, ICommonServiceClass
    {
        public CommonServiceClass(EndpointDetails endPoint, string ServiceName, IEnumerable<XFSConstants.ServiceClass> serviceClasses, IDevice device, ILogger logger)
            : base(endPoint, ServiceName, serviceClasses, device, logger)
        {
        }
        public async Task PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Common.Events.PowerSaveChangeEvent(Payload));

        public async Task DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.Common.Events.DevicePositionEvent(Payload));

    }
}
