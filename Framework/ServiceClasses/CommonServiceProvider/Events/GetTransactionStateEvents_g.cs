/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetTransactionStateEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:41:32
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.Common
{
    internal class GetTransactionStateEvents : CommonEvents, IGetTransactionStateEvents
    {

        public GetTransactionStateEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

    }
}
