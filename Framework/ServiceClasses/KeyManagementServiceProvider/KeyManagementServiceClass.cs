﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using XFS4IoT;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.KeyManagement;
using XFS4IoT.KeyManagement.Events;

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
            KeyDetails = PersistentData.Load< Dictionary<string, KeyDetail>> (ServiceProvider.Name + typeof(KeyDetail).FullName);
            if (KeyDetails is null)
                KeyDetails = new();

            CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(KeyManagementServiceClass));
            this.CommonService = CommonService.IsA<ICommonService>($"Invalid interface parameter specified for common service. " + nameof(KeyManagementServiceClass));

            GetStatus();
            GetCapabilities();
        }

        #region Common Service
        /// <summary>
        /// Common service interface
        /// </summary>
        private ICommonService CommonService { get; init; }

        /// <summary>
        /// Stores Common interface capabilites internally
        /// </summary>
        public CommonCapabilitiesClass CommonCapabilities { get => CommonService.CommonCapabilities; set => CommonService.CommonCapabilities = value; }

        /// <summary>
        /// Common Status
        /// </summary>
        public CommonStatusClass CommonStatus { get => CommonService.CommonStatus; set => CommonService.CommonStatus = value; }

        /// <summary>
        /// Stores KeyManagement interface capabilites internally
        /// </summary>
        public KeyManagementCapabilitiesClass KeyManagementCapabilities { get => CommonService.KeyManagementCapabilities; set => CommonService.KeyManagementCapabilities = value; }

        /// <summary>
        /// Stores KeyManagement interface status internally
        /// </summary>
        public KeyManagementStatusClass KeyManagementStatus { get => CommonService.KeyManagementStatus; set => CommonService.KeyManagementStatus = value; }

        #endregion

        #region Common unsolicited events
        public Task StatusChangedEvent(CommonStatusClass.DeviceEnum? Device,
                                       CommonStatusClass.PositionStatusEnum? Position,
                                       int? PowerSaveRecoveryTime,
                                       CommonStatusClass.AntiFraudModuleEnum? AntiFraudModule,
                                       CommonStatusClass.ExchangeEnum? Exchange,
                                       CommonStatusClass.EndToEndSecurityEnum? EndToEndSecurity) => CommonService.StatusChangedEvent(Device,
                                                                                                                                     Position,
                                                                                                                                     PowerSaveRecoveryTime,
                                                                                                                                     AntiFraudModule,
                                                                                                                                     Exchange,
                                                                                                                                     EndToEndSecurity);
        public Task NonceClearedEvent(string ReasonDescription) => throw new NotImplementedException("NonceClearedEvent is not supported in the Crypto Service.");

        public Task ErrorEvent(CommonStatusClass.ErrorEventIdEnum EventId,
                               CommonStatusClass.ErrorActionEnum Action,
                               string VendorDescription) => CommonService.ErrorEvent(EventId, Action, VendorDescription);

        #endregion

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
            KeyDetails.IsNotNull($"The list object {nameof(KeyDetail)} is null in the FindKeySlot.");

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
        public List<KeyDetail> GetKeyTable() => KeyDetails?.Values.ToList();

        /// <summary>
        /// Return detailed stored key information
        /// </summary>
        public KeyDetail GetKeyDetail(string KeyName)
        {
            KeyDetails.IsNotNull($"The list object {nameof(GetKeyDetail)} is null in the FindKeySlot.");

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
                           int KeyLength,
                           KeyDetail.KeyStatusEnum KeyStatus,
                           bool Preloaded,
                           string RestrictedKeyUsage,
                           string KeyVersionNumber,
                           string Exportability,
                           List<byte> OptionalKeyBlockHeader,
                           int? Generation,
                           DateTime? ActivatingDate,
                           DateTime? ExpiryDate,
                           int? Version)
        {
            if (KeyDetails.ContainsKey(KeyName))
                KeyDetails.Remove(KeyName);
            KeyDetails.Add(KeyName, new KeyDetail(KeyName,
                                                  KeySlot,
                                                  KeyUsage,
                                                  Algorithm,
                                                  ModeOfUse,
                                                  KeyLength,
                                                  KeyStatus,
                                                  Preloaded,
                                                  RestrictedKeyUsage,
                                                  KeyVersionNumber,
                                                  Exportability,
                                                  OptionalKeyBlockHeader,
                                                  Generation,
                                                  ActivatingDate,
                                                  ExpiryDate,
                                                  Version));

            PersistentData.Store<Dictionary<string, KeyDetail>>(ServiceProvider.Name + typeof(KeyDetail).FullName, KeyDetails);
        }

        /// <summary>
        /// Delete specified key from the collection and return key slot
        /// </summary>
        public void DeleteKey(string KeyName)
        {
            KeyDetails.IsNotNull($"The list object {nameof(GetKeyDetail)} is null in the DeleteKey.");

            if (KeyDetails.ContainsKey(KeyName))
            {
                KeyDetails.Remove(KeyName);
                PersistentData.Store<Dictionary<string, KeyDetail>>(ServiceProvider.Name + typeof(KeyDetail).FullName, KeyDetails);
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
            KeyDetails.IsNotNull($"The list object {nameof(GetKeyDetail)} is null in the UpdateKeyStatus.");

            KeyDetails.ContainsKey(KeyName).IsTrue($"No key found. {KeyName}" + nameof(UpdateKeyStatus));
            KeyDetail keyDetail = GetKeyDetail(KeyName);
            DeleteKey(KeyName);

            KeyDetails.Add(KeyName, new KeyDetail(KeyName,
                                                  keyDetail.KeySlot,
                                                  keyDetail.KeyUsage,
                                                  keyDetail.Algorithm,
                                                  keyDetail.ModeOfUse,
                                                  keyDetail.KeyLength,
                                                  Status,
                                                  keyDetail.Preloaded,
                                                  keyDetail.RestrictedKeyUsage,
                                                  keyDetail.KeyVersionNumber,
                                                  keyDetail.Exportability,
                                                  keyDetail.OptionalKeyBlockHeader,
                                                  keyDetail.Generation,
                                                  keyDetail.ActivatingDate,
                                                  keyDetail.ExpiryDate,
                                                  keyDetail.Version));

            PersistentData.Store<Dictionary<string, KeyDetail>>(ServiceProvider.Name + typeof(KeyDetail).FullName, KeyDetails);
        }

        private readonly SecureKeyEntryStatusClass SecureKeyEntryStatus = new();

        /// <summary>
        /// Return secure key entry component status
        /// The device specified class reset current status if the stored key components are claered except successful Initialization command.
        /// </summary>
        public SecureKeyEntryStatusClass GetSecureKeyEntryStatus() => SecureKeyEntryStatus;

        public Task IllegalKeyAccessEvent(string KeyName, KeyAccessErrorCodeEnum ErrorCode) => IllegalKeyAccessEvent(new IllegalKeyAccessEvent.PayloadData(KeyName, ErrorCode switch
        {
            KeyAccessErrorCodeEnum.AlgorithmNotSupp => XFS4IoT.KeyManagement.Events.IllegalKeyAccessEvent.PayloadData.ErrorCodeEnum.AlgorithmNotSupp,
            KeyAccessErrorCodeEnum.KeyNotFound => XFS4IoT.KeyManagement.Events.IllegalKeyAccessEvent.PayloadData.ErrorCodeEnum.KeyNotFound,
            KeyAccessErrorCodeEnum.KeyNoValue => XFS4IoT.KeyManagement.Events.IllegalKeyAccessEvent.PayloadData.ErrorCodeEnum.KeyNoValue,
            _ => XFS4IoT.KeyManagement.Events.IllegalKeyAccessEvent.PayloadData.ErrorCodeEnum.UseViolation,
        }));

        public Task CertificateChangeEvent(CertificateChangeEnum CertificateChange) => CertificateChangeEvent(new CertificateChangeEvent.PayloadData(CertificateChange switch
        {
            _ => XFS4IoT.KeyManagement.Events.CertificateChangeEvent.PayloadData.CertificateChangeEnum.Secondary
        }));

        private void GetStatus()
        {
            Logger.Log(Constants.DeviceClass, "KeyManagementDev.KeyManagementStatus");
            KeyManagementStatus = Device.KeyManagementStatus;
            Logger.Log(Constants.DeviceClass, "KeyManagementDev.KeyManagementStatus=");

            KeyManagementStatus.IsNotNull($"The device class set KeyManagementStatus property to null. The device class must report device status.");
        }

        private void GetCapabilities()
        {
            Logger.Log(Constants.DeviceClass, "KeyManagementDev.KeyManagementCapabilities");
            KeyManagementCapabilities = Device.KeyManagementCapabilities;
            Logger.Log(Constants.DeviceClass, "KeyManagementDev.KeyManagementCapabilities=");

            KeyManagementCapabilities.IsNotNull($"The device class set KeyManagementCapabilities property to null. The device class must report device capabilities.");
        }
    }
}
