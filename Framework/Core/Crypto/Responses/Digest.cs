/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * Digest.cs uses automatically generated parts. 
 * Digest.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Crypto.Responses
{


	[DataContract]
	[Response(Name = "Crypto.Digest")]
	public sealed class Digest : Response<DigestPayload>
	{

		public Digest(string RequestId, DigestPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DigestPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			AccessDenied,
		}


		public DigestPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string DigestOutput = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(DigestPayload)}");

			this.ErrorCode = ErrorCode;
			this.DigestOutput = DigestOutput;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"accessDenied\": The encryption module is either not initialized or not ready for any vendor specific reason.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
		/// <summary>
		///Contains the length and the data containing the calculated has.
		/// </summary>
		[DataMember(Name = "digestOutput")] 
		public string DigestOutput { get; private set; }
	}

}
