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
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.RawData")]
	public sealed class RawData : Response<RawDataPayload>
	{

		public RawData(string RequestId, RawDataPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RawDataPayload : MessagePayload
	{


		public RawDataPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string Data = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(RawDataPayload)}");

			this.Data = Data;
		}

		/// <summary>
		///BASE64 encoded device dependent data received from the device.
		/// </summary>
		[DataMember(Name = "data")] 
		public string Data { get; private set; }
	}

}
