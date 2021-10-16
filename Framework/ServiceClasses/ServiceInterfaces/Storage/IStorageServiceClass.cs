﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XFS4IoT.Storage.Events;
using XFS4IoTFramework.Common;

namespace XFS4IoTFramework.Storage
{
    public enum StorageTypeEnum
    {
        Cash,
        Card,
    }

    public interface IStorageService : ICommonService
    {
        /// <summary>
        /// Update storage count from the framework after media movement command is processed
        /// </summary>
        Task UpdateCardStorageCount(string storageId, int count);

        /// <summary>
        /// Return which type of storage SP is using
        /// </summary>
        StorageTypeEnum StorageType { get; set; }

        /// <summary>
        /// Store CardUnits and CashUnits persistently
        /// </summary>
        void StorePersistent();

        /// <summary>
        /// Card storage structure information of this device
        /// </summary>
        Dictionary<string, CardUnitStorage> CardUnits { get; set; }

        /// <summary>
        /// Cash storage structure information of this device
        /// </summary>
        Dictionary<string, CashUnitStorage> CashUnits { get; set; }
    }

    public interface IStorageServiceClass : IStorageService, IStorageUnsolicitedEvents
    {
    }
}
