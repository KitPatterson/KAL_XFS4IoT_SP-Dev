/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ResetHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class ResetHandler
    {
        /// <summary>
        /// ResetDeviceRequest
        /// Provide reset action information
        /// </summary>
        public sealed class ResetDeviceRequest
        {
            /// <summary>
            /// ResetDeviceRequest
            /// </summary>
            /// <param name="CardAction">Card action could be eject, capture or no move. if this value is set to null, the default action to be used.</param>
            public ResetDeviceRequest(ResetCommand.PayloadData.ResetInEnum? CardAction)
            {
                this.CardAction = CardAction;
            }

            public ResetCommand.PayloadData.ResetInEnum? CardAction { get; private set; }
        }

        /// <summary>
        /// ResetDeviceResult
        /// Return result of mechanical reset operation
        /// </summary>
        public sealed class ResetDeviceResult : BaseResult
        {
            public ResetDeviceResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                     ResetCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                     string ErrorDescription = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public ResetCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
        }

        private async Task<ResetCompletion.PayloadData> HandleReset(IResetEvents events, ResetCommand reset, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.ResetDevice()");
            var result = await Device.ResetDevice(events, new ResetDeviceRequest(reset.Payload.ResetIn));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ResetDevice() -> {result.CompletionCode}, {result.ErrorCode}");

            return new ResetCompletion.PayloadData(result.CompletionCode,
                                                   result.ErrorDescription,
                                                   result.ErrorCode);
        }

    }
}
