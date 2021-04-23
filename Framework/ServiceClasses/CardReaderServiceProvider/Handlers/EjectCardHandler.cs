/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
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
    public partial class EjectCardHandler
    {
        /// <summary>
        /// EjectCardRequest
        /// Eject card informatio including position where card to be moved.
        /// </summary>
        public sealed class EjectCardRequest
        {
            /// <summary>
            /// EjectCardRequest
            /// </summary>
            /// <param name="Position">Positon to move card on eject operation</param>
            public EjectCardRequest(EjectCardCommand.PayloadData.EjectPositionEnum? Position = null)
            {
                this.Position = Position;
            }

            public EjectCardCommand.PayloadData.EjectPositionEnum? Position { get; private set; }
        }

        /// <summary>
        /// EjectCardResult
        /// Return result of ejecting/returning card
        /// </summary>
        public sealed class EjectCardResult : DeviceResult
        {
            public EjectCardResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                   EjectCardCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                   string ErrorDescription = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public EjectCardCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; } = null;
        }

        private async Task<EjectCardCompletion.PayloadData> HandleEjectCard(IEjectCardEvents events, EjectCardCommand ejectCard, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.EjectCard()");
            var result = await Device.EjectCard(new EjectCardRequest(ejectCard.Payload.EjectPosition));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EjectCard() -> {result.CompletionCode}");

            return new EjectCardCompletion.PayloadData(result.CompletionCode,
                                                       result.ErrorDescription,
                                                       result.ErrorCode);
        }

    }
}
