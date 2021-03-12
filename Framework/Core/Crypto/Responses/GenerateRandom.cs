/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * GenerateRandom.cs uses automatically generated parts. 
 * GenerateRandom.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Crypto.Responses
{


	[DataContract]
	[Response(Name = "Crypto.GenerateRandom")]
	public sealed class GenerateRandom : Response<GenerateRandomPayload>
	{

		public GenerateRandom(string RequestId, GenerateRandomPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GenerateRandomPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			ModeNotSupported,
			AccessDenied,
		}


		public GenerateRandomPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string RandomNumber = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GenerateRandomPayload)}");

			this.ErrorCode = ErrorCode;
			this.RandomNumber = RandomNumber;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"modeNotSupported\": The mode specified by modeOfUse is not supported.\"accessDenied\": The encryption module is either not initialized or not ready for any vendor specific reason.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
		/// <summary>
		///The generated random number.
		/// </summary>
		[DataMember(Name = "randomNumber")] 
		public string RandomNumber { get; private set; }
	}

}
