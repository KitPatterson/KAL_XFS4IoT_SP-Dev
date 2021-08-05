/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoT.KeyManagement.Events;
using XFS4IoT.Common.Events;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.KeyManagement;

namespace XFS4IoTServer
{
    public partial class KeyManagementServiceClass
    {
        public KeyManagementServiceClass(IServiceProvider ServiceProvider,
                                         ICommonService CommonService,
                                         ILogger logger,
                                         IPersistentData PersistentData)
        : this(ServiceProvider, logger)
        {
            this.PersistentData = PersistentData.IsNotNull($"No persistent data interface is set in the " + typeof(KeyManagementServiceClass));
            KeyDetails = PersistentData.Load< Dictionary<string, KeyDetail>> (typeof(Dictionary<string, KeyDetail>).FullName);

            this.CommonService = CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(KeyManagementServiceClass));
        }

        /// <summary>
        /// Common service interface
        /// </summary>
        private ICommonService CommonService { get; init; }

        /// <summary>
        /// Persistent data storage access
        /// </summary>
        private readonly IPersistentData PersistentData;

        /// <summary>
        /// Stored key information of this device
        /// </summary>
        private Dictionary<string, KeyDetail> KeyDetails { get; init; }

        /// <summary>
        /// Find keyslot available or being used
        /// </summary>
        public int FindKeySlot(string KeyName)
        {
            int keySlot = 1;
            if (KeyDetails.ContainsKey(KeyName))
            {
                keySlot = KeyDetails[KeyName].KeySlot;
            }
            else
            {
                if (KeyDetails.Count > 0)
                {
                    int[] keySlots = KeyDetails.Values.Select(v => v.KeySlot).ToArray();
                    keySlot = keySlots.Where(s => !keySlots.Contains(s + 1)).Select(s => s + 1).Min();
                }
            }
            return keySlot;
        }

        /// <summary>
        /// Stored key information of this device
        /// </summary>
        public List<KeyDetail> GetKeyTable() => KeyDetails.Values.ToList();

        /// <summary>
        /// Return detailed stored key information
        /// </summary>
        public KeyDetail GetKeyDetail(string KeyName)
        {
            KeyDetail keyInfo = null;
            if (KeyDetails.ContainsKey(KeyName))
                keyInfo = KeyDetails[KeyName];

            return keyInfo;
        }


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
                           int? Version)
        {
            KeyDetails.Add(KeyName, new KeyDetail(KeyName,
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
                                                  Version));

            PersistentData.Store<Dictionary<string, KeyDetail>>(typeof(Dictionary<string, KeyDetail>).FullName, KeyDetails);
        }

        /// <summary>
        /// Delete specified key from the collection and return key slot
        /// </summary>
        public void DeleteKey(string KeyName)
        {
            if (KeyDetails.ContainsKey(KeyName))
            {
                KeyDetails.Remove(KeyName);
                PersistentData.Store<Dictionary<string, KeyDetail>>(typeof(Dictionary<string, KeyDetail>).FullName, KeyDetails);
            }
            else
            {
                Logger.Warning(Constants.Framework, $"Invalid KeyName specified to delete. doesn't exist. {KeyName}");
            }
        }

        /// <summary>
        /// Update key status
        /// </summary>
        public void UpdateKeyStatus(string KeyName, KeyDetail.KeyStatusEnum Status)
        {
            KeyDetails.ContainsKey(KeyName).IsTrue($"No key found. {KeyName}" + nameof(UpdateKeyStatus));
            KeyDetail keyDetail = GetKeyDetail(KeyName);
            DeleteKey(KeyName);

            KeyDetails.Add(KeyName, new KeyDetail(KeyName,
                                                  keyDetail.KeySlot,
                                                  keyDetail.KeyUsage,
                                                  keyDetail.Algorithm,
                                                  keyDetail.ModeOfUse,
                                                  keyDetail.RestrictedKeyUsage,
                                                  keyDetail.KeyVersionNumber,
                                                  keyDetail.Exportability,
                                                  Status,
                                                  keyDetail.Preloaded,
                                                  keyDetail.KeyLength,
                                                  keyDetail.OptionalKeyBlockHeader,
                                                  keyDetail.Generation,
                                                  keyDetail.ActivatingDate,
                                                  keyDetail.ExpiryDate,
                                                  keyDetail.Version));

            PersistentData.Store<Dictionary<string, KeyDetail>>(typeof(Dictionary<string, KeyDetail>).FullName, KeyDetails);
        }
    }
}
