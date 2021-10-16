using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Storage
{
    /// <summary>
    /// CardUnitStorage class representing XFS4IoT Storage class strcuture
    /// </summary>
    [Serializable()]
    public sealed record CardUnitStorage
    {
        public enum StatusEnum
        {
            Good,
            Inoperative,
            Missing,
            NotConfigured,
            Manipulated,
        }

        public CardUnitStorage(CardUnitStorageConfiguration StorageConfiguration)
        {
            this.PositionName = StorageConfiguration.PositionName;
            this.Capacity = StorageConfiguration.Capacity;
            this.Status = StatusEnum.NotConfigured;
            this.SerialNumber = StorageConfiguration.SerialNumber;

            this.Unit = new CardUnit(StorageConfiguration.Capabilities,
                                     StorageConfiguration.Configuration);
        }

        /// <summary>
        /// Fixed physical name for the position.
        /// </summary>
        public string PositionName { get; init; }

        /// <summary>
        /// Fixed physical name for the position.
        /// </summary>
        public int Capacity { get; init; }

        /// <summary>
        /// Status of this storage
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// The storage unit's serial number if it can be read electronically.
        /// </summary>
        public string SerialNumber { get; init; }

        /// <summary>
        /// Card Unit information
        /// </summary>
        public CardUnit Unit { get; init; }
    }

    /// <summary>
    /// Capabilities of this Card unit
    /// </summary>
    [Serializable()]
    public sealed record CardCapabilitiesClass
    {
        public enum TypeEnum
        {
            Retain,
            Dispense,
            Pard,
        }

        public CardCapabilitiesClass(TypeEnum Type, 
                                 bool HardwareSensors)
        {
            this.Type = Type;
            this.HardwareSensors = HardwareSensors;
        }

        public TypeEnum Type { get; init; }

        public bool HardwareSensors { get; init; }
    }

    /// <summary>
    /// Configuration of this Card Unit
    /// </summary>
    [Serializable()]
    public sealed record CardConfigurationClass
    {
        public CardConfigurationClass(int Threshold, 
                                  string CardId = null)
        {
            this.CardId = CardId;
            this.Threshold = Threshold;
        }

        public string CardId { get; set; }

        public int Threshold { get; set; }
    }

    /// <summary>
    /// Status of this Card Unit
    /// </summary>
    [Serializable()]
    public sealed record CardStatusClass
    {
        public enum ReplenishmentStatusEnum
        {
            Healthy,
            Full,
            Low,
            High,
            Empty,
        }

        public CardStatusClass(int InitialCount, 
                           int Count, 
                           int RetainCount, 
                           ReplenishmentStatusEnum ReplenishmentStatus)
        {
            this.InitialCount = InitialCount;
            this.Count = Count;
            this.RetainCount = RetainCount;
            this.ReplenishmentStatus = ReplenishmentStatus;
        }

        public int InitialCount { get; set; }

        public int Count { get; set; }

        public int RetainCount { get; set; }

        public ReplenishmentStatusEnum ReplenishmentStatus { get; set; }
    }

    /// <summary>
    /// Structure receiving from the device
    /// </summary>
    [Serializable()]
    public sealed record CardUnitStorageConfiguration
    {
        /// <summary>
        /// Fixed physical name for the position.
        /// </summary>
        public string PositionName { get; init; }

        /// <summary>
        /// Fixed physical name for the position.
        /// </summary>
        public int Capacity { get; init; }

        /// <summary>
        /// The storage unit's serial number if it can be read electronically.
        /// </summary>
        public string SerialNumber { get; init; }

        public CardCapabilitiesClass Capabilities { get; init; }

        public CardConfigurationClass Configuration { get; init; }
    }

    /// <summary>
    /// Card Unit strcuture the device class supports
    /// </summary>
    [Serializable()]
    public sealed record CardUnit
    {
        public CardUnit(CardCapabilitiesClass Capabilities,
                        CardConfigurationClass Configuration)
        {
            this.Capabilities = Capabilities;
            this.Configuration = Configuration;
            this.Status = new CardStatusClass(0, 0, 0, CardStatusClass.ReplenishmentStatusEnum.Empty);
        }

        public CardCapabilitiesClass Capabilities { get; init; }

        public CardConfigurationClass Configuration { get; init; }

        public CardStatusClass Status { get; init; }
    }

    /// <summary>
    /// Structure to update card unit status from the device if the device uses hardware sensors
    /// </summary>
    public sealed class CardUnitStatus
    {
        public CardUnitStatus(CardUnitStorage.StatusEnum StorageStatus,
                              CardStatusClass.ReplenishmentStatusEnum ReplenishmentStatus)
        {
            this.StorageStatus = StorageStatus;
            this.ReplenishmentStatus = ReplenishmentStatus;
        }

        public CardUnitStorage.StatusEnum StorageStatus { get; init; }
        public CardStatusClass.ReplenishmentStatusEnum ReplenishmentStatus { get; init; }
    }

    /// <summary>
    /// Structure to update card unit from the device
    /// </summary>
    public sealed class CardUnitCount
    {
        public CardUnitCount(int InitialCount,
                             int Count,
                             int RetainCount)
        {
            this.InitialCount = InitialCount;
            this.Count = Count;
            this.RetainCount = RetainCount;
        }

        public int InitialCount { get; init; }
        public int Count { get; init; }
        public int RetainCount { get; init; }
    }
}