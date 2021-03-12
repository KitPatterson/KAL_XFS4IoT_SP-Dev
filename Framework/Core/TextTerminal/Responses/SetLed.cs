/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetLed.cs uses automatically generated parts. 
 * SetLed.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.SetLed")]
	public sealed class SetLed : Response<SetLedPayload>
	{

		public SetLed(string RequestId, SetLedPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SetLedPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			InvalidLed,
		}


		public SetLedPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(SetLedPayload)}");

			this.ErrorCode = ErrorCode;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"invalidLed\": An attempt to set a LED to a new value was invalid because the LED does not exist.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
	}

}
