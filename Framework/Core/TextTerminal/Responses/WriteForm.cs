/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * WriteForm.cs uses automatically generated parts. 
 * WriteForm.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.WriteForm")]
	public sealed class WriteForm : Response<WriteFormPayload>
	{

		public WriteForm(string RequestId, WriteFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class WriteFormPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			FormNotFound,
			FormInvalid,
			MediaOverflow,
			FieldSpecFailure,
			CharacterSetsData,
			FieldError,
		}


		public WriteFormPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(WriteFormPayload)}");

			this.ErrorCode = ErrorCode;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"formNotFound\": The specified form definition cannot be found.\"formInvalid\": The specified form definition is invalid.\"mediaOverflow\": The form overflowed the media.\"fieldSpecFailure\": The syntax of the lpszFields member is invalid.\"characterSetsData\": Character set(s) supported by Service Provider is inconsistent with use of fields value.\"fieldError\": An error occurred while processing a field.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
	}

}
