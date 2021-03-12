/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetResolution.cs uses automatically generated parts. 
 * SetResolution.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.SetResolution")]
	public sealed class SetResolution : Response<SetResolutionPayload>
	{

		public SetResolution(string RequestId, SetResolutionPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SetResolutionPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			ResolutionNotSupported,
		}


		public SetResolutionPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(SetResolutionPayload)}");

			this.ErrorCode = ErrorCode;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"resolutionNotSupported\": The specified resolution is not supported by the display.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
	}

}
