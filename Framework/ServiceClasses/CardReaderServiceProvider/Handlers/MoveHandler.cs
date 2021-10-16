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
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class MoveHandler
    {
        private async Task<MoveCompletion.PayloadData> HandleMove(IMoveEvents events, MoveCommand move, CancellationToken cancel)
        {
            if (string.IsNullOrEmpty(move.Payload.From))
            {
                return new MoveCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                      "The property From is not specified.");
            }

            MoveCardRequest.MovePosition.MovePositionEnum fromPos = MoveCardRequest.MovePosition.MovePositionEnum.Storage;
            if (move.Payload.From != "exit" &&
                move.Payload.From != "transport")
            {
                if (!CardReader.CardUnits.ContainsKey(move.Payload.From))
                {
                    return new MoveCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"Invalid StorageId supplied for From property. {move.Payload.From}");
                }
            }

            MoveCardRequest.MovePosition from = new (fromPos, fromPos == MoveCardRequest.MovePosition.MovePositionEnum.Storage ? move.Payload.From : null);

            if (string.IsNullOrEmpty(move.Payload.To))
            {
                return new MoveCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                      "The property To is not specified.");
            }

            MoveCardRequest.MovePosition.MovePositionEnum toPos = MoveCardRequest.MovePosition.MovePositionEnum.Storage;
            if (move.Payload.To != "exit" &&
                move.Payload.To != "transport")
            {
                if (!CardReader.CardUnits.ContainsKey(move.Payload.To))
                {
                    return new MoveCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"Invalid StorageId supplied for To property. {move.Payload.To}");
                }
            }

            MoveCardRequest.MovePosition to = new (toPos, toPos == MoveCardRequest.MovePosition.MovePositionEnum.Storage ? move.Payload.To : null);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.MoveCardAsync()");
            var result = await Device.MoveCardAsync(events,
                                                    new MoveCardRequest(from, to),
                                                    cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.MoveCardAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success)
            {
                // Update counters and save persistently
                if (to.Position == MoveCardRequest.MovePosition.MovePositionEnum.Storage)
                {
                    string storageId = to.StorageId;
                    if (string.IsNullOrEmpty(to.StorageId))
                    {
                        // Default position and the device class most report storage id
                        storageId = result.StorageId;
                    }

                    if (string.IsNullOrEmpty(storageId))
                    {
                        Logger.Warning(Constants.Framework, $"The device class returned an empty storage ID for the default position. the counter for the storage won't be updated.");
                    }
                    else
                    {
                        await CardReader.UpdateCardStorageCount(result.StorageId, result.CountMoved);
                    }
                }
            }

            return new MoveCompletion.PayloadData(result.CompletionCode,
                                                  result.ErrorDescription,
                                                  result.ErrorCode);
        }
    }
}
