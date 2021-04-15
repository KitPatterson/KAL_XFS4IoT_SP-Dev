/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * StatusHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 13:51:50
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
    public partial class StatusHandler
    {

        private async Task HandleStatus(IConnection connection, StatusCommand status, CancellationToken cancel)
        {
            IStatusEvents events = new StatusEvents(connection, status.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CommonDev.Status()");
            var result = await Device.Status(events, status.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CommonDev.Status() -> {result.CompletionCode}");

            await connection.SendMessageAsync(new StatusCompletion(status.Headers.RequestId, result));
        }

    }
}
