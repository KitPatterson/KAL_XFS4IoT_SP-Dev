/***********************************************************************************************\
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
    public sealed class ImportKeyPartRequest
    {
        public ImportKeyPartRequest(string KeyName,
                                    int KeySlot,
                                    string KeyUsage,
                                    string Algorithm,
                                    string ModeOfUse,
                                    string RestrictedKeyUsage = null)
        {
            this.KeyName = KeyName;
            this.KeySlot = KeySlot;
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
        /// Key slot to use
        /// </summary>
        public int KeySlot { get; init; }

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
        /// Restricted key usage
        /// </summary>
        public string RestrictedKeyUsage { get; init; }
    }

    public sealed class ImportKeyRequest
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
        {
            this.KeyName = KeyName;
            this.KeySlot = KeySlot;
            this.KeyData = KeyData;
            this.KeyUsage = KeyUsage;
            this.Algorithm = Algorithm;
            this.ModeOfUse = ModeOfUse;
            this.RestrictedKeyUsage = RestrictedKeyUsage;
            this.VerifyAttribute = VerifyAttribute;
            this.DecryptAttribute = DecryptAttribute;
        }

        /// <summary>
        /// Specifies the key name to store
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// Key slot to use, if the device class needs to use specific number, update it in the result
        /// </summary>
        public int KeySlot { get; init; }

        /// <summary>
        /// Key data to load
        /// </summary>
        public List<byte> KeyData { get; init; }

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
        /// Restricted key usage
        /// </summary>
        public string RestrictedKeyUsage { get; init; }

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
        public sealed class LoadedKeyInformation
        {
            public LoadedKeyInformation(string KeyVersionNumber,
                                        string Exportability,
                                        int KeyLength,
                                        List<byte> OptionalKeyBlockHeader,
                                        int? Generation = null,
                                        DateTime? ActivatingDate = null,
                                        DateTime? ExpiryDate = null,
                                        int? Version = null)
            {
                this.KeyVersionNumber = KeyVersionNumber;

                Regex.IsMatch(Exportability, KeyDetail.regxExportability).IsTrue($"Invalid key usage specified. {Exportability}");
                this.Exportability = Exportability;

                this.KeyLength = KeyLength;
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
            /// Specifies the length, in bits, of the key. 0 if the key length is unknown.
            /// </summary>
            public int KeyLength { get; init; }

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
                               string ErrorDescription,
                               ImportKeyCompletion.PayloadData.ErrorCodeEnum? ErrorCode)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.VerificationData = null;
        }

        public ImportKeyResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               LoadedKeyInformation KeyInformation,
                               List<byte> VerificationData,
                               VerifyAttributeClass VerifyAttribute,
                               int? UpdatedKeySlot = null)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.KeyInformation = KeyInformation;
            this.VerificationData = VerificationData;
            this.VerifyAttribute = VerifyAttribute;
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
        public LoadedKeyInformation KeyInformation { get; init; }

        /// <summary>
        /// This parameter specifies the encryption algorithm, cryptographic method, and mode used to verify this command
        /// </summary>
        public List<byte> VerificationData { get; init; }

        /// <summary>
        /// Verify attribute
        /// </summary>
        public VerifyAttributeClass VerifyAttribute { get; init; }
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
                                    string ErrorDescription,
                                    InitializationCompletion.PayloadData.ErrorCodeEnum? ErrorCode)
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
}
