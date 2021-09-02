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
    public partial class SecureKeyEntryHandler
    {
        private async Task<SecureKeyEntryCompletion.PayloadData> HandleSecureKeyEntry(ISecureKeyEntryEvents events, SecureKeyEntryCommand secureKeyEntry, CancellationToken cancel)
        {
            if (secureKeyEntry.Payload.KeyLen is null)
            {
                return new SecureKeyEntryCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"No KeyLen specified.");
            }

            if (secureKeyEntry.Payload.VerificationType is null)
            {
                return new SecureKeyEntryCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"No VerificationType specified.");
            }

            if (secureKeyEntry.Payload.AutoEnd is null)
                Logger.Warning(Constants.Framework, $"No AutoEnd specified. use default false.");


            return new SecureKeyEntryCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, null);
        }
    }
}
