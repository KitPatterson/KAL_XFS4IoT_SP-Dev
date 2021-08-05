﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoT.Crypto.Events;
using XFS4IoT.Common.Events;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.KeyManagement;

namespace XFS4IoTServer
{
    public partial class CryptoServiceClass
    {
        public CryptoServiceClass(IServiceProvider ServiceProvider,
                                  IKeyManagementServiceClass KeyManagement,
                                  ICommonService CommonService,
                                  ILogger logger)
        : this(ServiceProvider, logger)
        {
            this.KeyManagementService = KeyManagement.IsNotNull($"Unexpected parameter set in the " + nameof(CryptoServiceClass));
            this.CommonService = CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(CryptoServiceClass));
        }

        /// <summary>
        /// KeyManagement service interface
        /// </summary>
        private IKeyManagementServiceClass KeyManagementService { get; init; }

        /// <summary>
        /// Common service interface
        /// </summary>
        private ICommonService CommonService { get; init; }

        /// <summary>
        /// Stores KeyManagement interface capabilites internally
        /// </summary>
        public KeyManagementCapabilitiesClass KeyManagementCapabilities { get => CommonService.KeyManagementCapabilities; set => CommonService.KeyManagementCapabilities = value; }

        /// <summary>
        /// Stores Crypto interface capabilites internally
        /// </summary>
        public CryptoCapabilitiesClass CryptoCapabilities { get => CommonService.CryptoCapabilities; set => CommonService.CryptoCapabilities = value; }

        /// <summary>
        /// Find keyslot available or being used
        /// </summary>
        public int FindKeySlot(string KeyName) => KeyManagementService.FindKeySlot(KeyName);

        /// <summary>
        /// Stored key information of this device
        /// </summary>
        public List<KeyDetail> GetKeyTable() => KeyManagementService.GetKeyTable();

        /// <summary>
        /// Return detailed stored key information
        /// </summary>
        public KeyDetail GetKeyDetail(string KeyName) => KeyManagementService.GetKeyDetail(KeyName);

        /// <summary>
        /// Add new key into the collection and return key slot
        /// </summary>
        public void AddKey(string KeyName,
                           int KeySlot,
                           string KeyUsage,
                           string Algorithm,
                           string ModeOfUse,
                           string RestrictedKeyUsage,
                           string KeyVersionNumber,
                           string Exportability,
                           KeyDetail.KeyStatusEnum KeyStatus,
                           bool Preloaded,
                           int KeyLength,
                           List<byte> OptionalKeyBlockHeader,
                           int? Generation,
                           DateTime? ActivatingDate,
                           DateTime? ExpiryDate,
                           int? Version) => KeyManagementService.AddKey(KeyName,
                                                                        KeySlot,
                                                                        KeyUsage,
                                                                        Algorithm,
                                                                        ModeOfUse,
                                                                        RestrictedKeyUsage,
                                                                        KeyVersionNumber,
                                                                        Exportability,
                                                                        KeyStatus,
                                                                        Preloaded,
                                                                        KeyLength,
                                                                        OptionalKeyBlockHeader,
                                                                        Generation,
                                                                        ActivatingDate,
                                                                        ExpiryDate,
                                                                        Version);

        /// <summary>
        /// Delete specified key from the collection and return key slot
        /// </summary>
        public void DeleteKey(string KeyName) => KeyManagementService.DeleteKey(KeyName);

        /// <summary>
        /// Update key status
        /// </summary>
        public void UpdateKeyStatus(string KeyName, KeyDetail.KeyStatusEnum Status) => KeyManagementService.UpdateKeyStatus(KeyName, Status);
    }
}
