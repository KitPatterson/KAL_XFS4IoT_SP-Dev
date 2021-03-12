/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * Read.cs uses automatically generated parts. 
 * Read.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.Read")]
	public sealed class Read : Response<ReadPayload>
	{

		public Read(string RequestId, ReadPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ReadPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			KeyInvalid,
			KeyNotSupported,
			NoActiveKeys,
		}


		public ReadPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string Input = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ReadPayload)}");

			this.ErrorCode = ErrorCode;
			this.Input = Input;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"keyInvalid\": At least one of the specified keys is invalid.\"keyNotSupported\": At least one of the specified keys is not supported by the Service Provider.\"noActiveKeys\": There are no active keys specified.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
		/// <summary>
		///Specifies a zero terminated string containing all the printable characters (numeric and alphanumeric) read from the text terminal unit key pad.
		/// </summary>
		[DataMember(Name = "input")] 
		public string Input { get; private set; }
	}

}
