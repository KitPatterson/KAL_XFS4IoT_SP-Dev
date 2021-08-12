using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Common
{
    public sealed class CryptoCapabilitiesClass
    {
        [Flags]
        public enum AlgorithmEnum
        {
            NotSupported = 0,
            ECB = 0x0001, //Electronic Code Book
            CBC = 0x0002, //Cipher Block Chaining
            CFB = 0x0004, //Cipher Feed Back
            RSA = 0x0008, //RSA Encryption
            ECMA = 0x0010, //ECMA Encryption
            DESMAC = 0x0020, //MAC calculation using CBC
            TripleDESECB = 0x0040, //Triple DES with Electronic Code Book
            TripleDESCBC = 0x0080, //Triple DES with Cipher Block Chaining
            TripleDESCFB = 0x0100, //Triple DES with Cipher Feed Back
            TripleDESMAC = 0x0200, //Last Block Triple DES MAC as defined in ISO/IEC 9797-1:1999 [Ref. 32], using: block length n=64, Padding Method 1 (when bPadding=0), MAC Algorithm 3, MAC length m where 32<=m<=64
            MAAMAC = 0x0400, //MAC calculation using the Message authenticator algorithm as defined in ISO 8731-2 
            TripleDESMAC2805 = 0x0800, //Triple DES MAC calculation as defined in ISO 16609:2004 and Australian Standard 2805.4 
            SM4 = 0x1000, //SM4 block cipher algorithm as defined in Password industry standard of the People's Republic of China GM/T 0002-2012 
            SM4AMC = 0x2000, //MAC calculation using the Message authenticator algorithm as defined in as defined in Password industry standard of the People's Republic of China GM/T 0002-2012 and in PBOC3.0 JR/T 0025.17-2013 
        }

        [Flags]
        public enum EMVHashAlgorithmEnum
        {
            NotSupported = 0,
            SHA1_Digest = 0x0001, //The SHA 1 digest algorithm is supported by the Digest command
            SHA256_Digest = 0x0002, //The SHA 256 digest algorithm, as defined in ISO/IEC 10118-3:2004 [Ref. 40] and FIPS 180-2 [Ref. 41], is supported by the Digest command
        }


        [Flags]
        public enum HashAlgorithmEnum
        {
            NotSupported = 0,
            SHA1 = 0x0001, //The SHA 1 digest algorithm 
            SHA256 = 0x0002, //The SHA 256 digest algorithm
        }

        public CryptoCapabilitiesClass(AlgorithmEnum Algorithms,
                                       EMVHashAlgorithmEnum EMVHashAlgorithms,
                                       Dictionary<string, Dictionary<string, Dictionary<string, CryptoAttributesClass>>> CryptoAttributes,
                                       Dictionary<string, Dictionary<string, Dictionary<string, VerifyAuthenticationAttributesClass>>> AuthenticationAttributes,
                                       Dictionary<string, Dictionary<string, Dictionary<string, VerifyAuthenticationAttributesClass>>> VerifyAttributes)
        {
            this.Algorithms = Algorithms;
            this.EMVHashAlgorithms = EMVHashAlgorithms;
            this.CryptoAttributes = CryptoAttributes;
            this.AuthenticationAttributes = AuthenticationAttributes;
            this.VerifyAttributes = VerifyAttributes;
        }

        /// <summary>
        /// Supported encryption modes.
        /// </summary>
        public AlgorithmEnum Algorithms { get; init; }

        /// <summary>
        /// Specifies which hash algorithm is supported for the calculation of the HASH.
        /// </summary>
        public EMVHashAlgorithmEnum EMVHashAlgorithms { get; init; }

        public sealed class CryptoAttributesClass
        {
            [Flags]
            public enum CryptoMethodEnum
            {
                NotSupported = 0,
                ECB = 0x0001,
                CBC = 0x0002,
                CFB = 0x0004,
                OFB = 0x0008,
                CTR = 0x0010,
                XTS = 0x0020,
                RSAES_PKCS1_V1_5 = 0x0040,
                RSAES_OAEP = 0x0080,
            }

            public CryptoAttributesClass(CryptoMethodEnum CryptoMethods)
            {
                this.CryptoMethods = CryptoMethods;
            }

            /// <summary>
            /// Specifies the cryptographic method supported by the CryptoData command.
            /// If the key usage is any of the MAC usages (i.e. M1), then the following properties can be true. 
            /// 
            /// * ```ECB``` - The ECB encryption method.
            /// * ```CBC``` - The CBC encryption method.
            /// * ```CFB``` - The CFB encryption method.
            /// * ```OFB``` - The OFB encryption method.
            /// * ```CTR``` - The CTR method defined in NIST SP800-38A.
            /// * ```XTS``` - The XTS method defined in NIST SP800-38E.
            /// 
            /// If the algorithm is 'R' and the key usage is D0, then the following properties can be true. 
            /// 
            /// * ```RSAES_PKCS1_V1_5``` - RSAES_PKCS1-v1.5 algorithm.
            /// * ```RSAES_OAEP``` - The RSAES OAEP algorithm. 
            /// </summary>
            public CryptoMethodEnum CryptoMethods { get; init; }

        }

        /// <summary>
        /// Key-value pair of attributes supported by the CryptoData command to encrypt
        /// or decrypt data.
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, CryptoAttributesClass>>> CryptoAttributes { get; init; }

        public sealed class VerifyAuthenticationAttributesClass
        {
            [Flags]
            public enum RSASignatureAlgorithmEnum
            {
                NotSupported = 0,
                RSASSA_PKCS1_V1_5 = 0x0001,     // SSA_PKCS_V1_5 Signatures supported
                RSASSA_PSS = 0x0002,            // SSA_PSS Signatures supported
            }

            public VerifyAuthenticationAttributesClass(RSASignatureAlgorithmEnum CryptoMethods, 
                                                 HashAlgorithmEnum HashAlgorithms)
            {
                this.CryptoMethods = CryptoMethods;
                this.HashAlgorithms = HashAlgorithms;
            }

            /// <summary>
            /// Specifies the asymmetric signature verification method supported by the Crypto.GenerateAuthentication command.
            /// If the key usage is any of the MAC usages (i.e. M1), then following value is not NotSupported.
            /// </summary>
            public RSASignatureAlgorithmEnum CryptoMethods { get; init; }

            /// <summary>
            /// Specifies the hash algorithm supported.
            /// </summary>
            public HashAlgorithmEnum HashAlgorithms { get; init; }

        }

        /// <summary>
        /// Key-value pair of attributes supported by the GenerateAuthentication command
        /// to generate authentication data.
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, VerifyAuthenticationAttributesClass>>> AuthenticationAttributes { get; init; }

        /// <summary>
        /// Key-value pair of attributes supported by the [Crypto.VerifyAuthentication](#crypto.verifyauthentication) command
        /// to verify authentication data.
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, VerifyAuthenticationAttributesClass>>> VerifyAttributes { get; init; }

    }
}
