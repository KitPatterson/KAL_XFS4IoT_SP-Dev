﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 * 
\***********************************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using XFS4IoTFramework.Common;
using XFS4IoT;

namespace XFS4IoTFramework.KeyManagement
{

    public enum RSASignatureAlgorithmEnum
    {
        NoSignature,
        RSASSA_PKCS1_V1_5,     // SSA_PKCS_V1_5 Signatures supported
        RSASSA_PSS,            // SSA_PSS Signatures supported
    }

    public class KeyInformationBase
    {
        public KeyInformationBase(string KeyVersionNumber = null,
                                  string Exportability = null,
                                  List<byte> OptionalKeyBlockHeader = null,
                                  int? Generation = null,
                                  DateTime? ActivatingDate = null,
                                  DateTime? ExpiryDate = null,
                                  int? Version = null)
        {
            this.KeyVersionNumber = KeyVersionNumber;

            Regex.IsMatch(Exportability, KeyDetail.regxExportability).IsTrue($"Invalid key usage specified. {Exportability}");
            this.Exportability = Exportability;
            this.OptionalKeyBlockHeader = OptionalKeyBlockHeader;
            this.Generation = Generation;
            this.ActivatingDate = ActivatingDate;
            this.ExpiryDate = ExpiryDate;
            this.Version = Version;
        }

        /// <summary>
        /// Specifies a two-digit ASCII character version number, which is optionally used to indicate that contents 
        /// of the key block are a component, or to prevent re-injection of old keys.
        /// See [Reference 35. ANS X9 TR-31 2018] for all possible values.
        /// </summary>
        public string KeyVersionNumber { get; init; }

        /// <summary>
        /// Specifies whether the key may be transferred outside of the cryptographic domain in which the key is found.
        /// See[Reference 35.ANS X9 TR - 31 2018] for all possible values.
        /// </summary>
        public string Exportability { get; init; }

        /// <summary>
        /// Contains any optional header blocks, as defined in [Reference 35. ANS X9 TR-31 2018].
        /// This value can be omitted if there are no optional block headers.
        /// </summary>
        public List<byte> OptionalKeyBlockHeader { get; init; }

        /// <summary>
        /// Specifies the generation of the key.
        /// Different generations might correspond to different environments(e.g.test or production environment).
        /// The content is vendor specific.
        /// </summary>
        public int? Generation { get; init; }

        /// <summary>
        /// Specifies the date when the key is activated in the format YYYYMMDD.
        /// </summary>
        public DateTime? ActivatingDate { get; init; }

        /// <summary>
        /// Specifies the date when the key expires in the format YYYYMMDD.
        /// </summary>
        public DateTime? ExpiryDate { get; init; }

        /// <summary>
        /// Specifies the version of the key (the year in which the key is valid, e.g. 1 for 2001).
        /// This value can be omitted if no such information is available for the key.
        /// </summary>
        public int? Version { get; init; }
    }

    public class ImportKeyBaseRequest
    {
        public ImportKeyBaseRequest(string KeyName,
                                    string KeyUsage,
                                    string Algorithm,
                                    string ModeOfUse,
                                    string RestrictedKeyUsage = null)
        {
            this.KeyName = KeyName;
            this.KeyUsage = KeyUsage;
            this.Algorithm = Algorithm;
            this.ModeOfUse = ModeOfUse;
            this.RestrictedKeyUsage = RestrictedKeyUsage;
        }

        /// <summary>
        /// Specifies the key name to store
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// Key usage associated with the key to be stored
        /// </summary>
        public string KeyUsage { get; init; }

        /// <summary>
        /// Algorithm associated with key usage
        /// </summary>
        public string Algorithm { get; init; }

        /// <summary>
        /// Mode of use associated with the Algorithm
        /// </summary>
        public string ModeOfUse { get; init; }

        /// <summary>
        /// Restricted key usage
        /// </summary>
        public string RestrictedKeyUsage { get; init; }
    }

