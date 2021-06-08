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
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Dispenser
{
    public partial class DenominateHandler
    {
        private async Task<DenominateCompletion.PayloadData> HandleDenominate(IDenominateEvents events, DenominateCommand denominate, CancellationToken cancel)
        {
            DispenserServiceClass CashDispenserService = Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");

            int mixNumber = 0;
            if (denominate.Payload.MixNumber is not null)
                mixNumber = (int)denominate.Payload.MixNumber;

            if (mixNumber != 0 &&
                CashDispenserService.Mixes.ContainsKey((int)denominate.Payload.MixNumber))
            {
                return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                            $"Invalid MixNumber specified. {mixNumber}",
                                                            DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidMixNumber);
            }

            if (denominate.Payload.Denomination.Currencies is null &&
                denominate.Payload.Denomination.Values is null)
            {
                return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                            $"Invalid amounts and values specified. either amount or values dispensing from each cash units required.",
                                                            DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination);
            }

            double totalAmount = 0;
            if (denominate.Payload.Denomination.Currencies is not null)
                totalAmount= denominate.Payload.Denomination.Currencies.Select(c => c.Value).Sum();

            if (mixNumber == 0)
            {
                try
                {
                    Logger.Log(Constants.DeviceClass, "CashDispenserDev.DenominateAsync()");

                    var result = await Device.DenominateAsync(events, 
                                                              new DenominateRequest(denominate.Payload.Denomination.Currencies,
                                                                                    denominate.Payload.Denomination.Values),
                                                              cancel);

                    Logger.Log(Constants.DeviceClass, $"CashDispenserDev.DenominateAsync() -> {result.CompletionCode}, {result.ErrorCode}");

                    return new DenominateCompletion.PayloadData(result.CompletionCode,
                                                                result.ErrorDescription,
                                                                result.ErrorCode,
                                                                denominate.Payload.Denomination.Currencies,
                                                                result.Values);
                }
                catch (NotImplementedException)
                {
                    Logger.Log(Constants.DeviceClass, $"CashDispenserDev.DenominateAsync() -> Not implemented");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            Denomination denomToDispense = new(denominate.Payload.Denomination.Currencies, denominate.Payload.Denomination.Values, Logger);

            ////////////////////////////////////////////////////////////////////////////
            // 1) Check that a given denomination can currently be paid out.
            if (mixNumber == 0 &&
                totalAmount == 0)
            {
                if (denominate.Payload.Denomination.Values.Count == 0)
                {
                    return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                "No counts specified to dispense items from the cash units.");
                }

                Denomination.IsDispensableResult Result = denomToDispense.IsDispensable(totalAmount, CashDispenserService.CashManagementService.CashUnits);
                switch (Result.Result)
                {
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitError:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                        $"Invalid Cash Unit specified to dispense. {cashUnit.Value},{cashUnit.Key}",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitLocked:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                        $"Cash unit is locked.{cashUnit.Value},{cashUnit.Key}",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitNotEnough:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                        $"Cash unit doesn't have enough notes to dispense.{cashUnit.Value},{cashUnit.Key}",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.TooManyItems);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidCurrency:
                        {
                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                        "Invalid currency specified. ",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidDenomination:
                        {
                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                        "Invalid denomination specified. ",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination);
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
                if (totalAmount != denomToDispense.TotalAmountToDispense(CashDispenserService.CashManagementService.CashUnits, out Denomination.IsDispensableResult.ResultEnum dispenseResult))
                {
                    return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                $"The total of amount and counts each cash units doesn't match.",
                                                                dispenseResult switch
                                                                {
                                                                    Denomination.IsDispensableResult.ResultEnum.InvalidCurrency => DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency,
                                                                    Denomination.IsDispensableResult.ResultEnum.InvalidDenomination => DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination,
                                                                    _ => DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination
                                                                });
                }

                Denomination.IsDispensableResult Result = denomToDispense.IsDispensable(totalAmount, CashDispenserService.CashManagementService.CashUnits);
                switch (Result.Result)
                {
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitError:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                        $"Invalid Cash Unit specified to dispense. {cashUnit.Value},{cashUnit.Key}",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitLocked:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                        $"Cash unit is locked.{cashUnit.Value},{cashUnit.Key}",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.CashUnitError);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.CashUnitNotEnough:
                        {
                            Contracts.Assert(Result.CashUnitIndex is not null, "No cash unit information provided.");
                            KeyValuePair<string, int> cashUnit = (KeyValuePair<string, int>)Result.CashUnitIndex;

                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.HardwareError,
                                                                        $"Cash unit doesn't have enough notes to dispense.{cashUnit.Value},{cashUnit.Key}",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.TooManyItems);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidCurrency:
                        {
                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                        "Invalid currency specified. ",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency);
                        }
                    case Denomination.IsDispensableResult.ResultEnum.InvalidDenomination:
                        {
                            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                        "Invalid denomination specified. ",
                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination);
                        }
                    default:
                        Contracts.Assert(Result.Result == Denomination.IsDispensableResult.ResultEnum.Good, $"Unexpected result received after an internal IsDispense call. {Result.Result}");
                        break;
                }
            }
            ////////////////////////////////////////////////////////////////////////////
            //  3) Calculate the denomination, given an amount and mix number.
            else if (mixNumber != 0 &&
                     denominate.Payload.Denomination.Values.Count == 0)
            {
                if (totalAmount == 0)
                {
                    return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"Specified amount is zero to dispense, but number of notes from each cash unit is not specified as well.");
                }

                denomToDispense = CashDispenserService.Mixes[mixNumber].Calculate(denomToDispense.CurrencyAmounts, CashDispenserService.CashManagementService.CashUnits, Logger);
            }
            ////////////////////////////////////////////////////////////////////////////
            //  4) Complete a partially specified denomination for a given amount.
            else if (mixNumber != 0 &&
                     denominate.Payload.Denomination.Values.Count != 0)
            {
                if (totalAmount == 0)
                {
                    return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"Specified amount is zero to dispense, but number of notes from each cash unit is not specified as well.");
                }

                Denomination mixDenom = CashDispenserService.Mixes[mixNumber].Calculate(denomToDispense.CurrencyAmounts, CashDispenserService.CashManagementService.CashUnits, Logger);
                if (mixDenom.Values != denomToDispense.Values)
                {
                    return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"Specified counts each cash unit to be dispensed is different from the result of mix algorithm.",
                                                                DenominateCompletion.PayloadData.ErrorCodeEnum.NotDispensable);
                }
            }
            else
            {
                Contracts.Assert(false, $"Unreachable code.");
            }

            return new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                        null,
                                                        null,
                                                        denomToDispense.CurrencyAmounts,
                                                        denomToDispense.Values,
                                                        denominate.Payload.Denomination.CashBox);
        }
    }
}
