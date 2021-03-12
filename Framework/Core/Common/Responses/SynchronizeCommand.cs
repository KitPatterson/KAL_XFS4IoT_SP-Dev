/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SynchronizeCommand.cs uses automatically generated parts. 
 * SynchronizeCommand.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Common.Responses
{


	[DataContract]
	[Response(Name = "Common.SynchronizeCommand")]
	public sealed class SynchronizeCommand : Response<SynchronizeCommandPayload>
	{

		public SynchronizeCommand(string RequestId, SynchronizeCommandPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SynchronizeCommandPayload : MessagePayload
	{


		public SynchronizeCommandPayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(SynchronizeCommandPayload)}");

		}

	}

}
