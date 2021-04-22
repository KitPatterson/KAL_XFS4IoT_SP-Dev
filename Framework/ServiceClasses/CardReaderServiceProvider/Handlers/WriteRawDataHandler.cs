/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteRawDataHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class WriteRawDataHandler
    {
        /// <summary>
        /// AcceptCardResult
        /// Accept card to write card tracks so that no card data to be read
        /// </summary>
        public sealed class AcceptCardResult : BaseResult
        {
            public AcceptCardResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    WriteRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                    string ErrorDescription = null) 
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public WriteRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode;
        }

        /// <summary>
        /// WriteCardDataRequest
        /// Information contains to perform operation for writing card data after the card is successfully inserted in write position
        /// </summary>
        public sealed class WriteCardDataRequest
        {
            /// <summary>
            /// Contains the data to write tracks with method
            /// </summary>
            public class CardData
            {
                /// <summary>
                /// CardDataToWrite
                /// </summary>
                /// <param name="Data">Data to write to the track</param>
                /// <param name="WriteMethod">The coercivity to write data</param>
                public CardData(List<byte> Data = null,
                                WriteRawDataCommand.PayloadData.DataClass.WriteMethodEnum? WriteMethod  = null)
                {
                    this.Data = Data;
                    this.WriteMethod = WriteMethod;
                }

                public WriteRawDataCommand.PayloadData.DataClass.WriteMethodEnum? WriteMethod { get; private set; }
                public List<byte> Data { get; private set; }
            }

            /// <summary>
            /// WriteCardDataRequest
            /// </summary>
            /// <param name="DataToWrite">Card data to write with destination. i.e. track1, 2 or 3</param>
            public WriteCardDataRequest(Dictionary<WriteRawDataCommand.PayloadData.DataClass.DestinationEnum, CardData> DataToWrite)
            {
                this.DataToWrite = DataToWrite;
            }

            public Dictionary<WriteRawDataCommand.PayloadData.DataClass.DestinationEnum, CardData> DataToWrite { get; private set; }
        }

        /// <summary>
        /// WriteCardDataResult
        /// Return result of writing data to the card tracks
        /// </summary>
        public sealed class WriteCardDataResult : BaseResult
        {
            public WriteCardDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       WriteRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                       string ErrorDescription = null) 
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public WriteRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
        }


        private async Task<WriteRawDataCompletion.PayloadData> HandleWriteRawData(IWriteRawDataEvents events, WriteRawDataCommand writeRawData, CancellationToken cancel)
        {
            Dictionary<WriteRawDataCommand.PayloadData.DataClass.DestinationEnum, WriteCardDataRequest.CardData> dataToWrite = new Dictionary<WriteRawDataCommand.PayloadData.DataClass.DestinationEnum, WriteCardDataRequest.CardData>();
            foreach (WriteRawDataCommand.PayloadData.DataClass data in writeRawData.Payload.Data)
            {
                // First data check
                if (data.Destination is null)
                {
                    return new WriteRawDataCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                  "No destination specified to write track data.");
                }
                if (string.IsNullOrEmpty(data.data))
                {
                    return new WriteRawDataCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                  "No data specified to write track.");
                }

                List<byte> writeData = new (Convert.FromBase64String(data.data));
                if (!ValidateData((WriteRawDataCommand.PayloadData.DataClass.DestinationEnum)data.Destination, writeData))
                {
                    return new WriteRawDataCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                  $"Invalid track data is specified for the {data.Destination}");
                }

                // Data seems to be ok and add it in the list to write data to the device class
                dataToWrite.Add((WriteRawDataCommand.PayloadData.DataClass.DestinationEnum)data.Destination,
                                 new WriteCardDataRequest.CardData(writeData, data.WriteMethod));
            }

            Logger.Log(Constants.DeviceClass, "CardReaderDev.AcceptCard()");
            var acceptCardResult = await Device.AcceptCard(events, writeRawData.Payload.Timeout, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.AcceptCard() -> {acceptCardResult.CompletionCode}, {acceptCardResult.ErrorCode}");

            if (acceptCardResult.CompletionCode != MessagePayload.CompletionCodeEnum.Success ||
                acceptCardResult.ErrorCode is not null)
            {
                return new WriteRawDataCompletion.PayloadData(acceptCardResult.CompletionCode,
                                                              acceptCardResult.ErrorDescription,
                                                              acceptCardResult.ErrorCode);
            }

            Logger.Log(Constants.DeviceClass, "CardReaderDev.WriteCardData()");
            var writeCardDataResult = await Device.WriteCardData(events, new WriteCardDataRequest(dataToWrite));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.WriteCardData() -> {writeCardDataResult.CompletionCode}, {writeCardDataResult.ErrorCode}");

            return new WriteRawDataCompletion.PayloadData(writeCardDataResult.CompletionCode,
                                                          writeCardDataResult.ErrorDescription,
                                                          writeCardDataResult.ErrorCode);
        }

        /// <summary>
        /// ValidateData
        /// Validate track data
        /// </summary>
        /// <param name="Track">Track data to validate</param>
        /// <param name="Data">Data to validate</param>
        /// <returns></returns>
        private bool ValidateData(WriteRawDataCommand.PayloadData.DataClass.DestinationEnum Track, List<byte> Data)
        {
            if (validTrackDataRange.ContainsKey(Track))
            {
                if (Data.Count > validTrackDataRange[Track].MaxLength)
                    return false;

                for (int i=0; i<Data.Count; i++)
                {
                    if (Data[i] < validTrackDataRange[Track].MinLegal ||
                        Data[i] > validTrackDataRange[Track].MaxLegal)
                    {
                        return false;
                    }

                    if (i < Data.Count - 1 && Data[i] == validTrackDataRange[Track].EndSentinel)
                        return false;
                }
            }

            return true;
        }

        private class ValidTrackDataRange
        {
            public ValidTrackDataRange(int MaxLength, byte MinLegal, byte MaxLegal, byte StartSentinel, byte EndSentinel)
            {
                this.MaxLength = MaxLength;
                this.MinLegal = MinLegal;
                this.MaxLegal = MaxLegal;
                this.StartSentinel = StartSentinel;
                this.EndSentinel = EndSentinel;
            }

            public int MaxLength { get; private set; }
            public byte MinLegal { get; private set; }
            public byte MaxLegal { get; private set; }
            public byte StartSentinel { get; private set; }
            public byte EndSentinel { get; private set; }
        }

        private readonly Dictionary<WriteRawDataCommand.PayloadData.DataClass.DestinationEnum, ValidTrackDataRange> validTrackDataRange = new()
        {
            { WriteRawDataCommand.PayloadData.DataClass.DestinationEnum.Track1, new ValidTrackDataRange(78,  0x20, 0x5f, 0x25, 0x3f) },
            { WriteRawDataCommand.PayloadData.DataClass.DestinationEnum.Track2, new ValidTrackDataRange(39,  0x30, 0x3e, 0x3b, 0x3f)},
            { WriteRawDataCommand.PayloadData.DataClass.DestinationEnum.Track3, new ValidTrackDataRange(106, 0x30, 0x3e, 0x3b, 0x3f)},
        };
    }
}
