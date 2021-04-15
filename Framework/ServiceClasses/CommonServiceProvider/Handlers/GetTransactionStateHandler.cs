/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetTransactionStateHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    public partial class GetTransactionStateHandler
    {

        private Task HandleGetTransactionState(IConnection connection, GetTransactionStateCommand getTransactionState, CancellationToken cancel)
        {
            IGetTransactionStateEvents events = new GetTransactionStateEvents(connection, getTransactionState.Headers.RequestId);
            //ToDo: Implement HandleGetTransactionState for Common.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetTransactionState for Common is not implemented in GetTransactionStateHandler.cs");
            #else
                #error HandleGetTransactionState for Common is not implemented in GetTransactionStateHandler.cs
            #endif
        }

    }
}
