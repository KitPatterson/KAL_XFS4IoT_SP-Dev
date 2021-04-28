/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipPowerHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    /// <summary>
    /// ChipPowerRequest
    /// Provide chip power action
    /// </summary>
    public sealed class ChipPowerRequest
    {
        /// <summary>
        /// ChipPowerRequest
        /// Handles the power actions that can be done on the chip.
        /// </summary>
        /// <param name="Action">Chip power action could be cold, warm or off</param>
        public ChipPowerRequest(ChipPowerCommand.PayloadData.ChipPowerEnum Action)
        {
            this.Action = Action;
        }

        public ChipPowerCommand.PayloadData.ChipPowerEnum Action { get; private set; }
    }

    /// <summary>
    /// ChipPowerResult
    /// Return result of power action to the chip.
    /// </summary>
    public sealed class ChipPowerResult : DeviceResult
    {
        public ChipPowerResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               ChipPowerCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                               string ErrorDescription = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public ChipPowerCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
    }

    public partial class ChipPowerHandler
    {
        private async Task<ChipPowerCompletion.PayloadData> HandleChipPower(IChipPowerEvents events, ChipPowerCommand chipPower, CancellationToken cancel)
        {
            if (chipPower.Payload.ChipPower is null)
            {
                return new ChipPowerCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           "No chip power action supplied.");
            }

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ChipIO()");
            var result = await Device.ChipPower(events,
                                                new ChipPowerRequest((ChipPowerCommand.PayloadData.ChipPowerEnum)chipPower.Payload.ChipPower),
                                                cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ChipIO() -> {result.CompletionCode}, {result.ErrorCode}");

            return new ChipPowerCompletion.PayloadData(result.CompletionCode,
                                                       result.ErrorDescription,
                                                       result.ErrorCode);
        }

    }
}
