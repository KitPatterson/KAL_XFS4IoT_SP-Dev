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
using XFS4IoT.Storage.Commands;
using XFS4IoT.Storage.Completions;
using XFS4IoT.Storage;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Storage
{
    public partial class SetStorageHandler
    {
        private async Task<SetStorageCompletion.PayloadData> HandleSetStorage(ISetStorageEvents events, SetStorageCommand setStorage, CancellationToken cancel)
        {
            MessagePayload.CompletionCodeEnum completionCode = MessagePayload.CompletionCodeEnum.InternalError;
            string errorDescription = string.Empty;
            SetStorageCompletion.PayloadData.ErrorCodeEnum? errorCode = null;

            if (setStorage.Payload.Storage is null ||
                setStorage.Payload.Storage.Count == 0)
            {
                return new SetStorageCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                            $"No storage information is specified.");
            }

            if (Storage.StorageType == StorageTypeEnum.Card)
            {
                Dictionary<string, SetCardUnitStorage> cardStorageToSet = new();

                foreach (var storage in setStorage.Payload.Storage)
                {
                    if (storage.Value.Card is null ||
                        (storage.Value.Card.Configuration is null &&
                         storage.Value.Card.Status.InitialCount is null))
                    {
                        continue;
                    }

                    cardStorageToSet.Add(storage.Key, new SetCardUnitStorage(storage.Value.Card.Configuration is null ? null : new SetCardConfiguration(storage.Value.Card.Configuration.Threshold,
                                                                                                                                                        storage.Value.Card.Configuration.CardID),
                                                                             storage.Value.Card.Status.InitialCount));
                }

                if (cardStorageToSet.Count == 0)
                {
                    return new SetStorageCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"No card configuration is set to all units.");
                }

                Logger.Log(Constants.DeviceClass, "StorageDev.SetCardStorageAsync()");

                var result = await Device.SetCardStorageAsync(new(cardStorageToSet), cancel);
                
                Logger.Log(Constants.DeviceClass, $"StorageDev.SetCardStorageAsync() -> {result.CompletionCode}, {result.ErrorCode}");

                if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success)
                {
                    foreach (var storage in Storage.CardUnits)
                    {
                        foreach (var storageToUpdate in cardStorageToSet)
                        {
                            if (storage.Key != storageToUpdate.Key)
                                continue;

                            if (storageToUpdate.Value.Configuration is not null)
                            {
                                storage.Value.Unit.Configuration.CardId = storageToUpdate.Value.Configuration.CardId;

                                if (storageToUpdate.Value.Configuration.Threshold is not null)
                                {
                                    storage.Value.Unit.Configuration.Threshold = (int)storageToUpdate.Value.Configuration.Threshold;
                                }
                            }

                            if (storageToUpdate.Value.InitialCount is not null)
                            {
                                storage.Value.Unit.Status.InitialCount = (int)storageToUpdate.Value.InitialCount;
                                storage.Value.Unit.Status.Count = storage.Value.Unit.Status.InitialCount;
                                storage.Value.Unit.Status.RetainCount = 0;
                            }
                        }
                    }
                }

                completionCode = result.CompletionCode;
                errorDescription = result.ErrorDescription;
                errorCode = result.ErrorCode;
            }
            else
            {

            }

            // Keep updated storage information on the hard disk
            Storage.StorePersistent();

            return new SetStorageCompletion.PayloadData(completionCode, errorDescription, errorCode);
        }
    }
}
