/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteTrack.cs uses automatically generated parts. 
 * WriteTrack.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.WriteTrack")]
	public sealed class WriteTrack : Response<WriteTrackPayload>
	{

		public WriteTrack(string RequestId, WriteTrackPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class WriteTrackPayload : MessagePayload
	{


		public WriteTrackPayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(WriteTrackPayload)}");

		}

	}

}