    public sealed class ImportKeyPartRequest : ImportKeyBaseRequest
    {
        public ImportKeyPartRequest(string KeyName,
                                    int ComponentNumber,
                                    string KeyUsage,
                                    string Algorithm,
                                    string ModeOfUse,
                                    string RestrictedKeyUsage = null)
            : base(KeyName, KeyUsage, Algorithm, ModeOfUse, RestrictedKeyUsage)
        {
            this.ComponentNumber = ComponentNumber;
        }

        /// <summary>
        /// Number of component to store temporarily
        /// </summary>
        public int ComponentNumber { get; init; }
    }

    public sealed class AssemblyKeyPartsRequest : ImportKeyBaseRequest
    {
        public AssemblyKeyPartsRequest(string KeyName,
                                       int KeySlot,
                                       string KeyUsage,
                                       string Algorithm,
                                       string ModeOfUse,
                                       string RestrictedKeyUsage = null)
            : base(KeyName, KeyUsage, Algorithm, ModeOfUse, RestrictedKeyUsage)
        {
            this.KeySlot = KeySlot;
        }

        /// <summary>
        /// Key slot to use, if the device class needs to use specific number, update it in the result
        /// </summary>
        public int KeySlot { get; init; }
    }

    public sealed class ImportKeyRequest : ImportKeyBaseRequest
    {
        public sealed class VerifyAttributeClass
        {
            public VerifyAttributeClass(string KeyName,
                                        KeyManagementCapabilitiesClass.VerifyMethodClass VerifyMethod)
            {
                this.KeyName = KeyName;
                this.VerifyMethod = VerifyMethod;
            }

            /// <summary>
            /// Key name for verification to use
            /// </summary>
            public string KeyName { get; init; }

            /// <summary>
            /// Data to verify
            /// </summary>
            public List<byte> VerificationData { get; init; }

            /// <summary>
            /// Cryptographic method to use
            /// </summary>
            public KeyManagementCapabilitiesClass.VerifyMethodClass VerifyMethod { get; init; }
        }

        public sealed class DecryptAttributeClass
        {
            public DecryptAttributeClass(string KeyName,
                                         KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum DecryptoMethod)
            {
                this.KeyName = KeyName;
                this.DecryptoMethod = DecryptoMethod;
            }

            /// <summary>
            /// Key name for verification to use
            /// </summary>
            public string KeyName { get; init; }

            /// <summary>
            /// Cryptographic method to use
            /// </summary>
            public KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum DecryptoMethod { get; init; }
        }

        public ImportKeyRequest(string KeyName,
                                int KeySlot,
                                List<byte> KeyData,
                                string KeyUsage,
                                string Algorithm,
                                string ModeOfUse,
                                string RestrictedKeyUsage = null,
                                VerifyAttributeClass VerifyAttribute = null,
                                DecryptAttributeClass DecryptAttribute = null)
            : base(KeyName, KeyUsage, Algorithm, ModeOfUse, RestrictedKeyUsage)
        {
            this.KeySlot = KeySlot;
            this.VerifyAttribute = VerifyAttribute;
            this.DecryptAttribute = DecryptAttribute;
        }

        /// <summary>
        /// Key slot to use, if the device class needs to use specific number, update it in the result
        /// </summary>
        public int KeySlot { get; init; }

        /// <summary>
        /// Key data to load
        /// </summary>
        public List<byte> KeyData { get; init; }

        /// <summary>
        /// Verify data if it's requested
        /// </summary>
        public VerifyAttributeClass VerifyAttribute { get; init; }

        /// <summary>
        /// Decrypt key before loading key specified
        /// </summary>
        public DecryptAttributeClass DecryptAttribute { get; init; }
    }

    public sealed class ImportKeyResult : DeviceResult
    {
        public sealed class VerifyAttributeClass
        {
            public VerifyAttributeClass(string KeyUsage,
                                        string Algorithm,
                                        KeyManagementCapabilitiesClass.VerifyMethodClass VerifyMethod)
            {
                this.KeyUsage = KeyUsage;
                this.Algorithm = Algorithm;
                this.VerifyMethod = VerifyMethod;
            }

