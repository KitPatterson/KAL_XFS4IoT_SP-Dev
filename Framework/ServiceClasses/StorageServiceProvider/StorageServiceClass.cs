/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoTFramework.Storage;
using XFS4IoTFramework.Common;
using XFS4IoT.Storage.Events;
using XFS4IoT.Storage;

namespace XFS4IoTServer
{
    public partial class StorageServiceClass
    {
        public StorageServiceClass(IServiceProvider ServiceProvider,
                                   ICommonService CommonService,
                                   ILogger logger, 
                                   IPersistentData PersistentData, 
                                   StorageTypeEnum StorageType)
            : this(ServiceProvider, logger)
        {
            this.CommonService = CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(StorageServiceClass));
            this.PersistentData = PersistentData;
            this.StorageType = StorageType;

            // Load persistent data
            CardUnits = PersistentData.Load<Dictionary<string, CardUnitStorage>>(ServiceProvider.Name + typeof(CardUnitStorage).FullName);
            if (CardUnits is null)
            {
                Logger.Warning(Constants.Framework, "Failed to load persistent data for card units. It could be a first run, SP type is CashDispenser/Recycler/Acceptor or no persistent exists on the file system.");
                CardUnits = new();
            }
            CashUnits = PersistentData.Load<Dictionary<string, CashUnitStorage>>(ServiceProvider.Name + typeof(CashUnitStorage).FullName);
            if (CashUnits is null)
            {
                Logger.Warning(Constants.Framework, "Failed to load persistent data for cash units. It could be a first run, SP type CardReader/CardDispenser or no persistent exists on the file system.");
                CashUnits = new();
            }

            if (StorageType == StorageTypeEnum.Card)
            {
                ConstructCardStorage();
            }
            else
            {
                ConstructCashUnits();
            }
        }

        /// <summary>
        /// Common service interface
        /// </summary>
        private ICommonService CommonService { get; init; }

        #region Card
        private void ConstructCardStorage()
        {
            Logger.Log(Constants.DeviceClass, "StorageDev.GetCardStorageConfiguration()");

            bool newConfiguration = Device.GetCardStorageConfiguration(out Dictionary<string, CardUnitStorageConfiguration> newCardUnits);

            Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardStorageConfiguration()-> {newConfiguration}");

            // first to update capabilites and configuration part of storage information
            if (newConfiguration)
            {
                CardUnits.Clear();
                foreach (var unit in newCardUnits)
                {
                    CardUnits.Add(unit.Key, new CardUnitStorage(unit.Value));
                }
            }

            // Update count from device
            Logger.Log(Constants.DeviceClass, "StorageDev.GetCardUnitCounts()");

            bool updateCounts = Device.GetCardUnitCounts(out Dictionary<string, CardUnitCount> unitCounts);

            Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardUnitCounts()-> {updateCounts}");

            if (updateCounts)
            {
                foreach (var status in unitCounts)
                {
                    CardUnits.ContainsKey(status.Key).IsTrue($"Specified card unit ID is not found. {status.Key}");

                    CardUnits[status.Key].Unit.Status.InitialCount = status.Value.InitialCount;
                    CardUnits[status.Key].Unit.Status.Count = status.Value.Count;
                    CardUnits[status.Key].Unit.Status.RetainCount = status.Value.RetainCount;
                }
            }

            // Update status from device
            foreach (var unit in CardUnits)
            {
                if (unit.Value.Unit.Configuration.Threshold != 0)
                {
                    // Use hardware status and check the device maintains status or not
                    Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardUnitStatus({unit.Key})");

                    bool updateStatus = Device.GetCardUnitStatus(unit.Key, out CardUnitStatus unitStatus);

                    Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardUnitStatus()-> {updateStatus}");

                    if (updateStatus)
                    {
                        unit.Value.Status = unitStatus.StorageStatus;
                        unit.Value.Unit.Status.ReplenishmentStatus = unitStatus.ReplenishmentStatus;
                    }
                }
            }

            // Save card units info persistently
            bool success = PersistentData.Store(ServiceProvider.Name + typeof(CardUnitStorage).FullName, CardUnits);
            if (!success)
            {
                Logger.Warning(Constants.Framework, $"Failed to save card unit counts.");
            }
        }

