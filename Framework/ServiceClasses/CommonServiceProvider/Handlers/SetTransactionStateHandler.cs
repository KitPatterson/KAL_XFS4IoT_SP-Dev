/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetTransactionStateHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
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

        private async Task<SetTransactionStateCompletion.PayloadData> HandleSetTransactionState(ISetTransactionStateEvents events, SetTransactionStateCommand setTransactionState, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CommonDev.SetTransactionState()");
            var result = await Device.SetTransactionState(setTransactionState.Payload);
            Logger.Log(Constants.DeviceClass, $"CommonDev.SetTransactionState() -> {result.CompletionCode}");

            return result;
        }
    }
}
