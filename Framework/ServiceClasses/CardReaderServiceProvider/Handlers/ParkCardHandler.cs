/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ParkCardHandler.cs uses automatically generated parts. 
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
    public partial class ParkCardHandler
    {
        /// <summary>
        /// ParkCardRequest
        /// Provide parking card information
        /// </summary>
        public sealed class ParkCardRequest
        {
            /// <summary>
            /// ParkCardRequest
            /// Location is provided in this object where the card to be moved in the parking station.
            /// </summary>
            /// <param name="PowerAction">Specifies which way to move the card. if this value is null, default action to be used.</param>
            /// <param name="ParkingStation">Specifies which which parking station should be used. if the value is null, default location to be used.</param>
            public ParkCardRequest(ParkCardCommand.PayloadData.DirectionEnum? PowerAction, int? ParkingStation)
            {
                this.PowerAction = PowerAction;
                this.ParkingStation = ParkingStation;
            }

            public ParkCardCommand.PayloadData.DirectionEnum? PowerAction { get; private set; }

            public int? ParkingStation { get; private set; }
        }

        /// <summary>
        /// ParkCardResult
        /// Return result of moving a card to the parking station.
        /// </summary>
        public sealed class ParkCardResult : BaseResult
        {
            public ParkCardResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                     ParkCardCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                     string ErrorDescription = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public ParkCardCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
        }

        private async Task<ParkCardCompletion.PayloadData> HandleParkCard(IParkCardEvents events, ParkCardCommand parkCard, CancellationToken cancel)
        {
            if (parkCard.Payload.Direction is null)
            {
                return new ParkCardCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                          "No chip IO data supplied.");
            }

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ParkCard()");
            var result = await Device.ParkCard(new ParkCardRequest(parkCard.Payload.Direction, parkCard.Payload.ParkingStation));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ParkCard() -> {result.CompletionCode}, {result.ErrorCode}");

            return new ParkCardCompletion.PayloadData(result.CompletionCode,
                                                      result.ErrorDescription,
                                                      result.ErrorCode);
        }

    }
}
