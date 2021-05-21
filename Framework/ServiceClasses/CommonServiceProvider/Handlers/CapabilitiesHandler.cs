/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using XFS4IoTServer.Common;

namespace XFS4IoTFramework.Common
{
    public partial class CapabilitiesHandler
    {

        private Task<CapabilitiesCompletion.PayloadData> HandleCapabilities(ICapabilitiesEvents events, CapabilitiesCommand capabilities, CancellationToken cancel)
        {
            Common.IsA<CommonServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");
            CommonServiceClass CommonService = Common as CommonServiceClass;

            Logger.Log(Constants.DeviceClass, "CommonDev.Capabilities()");
            var result = Device.Capabilities();
            Logger.Log(Constants.DeviceClass, $"CommonDev.Capabilities() -> {result.CompletionCode}");

            if (result.CashDispenser is not null)
            {
                Dictionary<CashDispenserCapabilitiesClass.RetractAreaEnum, bool> retractAreas = new()
                {
                    { CashDispenserCapabilitiesClass.RetractAreaEnum.ItemCassette, false },
                    { CashDispenserCapabilitiesClass.RetractAreaEnum.Reject, false },
                    { CashDispenserCapabilitiesClass.RetractAreaEnum.Retract, false },
                    { CashDispenserCapabilitiesClass.RetractAreaEnum.Stacker, false },
                    { CashDispenserCapabilitiesClass.RetractAreaEnum.Transport, false },
                    { CashDispenserCapabilitiesClass.RetractAreaEnum.Default, true }
                };

                if (result.CashDispenser.RetractAreas is not null)
                {
                    if (result.CashDispenser.RetractAreas.ItemCassette is not null &&
                        (bool)result.CashDispenser.RetractAreas.ItemCassette)
                    {
                        retractAreas[CashDispenserCapabilitiesClass.RetractAreaEnum.ItemCassette] = true;
                    }
                    if (result.CashDispenser.RetractAreas.Reject is not null &&
                        (bool)result.CashDispenser.RetractAreas.Reject)
                    {
                        retractAreas[CashDispenserCapabilitiesClass.RetractAreaEnum.Reject] = true;
                    }
                    if (result.CashDispenser.RetractAreas.Retract is not null &&
                        (bool)result.CashDispenser.RetractAreas.Retract)
                    {
                        retractAreas[CashDispenserCapabilitiesClass.RetractAreaEnum.Retract] = true;
                    }
                    if (result.CashDispenser.RetractAreas.Stacker is not null &&
                        (bool)result.CashDispenser.RetractAreas.Stacker)
                    {
                        retractAreas[CashDispenserCapabilitiesClass.RetractAreaEnum.Stacker] = true;
                    }
                    if (result.CashDispenser.RetractAreas.Transport is not null &&
                        (bool)result.CashDispenser.RetractAreas.Transport)
                    {
                        retractAreas[CashDispenserCapabilitiesClass.RetractAreaEnum.Transport] = true;
                    }
                }

                Dictionary<CashDispenserCapabilitiesClass.RetractStackerActionEnum, bool> retractStackerActions = new()
                {
                    { CashDispenserCapabilitiesClass.RetractStackerActionEnum.ItemCassette, false },
                    { CashDispenserCapabilitiesClass.RetractStackerActionEnum.Present, false },
                    { CashDispenserCapabilitiesClass.RetractStackerActionEnum.Reject, false },
                    { CashDispenserCapabilitiesClass.RetractStackerActionEnum.Retract, false }
                };

                if (result.CashDispenser.RetractStackerActions is not null)
                {
                    if (result.CashDispenser.RetractStackerActions.ItemCassette is not null &&
                        (bool)result.CashDispenser.RetractStackerActions.ItemCassette)
                    {
                        retractStackerActions[CashDispenserCapabilitiesClass.RetractStackerActionEnum.ItemCassette] = true;
                    }
                    if (result.CashDispenser.RetractStackerActions.Present is not null &&
                        (bool)result.CashDispenser.RetractStackerActions.Present)
                    {
                        retractStackerActions[CashDispenserCapabilitiesClass.RetractStackerActionEnum.Present] = true;
                    }
                    if (result.CashDispenser.RetractStackerActions.Reject is not null &&
                        (bool)result.CashDispenser.RetractStackerActions.Reject)
                    {
                        retractStackerActions[CashDispenserCapabilitiesClass.RetractStackerActionEnum.Reject] = true;
                    }
                    if (result.CashDispenser.RetractStackerActions.Retract is not null &&
                        (bool)result.CashDispenser.RetractStackerActions.Retract)
                    {
                        retractStackerActions[CashDispenserCapabilitiesClass.RetractStackerActionEnum.Retract] = true;
                    }
                }

                Dictionary<CashDispenserCapabilitiesClass.RetractTransportActionEnum, bool> retractTransportActions = new()
                {
                    { CashDispenserCapabilitiesClass.RetractTransportActionEnum.ItemCassette, false },
                    { CashDispenserCapabilitiesClass.RetractTransportActionEnum.Present, false },
                    { CashDispenserCapabilitiesClass.RetractTransportActionEnum.Reject, false },
                    { CashDispenserCapabilitiesClass.RetractTransportActionEnum.Retract, false }
                };

                if (result.CashDispenser.RetractTransportActions is not null)
                {
                    if (result.CashDispenser.RetractTransportActions.ItemCassette is not null &&
                        (bool)result.CashDispenser.RetractTransportActions.ItemCassette)
                    {
                        retractTransportActions[CashDispenserCapabilitiesClass.RetractTransportActionEnum.ItemCassette] = true;
                    }
                    if (result.CashDispenser.RetractTransportActions.Present is not null &&
                        (bool)result.CashDispenser.RetractTransportActions.Present)
                    {
                        retractTransportActions[CashDispenserCapabilitiesClass.RetractTransportActionEnum.Present] = true;
                    }
                    if (result.CashDispenser.RetractTransportActions.Reject is not null &&
                        (bool)result.CashDispenser.RetractTransportActions.Reject)
                    {
                        retractTransportActions[CashDispenserCapabilitiesClass.RetractTransportActionEnum.Reject] = true;
                    }
                    if (result.CashDispenser.RetractTransportActions.Retract is not null &&
                        (bool)result.CashDispenser.RetractTransportActions.Retract)
                    {
                        retractTransportActions[CashDispenserCapabilitiesClass.RetractTransportActionEnum.Retract] = true;
                    }
                }

                Dictionary<CashDispenserCapabilitiesClass.OutputPositionEnum, bool> outputPositions = new()
                {
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Center, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Default, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Front, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Left, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Rear, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Right, false },
                    { CashDispenserCapabilitiesClass.OutputPositionEnum.Top, false }
                };

                if (result.CashDispenser.Positions is not null)
                {
                    if (result.CashDispenser.Positions.Bottom is not null &&
                        (bool)result.CashDispenser.Positions.Bottom)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom] = true;
                    }
                    if (result.CashDispenser.Positions.Center is not null &&
                        (bool)result.CashDispenser.Positions.Center)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Center] = true;
                    }
                    if (result.CashDispenser.Positions.Front is not null &&
                        (bool)result.CashDispenser.Positions.Front)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Front] = true;
                    }
                    if (result.CashDispenser.Positions.Left is not null &&
                        (bool)result.CashDispenser.Positions.Left)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Left] = true;
                    }
                    if (result.CashDispenser.Positions.Rear is not null &&
                        (bool)result.CashDispenser.Positions.Rear)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Rear] = true;
                    }
                    if (result.CashDispenser.Positions.Right is not null &&
                        (bool)result.CashDispenser.Positions.Right)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Right] = true;
                    }
                    if (result.CashDispenser.Positions.Top is not null &&
                        (bool)result.CashDispenser.Positions.Top)
                    {
                        outputPositions[CashDispenserCapabilitiesClass.OutputPositionEnum.Top] = true;
                    }
                }

                Dictionary<CashDispenserCapabilitiesClass.MoveItemEnum, bool> moveItems = new()
                {
                    { CashDispenserCapabilitiesClass.MoveItemEnum.FromCashUnit, false },
                    { CashDispenserCapabilitiesClass.MoveItemEnum.ToCashUnit, false },
                    { CashDispenserCapabilitiesClass.MoveItemEnum.ToStacker, false },
                    { CashDispenserCapabilitiesClass.MoveItemEnum.ToTransport, false }
                };

                if (result.CashDispenser.MoveItems is not null)
                {
                    if (result.CashDispenser.MoveItems.FromCashUnit is not null &&
                        (bool)result.CashDispenser.MoveItems.FromCashUnit)
                    {
                        moveItems[CashDispenserCapabilitiesClass.MoveItemEnum.FromCashUnit] = true;
                    }
                    if (result.CashDispenser.MoveItems.ToCashUnit is not null &&
                        (bool)result.CashDispenser.MoveItems.ToCashUnit)
                    {
                        moveItems[CashDispenserCapabilitiesClass.MoveItemEnum.ToCashUnit] = true;
                    }
                    if (result.CashDispenser.MoveItems.ToStacker is not null &&
                        (bool)result.CashDispenser.MoveItems.ToStacker)
                    {
                        moveItems[CashDispenserCapabilitiesClass.MoveItemEnum.ToStacker] = true;
                    }
                    if (result.CashDispenser.MoveItems.ToTransport is not null &&
                        (bool)result.CashDispenser.MoveItems.ToTransport)
                    {
                        moveItems[CashDispenserCapabilitiesClass.MoveItemEnum.ToTransport] = true;
                    }
                }

                // Store internal object for other interfaces can be used
                CommonService.CashDispenserCapabilities = new CashDispenserCapabilitiesClass(result.CashDispenser.Type switch
                                                                                             {
                                                                                                 CapabilitiesCompletion.PayloadData.CashDispenserClass.TypeEnum.SelfServiceBill => CashDispenserCapabilitiesClass.TypeEnum.selfServiceBill,
                                                                                                 CapabilitiesCompletion.PayloadData.CashDispenserClass.TypeEnum.SelfServiceCoin => CashDispenserCapabilitiesClass.TypeEnum.selfServiceCoin,
                                                                                                 CapabilitiesCompletion.PayloadData.CashDispenserClass.TypeEnum.TellerBill => CashDispenserCapabilitiesClass.TypeEnum.tellerBill,
                                                                                                 _ => CashDispenserCapabilitiesClass.TypeEnum.tellerCoin
                                                                                             },
                                                                                             result.CashDispenser.MaxDispenseItems is null ? 0 : (int)result.CashDispenser.MaxDispenseItems,
                                                                                             result.CashDispenser.Shutter is not null && (bool)result.CashDispenser.Shutter,
                                                                                             result.CashDispenser.ShutterControl is not null && (bool)result.CashDispenser.ShutterControl,
                                                                                             retractAreas,
                                                                                             retractTransportActions,
                                                                                             retractStackerActions,
                                                                                             result.CashDispenser.IntermediateStacker is not null && (bool)result.CashDispenser.IntermediateStacker,
                                                                                             result.CashDispenser.ItemsTakenSensor is not null && (bool)result.CashDispenser.ItemsTakenSensor,
                                                                                             outputPositions,
                                                                                             moveItems,
                                                                                             result.CashDispenser.PrepareDispense is not null && (bool)result.CashDispenser.PrepareDispense);
            }

            return Task.FromResult(result);
        }
    }
}
