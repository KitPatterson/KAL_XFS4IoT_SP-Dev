/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;
using XFS4IoTServer.Common;
using XFS4IoTServer.CashDispenser;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Dispenser
{
    public partial class DispenseHandler
    {
        private async Task<DispenseCompletion.PayloadData> HandleDispense(IDispenseEvents events, DispenseCommand dispense, CancellationToken cancel)
        {
            DispenserServiceClass CashDispenserService = Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");

            CashDispenserCapabilitiesClass.OutputPositionEnum position = CashDispenserCapabilitiesClass.OutputPositionEnum.Default;
            if (dispense.Payload.Position is not null)
            {
                position = dispense.Payload.Position switch
                {
                    DispenseCommand.PayloadData.PositionEnum.Bottom => CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom,
                    DispenseCommand.PayloadData.PositionEnum.Center => CashDispenserCapabilitiesClass.OutputPositionEnum.Center,
                    DispenseCommand.PayloadData.PositionEnum.Default => CashDispenserCapabilitiesClass.OutputPositionEnum.Default,
                    DispenseCommand.PayloadData.PositionEnum.Front => CashDispenserCapabilitiesClass.OutputPositionEnum.Front,
                    DispenseCommand.PayloadData.PositionEnum.Left => CashDispenserCapabilitiesClass.OutputPositionEnum.Left,
                    DispenseCommand.PayloadData.PositionEnum.Rear => CashDispenserCapabilitiesClass.OutputPositionEnum.Rear,
                    DispenseCommand.PayloadData.PositionEnum.Right => CashDispenserCapabilitiesClass.OutputPositionEnum.Right,
                    DispenseCommand.PayloadData.PositionEnum.Top => CashDispenserCapabilitiesClass.OutputPositionEnum.Top,
                    _ => CashDispenserCapabilitiesClass.OutputPositionEnum.Default
                };
            }

            CashDispenserService.CommonService.CashDispenserCapabilities.OutputPositons.ContainsKey(position).IsTrue($"Unsupported position specified. {position}");

            if (!CashDispenserService.CommonService.CashDispenserCapabilities.OutputPositons[position])
            {
                return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          $"Unsupported position. {position}",
                                                          DispenseCompletion.PayloadData.ErrorCodeEnum.UnsupportedPosition);
            }

            // Reset an internal present status
            CashDispenserService.LastPresentStatus[position].Status = PresentStatus.PresentStatusEnum.NotPresented;
            CashDispenserService.LastPresentStatus[position].LastDenomination = new (new Dictionary<string, double>(), new Dictionary<string, int>(), Logger);

            if (dispense.Payload.Denomination.Currencies is null &&
                dispense.Payload.Denomination.Values is null)
            {
                return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                          $"Invalid amounts and values specified. either amount or values dispensing from each cash units required.",
                                                          DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination);
            }

            int mixNumber = 0;
            if (dispense.Payload.MixNumber is not null)
                mixNumber = (int)dispense.Payload.MixNumber;

            if (mixNumber != 0 &&
                CashDispenserService.Mixes.ContainsKey((int)dispense.Payload.MixNumber))
            {
                return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, $"Invalid MixNumber specified. {mixNumber}", DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidMixNumber);
            }

            double totalAmount = 0;
            if (dispense.Payload.Denomination.Currencies is not null)
                totalAmount = dispense.Payload.Denomination.Currencies.Select(c => c.Value).Sum();

            Denomination denomToDispense = new(dispense.Payload.Denomination.Currencies, 
                                               dispense.Payload.Denomination.Values,
                                               Logger);

            ////////////////////////////////////////////////////////////////////////////
            // 1) Check that a given denomination can currently be paid out.
            if (mixNumber == 0 &&
                totalAmount == 0)
            {
                if (dispense.Payload.Denomination.Values.Count == 0)
                {
                    return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                              "No counts specified to dispense items from the cash units.");
                }

                Denomination.IsDispensableResult Result = denomToDispense.IsDispensable(totalAmount, CashDispenserService.CashManagementService.CashUnits);
                switch (Result.Result)
                {
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitError:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError, 
                                                                      $"Invalid Cash Unit specified to dispense. {cashUnit.Value},{cashUnit.Key}",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitLocked:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError, 
                                                                      $"Cash unit is locked.{cashUnit.Value},{cashUnit.Key}",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitNotEnough:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                      $"Cash unit doesn't have enough notes to dispense.{cashUnit.Value},{cashUnit.Key}",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.TooManyItems);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidCurrency:
                        {
                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                      "Invalid currency specified. ",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidDenomination:
                        {
                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                      "Invalid denimination specified. ",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination);
                        }
                    default:
                        Contracts.Assert(Result.Result == Denomination.IsDispensableResult.ResultEnum.Good, $"Unexpected result received after an internal IsDispense call. {Result.Result}");
                        break;
                }
            }
            ////////////////////////////////////////////////////////////////////////////
            //  2) Test that a given amount matches a given denomination.
            else if (mixNumber == 0 &&
                     totalAmount != 0)
            {
                if (totalAmount != denomToDispense.TotalAmountToDispense(CashDispenserService.CashManagementService.CashUnits, out Denomination.IsDispensableResult.ResultEnum checkDispenseResult))
                {
                    return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                              $"The total of amount and counts each cash units doesn't match.",
                                                              checkDispenseResult switch
                                                              {
                                                                  Denomination.IsDispensableResult.ResultEnum.InvalidCurrency => DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency,
                                                                  Denomination.IsDispensableResult.ResultEnum.InvalidDenomination => DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination,
                                                                  _ => DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination
                                                              });
                }

                Denomination.IsDispensableResult Result = denomToDispense.IsDispensable(totalAmount, CashDispenserService.CashManagementService.CashUnits);
                switch (Result.Result)
                {
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitError:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                      $"Invalid Cash Unit specified to dispense. {cashUnit.Value},{cashUnit.Key}",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitLocked:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                      $"Cash unit is locked.{cashUnit.Value},{cashUnit.Key}",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitNotEnough:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                      $"Cash unit doesn't have enough notes to dispense.{cashUnit.Value},{cashUnit.Key}",
                                                                      DispenseCompletion.PayloadData.ErrorCodeEnum.TooManyItems);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidCurrency:
                        {
                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                                      "Invalid currency specified. ",
                                                                       DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidDenomination:
                        {
                            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                      "Invalid denomination specified. ",
                                                                       DispenseCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination);
                        }
                    default:
                        Contracts.Assert(Result.Result == Denomination.IsDispensableResult.ResultEnum.Good, $"Unexpected result received after an internal IsDispense call. {Result.Result}");
                        break;
                }
            }
            ////////////////////////////////////////////////////////////////////////////
            //  3) Calculate the denomination, given an amount and mix number.
            else if (mixNumber != 0 &&
                     dispense.Payload.Denomination.Values.Count == 0)
            {
                if (totalAmount == 0)
                {
                    return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                              $"Specified amount is zero to dispense, but number of notes from each cash unit is not specified as well.");
                }

                denomToDispense = CashDispenserService.Mixes[mixNumber].Calculate(denomToDispense.CurrencyAmounts, CashDispenserService.CashManagementService.CashUnits, Logger);
            }
            ////////////////////////////////////////////////////////////////////////////
            //  4) Complete a partially specified denomination for a given amount.
            else if (mixNumber != 0 &&
                     dispense.Payload.Denomination.Values.Count != 0)
            {
                if (totalAmount == 0)
                {
                    return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                              $"Specified amount is zero to dispense, but number of notes from each cash unit is not specified as well.");
                }

                Denomination mixDenom = CashDispenserService.Mixes[mixNumber].Calculate(denomToDispense.CurrencyAmounts, CashDispenserService.CashManagementService.CashUnits, Logger);
                if (mixDenom.Values != denomToDispense.Values)
                {
                    return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                              $"Specified counts each cash unit to be dispensed is different from the result of mix algorithm.",
                                                              DispenseCompletion.PayloadData.ErrorCodeEnum.NotDispensable);
                }
            }
            else
            {
                Contracts.Assert(false, $"Unreachable code.");
            }


            Logger.Log(Constants.DeviceClass, "CashDispenserDev.DispenseAsync()");

            var result = await Device.DispenseAsync(events, 
                                                    new DispenseRequest(denomToDispense.Values,
                                                                        position,
                                                                        dispense.Payload.Token), 
                                                    cancel);

            Logger.Log(Constants.DeviceClass, $"CashDispenserDev.DispenseAsync() -> {result.CompletionCode}, {result.ErrorCode}");


            PresentStatus presentStatus = null;
            try
            {
                Logger.Log(Constants.DeviceClass, "CashDispenserDev.GetPresentStatus()");

                presentStatus = Device.GetPresentStatus(position);

                Logger.Log(Constants.DeviceClass, $"CashDispenserDev.GetPresentStatus() -> {presentStatus}");
            }
            catch (NotImplementedException)
            {
                Logger.Log(Constants.DeviceClass, $"CashDispenserDev.GetPresentStatus() -> Not implemented");
            }
            catch (Exception)
            {
                throw;
            }

            // Update an internal present status
            if (result.CompletionCode != MessagePayload.CompletionCodeEnum.Success)
                CashDispenserService.LastPresentStatus[position].Status = PresentStatus.PresentStatusEnum.Unknown;
            else
                CashDispenserService.LastPresentStatus[position].LastDenomination = denomToDispense;

            if (presentStatus is not null)
            {
                CashDispenserService.LastPresentStatus[position].Status = (PresentStatus.PresentStatusEnum)presentStatus.Status;

                if (presentStatus.LastDenomination is not null)
                    CashDispenserService.LastPresentStatus[position].LastDenomination = presentStatus.LastDenomination;

                CashDispenserService.LastPresentStatus[position].Token = presentStatus.Token;
            }

            CashDispenserService.CashManagementService.UpdateCashUnitAccounting(result.MovementResult);

            return new DispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                      null,
                                                      null,
                                                      denomToDispense.CurrencyAmounts,
                                                      denomToDispense.Values,
                                                      dispense.Payload.Denomination.CashBox);
        }
    }
}
