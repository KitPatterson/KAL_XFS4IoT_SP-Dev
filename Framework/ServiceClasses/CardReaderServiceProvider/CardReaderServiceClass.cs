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
using XFS4IoTFramework.Common;
using XFS4IoTFramework.Storage;

namespace XFS4IoTServer
{
    public partial class CardReaderServiceClass
    {
        public CardReaderServiceClass(IServiceProvider ServiceProvider,
                                      ICommonService CommonService,
                                      IStorageServiceClass StorageService,
                                      ILogger logger)
            : this(ServiceProvider, logger)
        {
            this.CommonService = CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(CardReaderServiceClass));
            this.StorageService = StorageService.IsNotNull($"Unexpected parameter set in the " + nameof(CardReaderServiceClass));
        }

        /// <summary>
        /// Stores CardReader interface capabilites internally
        /// </summary>
        public CardReaderCapabilitiesClass CardReaderCapabilities { get => CommonService.CardReaderCapabilities; set => CommonService.CardReaderCapabilities = value; }

        /// <summary>
        /// Common service interface
        /// </summary>
        public ICommonService CommonService { get; init; }


        /// <summary>
        /// Card storage information device supports 
        /// </summary>
        public Dictionary<string, CardUnitStorage> CardStorages { get => StorageService.CardUnits; set => StorageService.CardUnits = value; }

        /// <summary>
        /// Storage service interface
        /// </summary>
        public IStorageServiceClass StorageService { get; init;  }
    }
}
