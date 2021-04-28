/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCardHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/

using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    /// <summary>
    /// WriteCardDataResult
    /// Return result of writing data to the card tracks
    /// </summary>
    public sealed class CaptureCardResult : DeviceResult
    {
        public CaptureCardResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 RetainCardCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                 string ErrorDescription = null,
                                 int? Count = null,
                                 RetainCardCompletion.PayloadData.PositionEnum? Position = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.Count = Count;
            this.Position = Position;
        }

        public CaptureCardResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 int? Count = null,
                                 RetainCardCompletion.PayloadData.PositionEnum? Position = null)
           : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.Count = Count;
            this.Position = Position;
        }

        public RetainCardCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }

        public int? Count { get; private set; }

        public RetainCardCompletion.PayloadData.PositionEnum? Position { get; private set; }
    }

    public partial class RetainCardHandler
    {
        private async Task<RetainCardCompletion.PayloadData> HandleRetainCard(IRetainCardEvents events, RetainCardCommand retainCard, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.CaptureCard()");
            var result = await Device.CaptureCard(events);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.CaptureCard() -> {result.CompletionCode}, {result.ErrorCode}");

            return new RetainCardCompletion.PayloadData(result.CompletionCode,
                                                        result.ErrorDescription,
                                                        result.ErrorCode,
                                                        result.Count,
                                                        result.Position);
        }

    }
}
