/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CommonEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:41:32
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.Common
{
    internal class CommonEvents : ICommonEvents
    {
        protected readonly IConnection connection;
        protected readonly string requestId;

        public CommonEvents(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public void PowerSaveChangeEvent(XFS4IoT.Common.Events.PowerSaveChangeEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.PowerSaveChangeEvent(requestId, Payload));

        public void DevicePositionEvent(XFS4IoT.Common.Events.DevicePositionEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.Common.Events.DevicePositionEvent(requestId, Payload));

    }
}
