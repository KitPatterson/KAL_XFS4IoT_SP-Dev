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
        public CapabilitiesClass(AlgorithmsClass Algorithms = null, EmvHashAlgorithmClass EmvHashAlgorithm = null, Dictionary<string, System.Text.Json.JsonElement> CryptoAttributes = null, Dictionary<string, System.Text.Json.JsonElement> AuthenticationAttributes = null, Dictionary<string, System.Text.Json.JsonElement> VerifyAttributes = null)
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

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.CryptoData](#crypto.cryptodata) command to encrypt
        /// or decrypt data.
        /// </summary>
        [DataMember(Name = "cryptoAttributes")]
        public Dictionary<string, System.Text.Json.JsonElement> CryptoAttributes { get; init; }

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.GenerateAuthentication](#crypto.generateauthentication) command
        /// to generate authentication data.
        /// </summary>
        [DataMember(Name = "authenticationAttributes")]
        public Dictionary<string, System.Text.Json.JsonElement> AuthenticationAttributes { get; init; }

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.VerifyAuthentication](#crypto.verifyauthentication) command
        /// to verify authentication data.
        /// </summary>
        [DataMember(Name = "verifyAttributes")]
        public Dictionary<string, System.Text.Json.JsonElement> VerifyAttributes { get; init; }

    }


}
