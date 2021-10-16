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
    public partial class GetStorageHandler
    { 
        private Task<GetStorageCompletion.PayloadData> HandleGetStorage(IGetStorageEvents events, GetStorageCommand getStorage, CancellationToken cancel)
        {
            Dictionary<string, StorageUnitClass> storageResponse = new();
            if (Storage.StorageType == StorageTypeEnum.Card)
            {
                foreach (var storage in Storage.CardUnits)
                {
                    StorageUnitClass thisStorage = new(storage.Value.PositionName,
                                                       storage.Value.Capacity,
                                                       storage.Value.Status switch
                                                       {
                                                           CardUnitStorage.StatusEnum.Good => StatusEnum.Ok,
                                                           CardUnitStorage.StatusEnum.Inoperative => StatusEnum.Inoperative,
                                                           CardUnitStorage.StatusEnum.Manipulated => StatusEnum.Manipulated,
                                                           CardUnitStorage.StatusEnum.Missing => StatusEnum.Missing,
                                                           _ => StatusEnum.NotConfigured,
                                                       },
                                                       storage.Value.SerialNumber,
                                                       Cash: null,
                                                       Card: new XFS4IoT.CardReader.StorageClass(new XFS4IoT.CardReader.StorageCapabilitiesClass(storage.Value.Unit.Capabilities.Type switch
                                                                                                                                                 {
                                                                                                                                                     CardCapabilitiesClass.TypeEnum.Dispense => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Dispense,
                                                                                                                                                     CardCapabilitiesClass.TypeEnum.Retain => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Retain,
                                                                                                                                                     _ => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Park,
                                                                                                                                                 },
                                                                                                                                                 storage.Value.Unit.Capabilities.HardwareSensors),
                                                                                                 new XFS4IoT.CardReader.StorageConfigurationClass(storage.Value.Unit.Configuration.CardId,
                                                                                                                                                  storage.Value.Unit.Configuration.Threshold),
                                                                                                 new XFS4IoT.CardReader.StorageStatusClass(storage.Value.Unit.Status.InitialCount,
                                                                                                                                           storage.Value.Unit.Status.Count,
                                                                                                                                           storage.Value.Unit.Status.RetainCount,
                                                                                                                                           storage.Value.Unit.Status.ReplenishmentStatus switch
                                                                                                                                           {
                                                                                                                                               CardStatusClass.ReplenishmentStatusEnum.Empty => ReplenishmentStatusEnumEnum.Empty,
                                                                                                                                               CardStatusClass.ReplenishmentStatusEnum.Full => ReplenishmentStatusEnumEnum.Full,
                                                                                                                                               CardStatusClass.ReplenishmentStatusEnum.High => ReplenishmentStatusEnumEnum.High,
                                                                                                                                               CardStatusClass.ReplenishmentStatusEnum.Low => ReplenishmentStatusEnumEnum.Low,
                                                                                                                                              _ => ReplenishmentStatusEnumEnum.Ok,
                                                                                                                                           })));
                    storageResponse.Add(storage.Key, thisStorage);
                }
            }
            else
            {

            }

            return Task.FromResult(new GetStorageCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                        null,
                                                                        storageResponse));
        }
    }
}
