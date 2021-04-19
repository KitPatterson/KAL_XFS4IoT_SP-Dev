/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetTransactionStateHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
    public partial class SetTransactionStateHandler
    {

        private Task HandleSetTransactionState(IConnection connection, SetTransactionStateCommand setTransactionState, CancellationToken cancel)
        {
            ISetTransactionStateEvents events = new SetTransactionStateEvents(connection, setTransactionState.Headers.RequestId);
            //ToDo: Implement HandleSetTransactionState for Common.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetTransactionState for Common is not implemented in SetTransactionStateHandler.cs");
            #else
                #error HandleSetTransactionState for Common is not implemented in SetTransactionStateHandler.cs
            #endif
        }

    }
}
