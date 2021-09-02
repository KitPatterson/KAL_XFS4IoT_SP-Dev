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
    public partial class DataEntryHandler
    {
        private async Task<DataEntryCompletion.PayloadData> HandleDataEntry(IDataEntryEvents events, DataEntryCommand dataEntry, CancellationToken cancel)
        {
            if (dataEntry.Payload.MaxLen is null)
                Logger.Warning(Constants.Framework, $"No MaxLen specified. use default 0.");

            if (dataEntry.Payload.AutoEnd is null)
                Logger.Warning(Constants.Framework, $"No AutoEnd specified. use default false.");

            // TO BE REMOVED
            await Task.Delay(100, cancel);

            return new DataEntryCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, null);
        }
    }
}
