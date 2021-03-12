/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ParseData.cs uses automatically generated parts. 
 * ParseData.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = ParseData
	[DataContract]
	[Command(Name = "CardReader.ParseData")]
	public sealed class ParseData : Command<ParseDataPayload>
	{

		public ParseData(string RequestId, ParseDataPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ParseDataPayload : MessagePayload
	{

		[DataContract]
		public sealed class DataClass
		{
			/// <summary>
			///Specifies the source of the card data as one of the following:**track1**
			////data contains data read from track 1.**track2**
			////data contains data read from track 2.**track3**
			////data contains data read from track 3.**chip**
			////data contains ATR data read from the chip. For contactless chip card readers, multiple identification information can be  returned if the card reader detects more than one chip. Each chip identification information is returned as an individual lppCardData  array element.**security**
			////data contains the value returned by the security module.**watermark**
			////data contains data read from the Swedish Watermark track.**memoryChip**
			////data contains Memory Card Identification data read from the memory chip.**track1Front**
			////data contains data read from the front track 1. In some countries this track is known as JIS II track.**frontImage**
			////data contains a null-terminated string containing the full path and file name of the BMP image file for the front of  the card.**backImage**
			////data contains a null-terminated string containing the full path and file name of the BMP image file for the back of the  card.**track1JIS**
			////data contains data read from JIS I track 1 (8bits/char).**track3JIS**
			////data contains data read from JIS I track 3 (8bits/char). **ddi**
			////data contains dynamic digital identification data read from magnetic stripe.
			/// </summary>
			public enum DataSourceEnum
			{
				Track1,
				Track2,
				Track3,
				Chip,
				Security,
				Watermark,
				MemoryChip,
				Track1Front,
				FrontImage,
				BackImage,
				Track1JIS,
				Track3JIS,
				Ddi,
			}

			/// <summary>
			///Status of reading the card data. Possible values are:**ok**
			////The data is OK. **errorDataMissing**
			////The track/chip/memory chip is blank. **errorDataInvalid**
			////The data contained on the track/chip/memory chip is invalid. This will typically be returned when lpbData reports WFS_IDC_SEC_BADREADLEVEL or WFS_IDC_SEC_DATAINVAL.**errorDataTooLong**
			////The data contained on the track/chip/memory chip is too long.**errorDataTooShort**
			////The data contained on the track/chip/memory chip is too short.**errorDataSourceNotSupported**
			////The data source to read from is not supported by the Service Provider.**errorDataSourceMissing**
			////The data source to read from is missing on the card, or is unable to be read due to a hardware problem, or the module has not been initialized. For example, this will be returned on a request to read a Memory Card and the customer has entered a  magnetic card without associated memory chip. This will also be reported when lpbData reports WFS_IDC_SEC_NODATA, WFS_IDC_SEC_NOINIT or  WFS_IDC_SEC_HWERROR. This will also be reported when the image reader could not create a BMP file due to the state of the image reader or  due to a failure.
			/// </summary>
			public enum StatusEnum
			{
				Ok,
				ErrorDataMissing,
				ErrorDataInvalid,
				ErrorDataTooLong,
				ErrorDataTooShort,
				ErrorDataSourceNotSupported,
				ErrorDataSourceMissing,
			}

			public string data;

			public DataClass(DataSourceEnum? DataSource = null, StatusEnum? Status = null, string Data = null)
				: base()
			{
				this.DataSource = DataSource;
				this.Status = Status;
				this.Data = Data;
			}

			/// <summary>
			///Specifies the source of the card data as one of the following:**track1**
			////data contains data read from track 1.**track2**
			////data contains data read from track 2.**track3**
			////data contains data read from track 3.**chip**
			////data contains ATR data read from the chip. For contactless chip card readers, multiple identification information can be  returned if the card reader detects more than one chip. Each chip identification information is returned as an individual lppCardData  array element.**security**
			////data contains the value returned by the security module.**watermark**
			////data contains data read from the Swedish Watermark track.**memoryChip**
			////data contains Memory Card Identification data read from the memory chip.**track1Front**
			////data contains data read from the front track 1. In some countries this track is known as JIS II track.**frontImage**
			////data contains a null-terminated string containing the full path and file name of the BMP image file for the front of  the card.**backImage**
			////data contains a null-terminated string containing the full path and file name of the BMP image file for the back of the  card.**track1JIS**
			////data contains data read from JIS I track 1 (8bits/char).**track3JIS**
			////data contains data read from JIS I track 3 (8bits/char). **ddi**
			////data contains dynamic digital identification data read from magnetic stripe.
			/// </summary>
			[DataMember(Name = "dataSource")] 
			public DataSourceEnum? DataSource { get; private set; }

			/// <summary>
			///Status of reading the card data. Possible values are:**ok**
			////The data is OK. **errorDataMissing**
			////The track/chip/memory chip is blank. **errorDataInvalid**
			////The data contained on the track/chip/memory chip is invalid. This will typically be returned when lpbData reports WFS_IDC_SEC_BADREADLEVEL or WFS_IDC_SEC_DATAINVAL.**errorDataTooLong**
			////The data contained on the track/chip/memory chip is too long.**errorDataTooShort**
			////The data contained on the track/chip/memory chip is too short.**errorDataSourceNotSupported**
			////The data source to read from is not supported by the Service Provider.**errorDataSourceMissing**
			////The data source to read from is missing on the card, or is unable to be read due to a hardware problem, or the module has not been initialized. For example, this will be returned on a request to read a Memory Card and the customer has entered a  magnetic card without associated memory chip. This will also be reported when lpbData reports WFS_IDC_SEC_NODATA, WFS_IDC_SEC_NOINIT or  WFS_IDC_SEC_HWERROR. This will also be reported when the image reader could not create a BMP file due to the state of the image reader or  due to a failure.
			/// </summary>
			[DataMember(Name = "status")] 
			public StatusEnum? Status { get; private set; }

			/// <summary>
			///BASE64 encoded representation of the data
			/// </summary>
			[DataMember(Name = "data")] 
			public string Data { get; private set; }

		}


		public ParseDataPayload(int Timeout, string FormName = null, List<DataClass> Data = null)
			: base(Timeout)
		{
			this.FormName = FormName;
			this.Data = Data;
		}

		/// <summary>
		///The name of the form that defines the behavior for the reading of tracks.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///An array of card data structures
		/// </summary>
		[DataMember(Name = "data")] 
		public List<DataClass> Data{ get; private set; }
	}

}
