/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SynchronizeCommandHandler.cs uses automatically generated parts. 
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
    public partial class SynchronizeCommandHandler
    {

        private async Task<SynchronizeCommandCompletion.PayloadData> HandleSynchronizeCommand(ISynchronizeCommandEvents events, SynchronizeCommandCommand synchronizeCommand, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CommonDev.SynchronizeCommand()");
            var result = await Device.SynchronizeCommand(synchronizeCommand.Payload);
            Logger.Log(Constants.DeviceClass, $"CommonDev.SynchronizeCommand() -> {result.CompletionCode}");

            return result;
        }
    }
}
