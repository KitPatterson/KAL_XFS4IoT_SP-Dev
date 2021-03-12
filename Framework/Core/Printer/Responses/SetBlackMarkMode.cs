/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SetBlackMarkMode.cs uses automatically generated parts. 
 * SetBlackMarkMode.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.SetBlackMarkMode")]
	public sealed class SetBlackMarkMode : Response<SetBlackMarkModePayload>
	{

		public SetBlackMarkMode(string RequestId, SetBlackMarkModePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SetBlackMarkModePayload : MessagePayload
	{


		public SetBlackMarkModePayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(SetBlackMarkModePayload)}");

		}

	}

}
