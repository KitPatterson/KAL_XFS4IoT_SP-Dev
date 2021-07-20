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
using XFS4IoT.Commands;

namespace XFS4IoT.KeyManagement.Commands
{
    //Original name = ImportKey
    [DataContract]
    [Command(Name = "KeyManagement.ImportKey")]
    public sealed class ImportKeyCommand : Command<ImportKeyCommand.PayloadData>
    {
        public ImportKeyCommand(int RequestId, ImportKeyCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, string Key = null, Dictionary<string, System.Text.Json.JsonElement> KeyAttributes = null, string Value = null, bool? Constructing = null, string DecryptKey = null, DecryptMethodEnum? DecryptMethod = null, string VerificationData = null, string VerifyKey = null, VerifyAttributesClass VerifyAttributes = null, string VendorAttributes = null)
                : base(Timeout)
            {
                this.Key = Key;
                this.KeyAttributes = KeyAttributes;
                this.Value = Value;
                this.Constructing = Constructing;
                this.DecryptKey = DecryptKey;
                this.DecryptMethod = DecryptMethod;
                this.VerificationData = VerificationData;
                this.VerifyKey = VerifyKey;
                this.VerifyAttributes = VerifyAttributes;
                this.VendorAttributes = VendorAttributes;
            }

            /// <summary>
            /// Specifies the name of key being loaded.
            /// </summary>
            [DataMember(Name = "key")]
            public string Key { get; init; }

            /// <summary>
            /// This parameter specifies the encryption algorithm, cryptographic method, and mode to be used for the key imported 
            /// by this command. For a list of valid values see the [KeyManagement.keyAttribute](#common.capabilities.completion.properties.keymanagement.keyattributes) 
            /// capability. The values specified must be compatible with the key identified by key.
            /// </summary>
            [DataMember(Name = "keyAttributes")]
            public Dictionary<string, System.Text.Json.JsonElement> KeyAttributes { get; init; }

            /// <summary>
            /// Specifies the value of key to be loaded formatted in base64.
            /// If it is an RSA key the first 4 bytes contain the exponent and the following 128 the modulus.
            /// This property is not required for secure key entry and can be omitted.
            /// </summary>
            [DataMember(Name = "value")]
            public string Value { get; init; }

            /// <summary>
            /// If the key is under construction through the import of multiple parts from a secure encryption key entry buffer, then this property is set to true.
            /// </summary>
            [DataMember(Name = "constructing")]
            public bool? Constructing { get; init; }

            /// <summary>
            /// Specifies the name of the key used to decrypt the key being loaded.
            /// If value contains a TR-31 key block, then decryptKey is the name of the key block protection key that is used to 
            /// verify and decrypt the key block. This property is not required if the data in value is not encrypted or the constructing property is true.
            /// </summary>
            [DataMember(Name = "decryptKey")]
            public string DecryptKey { get; init; }

            public enum DecryptMethodEnum
            {
                Ecb,
                Cbc,
                Cfb,
                Ofb,
                Ctr,
                Xts,
                RsaesPkcs1V15,
                RsaesOaep
            }

            /// <summary>
            /// Specifies the cryptographic method that shall be used with the key specified by decryptKey.
            /// The device shall use this method to decrypt the encrypted value in the value parameter.
            /// This property is not required if a keyblock is being imported, as the decrypt method is contained within the keyblock.
            /// This property specifies the cryptographic method that will be used with the encryption algorithm specified by algorithm.
            /// This property is not required if the constructing property is true.
            /// For a list of valid values see this property in the [KeyManagement.decryptAttribute](#common.capabilities.completion.properties.keymanagement.decryptattributes)
            /// capability.
            /// If the algorithm is ['A', 'D', or 'T'](#common.capabilities.completion.properties.keymanagement.decryptattributes.a), then this property can be one of the following values: 
            /// 
            /// * ```ecb``` - The ECB encryption method. 
            /// * ```cbc``` - The CBC encryption method. 
            /// * ```cfb``` - The CFB encryption method. 
            /// * ```ofb``` - The OFB encryption method. 
            /// * ```ctr``` - The CTR method defined in NIST SP800-38A. 
            /// * ```xts``` - The XTS method defined in NIST SP800-38E. 
            /// 
            /// If the algorithm is ['R'](#common.capabilities.completion.properties.keymanagement.decryptattributes.a), then this property can be one of the following values: 
            /// 
            /// * ```rsaesPkcs1V15``` - Use the RSAES_PKCS1-v1.5 algorithm. 
            /// * ```rsaesOaep``` - Use the RSAES OAEP algorithm. 
            /// 
            /// If the specified [key](#keymanagement.importkey.command.properties.key) is key usage ['K1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0), then this property can be omitted.
            /// TR-31 defines the cryptographic methods used for each key block version.
            /// </summary>
            [DataMember(Name = "decryptMethod")]
            public DecryptMethodEnum? DecryptMethod { get; init; }

            /// <summary>
            /// Contains the data to be verified before importing.
            /// This property can be omitted if no verification is needed before importing the key or the constructing property is true.
            /// </summary>
            [DataMember(Name = "verificationData")]
            public string VerificationData { get; init; }

