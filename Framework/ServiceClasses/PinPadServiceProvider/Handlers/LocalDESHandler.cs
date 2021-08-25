/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoT.PinPad.Commands;
using XFS4IoT.PinPad.Completions;
using XFS4IoT.Completions;
using XFS4IoTFramework.KeyManagement;

namespace XFS4IoTFramework.PinPad
{
    public partial class LocalDESHandler
    {
        private async Task<LocalDESCompletion.PayloadData> HandleLocalDES(ILocalDESEvents events, LocalDESCommand localDES, CancellationToken cancel)
        { 
            if (string.IsNullOrEmpty(localDES.Payload.ValidationData))
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"No validation data specified.");
            }

            if (string.IsNullOrEmpty(localDES.Payload.Key))
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"No key name specified to verify PIN locally.");
            }

            KeyDetail key = PinPad.GetKeyDetail(localDES.Payload.Key);
            if (key is null)
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                          $"Specified key is not loaded.",
                                                          LocalDESCompletion.PayloadData.ErrorCodeEnum.KeyNotFound);
            }

            if (key.KeyUsage != "V0")
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                          $"Specified key usage is not expected.{key.KeyUsage}",
                                                          LocalDESCompletion.PayloadData.ErrorCodeEnum.UseViolation);
            }

            if (!string.IsNullOrEmpty(localDES.Payload.KeyEncKey))
            {
                KeyDetail keyEncKey = PinPad.GetKeyDetail(localDES.Payload.KeyEncKey);
                if (keyEncKey is null)
                {
                    return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                              $"Specified key encryption key is not loaded.",
                                                              LocalDESCompletion.PayloadData.ErrorCodeEnum.KeyNotFound);
                }

                if (key.KeyUsage != "K0" &&
                    key.KeyUsage != "K1" &&
                    key.KeyUsage != "K2" &&
                    key.KeyUsage != "K3")
                {
                    return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                              $"Specified key encryption key usage is not expected.{keyEncKey.KeyUsage}",
                                                              LocalDESCompletion.PayloadData.ErrorCodeEnum.UseViolation);
                }
            }

            if (string.IsNullOrEmpty(localDES.Payload.DecTable))
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"No ASCII decimalization table specified.");
            }

            if (!string.IsNullOrEmpty(localDES.Payload.Offset))
            {
                foreach (char c in localDES.Payload.Offset)
                {
                    if (!Uri.IsHexDigit(c))
                    {
                        return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                  $"Offset data should be in hexstring. {localDES.Payload.Offset}");
                    }
                }
            }

            byte padding = 0;
            if (localDES.Payload.Padding is not null)
            {
                try
                {
                    _ = byte.TryParse(localDES.Payload.Padding, out padding);
                    if (padding > 0xf)
                    {
                        return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                 $"Padding value range if 0x0 - 0xf . {localDES.Payload.Padding}");
                    }
                }
                catch (Exception)
                { }
            }

            if (localDES.Payload.MaxPIN is null)
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"No max PIN specified.");
            }
            if (localDES.Payload.NoLeadingZero is null)
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"No NoLeadingZero specified.");
            }
            if (localDES.Payload.ValDigits is null)
            {
                return new LocalDESCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"No ValDigits specified.");
            }

            Logger.Log(Constants.DeviceClass, "PinPadDev.VerifyPINLocalDES()");

            var result = await Device.VerifyPINLocalDES(new VerifyPINLocalDESRequest(localDES.Payload.ValidationData,
                                                                                     localDES.Payload.Offset,
                                                                                     padding,
                                                                                     (int)localDES.Payload.MaxPIN,
                                                                                     (int)localDES.Payload.ValDigits,
                                                                                     localDES.Payload.NoLeadingZero is null ? false : (bool)localDES.Payload.NoLeadingZero,
                                                                                     localDES.Payload.Key,
                                                                                     localDES.Payload.KeyEncKey,
                                                                                     localDES.Payload.DecTable),
                                                        cancel);

            Logger.Log(Constants.DeviceClass, $"PinPadDev.VerifyPINLocalDES() -> {result.CompletionCode}, {result.ErrorCode}");

            return new LocalDESCompletion.PayloadData(result.CompletionCode,
                                                      result.ErrorDescription,
                                                      result.ErrorCode switch
                                                      {
                                                          VerifyPINLocalResult.ErrorCodeEnum.AccessDenied => LocalDESCompletion.PayloadData.ErrorCodeEnum.AccessDenied,
                                                          VerifyPINLocalResult.ErrorCodeEnum.FormatNotSupported => LocalDESCompletion.PayloadData.ErrorCodeEnum.FormatNotSupported,
                                                          VerifyPINLocalResult.ErrorCodeEnum.InvalidKeyLength => LocalDESCompletion.PayloadData.ErrorCodeEnum.InvalidKeyLength,
                                                          VerifyPINLocalResult.ErrorCodeEnum.KeyNotFound => LocalDESCompletion.PayloadData.ErrorCodeEnum.KeyNotFound,
                                                          VerifyPINLocalResult.ErrorCodeEnum.KeyNoValue => LocalDESCompletion.PayloadData.ErrorCodeEnum.KeyNoValue,
                                                          VerifyPINLocalResult.ErrorCodeEnum.NoPin => LocalDESCompletion.PayloadData.ErrorCodeEnum.NoPin,
                                                          _ => LocalDESCompletion.PayloadData.ErrorCodeEnum.AccessDenied,
                                                      },
                                                      result.Verified);
        }
    }
}
