/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RawData.cs uses automatically generated parts. 
 * RawData.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = RawData
	[DataContract]
	[Command(Name = "Printer.RawData")]
	public sealed class RawData : Command<RawDataPayload>
	{

		public RawData(string RequestId, RawDataPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RawDataPayload : MessagePayload
	{

		public enum InputDataEnum
		{
			No,
			Yes,
		}


		public RawDataPayload(int Timeout, InputDataEnum? InputData = null, string Data = null)
			: base(Timeout)
		{
			this.InputData = InputData;
			this.Data = Data;
		}

		/// <summary>
		///Specifies that input data from the device is expected in response to sending the raw data (i.e. the data contains a command requesting data). Possible values are:**no**
		////  No input data is expected.**yes**
		////  Input data is expected.
		/// </summary>
		[DataMember(Name = "inputData")] 
		public InputDataEnum? InputData { get; private set; }
		/// <summary>
		///BASE64 encoded device dependent data to be sent to the device.
		/// </summary>
		[DataMember(Name = "data")] 
		public string Data { get; private set; }
	}

}
