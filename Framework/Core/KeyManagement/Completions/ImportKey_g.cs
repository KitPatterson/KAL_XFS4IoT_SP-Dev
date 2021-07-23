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

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string VerificationData = null, Dictionary<string, Dictionary<string, Dictionary<string, VerifyAttributesClass>>> VerifyAttributes = null, int? KeyLength = null)
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

            [DataContract]
            public sealed class VerifyAttributesClass
            {
                public VerifyAttributesClass(CryptoMethodClass CryptoMethod = null, HashAlgorithmClass HashAlgorithm = null)
                {
                    this.CryptoMethod = CryptoMethod;
                    this.HashAlgorithm = HashAlgorithm;
                }

                [DataContract]
                public sealed class CryptoMethodClass
                {
                    public CryptoMethodClass(bool? KcvNone = null, bool? KcvSelf = null, bool? KcvZero = null, bool? SigNone = null, bool? RsassaPkcs1V15 = null, bool? RsassaPss = null)
                    {
                        this.KcvNone = KcvNone;
                        this.KcvSelf = KcvSelf;
                        this.KcvZero = KcvZero;
                        this.SigNone = SigNone;
                        this.RsassaPkcs1V15 = RsassaPkcs1V15;
                        this.RsassaPss = RsassaPss;
                    }

                    /// <summary>
                    /// The ECB encryption method. 
                    /// </summary>
                    [DataMember(Name = "kcvNone")]
                    public bool? KcvNone { get; init; }

                    /// <summary>
                    /// There is no key check value verification required. 
                    /// </summary>
                    [DataMember(Name = "kcvSelf")]
                    public bool? KcvSelf { get; init; }

                    /// <summary>
                    /// The key check value (KCV) is created by encrypting a zero value with the key. 
                    /// </summary>
                    [DataMember(Name = "kcvZero")]
                    public bool? KcvZero { get; init; }

                    /// <summary>
                    /// The No signature algorithm specified. No signature verification will take place.
                    /// </summary>
                    [DataMember(Name = "sigNone")]
                    public bool? SigNone { get; init; }

                    /// <summary>
                    /// The RSASSA-PKCS1-v1.5 algorithm. 
                    /// </summary>
                    [DataMember(Name = "rsassaPkcs1V15")]
                    public bool? RsassaPkcs1V15 { get; init; }

                    /// <summary>
                    /// The RSASSA-PSS algorithm.
                    /// </summary>
                    [DataMember(Name = "rsassaPss")]
                    public bool? RsassaPss { get; init; }

                }

                /// <summary>
                /// This parameter specifies the cryptographic method that will be used with encryption algorithm.
                /// 
                /// If the algorithm is 'A', 'D', or 'T' and the key usage is a MAC usage (i.e. 'M1'), then all properties are false. 
                /// 
                /// If the algorithm is 'A', 'D', or 'T' and the key usage is '00', then one of properties must be set true. 
                /// 
                /// * ```kcvNone``` - There is no key check value verification required. 
                /// * ```kcvSelf``` - The key check value (KCV) is created by an encryption of the key with itself. 
                /// * ```kcvZero``` - The key check value (KCV) is created by encrypting a zero value with the key. 
                /// 
                /// If the algorithm is 'R' and the key usage is not '00', then one of properties must be set true. 
                /// 
                /// * ```sigNone``` - No signature algorithm specified. No signature verification will take place and the 
                /// content of verificationData must be set. 
                /// * ```rsassaPkcs1V15``` - Use the RSASSA-PKCS1-v1.5 algorithm. 
                /// * ```rsassaPss``` - Use the RSASSA-PSS algorithm.
                /// </summary>
                [DataMember(Name = "cryptoMethod")]
                public CryptoMethodClass CryptoMethod { get; init; }

                [DataContract]
                public sealed class HashAlgorithmClass
                {
                    public HashAlgorithmClass(bool? Sha1 = null, bool? Sha256 = null)
                    {
                        this.Sha1 = Sha1;
                        this.Sha256 = Sha256;
                    }

                    /// <summary>
                    /// The SHA 1 digest algorithm.
                    /// </summary>
                    [DataMember(Name = "sha1")]
                    public bool? Sha1 { get; init; }

                    /// <summary>
                    /// The SHA 256 digest algorithm, as defined in ISO/IEC 10118-3:2004 and FIPS 180-2.
                    /// </summary>
                    [DataMember(Name = "sha256")]
                    public bool? Sha256 { get; init; }

                }

                /// <summary>
                /// For asymmetric signature verification methods (key usage is 'S0', 'S1', or 'S2'), then one of the following properties are true.
                /// If the key usage is any of the MAC usages (i.e. 'M1'), then properties both 'sha1' and 'sha256' are false.
                /// </summary>
                [DataMember(Name = "hashAlgorithm")]
                public HashAlgorithmClass HashAlgorithm { get; init; }

            }

            /// <summary>
            /// This parameter specifies the encryption algorithm, cryptographic method, and mode used to verify this command 
            /// For a list of valid values see the [Capabilities.verifyAttributes](#common.capabilities.completion.properties.keymanagement.verifyattributes)
            /// capability fields. This field is not set if there is no verification data.
            /// </summary>
            [DataMember(Name = "verifyAttributes")]
            public Dictionary<string, Dictionary<string, Dictionary<string, VerifyAttributesClass>>> VerifyAttributes { get; init; }

            /// <summary>
            /// Specifies the length, in bits, of the key. 0 is the key length is unknown.
            /// </summary>
            [DataMember(Name = "keyLength")]
            public int? KeyLength { get; init; }

        }
    }
}
