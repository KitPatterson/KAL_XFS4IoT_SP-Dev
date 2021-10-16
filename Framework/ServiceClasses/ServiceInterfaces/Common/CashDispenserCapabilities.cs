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

namespace XFS4IoTFramework.Common
{
    /// <summary>
    /// CashDispenserCapabilities
    /// Store device capabilites for the cash dispenser device
    /// </summary>
    public sealed class CashDispenserCapabilitiesClass
    {
        /// <summary>
        /// tellerBill - The Dispenser is a Teller Bill Dispenser.
        /// selfServiceBill - The Dispenser is a Self-Service Bill Dispenser.
        /// tellerCoin - The Dispenser is a Teller Coin Dispenser.
        /// selfServiceCoin - The Dispenser is a Self-Service Coin Dispenser.
        /// </summary>
        public enum TypeEnum
        {
            tellerBill,
            selfServiceBill,
            tellerCoin,
            selfServiceCoin,
        }

        /// <summary>
        /// Common output shutter position
        /// default - Output location is determined by Service.
        /// left - Present items to left side of device.
        /// right - Present items to right side of device.
        /// center - Present items to center output position.
        /// top - Present items to the top output position.
        /// bottom - Present items to the bottom output position.
        /// front - Present items to the front output position.
        /// rear - Present items to the rear output position.
        /// reject - Reject bin is used as output location.
        /// </summary>
        public enum OutputPositionEnum
        {
            Default,
            Left,
            Right,
            Center,
            Top,
            Bottom,
            Front,
            Rear,
        }

        /// <summary>
        /// Retract - The items may be retracted to a retract cash unit.
        /// Transport - The items may be retracted to the transport.
        /// Stacker - The items may be retracted to the intermediate stacker.
        /// Reject - The items may be retracted to a reject cash unit.
        /// ItemCassette - The items may be retracted to the item cassettes, i.e. cassettes that can be dispensed from.
        /// Default - The item may be retracted to the default position.
        /// /// </summary>
        public enum RetractAreaEnum
        {
            Retract,
            Transport,
            Stacker,
            Reject,
            ItemCassette,
            Default,
        }

        /// <summary>
        /// Present - The items may be presented.
        /// Retract - The items may be moved to a retract cash unit.
        /// Reject - The items may be moved to a reject bin.
        /// ItemCassette - The items may be moved to the item cassettes, i.e. cassettes that can be dispensed from.
        /// </summary>
        public enum RetractTransportActionEnum
        {
            Present,
            Retract,
            Reject,
            ItemCassette,
        }

        /// <summary>
        /// Present - The items may be presented.
        /// Retract - The items may be moved to a retract cash unit.
        /// Reject - The items may be moved to a reject bin.
        /// ItemCassette - The items may be moved to the item cassettes, i.e. cassettes that can be dispensed from.
        /// </summary>
        public enum RetractStackerActionEnum
        {
            Present,
            Retract,
            Reject,
            ItemCassette,
        }

        /// <summary>
        /// FromCashUnit - The Dispenser can dispense items from the cash units to the intermediate stacker while there are items on the transport.
        /// ToCashUnit - The Dispenser can retract items to the cash units while there are items on the intermediate stacker.
        /// ToTransport - The Dispenser can retract items to the transport while there are items on the intermediate stacker.
        /// ToStacker - The Dispenser can dispense items from the cash units to the intermediate stacker while there are already items on the
        /// intermediate stacker that have not been in customer access.Items remaining on the stacker from a previous dispense
        /// may first need to be rejected explicitly by the application if they are not to be presented.
        /// </summary>
        public enum MoveItemEnum
        {
            FromCashUnit,
            ToCashUnit,
            ToTransport,
            ToStacker,
        }

        public CashDispenserCapabilitiesClass(TypeEnum Type,
                                              int MaxDispenseItems,
                                              bool ShutterControl,
                                              Dictionary<RetractAreaEnum, bool> RetractAreas,
                                              Dictionary<RetractTransportActionEnum, bool> RetractTransportActions,
                                              Dictionary<RetractStackerActionEnum, bool> RetractStackerActions,
                                              bool IntermediateStacker,
                                              bool ItemsTakenSensor,
                                              Dictionary<OutputPositionEnum, bool> OutputPositons,
                                              Dictionary<MoveItemEnum, bool> MoveItems)
        {
            this.Type = Type;
            this.MaxDispenseItems = MaxDispenseItems;
            this.ShutterControl = ShutterControl;
            this.RetractAreas = RetractAreas;
            this.RetractTransportActions = RetractTransportActions;
            this.RetractStackerActions = RetractStackerActions;
            this.IntermediateStacker = IntermediateStacker;
            this.ItemsTakenSensor = ItemsTakenSensor;
            this.OutputPositons = OutputPositons;
            this.MoveItems = MoveItems;
        }

        /// <summary>
        /// Supplies the type of CDM
        /// </summary>
        public TypeEnum Type { get; init; }

        /// <summary>
        /// Supplies the maximum number of items that can be dispensed in a single dispense operation. 
        /// </summary>
        public int MaxDispenseItems { get; init; }

        /// <summary>
        /// Specifies whether or not the commands Dispenser.OpenShutter and Dispenser.CloseShutter are supported.
        /// </summary>
        public bool Shutter { get; init; }

        /// <summary>
        /// If set to TRUE the shutter is controlled implicitly by the Service. 
        /// If set to FALSE the shutter must be controlled explicitly by the application
        /// using the Dispenser.OpenShutter and the Dispenser.CloseShutter commands.
        /// This property is always true if the device has no shutter. This field applies to all shutters and all positions.
        /// </summary>
        public bool ShutterControl { get; init; }

        /// <summary>
        /// Retract areas support of this device
        /// </summary>
        public Dictionary<RetractAreaEnum, bool> RetractAreas { get; init; }

        /// <summary>
        /// Action support on retracting cash to the transport
        /// </summary>
        public Dictionary<RetractTransportActionEnum, bool> RetractTransportActions { get; init; }

        /// <summary>
        /// Action support on retracting cash to the stacker
        /// </summary>
        public Dictionary<RetractStackerActionEnum, bool> RetractStackerActions { get; init; }

        /// <summary>
        /// Specifies whether or not the Dispenser supports stacking items to an intermediate position before the items are moved to the exit position.
        /// </summary>
        public bool IntermediateStacker { get; init; }

        /// <summary>
        ///  Specifies whether the Dispenser can detect when items at the exit position are taken by the user. 
        /// </summary>
        public bool ItemsTakenSensor { get; init; }

        /// <summary>
        /// Supported output positions
        /// </summary>
        public Dictionary<OutputPositionEnum, bool> OutputPositons { get; init; }

        /// <summary>
        /// Move items from stacker or transport to the unit
        /// </summary>
        public Dictionary<MoveItemEnum, bool> MoveItems { get; init; }
    }
}
