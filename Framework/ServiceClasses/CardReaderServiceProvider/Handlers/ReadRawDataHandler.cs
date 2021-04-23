/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using XFS4IoTServer;
using XFS4IoT;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class ReadRawDataHandler
    {
        /// <summary>
        /// AcceptCardRequest
        /// Information contains to perform operation for accepting card and read card data if the device can read data while accepting card
        /// </summary>
        public sealed class AcceptCardRequest
        {
            /// <summary>
            /// AcceptCardRequest
            /// Card Data types to be read after card is accepted if the device has a capability to accept and read data
            /// </summary>
            /// <param name="DataToRead">The data type to be read in bitmap flags</param>
            /// <param name="FluxInactive">If this value is true, the flux senstor to be inactive, otherwise active</param>
            /// <param name="Timeout">Timeout on waiting a card is inserted</param>
            public AcceptCardRequest(ReadCardDataRequest.CardDataTypes DataToRead, bool FluxInactive, int Timeout)
            {
                this.DataToRead = DataToRead;
                this.FluxInactive = FluxInactive;
                this.Timeout = Timeout;
            }

            public ReadCardDataRequest.CardDataTypes DataToRead { get; private set; }
            public bool FluxInactive { get; private set; }
            public int Timeout { get; private set; }
        }

        /// <summary>
        /// AcceptCardResult
        /// Return result of accepting card, the card data must be cached until ReadCardData method gets called if the firmware command has capability to read card data and accept card
        /// </summary>
        public sealed class AcceptCardResult : DeviceResult
        {
            public AcceptCardResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    ReadRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                    string ErrorDescription = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public ReadRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
        }

        /// <summary>
        /// ReadCardDataRequest
        /// Information contains to perform operation for reading card after the card is successfully inserted in read position
        /// </summary>
        public sealed class ReadCardDataRequest
        {
            [Flags]
            public enum CardDataTypes
            {
                NoDataRead = 0,
                Track1 = 0x0001,
                Track2 = 0x0002,
                Track3 = 0x0004,
                Chip = 0x0008,
                Security = 0x0010,
                MemoryChip = 0x0040,
                Track1Front = 0x0080,
                FrontImage = 0x0100,
                BackImage = 0x0200,
                Track1JIS = 0x0400,
                Track3JIS = 0x0800,
                Ddi = 0x4000,
                Watermark = 0x8000,
            }

            /// <summary>
            /// AcceptCardRequest
            /// Card Data types to be read after card is accepted
            /// </summary>
            /// <param name="DataToRead">Data type to be read in bitmap flags</param>
            public ReadCardDataRequest(CardDataTypes DataToRead)
            {
                this.DataToRead = DataToRead;
            }

            public CardDataTypes DataToRead { get; private set; }
        }

        /// <summary>
        /// AcceptCardResult
        /// Return result of accepting card, the card data must be cached until ReadCardData method gets called if the firmware command has capability to read card data and accept card
        /// </summary>
        public sealed class ReadCardDataResult : DeviceResult
        {
            /// <summary>
            /// Contains the data read from track 2.
            /// </summary>
            public class CardData
            {
                public enum DataStatusEnum
                {
                    Ok,
                    DataMissing,
                    DataInvalid,
                    DataTooLong,
                    DataTooShort,
                    DataSourceNotSupported,
                    DataSourceMissing,
                }

                /// <summary>
                /// CardData
                /// Store card data read by the device class
                /// </summary>
                /// <param name="DataStatus">Status of reading the card data</param>
                /// <param name="MemcoryChipDataStatus">Status of reading the memory chip data</param>
                /// <param name="SecutiryDataStatus">Status of reading the security data</param>
                /// <param name="Data">Read binary data</param>
                public CardData(DataStatusEnum? DataStatus = null,
                                ReadRawDataCompletion.PayloadData.MemoryChipClass.DataEnum? MemcoryChipDataStatus = null,
                                ReadRawDataCompletion.PayloadData.SecurityClass.DataEnum? SecutiryDataStatus = null,
                                List<byte> Data = null)
                {
                    this.DataStatus = DataStatus;
                    this.MemcoryChipDataStatus = MemcoryChipDataStatus;
                    this.SecutiryDataStatus = SecutiryDataStatus;
                    this.Data = Data;
                }

                /// <summary>
                /// This field must be set for all requested the card data types.
                /// If there are hardware error on reading data, it can be omitted.
                /// </summary>
                public DataStatusEnum? DataStatus { get; private set; }

                /// <summary>
                /// This field must be set if the card data type MemoryChip is requested to be read, otherwise omitted.
                /// </summary>
                public ReadRawDataCompletion.PayloadData.MemoryChipClass.DataEnum? MemcoryChipDataStatus { get; private set; }

                /// <summary>
                /// This field must be set if the card data type Security is requested to be read, otherwise omitted.
                /// </summary>
                public ReadRawDataCompletion.PayloadData.SecurityClass.DataEnum? SecutiryDataStatus { get; private set; }

                /// <summary>
                /// The card data read to be stored except Memory chip and Secutiry data
                /// </summary>
                public List<byte> Data { get; private set; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="CompletionCode">Generic completion codes</param>
            /// <param name="ErrorCode">Command specific error codes</param>
            /// <param name="ErrorDescription">Details of error description</param>
            /// <param name="DataRead">Card data read in binary</param>
            /// <param name="ChipATRRead">Read chip ATR received</param>
            public ReadCardDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                      ReadRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                      string ErrorDescription = null,
                                      Dictionary<ReadCardDataRequest.CardDataTypes, CardData> DataRead = null,
                                      List<CardData> ChipATRRead = null) 
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.DataRead = DataRead;
                this.ChipATRRead = ChipATRRead;
            }

            /// <summary>
            /// ErrorCode
            /// This error code is set if the operation is failed, otherwise omitted
            /// </summary>
            public ReadRawDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }

            /// <summary>
            /// ReadData
            /// All read card data to be stored except chip ATR
            /// </summary>
            public Dictionary<ReadCardDataRequest.CardDataTypes, CardData> DataRead { get; private set; }

            /// <summary>
            /// Contains the ATR data read from the chip. For contactless chip card readers, multiple identification
            /// information can be returned if the card reader detects more than one chip.
            /// </summary>
            public List<CardData> ChipATRRead { get; private set; }
        }

        private async Task<ReadRawDataCompletion.PayloadData> HandleReadRawData(IReadRawDataEvents events, ReadRawDataCommand readRawData, CancellationToken cancel)
        {
            ReadCardDataRequest.CardDataTypes dataTypes = ReadCardDataRequest.CardDataTypes.NoDataRead;

            if (readRawData.Payload.Track1 is not null && (bool)readRawData.Payload.Track1)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Track1;
            if (readRawData.Payload.Track2 is not null && (bool)readRawData.Payload.Track2)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Track2;
            if (readRawData.Payload.Track3 is not null && (bool)readRawData.Payload.Track3)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Track3;
            if (readRawData.Payload.Chip is not null && (bool)readRawData.Payload.Chip)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Chip;
            if (readRawData.Payload.Security is not null && (bool)readRawData.Payload.Security)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Security;
            if (readRawData.Payload.MemoryChip is not null && (bool)readRawData.Payload.MemoryChip)
                dataTypes |= ReadCardDataRequest.CardDataTypes.MemoryChip;
            if (readRawData.Payload.Track1Front is not null && (bool)readRawData.Payload.Track1Front)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Track1Front;
            if (readRawData.Payload.FrontImage is not null && (bool)readRawData.Payload.FrontImage)
                dataTypes |= ReadCardDataRequest.CardDataTypes.FrontImage;
            if (readRawData.Payload.BackImage is not null && (bool)readRawData.Payload.BackImage)
                dataTypes |= ReadCardDataRequest.CardDataTypes.BackImage;
            if (readRawData.Payload.Track1JIS is not null && (bool)readRawData.Payload.Track1JIS)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Track1JIS;
            if (readRawData.Payload.Track3JIS is not null && (bool)readRawData.Payload.Track3JIS)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Track3JIS;
            if (readRawData.Payload.Ddi is not null && (bool)readRawData.Payload.Ddi)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Ddi;
            if (readRawData.Payload.Watermark is not null && (bool)readRawData.Payload.Watermark)
                dataTypes |= ReadCardDataRequest.CardDataTypes.Watermark;

            bool fluxInactive = readRawData.Payload.FluxInactive ?? true;
            if (readRawData.Payload.FluxInactive is not null)
                fluxInactive = (bool)readRawData.Payload.FluxInactive;

            Logger.Log(Constants.DeviceClass, "CardReaderDev.AcceptCard()");
            var acceptCardResult = await Device.AcceptCard(events, new AcceptCardRequest(dataTypes, fluxInactive, readRawData.Payload.Timeout), cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.AcceptCard() -> {acceptCardResult.CompletionCode}, {acceptCardResult.ErrorCode}");

            if (acceptCardResult.CompletionCode != MessagePayload.CompletionCodeEnum.Success ||
                acceptCardResult.ErrorCode is not null ||
                dataTypes == ReadCardDataRequest.CardDataTypes.NoDataRead)
            {
                return new ReadRawDataCompletion.PayloadData(acceptCardResult.CompletionCode, 
                                                             acceptCardResult.ErrorDescription, 
                                                             acceptCardResult.ErrorCode);
            }

            // Card is accepted now and in the device, try to read card data now
            Logger.Log(Constants.DeviceClass, "CardReaderDev.ReadCardData()");
            var readCardDataResult = await Device.ReadCardData(events, new ReadCardDataRequest(dataTypes));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ReadCardData() -> {readCardDataResult.CompletionCode}, {readCardDataResult.ErrorCode}");

            if (readCardDataResult.CompletionCode != MessagePayload.CompletionCodeEnum.Success ||
                readCardDataResult.ErrorCode is not null)
            {
                return new ReadRawDataCompletion.PayloadData(readCardDataResult.CompletionCode,
                                                             readCardDataResult.ErrorDescription,
                                                             readCardDataResult.ErrorCode);
            }

            // build output data
            ReadRawDataCompletion.PayloadData.Track1Class track1 = null;
            ReadRawDataCompletion.PayloadData.Track2Class track2 = null;
            ReadRawDataCompletion.PayloadData.Track3Class track3 = null;
            ReadRawDataCompletion.PayloadData.WatermarkClass watermark = null;
            ReadRawDataCompletion.PayloadData.Track1FrontClass track1Front = null;
            ReadRawDataCompletion.PayloadData.FrontImageClass frontImage = null;
            ReadRawDataCompletion.PayloadData.BackImageClass backImage = null;
            ReadRawDataCompletion.PayloadData.Track1JISClass track1JIS = null;
            ReadRawDataCompletion.PayloadData.Track3JISClass track3JIS = null;
            ReadRawDataCompletion.PayloadData.DdiClass ddi = null;
            ReadRawDataCompletion.PayloadData.SecurityClass security = null;
            ReadRawDataCompletion.PayloadData.MemoryChipClass memoryChip = null;

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Track1))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Track1).IsTrue("Ttrack1 data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1].DataStatus, "Unexpected track1 data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1].Data?.ToArray();
                track1 = new ReadRawDataCompletion.PayloadData.Track1Class((ReadRawDataCompletion.PayloadData.Track1Class.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1].DataStatus,
                                                                           (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Track2))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Track2).IsTrue("Track2 data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track2].DataStatus, "Unexpected track2 data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track2].Data?.ToArray();
                track2 = new ReadRawDataCompletion.PayloadData.Track2Class((ReadRawDataCompletion.PayloadData.Track2Class.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track2].DataStatus,
                                                                           (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Track3))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Track3).IsTrue("Track3 data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track3].DataStatus, "Unexpected track3 data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track3].Data?.ToArray();
                track3 = new ReadRawDataCompletion.PayloadData.Track3Class((ReadRawDataCompletion.PayloadData.Track3Class.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track3].DataStatus,
                                                                           (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Watermark))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Watermark).IsTrue("Watermark data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Watermark].DataStatus, "Unexpected watermak data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Watermark].Data?.ToArray();
                watermark = new ReadRawDataCompletion.PayloadData.WatermarkClass((ReadRawDataCompletion.PayloadData.WatermarkClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Watermark].DataStatus,
                                                                                 (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Track1Front))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Track1Front).IsTrue("Track1Front data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1Front].DataStatus, "Unexpected track1 front data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1Front].Data?.ToArray();
                track1Front = new ReadRawDataCompletion.PayloadData.Track1FrontClass((ReadRawDataCompletion.PayloadData.Track1FrontClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1Front].DataStatus,
                                                                           (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.FrontImage))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.FrontImage).IsTrue("FrontImage data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.FrontImage].DataStatus, "Unexpected front image data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.FrontImage].Data?.ToArray();
                frontImage = new ReadRawDataCompletion.PayloadData.FrontImageClass((ReadRawDataCompletion.PayloadData.FrontImageClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.FrontImage].DataStatus,
                                                                                   (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.BackImage))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.BackImage).IsTrue("BackImage data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.BackImage].DataStatus, "Unexpected back image data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.BackImage].Data?.ToArray();
                backImage = new ReadRawDataCompletion.PayloadData.BackImageClass((ReadRawDataCompletion.PayloadData.BackImageClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.BackImage].DataStatus,
                                                                              (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Track1JIS))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Track1JIS).IsTrue("Track1JIS data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1JIS].DataStatus, "Unexpected track JIS1 data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1JIS].Data?.ToArray();
                track1JIS = new ReadRawDataCompletion.PayloadData.Track1JISClass((ReadRawDataCompletion.PayloadData.Track1JISClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track1JIS].DataStatus,
                                                                                  (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Track3JIS))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Track3JIS).IsTrue("Track3JIS data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track3JIS].DataStatus, "Unexpected track JIS3 data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track3JIS].Data?.ToArray();
                track3JIS = new ReadRawDataCompletion.PayloadData.Track3JISClass((ReadRawDataCompletion.PayloadData.Track3JISClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Track3JIS].DataStatus,
                                                                                 (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Ddi))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Ddi).IsTrue("DDI data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Ddi].DataStatus, "Unexpected DDI data status is set by the device class. DataStatus field should not be null.");
                byte[] data = readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Ddi].Data?.ToArray();
                ddi = new ReadRawDataCompletion.PayloadData.DdiClass((ReadRawDataCompletion.PayloadData.DdiClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Ddi].DataStatus,
                                                                     (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Security))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.Security).IsTrue("Security data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Security].DataStatus, "Unexpected security data status is set by the device class. DataStatus field should not be null.");
                security = new ReadRawDataCompletion.PayloadData.SecurityClass((ReadRawDataCompletion.PayloadData.SecurityClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Security].DataStatus,
                                                                               readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.Security].SecutiryDataStatus);
            }

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.MemoryChip))
            {
                readCardDataResult.DataRead.ContainsKey(ReadCardDataRequest.CardDataTypes.MemoryChip).IsTrue("MemoryChip data is not set by the device class.");
                Contracts.IsNotNull(readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.MemoryChip].DataStatus, "Unexpected memocy chip data status is set by the device class. DataStatus field should not be null.");
                memoryChip = new ReadRawDataCompletion.PayloadData.MemoryChipClass((ReadRawDataCompletion.PayloadData.MemoryChipClass.StatusEnum)readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.MemoryChip].DataStatus,
                                                                                   readCardDataResult.DataRead[ReadCardDataRequest.CardDataTypes.MemoryChip].MemcoryChipDataStatus);
            }

            List<ReadRawDataCompletion.PayloadData.ChipClass> chip = null;

            if (dataTypes.HasFlag(ReadCardDataRequest.CardDataTypes.Chip) &&
                readCardDataResult.ChipATRRead is not null && 
                readCardDataResult.ChipATRRead.Count > 0)
            {
                chip = new List<ReadRawDataCompletion.PayloadData.ChipClass>();
                foreach (ReadCardDataResult.CardData atr in readCardDataResult.ChipATRRead)
                {
                    Contracts.IsNotNull(atr.DataStatus, "Unexpected chip data status is set by the device class. DataStatus field should not be null.");
                    byte[] data = atr.Data?.ToArray();
                    chip.Add(new ReadRawDataCompletion.PayloadData.ChipClass((ReadRawDataCompletion.PayloadData.ChipClass.StatusEnum)atr.DataStatus,
                                                                             (data is not null && data.Length > 0) ? Convert.ToBase64String(data) : string.Empty));
                }
            }

            return new ReadRawDataCompletion.PayloadData(readCardDataResult.CompletionCode,
                                                         readCardDataResult.ErrorDescription,
                                                         readCardDataResult.ErrorCode,
                                                         track1,
                                                         track2,
                                                         track3,
                                                         chip,
                                                         security,
                                                         watermark,
                                                         memoryChip,
                                                         track1Front,
                                                         frontImage,
                                                         backImage,
                                                         track1JIS,
                                                         track3JIS,
                                                         ddi);
        }
    }
}
