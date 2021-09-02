/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Keyboard.Commands;
using XFS4IoT.Keyboard.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Keyboard
{
    public partial class PinEntryHandler
    {
        private async Task<PinEntryCompletion.PayloadData> HandlePinEntry(IPinEntryEvents events, PinEntryCommand pinEntry, CancellationToken cancel)
        {
            if (pinEntry.Payload.MaxLen is null)
                Logger.Warning(Constants.Framework, $"No MaxLen specified. use default 0.");

            if (pinEntry.Payload.MinLen is null)
                Logger.Warning(Constants.Framework, $"No MinLen specified. use default 0.");

            if (pinEntry.Payload.AutoEnd is null)
                Logger.Warning(Constants.Framework, $"No AutoEnd specified. use default false.");

            // TO BE REMOVED
            await Task.Delay(100, cancel);

            return new PinEntryCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, null);
        }
    }
}
