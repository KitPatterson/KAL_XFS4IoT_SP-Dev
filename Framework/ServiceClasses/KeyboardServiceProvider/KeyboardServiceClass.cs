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
using XFS4IoTFramework.Keyboard;
using XFS4IoTFramework.Common;

namespace XFS4IoTServer
{
    public partial class KeyboardServiceClass
    {
        public KeyboardServiceClass(IServiceProvider ServiceProvider,
                                    ICommonService CommonService,
                                    ILogger logger)
        : this(ServiceProvider, logger)
        {
            this.CommonService = CommonService.IsNotNull($"Unexpected parameter set in the " + nameof(KeyboardServiceClass));
            FirstGetLayoutCommand = true;
            SupportedFunctionKeys = new();
            SupportedFunctionKeysWithShift = new();
        }

        /// <summary>
        /// Stores KeyManagement interface capabilites internally
        /// </summary>
        public KeyboardCapabilitiesClass KeyboardCapabilitiesCapabilities { get => CommonService.KeyboardCapabilities; set => CommonService.KeyboardCapabilities = value; }

        /// <summary>
        /// Common service interface
        /// </summary>
        private ICommonService CommonService { get; init; }

        /// <summary>
        /// True when the framework received a keyboard layout information from the device class
        /// </summary>
        public bool FirstGetLayoutCommand { get; set; }

        /// <summary>
        /// Function keys device supported
        /// </summary>
        public Dictionary<EntryModeEnum, List<string>> SupportedFunctionKeys { get; set; }

        /// <summary>
        /// Function keys device supported with shift key
        /// </summary>
        public Dictionary<EntryModeEnum, List<string>> SupportedFunctionKeysWithShift { get; set; }

        /// <summary>
        /// Keyboard layout device supported
        /// </summary>
        public Dictionary<EntryModeEnum, List<FrameClass>> KeyboardLayouts { get; set; }
    }
}