            /// <summary>
            /// Specifies the name of the previously loaded key which will be used to verify the verificationData.
            /// This property can be omitted when no verification is needed before importing the key or the constructing property is true.
            /// </summary>
            [DataMember(Name = "verifyKey")]
            public string VerifyKey { get; init; }

            [DataContract]
            public sealed class VerifyAttributesClass
            {
                public VerifyAttributesClass(string Algorithm = null, CryptoMethodEnum? CryptoMethod = null, HashAlgorithmEnum? HashAlgorithm = null)
                {
                    this.Algorithm = Algorithm;
                    this.CryptoMethod = CryptoMethod;
                    this.HashAlgorithm = HashAlgorithm;
                }

                /// <summary>
                /// Specifies the encryption algorithm.
                /// The following values are possible:
                /// 
                /// * ```A``` - AES.
                /// * ```D``` - DEA. 
                /// * ```R``` - RSA. 
                /// * ```T``` - Triple DEA (also referred to as TDEA). 
                /// * ```"0" - "9"``` - These numeric values are reserved for proprietary use.
                /// </summary>
                [DataMember(Name = "algorithm")]
                [DataTypes(Pattern = "^[0-9ADRT]$")]
                public string Algorithm { get; init; }

                public enum CryptoMethodEnum
                {
                    KcvNone,
                    KcvSelf,
                    KcvZero,
                    SigNone,
                    RsassaPkcs1V15,
                    RsassaPs
                }

                /// <summary>
                /// This parameter specifies the [cryptographic method](#common.capabilities.completion.properties.keymanagement.verifyattributes.m0.t.v.cryptomethod) that will be used with encryption algorithm.
                /// 
                /// If the algorithm is ['A', 'D', or 'T'](#common.capabilities.completion.properties.keymanagement.verifyattributes.m0.t) and specified [verifyKey](#keymanagement.importkey.command.properties.verifykey) is MAC key usage (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0)), then this property can be omitted. 
                /// 
                /// If the algorithm is ['A', 'D', or 'T'](#common.capabilities.completion.properties.keymanagement.verifyattributes.m0.t) and specified [verifyKey](#keymanagement.importkey.command.properties.verifykey) is key usage ['00'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0), then this property can be one of the following values: 
                /// 
                /// * ```kcvNone``` - There is no key check value verification required. 
                /// * ```kcvSelf``` - The key check value (KCV) is created by an encryption of the key with itself.
                /// * ```kcvZero``` - The key check value (KCV) is created by encrypting a zero value with the key. 
                /// 
                /// If the algorithm is ['R'](#common.capabilities.completion.properties.keymanagement.verifyattributes.m0.t) and specified [verifyKey](#keymanagement.importkey.command.properties.verifykey) is not key usage ['00'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0), then this property can be one of the following values: 
                /// 
                /// * ```sigNone``` - No signature algorithm specified. No signature verification will take place and the content of verificationData is not required. 
                /// * ```rsassaPkcs1V15``` - Use the RSASSA-PKCS1-v1.5 algorithm. 
                /// * ```rsassaPss``` - Use the RSASSA-PSS algorithm.
                /// </summary>
                [DataMember(Name = "cryptoMethod")]
                public CryptoMethodEnum? CryptoMethod { get; init; }

                public enum HashAlgorithmEnum
                {
                    Sha1,
                    Sha256
                }

                /// <summary>
                /// For asymmetric signature verification methods (Specified [verifyKey](#keymanagement.command.importkey.properties.verifykey) usage is ['S0', 'S1', or 'S2'](#common.capabilities.completion.properties.keymanagement.keyattributes.k1)), this can be one of the following values to be used.
                /// If the specified [verifyKey](#keymanagement.importkey.properties.verifykey) is key usage any of the MAC usages (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.k1)), then this property can be omitted.
                /// 
                /// * ```sha1``` - The SHA 1 digest algorithm. 
                /// * ```sha256``` - The SHA 256 digest algorithm, as defined in ISO/IEC 10118-3:2004 and FIPS 180-2.
                /// </summary>
                [DataMember(Name = "hashAlgorithm")]
                public HashAlgorithmEnum? HashAlgorithm { get; init; }

            }

            /// <summary>
            /// This parameter specifies the encryption algorithm, cryptographic method, and mode to be used to verify this command or to 
            /// generate verification output data. Verifying input data will result in no verification output data.
            /// For a list of valid values see the [Capabilities.verifyAttributes](#common.capabilities.completion.properties.keymanagement.verifyattributes)
            /// capability. This property can be omitted if verificationData is not required or the constructing property is true.
            /// </summary>
            [DataMember(Name = "verifyAttributes")]
            public VerifyAttributesClass VerifyAttributes { get; init; }

            /// <summary>
            /// Specifies the vendor attributes of the key to be imported.
            /// Refer to vendor documentation for details.
            /// If no vendor attributes are used or the constructing property is true, then this property can be omitted.
            /// </summary>
            [DataMember(Name = "vendorAttributes")]
            public string VendorAttributes { get; init; }

        }
    }
}
