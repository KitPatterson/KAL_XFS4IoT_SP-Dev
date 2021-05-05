using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoT.Common.Events;
using XFS4IoT.Dispenser.Events;
using XFS4IoTServer;

namespace DispenserServiceProvider
{
    /// <summary>
    /// Default implimentation of a dispenser service provider. 
    /// </summary>
    /// <remarks> 
    /// This represents a typical dispenser, which only implements the Dispenser, CashManagement and Common interfaces. 
    /// It's possible to create other service provider types by combining multiple service classes in the 
    /// same way. 
    /// </remarks>
    class DispenserServiceProvider : ServiceProvider, IDispenserServiceClass, ICashManagementServiceClass, ICommonServiceClass
    {
        public DispenserServiceProvider(EndpointDetails endpointDetails, string ServiceName, IDevice device, ILogger logger)
            :
            base(endpointDetails,
                 ServiceName,
                 new[] { XFSConstants.ServiceClass.Common, XFSConstants.ServiceClass.CashManagement, XFSConstants.ServiceClass.Dispenser },
                 device,
                 logger)
        {
            Dispenser = new DispenserServiceClass(this, logger);
            CashManagement = new CashManagementServiceClass(this, logger);
            Common = new CommonServiceClass(this, logger);
        }

        private readonly DispenserServiceClass Dispenser;
        private readonly CashManagementServiceClass CashManagement;
        private readonly CommonServiceClass Common;


        #region Dispenser unsolicited events
        public Task ItemsTakenEvent(ItemsTakenEvent.PayloadData Payload) => Dispenser.ItemsTakenEvent(Payload);

        public Task ShutterStatusChangedEvent(ShutterStatusChangedEvent.PayloadData Payload) => Dispenser.ShutterStatusChangedEvent(Payload);

        public Task MediaDetectedEvent(MediaDetectedEvent.PayloadData Payload) => Dispenser.MediaDetectedEvent(Payload);

        public Task ItemsPresentedEvent() => Dispenser.ItemsPresentedEvent();
        #endregion

        #region CashManagement unsolicited events
        public Task TellerInfoChangedEvent(XFS4IoT.CashManagement.Events.TellerInfoChangedEvent.PayloadData Payload) => CashManagement.TellerInfoChangedEvent(Payload);

        public Task CashUnitThresholdEvent(XFS4IoT.CashManagement.Events.CashUnitThresholdEvent.PayloadData Payload) => CashManagement.CashUnitThresholdEvent(Payload);

        public Task CashUnitInfoChangedEvent(XFS4IoT.CashManagement.Events.CashUnitInfoChangedEvent.PayloadData Payload) => CashManagement.CashUnitInfoChangedEvent(Payload);

        public Task SafeDoorOpenEvent() => CashManagement.SafeDoorOpenEvent();

        public Task SafeDoorClosedEvent() => CashManagement.SafeDoorClosedEvent();
        #endregion

        #region Common unsolicited events
        public Task PowerSaveChangeEvent(PowerSaveChangeEvent.PayloadData Payload) => Common.PowerSaveChangeEvent(Payload);

        public Task DevicePositionEvent(DevicePositionEvent.PayloadData Payload) => Common.DevicePositionEvent(Payload);
        #endregion

    }
}
