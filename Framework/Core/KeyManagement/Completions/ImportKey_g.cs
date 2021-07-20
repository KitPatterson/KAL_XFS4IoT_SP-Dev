/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * ImportKey_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.KeyManagement.Completions
{
    [DataContract]
    [Completion(Name = "KeyManagement.ImportKey")]
    public sealed class ImportKeyCompletion : Completion<ImportKeyCompletion.PayloadData>
    {
        public ImportKeyCompletion(int RequestId, ImportKeyCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string VerificationData = null, Dictionary<string, System.Text.Json.JsonElement> VerifyAttributes = null, int? KeyLength = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.VerificationData = VerificationData;
                this.VerifyAttributes = VerifyAttributes;
                this.KeyLength = KeyLength;
            }

            public enum ErrorCodeEnum
            {
                KeyNotFound,
                AccessDenied,
                DuplicateKey,
                KeyNoValue,
                UseViolation,
                FormatNotSupported,
                InvalidKeyLength,
                NoKeyRam,
                SignatureNotSupported,
                SignatureInvalid,
                RandomInvalid,
                AlgorithmNotSupported,
                ModeNotSupported,
                CryptoMethodNotSupported
            }

            /// <summary>
            /// Specifies the error code if applicable. The following values are possible:
            /// * ```keyNotFound``` - One of the keys specified was not found.
            /// * ```accessDenied``` - The encryption module is either not initialized or not ready for any vendor specific reason.
            /// * ```duplicateKey``` - A key exists with that name and cannot be overwritten.
            /// * ```keyNoValue``` - One of the specified keys is not loaded.
            /// * ```useViolation``` - The use specified by keyUsage is not supported or conflicts with a previously loaded key with the same name as key.
            /// * ```formatNotSupported``` - The specified format is not supported.
            /// * ```invalidKeyLength``` - The length of value is not supported.
            /// * ```noKeyRam``` - There is no space left in the key RAM for a key of the specified type.
            /// * ```signatureNotSupported``` - The cryptoMethod of the verifyAttributes is not supported. The key is not stored in the PIN.
            /// * ```signatureInvalid``` - The verification data in the input data is invalid. The key is not stored in the PIN.
            /// * ```randomInvalid``` - The encrypted random number in the input data does not match the one previously provided by the PIN device. 
            /// The key is not stored in the PIN.
            /// * ```algorithmNotSupported``` - The algorithm specified by bAlgorithm is not supported by this command.
            /// * ```modeNotSupported``` - The mode specified by modeOfUse is not supported.
            /// * ```cryptoMethodNotSupported``` - The cryptographic method specified by cryptoMethod for keyAttributes or verifyAttributes is not supported.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            /// <summary>
            /// The verification data. This field can be omitted if there is no verification data.
            /// </summary>
            [DataMember(Name = "verificationData")]
            public string VerificationData { get; init; }

            /// <summary>
            /// This parameter specifies the encryption algorithm, cryptographic method, and mode used to verify this command 
            /// For a list of valid values see the [Capabilities.verifyAttributes](#common.capabilities.completion.properties.keymanagement.verifyattributes)
            /// capability fields. This field is not set if there is no verification data.
            /// </summary>
            [DataMember(Name = "verifyAttributes")]
            public Dictionary<string, System.Text.Json.JsonElement> VerifyAttributes { get; init; }

            /// <summary>
            /// Specifies the length, in bits, of the key. 0 is the key length is unknown.
            /// </summary>
            [DataMember(Name = "keyLength")]
            public int? KeyLength { get; init; }

        }
    }
}
