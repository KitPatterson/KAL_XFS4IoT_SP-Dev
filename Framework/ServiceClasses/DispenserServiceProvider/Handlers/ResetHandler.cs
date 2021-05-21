/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;
using XFS4IoT.Completions;
using XFS4IoTServer.Common;

namespace XFS4IoTFramework.Dispenser
{
    public partial class ResetHandler
    {
        private async Task<ResetCompletion.PayloadData> HandleReset(IResetEvents events, ResetCommand reset, CancellationToken cancel)
        {
            CashDispenserCapabilitiesClass.OutputPositionEnum position = CashDispenserCapabilitiesClass.OutputPositionEnum.Default;
            if (reset.Payload.OutputPosition is not null)
            {
                position = reset.Payload.OutputPosition switch
                {
                    ResetCommand.PayloadData.OutputPositionEnum.Bottom => CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom,
                    ResetCommand.PayloadData.OutputPositionEnum.Center => CashDispenserCapabilitiesClass.OutputPositionEnum.Center,
                    ResetCommand.PayloadData.OutputPositionEnum.Default => CashDispenserCapabilitiesClass.OutputPositionEnum.Default,
                    ResetCommand.PayloadData.OutputPositionEnum.Front => CashDispenserCapabilitiesClass.OutputPositionEnum.Front,
                    ResetCommand.PayloadData.OutputPositionEnum.Left => CashDispenserCapabilitiesClass.OutputPositionEnum.Left,
                    ResetCommand.PayloadData.OutputPositionEnum.Rear => CashDispenserCapabilitiesClass.OutputPositionEnum.Rear,
                    ResetCommand.PayloadData.OutputPositionEnum.Right => CashDispenserCapabilitiesClass.OutputPositionEnum.Right,
                    ResetCommand.PayloadData.OutputPositionEnum.Top => CashDispenserCapabilitiesClass.OutputPositionEnum.Top,
                    _ => CashDispenserCapabilitiesClass.OutputPositionEnum.Default
                };
            }

            Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");
            DispenserServiceClass CashDispenserService = Dispenser as DispenserServiceClass;

            CashDispenserService.CommonService.CashDispenserCapabilities.OutputPositons.ContainsKey(position).IsTrue($"Unsupported position specified. {position}");

            if (!CashDispenserService.CommonService.CashDispenserCapabilities.OutputPositons[position])
            {
                return new ResetCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                       $"Specified unsupported position {position}",
                                                       ResetCompletion.PayloadData.ErrorCodeEnum.UnsupportedPosition);
            }

            int? index = null;
            CashDispenserCapabilitiesClass.RetractAreaEnum retractArea = CashDispenserCapabilitiesClass.RetractAreaEnum.Default;
            if (reset.Payload.RetractArea is not null)
            {
                reset.Payload.RetractArea.IsA<ResetCommand.PayloadData.RetractAreaClass>("RetractArea object must be the RetractAreaClass.");
                ResetCommand.PayloadData.RetractAreaClass retract = reset.Payload.RetractArea as ResetCommand.PayloadData.RetractAreaClass;
                retractArea = CashDispenserCapabilitiesClass.RetractAreaEnum.Default;
                if (retract.RetractArea is not null)
                {
                    retractArea = retract.RetractArea switch
                    {
                        ResetCommand.PayloadData.RetractAreaClass.RetractAreaEnum.ItemCassette => CashDispenserCapabilitiesClass.RetractAreaEnum.ItemCassette,
                        ResetCommand.PayloadData.RetractAreaClass.RetractAreaEnum.Reject => CashDispenserCapabilitiesClass.RetractAreaEnum.Reject,
                        ResetCommand.PayloadData.RetractAreaClass.RetractAreaEnum.Retract => CashDispenserCapabilitiesClass.RetractAreaEnum.Retract,
                        ResetCommand.PayloadData.RetractAreaClass.RetractAreaEnum.Stacker => CashDispenserCapabilitiesClass.RetractAreaEnum.Stacker,
                        ResetCommand.PayloadData.RetractAreaClass.RetractAreaEnum.Transport => CashDispenserCapabilitiesClass.RetractAreaEnum.Transport,
                        _ => CashDispenserCapabilitiesClass.RetractAreaEnum.Default
                    };

                    if (!CashDispenserService.CommonService.CashDispenserCapabilities.RetractAreas[retractArea])
                    {
                        return new ResetCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                               $"Specified unsupported retract area. {retractArea}",
                                                               ResetCompletion.PayloadData.ErrorCodeEnum.NotRetractArea);
                    }
                }

                if (retractArea == CashDispenserCapabilitiesClass.RetractAreaEnum.Retract &&
                    retract.Index is null)
                {
                    return new ResetCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, "Index property is set to null where the retract area is specified to retract position.");
                }

                index = retract.Index;
            }

            Logger.Log(Constants.DeviceClass, "CashDispenserDev.ResetDeviceAsync()");

            var result = await Device.ResetDeviceAsync(events, 
                                                       new ResetDeviceRequest(reset.Payload.Cashunit, 
                                                                              new Retract(retractArea, index), 
                                                                              position), 
                                                       cancel);

            Logger.Log(Constants.DeviceClass, $"CashDispenserDev.ResetDeviceAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            return new ResetCompletion.PayloadData(result.CompletionCode, result.ErrorDescription, result.ErrorCode);
        }
    }
}
