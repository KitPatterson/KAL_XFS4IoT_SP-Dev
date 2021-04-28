/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * SetKeyHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    /// <summary>
    /// SetKeyRequest
    /// Provide key information to be loaded into the module.
    /// </summary>
    public sealed class SetCIM86KeyRequest
    {
        /// <summary>
        /// SetKeyRequest
        /// </summary>
        /// <param name="KeyValue">Key value to be loaded into CIM86 module</param>
        public SetCIM86KeyRequest(List<byte> KeyValue = null)
        {
            this.KeyValue = KeyValue;
        }

        public List<byte> KeyValue { get; private set; }
    }

    /// <summary>
    /// SetKeyResult
    /// Return result of loading key value into the module.
    /// </summary>
    public sealed class SetCIM86KeyResult : DeviceResult
    {
        public SetCIM86KeyResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 SetKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                 string ErrorDescription = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public SetKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
    }

    public partial class SetKeyHandler
    {
        private async Task<SetKeyCompletion.PayloadData> HandleSetKey(ISetKeyEvents events, SetKeyCommand setKey, CancellationToken cancel)
        {
            if (string.IsNullOrEmpty(setKey.Payload.KeyValue))
            {
                return new SetKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                        "No key data supplied.",
                                                        SetKeyCompletion.PayloadData.ErrorCodeEnum.InvalidKey);
            }

            List<byte> keyValue = new(Convert.FromBase64String(setKey.Payload.KeyValue));

            Logger.Log(Constants.DeviceClass, "CardReaderDev.SetKey()");
            var result = await Device.SetCIM86Key(new SetCIM86KeyRequest(keyValue));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.SetKey() -> {result.CompletionCode}");

            return new SetKeyCompletion.PayloadData(result.CompletionCode,
                                                    result.ErrorDescription,
                                                    result.ErrorCode);
        }

    }
}
