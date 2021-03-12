/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * CryptoData.cs uses automatically generated parts. 
 * CryptoData.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Crypto.Responses
{


	[DataContract]
	[Response(Name = "Crypto.CryptoData")]
	public sealed class CryptoData : Response<CryptoDataPayload>
	{

		public CryptoData(string RequestId, CryptoDataPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class CryptoDataPayload : MessagePayload
	{

		public enum ErrorCodeEnum
		{
			KeyNotFound,
			ModeNotSupported,
			AccessDenied,
			KeyNoValue,
			UseViolation,
			InvalidKeyLength,
			NoChipTransactionActive,
			AlgorithmNotSupported,
			MacInvalid,
			SignatureInvalid,
			CryptoMethodNotSupported,
		}


		public CryptoDataPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string CryptData = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(CryptoDataPayload)}");

			this.ErrorCode = ErrorCode;
			this.CryptData = CryptData;
		}

		/// <summary>
		///Specifies the error code if applicable. The following values are possible:\"keyNotFound\": The specified key was not found.\"modeNotSupported\": The mode specified by modeOfUse is not supported.\"accessDenied\": The encryption module is either not initialized or not ready for any vendor specific reason.\"keyNoValue\": The specified key name was found but the corresponding key value has not been loaded.\"useViolation\": The use specified by keyUsage is not supported.\"invalidKeyLength\": The length of startValue is not supported or the length of an encryption key is not compatible with the encryption operation required.\"noChipTransactionActive\": A chipcard key is used as encryption key and there is no chip transaction active.\"algorithmNotSupported\": The algorithm specified by bAlgorithm is not supported.\"macInvalid\": The MAC verification failed.\"signatureInvalid\": The signature verification failed.\"cryptoMethodNotSupported\": The cryptographic method specified by cryptoMethod is not supported.
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
		/// <summary>
		///The encrypted or decrypted data.
		/// </summary>
		[DataMember(Name = "cryptData")] 
		public string CryptData { get; private set; }
	}

}
