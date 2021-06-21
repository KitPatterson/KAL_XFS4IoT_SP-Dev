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
using XFS4IoTServer.CashDispenser;
using XFS4IoTServer.Common;

namespace XFS4IoTFramework.Dispenser
{
    public partial class GetPresentStatusHandler
    {
        private Task<GetPresentStatusCompletion.PayloadData> HandleGetPresentStatus(IGetPresentStatusEvents events, GetPresentStatusCommand getPresentStatus, CancellationToken cancel)
        {
            CashDispenserCapabilitiesClass.OutputPositionEnum position = CashDispenserCapabilitiesClass.OutputPositionEnum.Default;
            if (getPresentStatus.Payload.Position is not null)
            {
                position = getPresentStatus.Payload.Position switch
                {
                    GetPresentStatusCommand.PayloadData.PositionEnum.Bottom => CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Center => CashDispenserCapabilitiesClass.OutputPositionEnum.Center,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Default => CashDispenserCapabilitiesClass.OutputPositionEnum.Default,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Front => CashDispenserCapabilitiesClass.OutputPositionEnum.Front,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Left => CashDispenserCapabilitiesClass.OutputPositionEnum.Left,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Rear => CashDispenserCapabilitiesClass.OutputPositionEnum.Rear,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Right => CashDispenserCapabilitiesClass.OutputPositionEnum.Right,
                    GetPresentStatusCommand.PayloadData.PositionEnum.Top => CashDispenserCapabilitiesClass.OutputPositionEnum.Top,
                    _ => CashDispenserCapabilitiesClass.OutputPositionEnum.Default
                };
            }

            DispenserServiceClass CashDispenserService = Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");
            CashDispenserService.CommonService.CashDispenserCapabilities.OutputPositons.ContainsKey(position).IsTrue($"Unsupported position specified. {position}");

            if (!CashDispenserService.CommonService.CashDispenserCapabilities.OutputPositons[position])
            {
                return Task.FromResult(new GetPresentStatusCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                                  $"Specified unsupported position {position}",
                                                                                  GetPresentStatusCompletion.PayloadData.ErrorCodeEnum.UnsupportedPosition));
            }

            CashDispenserService.LastPresentStatus.ContainsKey(position).IsTrue($"Unexpected position is specified. {position}");

            return Task.FromResult(new GetPresentStatusCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                              null,
                                                                              null,
                                                                              new GetPresentStatusCompletion.PayloadData.DenominationClass(
                                                                                  CashDispenserService.LastPresentStatus[position].LastDenomination.CurrencyAmounts, 
                                                                                  CashDispenserService.LastPresentStatus[position].LastDenomination.Values),
                                                                              CashDispenserService.LastPresentStatus[position].Status switch
                                                                              {
                                                                                  PresentStatus.PresentStatusEnum.NotPresented => GetPresentStatusCompletion.PayloadData.PresentStateEnum.NotPresented,
                                                                                  PresentStatus.PresentStatusEnum.Presented => GetPresentStatusCompletion.PayloadData.PresentStateEnum.Presented,
                                                                                  _ => GetPresentStatusCompletion.PayloadData.PresentStateEnum.Unknown
                                                                              },
                                                                              null,
                                                                              CashDispenserService.LastPresentStatus[position].Token));
        }
    }
}