        /// <summary>
        /// Update storage count from the framework after media movement command is processed
        /// </summary>
        public async Task UpdateCardStorageCount(string storageId, int count)
        {
            CardUnits.ContainsKey(storageId).IsTrue($"Unexpected storageId is passed in before updating card unit counters. {storageId}");

            CardUnitStorage preserved = CardUnits[storageId];

            // Update count from device
            Logger.Log(Constants.DeviceClass, "StorageDev.GetCardUnitCounts()");

            bool updateCounts = Device.GetCardUnitCounts(out Dictionary<string, CardUnitCount> unitCounts);

            Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardUnitCounts()-> {updateCounts}");

            int beforeCountUpdate = 0;
            if (updateCounts)
            {
                CardUnits.ContainsKey(storageId).IsTrue($"Specified card unit ID is not found. {storageId}");
                beforeCountUpdate = CardUnits[storageId].Unit.Status.Count;
                if (CardUnits[storageId].Unit.Capabilities.Type == CardCapabilitiesClass.TypeEnum.Retain ||
                    CardUnits[storageId].Unit.Capabilities.Type == CardCapabilitiesClass.TypeEnum.Pard)
                {
                    CardUnits[storageId].Unit.Status.Count += count;
                }
                else
                {
                    CardUnits[storageId].Unit.Status.Count -= count;
                }

                if (CardUnits[storageId].Unit.Status.Count < 0)
                {
                    CardUnits[storageId].Unit.Status.Count = 0;
                }
            }

            // Update status from device
            if (CardUnits[storageId].Unit.Configuration.Threshold != 0)
            {
                // Use hardware status and check the device maintains status or not
                Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardUnitStatus({storageId})");

                bool updateStatus = Device.GetCardUnitStatus(storageId, out CardUnitStatus unitStatus);

                Logger.Log(Constants.DeviceClass, $"StorageDev.GetCardUnitStatus()-> {updateStatus}");

                if (updateStatus)
                {
                    CardUnits[storageId].Status = unitStatus.StorageStatus;
                    CardUnits[storageId].Unit.Status.ReplenishmentStatus = unitStatus.ReplenishmentStatus;
                }

                if (!updateStatus &&
                    (CardUnits[storageId].Unit.Capabilities.Type == CardCapabilitiesClass.TypeEnum.Dispense &&
                     beforeCountUpdate < CardUnits[storageId].Unit.Configuration.Threshold) ||
                    (CardUnits[storageId].Unit.Capabilities.Type == CardCapabilitiesClass.TypeEnum.Retain &&
                     beforeCountUpdate > CardUnits[storageId].Unit.Configuration.Threshold))
                {
                    // Park type of storage just ingore
                    StorageUnitClass payload = new(CardUnits[storageId].PositionName,
                                                   CardUnits[storageId].Capacity,
                                                   CardUnits[storageId].Status switch
                                                   { 
                                                       CardUnitStorage.StatusEnum.Good => StatusEnum.Ok,
                                                       CardUnitStorage.StatusEnum.Inoperative => StatusEnum.Inoperative,
                                                       CardUnitStorage.StatusEnum.Manipulated => StatusEnum.Manipulated,
                                                       CardUnitStorage.StatusEnum.Missing => StatusEnum.Missing,
                                                       _ => StatusEnum.NotConfigured,
                                                   },
                                                   CardUnits[storageId].SerialNumber,
                                                   Cash: null,
                                                   Card: new XFS4IoT.CardReader.StorageClass(
                                                            new XFS4IoT.CardReader.StorageCapabilitiesClass(CardUnits[storageId].Unit.Capabilities.Type switch
                                                                                                            {
                                                                                                                CardCapabilitiesClass.TypeEnum.Dispense => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Dispense,
                                                                                                                CardCapabilitiesClass.TypeEnum.Retain => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Retain,
                                                                                                                _ => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Park,
                                                                                                            },
                                                                                                            CardUnits[storageId].Unit.Capabilities.HardwareSensors),
                                                            new XFS4IoT.CardReader.StorageConfigurationClass(CardUnits[storageId].Unit.Configuration.CardId,
                                                                                                             CardUnits[storageId].Unit.Configuration.Threshold),
                                                            new XFS4IoT.CardReader.StorageStatusClass(CardUnits[storageId].Unit.Status.InitialCount,
                                                                                                      CardUnits[storageId].Unit.Status.Count,
                                                                                                      CardUnits[storageId].Unit.Status.RetainCount,
                                                                                                      CardUnits[storageId].Unit.Status.ReplenishmentStatus switch
                                                                                                      {
                                                                                                          CardStatusClass.ReplenishmentStatusEnum.Empty => ReplenishmentStatusEnumEnum.Empty,
                                                                                                          CardStatusClass.ReplenishmentStatusEnum.Full => ReplenishmentStatusEnumEnum.Full,
                                                                                                          CardStatusClass.ReplenishmentStatusEnum.High => ReplenishmentStatusEnumEnum.High,
                                                                                                          CardStatusClass.ReplenishmentStatusEnum.Low => ReplenishmentStatusEnumEnum.Low,
                                                                                                          _ => ReplenishmentStatusEnumEnum.Ok,
                                                                                                      })));

                    // Device class must fire threshold event if the count is managed.
                    await StorageThresholdEvent(new StorageThresholdEvent.PayloadData(payload));

                    if (CardUnits[storageId].Unit.Capabilities.Type == CardCapabilitiesClass.TypeEnum.Dispense)
                        CardUnits[storageId].Unit.Status.ReplenishmentStatus =CardStatusClass.ReplenishmentStatusEnum.Low;
                    else if (CardUnits[storageId].Unit.Capabilities.Type == CardCapabilitiesClass.TypeEnum.Retain)
                        CardUnits[storageId].Unit.Status.ReplenishmentStatus = CardStatusClass.ReplenishmentStatusEnum.High;
                }
            }

            // Save card units info persistently
            bool success = PersistentData.Store(ServiceProvider.Name + typeof(CardUnitStorage).FullName, CardUnits);
            if (!success)
            {
                Logger.Warning(Constants.Framework, $"Failed to save card unit counts.");
            }

            // Send changed event
            if (preserved != CardUnits[storageId])
            {
                StorageUnitClass payload = new(CardUnits[storageId].PositionName,
                                               CardUnits[storageId].Capacity,
                                               CardUnits[storageId].Status switch
                                               {
                                                   CardUnitStorage.StatusEnum.Good => StatusEnum.Ok,
                                                   CardUnitStorage.StatusEnum.Inoperative => StatusEnum.Inoperative,
                                                   CardUnitStorage.StatusEnum.Manipulated => StatusEnum.Manipulated,
                                                   CardUnitStorage.StatusEnum.Missing => StatusEnum.Missing,
                                                   _ => StatusEnum.NotConfigured,
                                               },
                                               CardUnits[storageId].SerialNumber,
                                               Cash: null,
                                               Card: new XFS4IoT.CardReader.StorageClass(
                                                        new XFS4IoT.CardReader.StorageCapabilitiesClass(CardUnits[storageId].Unit.Capabilities.Type switch
                                                                                                        {
                                                                                                            CardCapabilitiesClass.TypeEnum.Dispense => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Dispense,
                                                                                                            CardCapabilitiesClass.TypeEnum.Retain => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Retain,
                                                                                                            _ => XFS4IoT.CardReader.StorageCapabilitiesClass.TypeEnum.Park,
                                                                                                        },
                                                                                                        CardUnits[storageId].Unit.Capabilities.HardwareSensors),
                                                        new XFS4IoT.CardReader.StorageConfigurationClass(CardUnits[storageId].Unit.Configuration.CardId,
                                                                                                         CardUnits[storageId].Unit.Configuration.Threshold),
                                                        new XFS4IoT.CardReader.StorageStatusClass(CardUnits[storageId].Unit.Status.InitialCount,
                                                                                                  CardUnits[storageId].Unit.Status.Count,
                                                                                                  CardUnits[storageId].Unit.Status.RetainCount,
                                                                                                  CardUnits[storageId].Unit.Status.ReplenishmentStatus switch
                                                                                                  {
                                                                                                      CardStatusClass.ReplenishmentStatusEnum.Empty => ReplenishmentStatusEnumEnum.Empty,
                                                                                                      CardStatusClass.ReplenishmentStatusEnum.Full => ReplenishmentStatusEnumEnum.Full,
                                                                                                      CardStatusClass.ReplenishmentStatusEnum.High => ReplenishmentStatusEnumEnum.High,
                                                                                                      CardStatusClass.ReplenishmentStatusEnum.Low => ReplenishmentStatusEnumEnum.Low,
                                                                                                      _ => ReplenishmentStatusEnumEnum.Ok,
                                                                                                  })));

                // Device class must fire threshold event if the count is managed.
                await StorageChangedEvent(new  StorageChangedEvent.PayloadData(payload));
            }
        }
        #endregion

