/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * CashManagementEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.CashManagement
{
    internal abstract class CashManagementEvents
    {
        protected readonly IConnection connection;
        protected readonly string requestId;

        public CashManagementEvents(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

    }
}
