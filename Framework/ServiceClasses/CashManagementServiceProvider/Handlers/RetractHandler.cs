/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoTFramework.Common;
using XFS4IoT.CashManagement.Commands;
using XFS4IoT.CashManagement.Completions;
using XFS4IoT.CashManagement;

namespace XFS4IoTFramework.CashManagement
{
    public partial class RetractHandler
    {

        private async Task<RetractCompletion.PayloadData> HandleRetract(IRetractEvents events, RetractCommand retract, CancellationToken cancel)
        {
            if (retract.Payload.RetractArea is not null &&
                retract.Payload.OutputPosition is not null)
            {
                return new RetractCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                         $"Specified both RetractArea and OutputPosition properties, the Service Provider doesn't know where the items to be moved.");
            }

            ItemPosition itemPosition = null;

            if (retract.Payload.RetractArea is not null)
            {
                CashDispenserCapabilitiesClass.RetractAreaEnum retractArea = CashDispenserCapabilitiesClass.RetractAreaEnum.Default;
                retractArea = retract.Payload.RetractArea switch
                {
                    RetractAreaEnum.ItemCassette => CashDispenserCapabilitiesClass.RetractAreaEnum.ItemCassette,
                    RetractAreaEnum.Reject => CashDispenserCapabilitiesClass.RetractAreaEnum.Reject,
                    RetractAreaEnum.Retract => CashDispenserCapabilitiesClass.RetractAreaEnum.Retract,
                    RetractAreaEnum.Stacker => CashDispenserCapabilitiesClass.RetractAreaEnum.Stacker,
                    RetractAreaEnum.Transport => CashDispenserCapabilitiesClass.RetractAreaEnum.Transport,
                    _ => CashDispenserCapabilitiesClass.RetractAreaEnum.Default
                };

                if (!CashManagement.CashDispenserCapabilities.RetractAreas[retractArea])
                {
                    return new RetractCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                             $"Specified unsupported retract area. {retractArea}",
                                                             RetractCompletion.PayloadData.ErrorCodeEnum.InvalidRetractPosition);
                }

                if (retractArea == CashDispenserCapabilitiesClass.RetractAreaEnum.Retract)
                {
                    if (retract.Payload.Index is null)
                    {
                        return new RetractCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                 $"Index property is set to null where the retract area is specified to retract position.");
                    }

                    int index = (int)retract.Payload.Index;

                    // Check the index is valid
                    int totalRetractUnits = (from unit in CashDispenser.CashUnits
                                             where unit.Value.Type == CashUnit.TypeEnum.RetractCassette
                                             select unit).Count();
                    if ((int)index > totalRetractUnits)
                    {
                        return new RetractCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                 $"Unexpected index property value is set where the retract area is specified to retract position. " +
                                                                 $"The value of index one is the first retract position and increments by one for each subsequent position. {index}");
                    }

                    itemPosition = new ItemPosition(new Retract(retractArea, index));
                }
            }
            else if (retract.Payload.OutputPosition is not null)
            {
                CashDispenserCapabilitiesClass.OutputPositionEnum position = retract.Payload.OutputPosition switch
                {
                    OutputPositionEnum.OutBottom => CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom,
                    OutputPositionEnum.OutCenter => CashDispenserCapabilitiesClass.OutputPositionEnum.Center,
                    OutputPositionEnum.OutFront => CashDispenserCapabilitiesClass.OutputPositionEnum.Front,
                    OutputPositionEnum.OutLeft => CashDispenserCapabilitiesClass.OutputPositionEnum.Left,
                    OutputPositionEnum.OutRear => CashDispenserCapabilitiesClass.OutputPositionEnum.Rear,
                    OutputPositionEnum.OutRight => CashDispenserCapabilitiesClass.OutputPositionEnum.Right,
                    OutputPositionEnum.OutTop => CashDispenserCapabilitiesClass.OutputPositionEnum.Top,
                    _ => CashDispenserCapabilitiesClass.OutputPositionEnum.Default,

                };

                if (!CashManagement.CashDispenserCapabilities.OutputPositons[position])
                {
                    return new RetractCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                $"Specified unsupported output position. {position}");
                }

                itemPosition = new ItemPosition(position);
            }

            Logger.Log(Constants.DeviceClass, "CashDispenserDev.RetractAsync()");

            var result = await Device.RetractAsync(events,
                                                   new RetractRequest(itemPosition),
                                                   cancel);

            Logger.Log(Constants.DeviceClass, $"CashDispenserDev.RetractAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            List<ItemNumberClass> itemNumber = null;
            Dictionary<string, ItemMovement> itemMovementResult = new();

            if (result.MovementResult != null &&
                result.MovementResult.Count > 0)
            {
                itemNumber = new List<RetractCompletion.PayloadData.ItemNumberClass>();

                foreach (var movement in result.MovementResult)
                {
                    itemNumber.Add(new RetractCompletion.PayloadData.ItemNumberClass(movement.CurrencyID,
                                                                                     movement.Values,
                                                                                     movement.Release,
                                                                                     movement.Count,
                                                                                     movement.CashUnit));
                    if (string.IsNullOrEmpty(movement.CashUnit) ||
                        !CashDispenser.CashUnits.ContainsKey(movement.CashUnit))
                    {
                        continue; // it's not moved into cash unit
                    }

                    if (!itemMovementResult.ContainsKey(movement.CashUnit))
                        itemMovementResult.Add(movement.CashUnit, new ItemMovement(DispensedCount: null,
                                                                                   PresentedCount: null,
                                                                                   RetractedCount: movement.Count,
                                                                                   RejectCount: null,
                                                                                   StoredBankNoteList: null));
                    else
                        itemMovementResult[movement.CashUnit].RetractedCount += movement.Count;
                }
            }

            CashDispenser.UpdateCashUnitAccounting(itemMovementResult);

            return new RetractCompletion.PayloadData(result.CompletionCode,
                                                     result.ErrorDescription,
                                                     result.ErrorCode,
                                                     itemNumber);
        }

    }
}