        #region Cash
        /// <summary>
        /// ConstructCashUnits
        /// The method retreive cash unit structures from the device class. The device class must provide cash unit structure info
        /// </summary>
        private void ConstructCashUnits()
        {
            Logger.Log(Constants.DeviceClass, "StorageDev.GetCashUnitConfiguration()");

            bool newConfiguration = Device.GetCashStorageConfiguration(out Dictionary<string, CashUnitStorage> newCashUnits);

            Logger.Log(Constants.DeviceClass, $"StorageDev.GetCashUnitConfiguration()-> {newConfiguration}");

            newCashUnits.IsNotNull("The device class returned an empty cash unit structure information on the GetCashUnitStructure.");

            if (newConfiguration)
            {
                CashUnits.Clear();
                foreach (var unit in newCashUnits)
                {
                    CashUnits.Add(unit.Key, new CashUnitStorage(unit.Value));
                }
            }
            else
            {
                bool identical = newCashUnits.Count == CashUnits.Count;
                foreach (var unit in newCashUnits)
                {
                    identical = CashUnits.ContainsKey(unit.Key);
                    if (!identical)
                    {
                        Logger.Warning(Constants.Framework, $"Existing cash unit information doesn't contain key specified by the device class. {unit.Key}. Construct new cash unit infomation.");
                        break;
                    }

                    identical = CashUnits[unit.Key].Configuration == unit.Value;
                    if (!identical)
                    {
                        Logger.Warning(Constants.Framework, $"Existing cash unit information doesn't have an identical cash unit structure information specified by the device class. {unit.Key}. Construct new cash unit infomation.");
                        break;
                    }
                }

                if (!identical)
                {
                    CashUnits.Clear();
                    foreach (var unit in newCashUnits)
                    {
                        CashUnits.Add(unit.Key, new CashUnit(unit.Value));
                    }
                }
            }

            if (!PersistentData.Store(ServiceProvider.Name + typeof(CashUnit).FullName, CashUnits))
            {
                Logger.Warning(Constants.Framework, "Failed to save persistent data.");
            }
        }

