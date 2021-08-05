/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using XFS4IoT.Completions;
using XFS4IoTFramework.Common;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class InitializationHandler
    {
        private async Task<InitializationCompletion.PayloadData> HandleInitialization(IInitializationEvents events, InitializationCommand initialization, CancellationToken cancel)
        {
            if (string.IsNullOrEmpty(initialization.Payload.Key))
            {
                KeyDetail keyinfo = KeyManagement.GetKeyDetail(initialization.Payload.Key);
                if (keyinfo is null)
                {
                    return new InitializationCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                    $"Specified key is not found. {initialization.Payload.Key}",
                                                                    InitializationCompletion.PayloadData.ErrorCodeEnum.AccessDenied);
                }
            }

            Logger.Log(Constants.DeviceClass, "CryptoDev.Initialization()");

            var result = await Device.Initialization(events,
                                                     new InitializationRequest(initialization.Payload.Key,
                                                                               string.IsNullOrEmpty(initialization.Payload.Ident) ? null : Convert.FromBase64String(initialization.Payload.Ident).ToList()),
                                                     cancel);

            Logger.Log(Constants.DeviceClass, $"CryptoDev.Initialization() -> {result.CompletionCode}, {result.ErrorCode}");

            return new InitializationCompletion.PayloadData(result.CompletionCode,
                                                            result.ErrorDescription,
                                                            result.ErrorCode,
                                                            result.Identification is not null || result.Identification.Count == 0 ? null : Convert.ToBase64String(result.Identification.ToArray()));
        }
    }
}
