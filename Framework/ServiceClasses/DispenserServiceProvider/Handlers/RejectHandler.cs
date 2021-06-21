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

namespace XFS4IoTFramework.Dispenser
{
    public partial class RejectHandler
    {
        private async Task<RejectCompletion.PayloadData> HandleReject(IRejectEvents events, RejectCommand reject, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CashDispenserDev.RejectAsync()");

            var result = await Device.RejectAsync(events, cancel);

            Logger.Log(Constants.DeviceClass, $"CashDispenserDev.RejectAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            DispenserServiceClass CashDispenserService = Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");

            CashDispenserService.CashManagementService.UpdateCashUnitAccounting(result.MovementResult);

            return new RejectCompletion.PayloadData(result.CompletionCode, 
                                                    result.ErrorDescription, 
                                                    result.ErrorCode);
        }
    }
}