        /// <summary>
        /// UpdateCashUnitAccounting
        /// Update cash unit status and counts managed by the device specific class.
        /// </summary>
        public void UpdateCashUnitAccounting(Dictionary<string, ItemMovement> MovementResult = null)
        {
            // First to update item movement reported by the device class, then update entire counts if the device class maintains cash unit counts.
            if (MovementResult is not null)
            {
                foreach (var movement in MovementResult)
                {
                    if (!CashUnits.ContainsKey(movement.Key) ||
                        movement.Value != null)
                    {
                        continue;
                    }
                    // update counts
                    int dispensedCount = movement.Value.DispensedCount ?? 0;
                    CashUnits[movement.Key].Count -= dispensedCount;
                    if (CashUnits[movement.Key].Count < 0)
                        CashUnits[movement.Key].Count = 0;
                    CashUnits[movement.Key].DispensedCount += dispensedCount;
                    CashUnits[movement.Key].RejectCount += movement.Value.RejectCount ?? 0;
                    CashUnits[movement.Key].RetractedCount += movement.Value.RetractedCount ?? 0;
                    CashUnits[movement.Key].PresentedCount += movement.Value.PresentedCount ?? 0;
                    if (movement.Value.StoredBankNoteList is not null &&
                        movement.Value.StoredBankNoteList.Count > 0)
                    {
                        foreach (var (bkMoved, bkCount) in from BankNoteNumber bkMoved in movement.Value.StoredBankNoteList
                                                           from BankNoteNumber bkCount in CashUnits[movement.Key].BankNoteNumberList
                                                           where bkMoved.NoteID == bkCount.NoteID
                                                           select (bkMoved, bkCount))
                        {
                            bkCount.Count += bkMoved.Count;
                            CashUnits[movement.Key].CashInCount += bkMoved.Count;
                            break;
                        }
                    }
                }
            }

            Logger.Log(Constants.DeviceClass, "CashManagementDev.GetCashUnitAccounting()");

            Dictionary<string, CashUnitAccounting> cashUnitAccounting = Device.GetCashUnitAccounting();

            Logger.Log(Constants.DeviceClass, $"CashManagementDev.GetCashUnitAccounting()-> {cashUnitAccounting}");

            if (cashUnitAccounting is not null)
            {
                bool identical = cashUnitAccounting.Count == CashUnits.Count;
                foreach (var accounting in cashUnitAccounting)
                {
                    identical = CashUnits.ContainsKey(accounting.Key);
                    if (!identical)
                    {
                        Logger.Warning(Constants.Framework, $"Existing cash unit information doesn't contain key specified by the device class. {accounting.Key}. Reset count to zero.");
                        break;
                    }
                }

                if (identical)
                {
                    foreach (var unit in cashUnitAccounting)
                        CashUnits[unit.Key].Accounting = unit.Value;
                }
                else
                {
                    foreach (var unit in CashUnits)
                    {
                        unit.Value.Accounting = new CashUnitAccounting();
                    }
                }
            }

            Logger.Log(Constants.DeviceClass, "CashManagementDev.GetCashUnitStatus()");
            
            Dictionary<string, CashUnit.StatusEnum> cashUnitStatus = Device.GetCashUnitStatus();

            Logger.Log(Constants.DeviceClass, $"CashManagementDev.GetCashUnitStatus()-> {cashUnitStatus}");

            if (cashUnitStatus is not null)
            {
                foreach (var unit in cashUnitStatus)
                {
                    if (CashUnits.ContainsKey(unit.Key))
                        CashUnits[unit.Key].Status = unit.Value;
                }
            }
            else
            {
                foreach (var unit in CashUnits)
                {
                    if (unit.Value.Type == CashUnit.TypeEnum.NotApplicable)
                    {
                        unit.Value.Status = CashUnit.StatusEnum.NoValue;
                        continue;
                    }

                    unit.Value.Status = CashUnit.StatusEnum.Ok;

                    if (unit.Value.Type == CashUnit.TypeEnum.BillCassette ||
                        unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                        unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                        unit.Value.Type == CashUnit.TypeEnum.Coupon ||
                        unit.Value.Type == CashUnit.TypeEnum.Document ||
                        unit.Value.Type == CashUnit.TypeEnum.Recycling)
                    {

                        if (unit.Value.Count == 0)
                        {
                            unit.Value.Status = CashUnit.StatusEnum.Empty;
                        }
                        else if (unit.Value.Minimum != 0 &&
                                 unit.Value.Minimum >= unit.Value.Count)
                        {
                            unit.Value.Status = CashUnit.StatusEnum.Low;
                        }
                    }
                    else
                    {
                        if (unit.Value.MaximumCapacity != 0 &&
                            unit.Value.MaximumCapacity >= unit.Value.Count)
                        {
                            unit.Value.Status = CashUnit.StatusEnum.Full;
                        }
                        else if (unit.Value.Maximum != 0 &&
                                 unit.Value.Maximum >= unit.Value.Count)
                        {
                            unit.Value.Status = CashUnit.StatusEnum.High;
                        }
                    }
                }
            }

            if (!PersistentData.Store(ServiceProvider.Name + typeof(CashUnit).FullName, CashUnits))
            {
                Logger.Warning(Constants.Framework, "Failed to save persistent data.");
            }
        }

        #endregion

        /// <summary>
        /// Store CardUnits and CashUnits persistently
        /// </summary>
        public void StorePersistent()
        {
            if (!PersistentData.Store(ServiceProvider.Name + typeof(CashUnit).FullName, CashUnits))
            {
                Logger.Warning(Constants.Framework, "Failed to save persistent data.");
            }
        }

        /// <summary>
        /// Persistent data storage access
        /// </summary>
        private readonly IPersistentData PersistentData;

        /// <summary>
        /// Type of storage
        /// </summary>
        public StorageTypeEnum StorageType { get; init; }

        /// <summary>
        /// Card storage structure information of this device
        /// </summary>
        public Dictionary<string, CardUnitStorage> CardUnits { get; set; }

        /// <summary>
        /// Cash storage structure information of this device
        /// </summary>
        public Dictionary<string, CashUnitStorage> CashUnits { get; set; }
    }
}
