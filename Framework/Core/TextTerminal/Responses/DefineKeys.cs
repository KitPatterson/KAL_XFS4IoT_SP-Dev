/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DefineKeys.cs uses automatically generated parts. 
 * DefineKeys.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.DefineKeys")]
	public sealed class DefineKeys : Response<DefineKeysPayload>
	{

		public DefineKeys(string RequestId, DefineKeysPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DefineKeysPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			KeyInvalid,
			KeyNotSupported,
			NoActiveKeys,
		}


		public DefineKeysPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(DefineKeysPayload)}");

			this.ErrorCode = ErrorCode;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"keyInvalid\": At least one of the specified keys is invalid.\"keyNotSupported\": At least one of the specified keys is not supported by the Service Provider.\"noActiveKeys\": There are no active keys specified.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
	}

}
