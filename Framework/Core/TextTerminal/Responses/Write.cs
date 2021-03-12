/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * Write.cs uses automatically generated parts. 
 * Write.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.Write")]
	public sealed class Write : Response<WritePayload>
	{

		public Write(string RequestId, WritePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class WritePayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			CharacterSetsData,
		}


		public WritePayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(WritePayload)}");

			this.ErrorCode = ErrorCode;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"characterSetsData\": Character set(s) supported by Service Provider is inconsistent with use of text value.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
	}

}
