/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ClearScreen.cs uses automatically generated parts. 
 * ClearScreen.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.ClearScreen")]
	public sealed class ClearScreen : Response<ClearScreenPayload>
	{

		public ClearScreen(string RequestId, ClearScreenPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ClearScreenPayload : MessagePayload
	{


		public ClearScreenPayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ClearScreenPayload)}");

		}

	}

}
