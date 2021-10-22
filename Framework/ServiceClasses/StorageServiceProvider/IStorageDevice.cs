/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoTServer;

// KAL specific implementation of storage. 
namespace XFS4IoTFramework.Storage
{
    public interface IStorageDevice : IDevice
    {
        #region Card

        /// <summary>
        /// Return storage information for current configuration and capabilities on the startup.
        /// </summary>
        /// <returns></returns>
        bool GetCardStorageConfiguration(out Dictionary<string, CardUnitStorageConfiguration> newCardUnits);

        /// <summary>
        /// This method is call after card is moved to the storage. Move or Reset command.
        /// </summary>
        /// <returns>Return true if the device maintains hardware counters for the card units</returns>
        bool GetCardUnitCounts(out Dictionary<string, CardUnitCount> unitCounts);

        /// <summary>
        /// Update card unit hardware status by device class. the maintaining status by the framework will be overwritten.
        /// The framework can't handle threshold event if the device class maintains hardware storage status on threshold value is not zero.
        /// </summary>
        /// <returns>Return true if the device maintains hardware card unit status</returns>
        bool GetCardUnitStatus(out Dictionary<string, CardStatusClass.ReplenishmentStatusEnum> unitStatus);

        /// <summary>
        /// Update card unit hardware storage status by device class.
        /// </summary>
        /// <returns>Return true if the device maintains hardware card storage status</returns>
        bool GetCardStorageStatus(out Dictionary<string, CardUnitStorage.StatusEnum> storageStatus);

        /// <summary>
        /// Set new configuration and counters
        /// </summary>
        /// <returns>Return operation is completed successfully or not and report updates storage information.</returns>
        Task<SetCardStorageResult> SetCardStorageAsync(SetCardStorageRequest request, CancellationToken cancellation);

        #endregion

        #region Cash

        /// <summary>
        /// Return storage information for current configuration and capabilities on the startup.
        /// </summary>
        /// <returns>Return true if the cash unit configuration or capabilities are changed, otherwise false</returns>
        bool GetCashStorageConfiguration(out Dictionary<string, CashUnitStorageConfiguration> newCardUnits);

        /// <summary>
        /// Return return cash unit counts maintained by the device
        /// </summary>
        /// <returns>Return true if the device class maintained counts, otherwise false</returns>
        bool GetCashUnitCounts(out Dictionary<string, CashUnitCountClass> unitCounts);

        /// <summary>
        /// Return return cash storage status
        /// </summary>
        /// <returns>Return true if the device class uses hardware status, otherwise false</returns>
        bool GetCashStorageStatus(out Dictionary<string, CashUnitStorage.StatusEnum> storageStatus);

        /// <summary>
        /// Return return cash unit status maintained by the device class
        /// </summary>
        /// <returns>Return true if the device class uses hardware status, otherwise false</returns>
        bool GetCashUnitStatus(out Dictionary<string, CashStatusClass.ReplenishmentStatusEnum> unitStatus);

        /// <summary>
        /// Return accuracy of counts. This method is called if the device class supports feature for count accuray
        /// </summary>
        void GetCashUnitAccuray(string storageId, out CashStatusClass.AccuracyEnum unitAccuracy);

        /// <summary>
        /// Set new configuration and counters
        /// </summary>
        /// <returns>Return operation is completed successfully or not and report updates storage information.</returns>
        Task<SetCashStorageResult> SetCashStorageAsync(SetCashStorageRequest request, CancellationToken cancellation);

        #endregion

        /// <summary>
        /// $ref: ../Docs/StartExchangeDescription.md
        /// </summary>
        Task<StartExchangeResult> StartExchangeAsync(CancellationToken cancellation);

        /// <summary>
        /// $ref: ../Docs/EndExchangeDescription.md
        /// </summary>
        Task<EndExchangeResult> EndExchangeAsync(CancellationToken cancellation);

    }
}
