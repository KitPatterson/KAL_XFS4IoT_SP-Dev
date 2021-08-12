/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class DeleteKeyHandler
    {
        private async Task<DeleteKeyCompletion.PayloadData> HandleDeleteKey(IDeleteKeyEvents events, DeleteKeyCommand deleteKey, CancellationToken cancel)
        {
            if (!string.IsNullOrEmpty(deleteKey.Payload.Key))
            {
                KeyDetail keyDetail = KeyManagement.GetKeyDetail(deleteKey.Payload.Key);
                if (keyDetail is null)
                {
                    return new DeleteKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                               $"Specified key doesn't exist. {deleteKey.Payload.Key}");
                }
            }

            Logger.Log(Constants.DeviceClass, "KeyManagement.DeleteKey()");

            var result = await Device.DeleteKey(new DeleteKeyRequest(deleteKey.Payload.Key), cancel);

            Logger.Log(Constants.DeviceClass, $"KeyManagement.DeleteKey() -> {result.CompletionCode}");

            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success)
            {
                // Delete internal key information
                if (string.IsNullOrEmpty(deleteKey.Payload.Key))
                {
                    foreach (var key in KeyManagement.GetKeyTable())
                    {
                        if (!key.Preloaded)
                            KeyManagement.DeleteKey(key.KeyName);
                    }
                }
                else
                {
                    KeyManagement.DeleteKey(deleteKey.Payload.Key);
                }
            }

            return new DeleteKeyCompletion.PayloadData(result.CompletionCode,
                                                       result.ErrorDescription);
        }
    }
}
