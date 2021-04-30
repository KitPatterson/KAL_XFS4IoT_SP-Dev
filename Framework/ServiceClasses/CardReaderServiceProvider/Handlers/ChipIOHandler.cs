/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipIOHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class ChipIOHandler
    {
        private async Task<ChipIOCompletion.PayloadData> HandleChipIO(IChipIOEvents events, ChipIOCommand chipIO, CancellationToken cancel)
        {
            if (string.IsNullOrEmpty(chipIO.Payload.ChipData))
            {
                return new ChipIOCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                        "No chip IO data supplied.",
                                                        ChipIOCompletion.PayloadData.ErrorCodeEnum.InvalidData);
            }

            ChipIORequest.ChipProtocolEnum? chipProtocol = chipIO.Payload.ChipProtocol switch
            {
                "chipT0" => ChipIORequest.ChipProtocolEnum.chipT0,
                "chipT1" => ChipIORequest.ChipProtocolEnum.chipT1,
                "chipTypeAPart3" => ChipIORequest.ChipProtocolEnum.chipTypeAPart3,
                "chipTypeAPart4" => ChipIORequest.ChipProtocolEnum.chipTypeAPart4,
                "chipTypeB" => ChipIORequest.ChipProtocolEnum.chipTypeB,
                "chipTypeNFC" => ChipIORequest.ChipProtocolEnum.chipTypeNFC,
                _ => null
            };

            if (chipProtocol is null)
            {
                return new ChipIOCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                        $"Unsupported chip protocol supplied. {chipIO.Payload.ChipProtocol}",
                                                        ChipIOCompletion.PayloadData.ErrorCodeEnum.InvalidData);
            }

            List<byte> chipData = new(Convert.FromBase64String(chipIO.Payload.ChipData));

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ChipIOAsync()");
            var result = await Device.ChipIOAsync(new ChipIORequest((ChipIORequest.ChipProtocolEnum)chipProtocol, chipData),
                                                  cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ChipIOAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            return new ChipIOCompletion.PayloadData(result.CompletionCode,
                                                    result.ErrorDescription,
                                                    result.ErrorCode,
                                                    chipIO.Payload.ChipProtocol,
                                                    result.ChipData == null ? null : Convert.ToBase64String(result.ChipData.ToArray()));
        }

    }
}
