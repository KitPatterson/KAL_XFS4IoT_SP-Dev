/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * Reset.cs uses automatically generated parts. 
 * Reset.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.Reset")]
	public sealed class Reset : Response<ResetPayload>
	{

		public Reset(string RequestId, ResetPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ResetPayload : MessagePayload
	{


		public ResetPayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ResetPayload)}");

		}

	}

}
