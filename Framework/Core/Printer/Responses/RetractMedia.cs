/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RetractMedia.cs uses automatically generated parts. 
 * RetractMedia.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.RetractMedia")]
	public sealed class RetractMedia : Response<RetractMediaPayload>
	{

		public RetractMedia(string RequestId, RetractMediaPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RetractMediaPayload : MessagePayload
	{


		public RetractMediaPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, int? BinNumber = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(RetractMediaPayload)}");

			this.BinNumber = BinNumber;
		}

		/// <summary>
		///The number of the retract bin where the media has actually been deposited.
		/// </summary>
		[DataMember(Name = "binNumber")] 
		public int? BinNumber { get; private set; }
	}

}
