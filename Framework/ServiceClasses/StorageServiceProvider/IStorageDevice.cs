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
        /// This methid is called if the storage threshold is set to zero to report hardware status, otherwise the framework will not make a call.
        /// </summary>
        /// <returns>Return true if the device maintains hardware card unit status</returns>
        bool GetCardUnitStatus(string storageId, out CardUnitStatus unitStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return operation is completed successfully or not and report updates storage information.</returns>
        Task<SetCardStorageResult> SetCardStorageAsync(SetCardStorageRequest request, CancellationToken cancellation);

        #endregion

        #region Cash

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
