/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.

\***********************************************************************************************/

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.CashManagement;
using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTServer
{
    public partial class CashManagementServiceClass
    {
        public CashManagementServiceClass(IServiceProvider ServiceProvider,
                                          ICommonService CommonService,
                                          IStorageServiceClass StorageService,
                                          ILogger logger, 
                                          IPersistentData PersistentData) 
            : this(ServiceProvider, logger)
        {
            this.PersistentData = PersistentData.IsNotNull($"No persistent data interface is set. " + nameof(CashManagementServiceClass));

            // Load persistent data
            CashUnits = PersistentData.Load<Dictionary<string, CashUnit>>(ServiceProvider.Name + typeof(CashUnit).FullName);
            if (CashUnits is null)
            {
                Logger.Warning(Constants.Framework, "Failed to load persistent data. It could be a first run and no persistent exists on the file system.");
                CashUnits = new Dictionary<string, CashUnit>();
            }

            FirstCashUnitInfoCommand = true;
            this.CommonService = CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(CashManagementServiceClass));
        }

        /// <summary>
        /// Common service interface
        /// </summary>
        private ICommonService CommonService { get; init; }

        /// <summary>
        /// Stores CashDispenser interface capabilites internally
        /// </summary>
        public CashDispenserCapabilitiesClass CashDispenserCapabilities { get => CommonService.CashDispenserCapabilities; set => CommonService.CashDispenserCapabilities = value; }

        /// <summary>
        /// Stores CashManagement interface capabilites internally
        /// </summary>
        public CashManagementCapabilitiesClass CashManagementCapabilities { get => CommonService.CashManagementCapabilities; set => CommonService.CashManagementCapabilities = value; }
    }
}
