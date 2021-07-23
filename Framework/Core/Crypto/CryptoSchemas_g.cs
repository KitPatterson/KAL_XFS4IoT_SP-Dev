/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * CryptoSchemas_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFS4IoT.Crypto
{

    [DataContract]
    public sealed class CapabilitiesClass
    {
        public CapabilitiesClass(AlgorithmsClass Algorithms = null, EmvHashAlgorithmClass EmvHashAlgorithm = null, Dictionary<string, Dictionary<string, Dictionary<string, CryptoAttributesClass>>> CryptoAttributes = null, Dictionary<string, Dictionary<string, Dictionary<string, AuthenticationAttributesClass>>> AuthenticationAttributes = null, Dictionary<string, Dictionary<string, Dictionary<string, VerifyAttributesClass>>> VerifyAttributes = null)
        {
            this.Algorithms = Algorithms;
            this.EmvHashAlgorithm = EmvHashAlgorithm;
            this.CryptoAttributes = CryptoAttributes;
            this.AuthenticationAttributes = AuthenticationAttributes;
            this.VerifyAttributes = VerifyAttributes;
        }

        [DataContract]
        public sealed class AlgorithmsClass
        {
            public AlgorithmsClass(bool? Ecb = null, bool? Cbc = null, bool? Cfb = null, bool? Rsa = null, bool? Cma = null, bool? DesMac = null, bool? TriDesEcb = null, bool? TriDesCbc = null, bool? TriDesCfb = null, bool? TriDesMac = null, bool? MaaMac = null, bool? TriDesMac2805 = null, bool? Sm4 = null, bool? Sm4Mac = null)
            {
                this.Ecb = Ecb;
                this.Cbc = Cbc;
                this.Cfb = Cfb;
                this.Rsa = Rsa;
                this.Cma = Cma;
                this.DesMac = DesMac;
                this.TriDesEcb = TriDesEcb;
                this.TriDesCbc = TriDesCbc;
                this.TriDesCfb = TriDesCfb;
                this.TriDesMac = TriDesMac;
                this.MaaMac = MaaMac;
                this.TriDesMac2805 = TriDesMac2805;
                this.Sm4 = Sm4;
                this.Sm4Mac = Sm4Mac;
            }

            /// <summary>
            /// Electronic Code Book.
            /// </summary>
            [DataMember(Name = "ecb")]
            public bool? Ecb { get; init; }

            /// <summary>
            /// Cipher Block Chaining.
            /// </summary>
            [DataMember(Name = "cbc")]
            public bool? Cbc { get; init; }

            /// <summary>
            /// Cipher Feed Back.
            /// </summary>
            [DataMember(Name = "cfb")]
            public bool? Cfb { get; init; }

            /// <summary>
            /// RSA Encryption.
            /// </summary>
            [DataMember(Name = "rsa")]
            public bool? Rsa { get; init; }

            /// <summary>
            /// ECMA Encryption.
            /// </summary>
            [DataMember(Name = "cma")]
            public bool? Cma { get; init; }

            /// <summary>
            /// MAC calculation using CBC.
            /// </summary>
            [DataMember(Name = "desMac")]
            public bool? DesMac { get; init; }

            /// <summary>
            /// Triple DES with Electronic Code Book.
            /// </summary>
            [DataMember(Name = "triDesEcb")]
            public bool? TriDesEcb { get; init; }

            /// <summary>
            /// Triple DES with Cipher Block Chaining.
            /// </summary>
            [DataMember(Name = "triDesCbc")]
            public bool? TriDesCbc { get; init; }

            /// <summary>
            /// Triple DES with Cipher Feed Back.
            /// </summary>
            [DataMember(Name = "triDesCfb")]
            public bool? TriDesCfb { get; init; }

            /// <summary>
            /// Last Block Triple DES MAC as defined in ISO/IEC 9797-1:1999 [Ref. 32], using: 
            /// block length n=64, padding Method 1 (when padding=0), MAC Algorithm 3, MAC length m where 32&lt;=m&lt;=64.
            /// </summary>
            [DataMember(Name = "triDesMac")]
            public bool? TriDesMac { get; init; }

            /// <summary>
            /// MAC calculation using the Message authenticator algorithm as defined in ISO 8731-2.
            /// </summary>
            [DataMember(Name = "maaMac")]
            public bool? MaaMac { get; init; }

            /// <summary>
            /// Triple DES MAC calculation as defined in ISO 16609:2004 and and Australian Standard 2805.4.
            /// </summary>
            [DataMember(Name = "triDesMac2805")]
            public bool? TriDesMac2805 { get; init; }

            /// <summary>
            /// SM4 block cipher algorithm as defined in Password industry standard of the People's Republic of China GM/T 0002-2012.
            /// </summary>
            [DataMember(Name = "sm4")]
            public bool? Sm4 { get; init; }

            /// <summary>
            /// EMAC calculation using the Message authenticator algorithm as defined in as defined in Password 
            /// industry standard of the People's Republic of China GM/T 0002-2012.
            /// and and in PBOC3.0 JR/T 0025.17-2013.
            /// </summary>
            [DataMember(Name = "sm4Mac")]
            public bool? Sm4Mac { get; init; }

        }

        /// <summary>
        /// Supported encryption modes.
        /// </summary>
        [DataMember(Name = "algorithms")]
        public AlgorithmsClass Algorithms { get; init; }

        [DataContract]
        public sealed class EmvHashAlgorithmClass
        {
            public EmvHashAlgorithmClass(bool? Sha1Digest = null, bool? Sha256Digest = null)
            {
                this.Sha1Digest = Sha1Digest;
                this.Sha256Digest = Sha256Digest;
            }

            /// <summary>
            /// The SHA 1 digest algorithm is supported by the [Crypto.Digest](#crypto.digest) command.
            /// </summary>
            [DataMember(Name = "sha1Digest")]
            public bool? Sha1Digest { get; init; }

            /// <summary>
            /// The SHA 256 digest algorithm, as defined in ISO/IEC 10118-3:2004 and FIPS 180-2, is supported 
            /// by the [Crypto.Digest](#crypto.digest) command.
            /// </summary>
            [DataMember(Name = "sha256Digest")]
            public bool? Sha256Digest { get; init; }

        }

        /// <summary>
        /// Specifies which hash algorithm is supported for the calculation of the HASH.
        /// </summary>
        [DataMember(Name = "emvHashAlgorithm")]
        public EmvHashAlgorithmClass EmvHashAlgorithm { get; init; }

        [DataContract]
        public sealed class CryptoAttributesClass
        {
            public CryptoAttributesClass(CryptoMethodClass CryptoMethod = null)
            {
                this.CryptoMethod = CryptoMethod;
            }

            [DataContract]
            public sealed class CryptoMethodClass
            {
                public CryptoMethodClass(bool? Ecb = null, bool? Cbc = null, bool? Cfb = null, bool? Ofb = null, bool? Ctr = null, bool? Xts = null, bool? RsaesPkcs1V15 = null, bool? RsaesOaep = null)
                {
                    this.Ecb = Ecb;
                    this.Cbc = Cbc;
                    this.Cfb = Cfb;
                    this.Ofb = Ofb;
                    this.Ctr = Ctr;
                    this.Xts = Xts;
                    this.RsaesPkcs1V15 = RsaesPkcs1V15;
                    this.RsaesOaep = RsaesOaep;
                }

                /// <summary>
                /// The ECB encryption method. 
                /// </summary>
                [DataMember(Name = "ecb")]
                public bool? Ecb { get; init; }

                /// <summary>
                /// The CBC encryption method. 
                /// </summary>
                [DataMember(Name = "cbc")]
                public bool? Cbc { get; init; }

                /// <summary>
                /// The CFB encryption method. 
                /// </summary>
                [DataMember(Name = "cfb")]
                public bool? Cfb { get; init; }

                /// <summary>
                /// The The OFB encryption method. 
                /// </summary>
                [DataMember(Name = "ofb")]
                public bool? Ofb { get; init; }

                /// <summary>
                /// The CTR method defined in NIST SP800-38A. 
                /// </summary>
                [DataMember(Name = "ctr")]
                public bool? Ctr { get; init; }

                /// <summary>
                /// The XTS method defined in NIST SP800-38E. 
                /// </summary>
                [DataMember(Name = "xts")]
                public bool? Xts { get; init; }

                /// <summary>
                /// The RSAES_PKCS1-v1.5 algorithm.
                /// </summary>
                [DataMember(Name = "rsaesPkcs1V15")]
                public bool? RsaesPkcs1V15 { get; init; }

                /// <summary>
                /// The RSAES OAEP algorithm. 
                /// </summary>
                [DataMember(Name = "rsaesOaep")]
                public bool? RsaesOaep { get; init; }

            }

            /// <summary>
            /// Specifies the cryptographic method supported by the [Crypto.CryptoData](#crypto.cryptodata) command.
            /// If the key usage is any of the MAC usages (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0)), then the following properties can be true. 
            /// 
            /// * ```ecb``` - The ECB encryption method.
            /// * ```cbc``` - The CBC encryption method.
            /// * ```cfb``` - The CFB encryption method.
            /// * ```ofb``` - The OFB encryption method.
            /// * ```ctr``` - The CTR method defined in NIST SP800-38A.
            /// * ```xts``` - The XTS method defined in NIST SP800-38E.
            /// 
            /// If the algorithm is 'R' and the key usage is ['D0'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0), then the following properties can be true. 
            /// 
            /// * ```rsaesPkcs1V15``` - RSAES_PKCS1-v1.5 algorithm.
            /// * ```rsaesOaep``` - The RSAES OAEP algorithm. 
            /// </summary>
            [DataMember(Name = "cryptoMethod")]
            public CryptoMethodClass CryptoMethod { get; init; }

        }

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.CryptoData](#crypto.cryptodata) command to encrypt
        /// or decrypt data.
        /// </summary>
        [DataMember(Name = "cryptoAttributes")]
        public Dictionary<string, Dictionary<string, Dictionary<string, CryptoAttributesClass>>> CryptoAttributes { get; init; }

        [DataContract]
        public sealed class AuthenticationAttributesClass
        {
            public AuthenticationAttributesClass(CryptoMethodClass CryptoMethod = null, HashAlgorithmClass HashAlgorithm = null)
            {
                this.CryptoMethod = CryptoMethod;
                this.HashAlgorithm = HashAlgorithm;
            }

            [DataContract]
            public sealed class CryptoMethodClass
            {
                public CryptoMethodClass(bool? RsassaPkcs1V15 = null, bool? RsassaPss = null)
                {
                    this.RsassaPkcs1V15 = RsassaPkcs1V15;
                    this.RsassaPss = RsassaPss;
                }

                /// <summary>
                /// The RSASSA-PKCS1-v1.5 algorithm. 
                /// </summary>
                [DataMember(Name = "rsassaPkcs1V15")]
                public bool? RsassaPkcs1V15 { get; init; }

                /// <summary>
                /// The the RSASSA-PSS algorithm.
                /// </summary>
                [DataMember(Name = "rsassaPss")]
                public bool? RsassaPss { get; init; }

            }

            /// <summary>
            /// Specifies the asymmetric signature verification method supported by the [Crypto.GenerateAuthentication](#crypto.generateauthentication) command.
            /// If the key usage is any of the MAC usages (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0)), then following properties are false.
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
                /// If the key usage is any of the MAC usages (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0)), then following properties are false.
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
            /// Specifies the hash algorithm supported.
            /// </summary>
            [DataMember(Name = "hashAlgorithm")]
            public HashAlgorithmClass HashAlgorithm { get; init; }

        }

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.GenerateAuthentication](#crypto.generateauthentication) command
        /// to generate authentication data.
        /// </summary>
        [DataMember(Name = "authenticationAttributes")]
        public Dictionary<string, Dictionary<string, Dictionary<string, AuthenticationAttributesClass>>> AuthenticationAttributes { get; init; }

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
                public CryptoMethodClass(bool? RsassaPkcs1V15 = null, bool? RsassaPss = null)
                {
                    this.RsassaPkcs1V15 = RsassaPkcs1V15;
                    this.RsassaPss = RsassaPss;
                }

                /// <summary>
                /// The RSASSA-PKCS1-v1.5 algorithm. 
                /// </summary>
                [DataMember(Name = "rsassaPkcs1V15")]
                public bool? RsassaPkcs1V15 { get; init; }

                /// <summary>
                /// The the RSASSA-PSS algorithm.
                /// </summary>
                [DataMember(Name = "rsassaPss")]
                public bool? RsassaPss { get; init; }

            }

            /// <summary>
            /// Specifies the asymmetric signature verification method supported by the [Crypto.VerifyAuthentication](#crypto.verifyauthentication) command.
            /// If the key usage is any of the MAC usages (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0)), then following properties are false.
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
            /// Specifies the hash algorithm supported. 
            /// If the key usage is any of the MAC usages (i.e. ['M1'](#common.capabilities.completion.properties.keymanagement.keyattributes.m0)), then following properties are false.
            /// </summary>
            [DataMember(Name = "hashAlgorithm")]
            public HashAlgorithmClass HashAlgorithm { get; init; }

        }

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.VerifyAuthentication](#crypto.verifyauthentication) command
        /// to verify authentication data.
        /// </summary>
        [DataMember(Name = "verifyAttributes")]
        public Dictionary<string, Dictionary<string, Dictionary<string, VerifyAttributesClass>>> VerifyAttributes { get; init; }

    }


}
