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
    public partial class PrepareDispenseHandler
    {
        private async Task<PrepareDispenseCompletion.PayloadData> HandlePrepareDispense(IPrepareDispenseEvents events, PrepareDispenseCommand prepareDispense, CancellationToken cancel)
        {
            if (prepareDispense.Payload.Action is null)
            {
                return new PrepareDispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, "Index property is set to null where the retract area is specified to retract position.");
            }

            Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");
            DispenserServiceClass CashDispenserService = Dispenser as DispenserServiceClass;

            if (!CashDispenserService.CommonService.CashDispenserCapabilities.PrepareDispense)
            {
                return new PrepareDispenseCompletion.PayloadData(MessagePayload.CompletionCodeEnum.UnsupportedCommand, "PrepareDispense command is not supported. see capabilities PrepareDispense is false.");
            }

            Logger.Log(Constants.DeviceClass, "CashDispenserDev.PrepareDispenseAsync()");

            var result = await Device.PrepareDispenseAsync(new PrepareDispenseRequest((prepareDispense.Payload.Action == PrepareDispenseCommand.PayloadData.ActionEnum.Start) ? PrepareDispenseRequest.ActionEnum.Start : PrepareDispenseRequest.ActionEnum.Start), cancel);

            Logger.Log(Constants.DeviceClass, $"CashDispenserDev.PrepareDispenseAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            // PrepareDispenseCompletion payload doesn't have a error ExchangeActive, it should be there.
            /*
            PrepareDispenseCompletion.PayloadData.ErrorCodeEnum? errorCode = null;
            if (result.ErrorCode is not null)
            {
                errorCode = result.ErrorCode switch
                {
                    _ => ResetCompletion.PayloadData.ErrorCodeEnum.ExchangeActive,
                };
            }
            */

            return new PrepareDispenseCompletion.PayloadData(result.CompletionCode, result.ErrorDescription);
        }

    }
}