            /// <summary>
            /// Key usage to verify data
            /// </summary>
            public string KeyUsage { get; init; }

            /// <summary>
            /// Algorithm to use to verify data
            /// </summary>
            public string Algorithm { get; init; }

            /// <summary>
            /// Cryptographic method to use
            /// </summary>
            public KeyManagementCapabilitiesClass.VerifyMethodClass VerifyMethod { get; init; }
        }

        public ImportKeyResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               string ErrorDescription = null,
                               ImportKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.VerificationData = null;
            this.KeyLength = 0;
        }

        public ImportKeyResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               KeyInformationBase KeyInformation,
                               List<byte> VerificationData,
                               VerifyAttributeClass VerifyAttribute,
                               int KeyLength,
                               int? UpdatedKeySlot = null)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.KeyInformation = KeyInformation;
            this.VerificationData = VerificationData;
            this.VerifyAttribute = VerifyAttribute;
            this.KeyLength = KeyLength;
            this.UpdatedKeySlot = UpdatedKeySlot;
        }

        public ImportKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// The KeySlot number is provided by the framework as an input parameter.
        /// It's possible to assign different KeySlot number by the device class
        /// </summary>
        public int? UpdatedKeySlot { get; init; }

        /// <summary>
        /// Store key information loaded by the device class
        /// </summary>
        public KeyInformationBase KeyInformation { get; init; }

        /// <summary>
        /// This parameter specifies the encryption algorithm, cryptographic method, and mode used to verify this command
        /// </summary>
        public List<byte> VerificationData { get; init; }

        /// <summary>
        /// Verify attribute
        /// </summary>
        public VerifyAttributeClass VerifyAttribute { get; init; }

        /// <summary>
        /// Specifies the length, in bits, of the key. 0 if the key length is unknown.
        /// </summary>
        public int KeyLength { get; init; }
    }

    public sealed class InitializationRequest
    {
        public InitializationRequest(string KeyName,
                                     List<byte> Identification)
        {
            this.KeyName = KeyName;
            this.Identification = Identification;
        }

        /// <summary>
        /// Key name to initialize, if this is null, all keys to be deleted
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// ID key encrypted by the encryption key
        /// </summary>
        public List<byte> Identification { get; init; }
    }

    public sealed class InitializationResult : DeviceResult
    {
        public InitializationResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    string ErrorDescription = null,
                                    InitializationCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.Identification = null;
        }

        public InitializationResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    List<byte> Identification)
                : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.Identification = Identification;
        }

        public InitializationCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// ID key encrypted by the encryption key
        /// </summary>
        public List<byte> Identification { get; init; }
    }

    public sealed class DeleteKeyRequest
    {
        public DeleteKeyRequest(string KeyName)
        {
            this.KeyName = KeyName;
        }

        /// <summary>
        /// Key name to delete, if the value is null or empty string, all key to be deleted
        /// </summary>
        public string KeyName { get; init; }
    }

    public sealed class GenerateKCVRequest
    {
        public enum KeyCheckValueEnum
        {
            Self,
            Zero,
        }

        public GenerateKCVRequest(string KeyName,
                                  KeyCheckValueEnum KCVMode)
        {
            this.KeyName = KeyName;
        }

        /// <summary>
        /// Key name to generate KCV
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// KCV mode to generate
        /// </summary>
        public KeyCheckValueEnum KVCMode { get; init; }
    }

    public sealed class GenerateKCVResult : DeviceResult
    {
        public GenerateKCVResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    string ErrorDescription = null,
                                    GenerateKCVCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.KCV = null;
        }

        public GenerateKCVResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 List<byte> KCV)
                : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.KCV = KCV;
        }

        public GenerateKCVCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// Key check value generated
        /// </summary>
        public List<byte> KCV { get; init; }
    }

    public sealed class DeriveKeyRequest
    {

        public DeriveKeyRequest(string KeyName,
                                int KeySlot,
                                string KeyGeneratingKey,
                                int KeyGeneratingKeySlot,
                                int DerivationAlgorithm,
                                List<byte> IVData,
                                string IVKey,
                                int IVKeySlot,
                                byte Padding,
                                List<byte> Data,
                                List<byte> IdentificationData)
        {
            this.KeyName = KeyName;
            this.KeySlot = KeySlot;
            this.KeyGeneratingKey = KeyGeneratingKey;
            this.KeyGeneratingKeySlot = KeyGeneratingKeySlot;
            this.DerivationAlgorithm = DerivationAlgorithm;
            this.IVData = IVData;
            this.IVKey = IVKey;
            this.IVKeySlot = IVKeySlot;
            this.Padding = Padding;
            this.Data = Data;
            this.IdentificationData = IdentificationData;
        }

        /// <summary>
        /// Key name to derive key
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// The key slot of the derived key
        /// </summary>
        public int KeySlot { get; init; }

        /// <summary>
        /// Specifies the name of the key generating key that is used for the derivation.
        /// </summary>
        public string KeyGeneratingKey { get; init; }

        /// <summary>
        /// The key slot of the key generating key
        /// </summary>
        public int KeyGeneratingKeySlot { get; init; }

        /// <summary>
        /// Specifies the algorithm that is used for derivation.
        /// </summary>
        public int DerivationAlgorithm { get; init; }

        /// <summary>
        /// Data of the initialization vector
        /// </summary>
        public List<byte> IVData { get; init; }

        /// <summary>
        /// The key name of the initialization vector
        /// </summary>
        public string IVKey { get; init; }

        /// <summary>
        /// The key slot of the initialization vector
        /// </summary>
        public int IVKeySlot { get; init; }


        /// <summary>
        /// Specifies the padding character for the encryption step within the derivation. The valid range is 0 to 255
        /// </summary>
        public byte Padding { get; init; }

        /// <summary>
        /// Data to be used for key derivation.
        /// </summary>
        public List<byte> Data { get; init; }

        /// <summary>
        /// Specifies the key owner identification.
        /// It is a handle to the encryption module and is returned to the application in the[KeyManagement.Initialization] (#keymanagement.initialization) command.
        /// See idKey property in the capabilities for whether this value is required.
        /// If not required, this field should not be set.The use of this parameter is vendor dependent.
        /// </summary>
        public List<byte> IdentificationData { get; init; }
    }

    public sealed class DeriveKeyResult : DeviceResult
    {
        public sealed class LoadedKeyInformation : KeyInformationBase
        {
            public LoadedKeyInformation(string KeyUsage,
                                        string Algorithm,
                                        string ModeOfUse,
                                        int KeyLength,
                                        string KeyVersionNumber,
                                        string Exportability,
                                        List<byte> OptionalKeyBlockHeader,
                                        int? Generation = null,
                                        DateTime? ActivatingDate = null,
                                        DateTime? ExpiryDate = null,
                                        int? Version = null)
                : base(KeyVersionNumber, Exportability, OptionalKeyBlockHeader, Generation, ActivatingDate, ExpiryDate, Version)
            {
                this.KeyUsage = KeyUsage;
                this.Algorithm = Algorithm;
                this.ModeOfUse = ModeOfUse;
                this.KeyLength = KeyLength;
            }

            /// <summary>
            /// Key usage to load
            /// </summary>
            public string KeyUsage { get; init; }

            /// <summary>
            /// Algorithm associated with key usage
            /// </summary>
            public string Algorithm { get; init; }

            /// <summary>
            /// Mode of use associated with the Algorithm
            /// </summary>
            public string ModeOfUse { get; init; }

            /// <summary>
            /// Specifies the length, in bits, of the key. 0 if the key length is unknown.
            /// </summary>
            public int KeyLength { get; init; }
        }

        public DeriveKeyResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               string ErrorDescription = null,
                               DeriveKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.LoadedKeyDetail = null;
        }

        public DeriveKeyResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               LoadedKeyInformation LoadedKeyDetail,
                               int? UpdatedKeySlot)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.LoadedKeyDetail = LoadedKeyDetail;
            this.UpdatedKeySlot = UpdatedKeySlot;
        }

        public DeriveKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// if the key slot number is changed from an input parameter
        /// </summary>
        public int? UpdatedKeySlot { get; init; }

        /// <summary>
        /// Loaded derived key information
        /// </summary>
        public LoadedKeyInformation LoadedKeyDetail { get; init; }
    }

    public sealed class ExportEPPIdRequest
    {

        public ExportEPPIdRequest(string SignatureKeyName,
                                  RSASignatureAlgorithmEnum SignatureAlgorithm)
        {
            this.SignatureKeyName = SignatureKeyName;
            this.SignatureAlgorithm = SignatureAlgorithm;
        }

        /// <summary>
        /// Specifies the name of the private key to use to sign the exported item. 
        /// </summary>
        public string SignatureKeyName { get; init; }

        /// <summary>
        /// RSA signature algorithm to sign
        /// </summary>
        public RSASignatureAlgorithmEnum SignatureAlgorithm { get; init; }
    }

    public sealed class ExportSignedItemRequest
    {

        public ExportSignedItemRequest(string KeyName,
                                       string SignatureKeyName,
                                       RSASignatureAlgorithmEnum SignatureAlgorithm)
        {
            this.KeyName = KeyName;
            this.SignatureKeyName = SignatureKeyName;
            this.SignatureAlgorithm = SignatureAlgorithm;
        }

        /// <summary>
        /// Specifies the name of the public key to be exported. 
        /// The private/public key pair was installed during manufacture (Default Keys and Security Item loaded during manufacture) for a definition of these default keys. 
        /// If this value is null or empty, then the default EPP public key that is used for symmetric key encryption is exported
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// Specifies the name of the private key to use to sign the exported item. 
        /// </summary>
        public string SignatureKeyName { get; init; }

        /// <summary>
        /// RSA signature algorithm to sign
        /// </summary>
        public RSASignatureAlgorithmEnum SignatureAlgorithm { get; init; }
    }

    public sealed class RSASignedItemResult : DeviceResult
    {

        public RSASignedItemResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                   string ErrorDescription = null,
                                   ExportRSAEPPSignedItemCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
           : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.Data = null;
            this.SelfSignature = null;
            this.Signature = null;
        }

        public RSASignedItemResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                   List<byte> Data,
                                   List<byte> Signature = null,
                                   List<byte> SelfSignature = null)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.Data = Data;
            this.SelfSignature = SelfSignature;
            this.Signature = Signature;
        }

        public ExportRSAEPPSignedItemCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// EPP ID or Public key
        /// </summary>
        public List<byte> Data { get; init; }

        /// <summary>
        /// If a public key was requested then this property contains the RSA signature of the public key exported, 
        /// generated with the key-pair’s private component. this property can be null when key Self-Signatures are not supported or required.
        /// </summary>
        public List<byte> SelfSignature { get; init; }
        
        /// <summary>
        /// Signed signature data
        /// </summary>
        public List<byte> Signature { get; init; }
    }

    public sealed class ExportCertificateRequest
    {
        public enum CertificateTypeEnum
        {
            EncryptionKey,
            VerificationKey,
            HostKey,
        }

        public ExportCertificateRequest(CertificateTypeEnum Type)
        {
            this.Type = Type;
        }

        /// <summary>
        /// Specifies which public key certificate is requested.
        /// If the Status command indicates Primary Certificates are accepted, then the Primary Public Encryption Key or the Primary Public Verification Key will be read out.
        /// If the Status command indicates Secondary Certificates are accepted, then the Secondary Public Encryption Key or the Secondary Public Verification Key will be read out.
        /// </summary>
        public CertificateTypeEnum Type { get; init; }
    }

    public sealed class ExportCertificateResult : DeviceResult
    {

        public ExportCertificateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       string ErrorDescription = null,
                                       GetCertificateCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
           : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.Certificate = null;
        }

        public ExportCertificateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       List<byte> Certificate)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.Certificate = Certificate;
        }

        public GetCertificateCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// Exported certificate data
        /// </summary>
        public List<byte> Certificate { get; init; }
    }

    public sealed class GenerateRSAKeyPairRequest
    {
        public enum ExponentEnum
        {
            Default,
            Exponent1,
            Exponent4,
            Exponent16,
        }

        public enum ModeOfUseEnum
        {
            S,
            T,
        }


        public GenerateRSAKeyPairRequest(string KeyName,
                                         int KeySlot,
                                         ModeOfUseEnum PrivateKeyUsage,
                                         int ModulusLength,
                                         ExponentEnum Exponent)
        {
            this.KeyName = KeyName;
            this.KeySlot = KeySlot;
            this.PrivateKeyUsage = PrivateKeyUsage;
            this.ModulusLength = ModulusLength;
            this.Exponent = Exponent;
        }

        /// <summary>
        /// Specifies the name of the new key-pair to be generated. 
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// Key slot number to use
        /// </summary>
        public int KeySlot { get; init; }

        /// <summary>
        /// Specifies mode of use
        /// S - Signature Only.
        /// T - Both Sign and Decrypt
        /// </summary>
        public ModeOfUseEnum PrivateKeyUsage { get; init; }

        /// <summary>
        /// Specifies the number of bits for the modulus of the RSA key pair to be generated. 
        /// When zero is specified then the PIN device will be responsible for defining the length
        /// </summary>
        public int ModulusLength { get; init; }

        /// <summary>
        /// Specifies the value of the exponent of the RSA key pair to be generated
        /// </summary>
        public ExponentEnum Exponent { get; init; }
    }

    public sealed class GenerateRSAKeyPairResult : DeviceResult
    {
        public sealed class LoadedKeyInformation : KeyInformationBase
        {
            public LoadedKeyInformation(string KeyUsage,
                                        string Algorithm,
                                        string ModeOfUse,
                                        int KeyLength,
                                        string KeyVersionNumber,
                                        string Exportability,
                                        List<byte> OptionalKeyBlockHeader,
                                        int? Generation = null,
                                        DateTime? ActivatingDate = null,
                                        DateTime? ExpiryDate = null,
                                        int? Version = null) 
                : base(KeyVersionNumber, Exportability, OptionalKeyBlockHeader, Generation, ActivatingDate, ExpiryDate, Version)
            {
                this.KeyUsage = KeyUsage;
                this.Algorithm = Algorithm;
                this.ModeOfUse = ModeOfUse;
                this.KeyLength = KeyLength;
            }

            /// <summary>
            /// Key usage for the generated asymmetric key
            /// It should be S0 to S2 or 00 - 99
            /// </summary>
            public string KeyUsage { get; init; }

            /// <summary>
            /// Algorithm
            /// It should be R
            /// or 0 - 9
            /// </summary>
            public string Algorithm { get; init; }

            /// <summary>
            /// Mode of use
            /// It should be S ot T
            /// or 0 - 9
            /// </summary>

            public string ModeOfUse { get; init; }

            /// <summary>
            /// Specifies the length, in bits, of the key. 0 if the key length is unknown.
            /// </summary>
            public int KeyLength { get; init; }
        }

        public GenerateRSAKeyPairResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                        string ErrorDescription = null,
                                        GenerateRSAKeyPairCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
           : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.UpdatedKeySlot = null;
        }

        public GenerateRSAKeyPairResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                        LoadedKeyInformation LoadedKeyDetail,
                                        int? UpdatedKeySlot)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.LoadedKeyDetail = LoadedKeyDetail;
            this.UpdatedKeySlot = UpdatedKeySlot;
        }

        public GenerateRSAKeyPairCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// if the key slot number is changed from an input parameter
        /// </summary>
        public int? UpdatedKeySlot { get; init; }

        /// <summary>
        /// Loaded derived key information
        /// </summary>
        public LoadedKeyInformation LoadedKeyDetail { get; init; }
    }

    public sealed class ReplaceCertificateRequest
    {
        public enum CertificateTypeEnum
        {
            EncryptionKey,
            VerificationKey,
            HostKey,
        }

        public ReplaceCertificateRequest(List<byte> Certificate)
        {
            this.Certificate = Certificate;
        }

        /// <summary>
        /// Pointer to the PKCS # 7 message that will replace the current Certificate Authority. 
        /// The outer content uses the Signed-data content type, the inner content is a degenerate certificate only content containing the new CA certificate and Inner Signed Data type The certificate should be in a format represented in DER encoded ASN.1 notation.
        /// </summary>
        public List<byte> Certificate { get; init; }
    }

    public sealed class ReplaceCertificateResult : DeviceResult
    {

        public ReplaceCertificateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       string ErrorDescription = null,
                                       ReplaceCertificateCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
           : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.Digest = null;
        }

        public ReplaceCertificateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       List<byte> Digest)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.Digest = Digest;
        }

        public ReplaceCertificateCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// A PKCS #7 structure using a Digested-data content type. 
        /// The digest parameter should contain the thumb print value.
        /// </summary>
        public List<byte> Digest { get; init; }
    }

    public sealed class ImportCertificateRequest
    {
        public enum LoadOptionEnum
        {
            NewHost,
            ReplaceHost,
        }

        public enum SignerEnum
        {
            Host,
            CA,
            HL
        }

        public ImportCertificateRequest(LoadOptionEnum LoadOption,
                                        SignerEnum Signer,
                                        List<byte> Certificate)
        {
            this.LoadOption = LoadOption;
            this.Signer = Signer;
            this.Certificate = Certificate;
        }

        /// <summary>
        /// Specifies the method to use to load the certificate
        /// </summary>
        public LoadOptionEnum LoadOption { get; init; }

        /// <summary>
        /// Specifies the signer of the certificate to be loaded
        /// </summary>
        public SignerEnum Signer { get; init; }

        /// <summary>
        /// The certificate that is to be loaded represented in DER encoded ASN.1 notation in DER encoded ASN.1 notation.
        /// </summary>
        public List<byte> Certificate { get; init; }
    }

    public sealed class ImportCertificateResult : DeviceResult
    {
        public enum RSAKeyCheckModeEnum
        {
            None,
            SHA1,
            SHA256,
        }

        public ImportCertificateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       string ErrorDescription = null,
                                       LoadCertificateCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
           : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.RSAData = null;
        }

        public ImportCertificateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                       RSAKeyCheckModeEnum KeyCheckMode,
                                       List<byte> RSAData)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.RSAKeyCheckMode = RSAKeyCheckMode;
            this.RSAData = RSAData;
        }

        public LoadCertificateCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// Defines algorithm/method used to generate the public key check value/thumb print.
        /// The check value can be used to verify that the public key has been imported correctly.
        /// </summary>
        public RSAKeyCheckModeEnum RSAKeyCheckMode { get; init; }
        
        /// <summary>
        /// A PKCS #7 structure using a Digested-data content type. 
        /// The digest parameter should contain the thumb print value calculated by the algorithm specified by RSAKeyCheckMode. 
        /// If RSAKeyCheckMode is None, then this property can be set to null or empty list.
        /// </summary>
        public List<byte> RSAData { get; init; }
    }

    public sealed class StartKeyExchangeResult : DeviceResult
    {
        public enum RSAKeyCheckModeEnum
        {
            None,
            SHA1,
            SHA256,
        }

        public StartKeyExchangeResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                      string ErrorDescription = null,
                                      StartKeyExchangeCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
           : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.RandomItem = null;
        }

        public StartKeyExchangeResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                      List<byte> RandomItem)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.RandomItem = RandomItem;
        }

        public StartKeyExchangeCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// A randomly generated number created by the encryptor. 
        /// If the PIN device does not support random number generation and verification, this property can be null or zero length of list
        /// </summary>
        public List<byte> RandomItem { get; init; }
    }
}
