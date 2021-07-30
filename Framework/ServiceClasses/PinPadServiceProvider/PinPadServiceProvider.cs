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
using XFS4IoT.Common.Events;
using XFS4IoTFramework.Crypto;
using XFS4IoTFramework.Common;
using XFS4IoT.PinPad.Events;

namespace XFS4IoTServer
{
    /// <summary>
    /// Default implimentation of a pinpad service provider. 
    /// </summary>
    /// <remarks> 
    /// This represents a typical pinpad, which implements the PinPad, KeyManagement, Keyboard, Crypto and Common interfaces. 
    /// It's possible to create other service provider types by combining multiple service classes in the 
    /// same way. 
    /// </remarks>
    public class PinPadServiceProvider : ServiceProvider, IPinPadServiceClass, IKeyManagementServiceClass, IKeyboardServiceClass, ICryptoServiceClass, ICommonServiceClass
    {
        public PinPadServiceProvider(EndpointDetails endpointDetails, string ServiceName, IDevice device, ILogger logger, IPersistentData persistentData)
            :
            base(endpointDetails,
                 ServiceName,
                 new[] { XFSConstants.ServiceClass.Common, XFSConstants.ServiceClass.Crypto, XFSConstants.ServiceClass.Keyboard, XFSConstants.ServiceClass.KeyManagement, XFSConstants.ServiceClass.PinPad },
                 device,
                 logger)
        {
            CommonService = new CommonServiceClass(this, logger);
            CryptoService = new CryptoServiceClass(this, logger);
            KeyboardService = new KeyboardServiceClass(this, logger);
            KeyManagementService = new KeyManagementServiceClass(this, logger);
            PinPadService = new PinPadServiceClass(this, logger);
        }

        private readonly PinPadServiceClass PinPadService;
        private readonly KeyManagementServiceClass KeyManagementService;
        private readonly KeyboardServiceClass KeyboardService;
        private readonly CryptoServiceClass CryptoService;
        private readonly CommonServiceClass CommonService;


        #region PinPad unsolicited events
        public Task IllegalKeyAccessEvent(IllegalKeyAccessEvent.PayloadData Payload) => PinPadService.IllegalKeyAccessEvent(Payload);
        #endregion

        #region KeyManagement unsolicited events
        public Task InitializedEvent(XFS4IoT.KeyManagement.Events.InitializedEvent.PayloadData Payload) => KeyManagementService.InitializedEvent(Payload);

        public Task IllegalKeyAccessEvent(XFS4IoT.KeyManagement.Events.IllegalKeyAccessEvent.PayloadData Payload) => KeyManagementService.IllegalKeyAccessEvent(Payload);

        public Task CertificateChangeEvent(XFS4IoT.KeyManagement.Events.CertificateChangeEvent.PayloadData Payload) => KeyManagementService.CertificateChangeEvent(Payload);
        #endregion

        #region Crypto unsolicited events
        public Task IllegalKeyAccessEvent(XFS4IoT.Crypto.Events.IllegalKeyAccessEvent.PayloadData Payload) => CryptoService.IllegalKeyAccessEvent(Payload);
        #endregion

        #region Common unsolicited events
        public Task PowerSaveChangeEvent(PowerSaveChangeEvent.PayloadData Payload) => CommonService.PowerSaveChangeEvent(Payload);

        public Task DevicePositionEvent(DevicePositionEvent.PayloadData Payload) => CommonService.DevicePositionEvent(Payload);

        public Task NonceClearedEvent(NonceClearedEvent.PayloadData Payload) => CommonService.NonceClearedEvent(Payload);
        #endregion

    }
}
