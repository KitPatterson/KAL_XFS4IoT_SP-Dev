/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 * 
\***********************************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.Crypto.Commands;
using XFS4IoT.Crypto.Completions;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.KeyManagement;
using XFS4IoT;

namespace XFS4IoTFramework.Crypto
{
    public enum HashAlgorithmEnum
    {
        SHA1,
        SHA256,
    }

    public sealed class GenerateRandomNumberResult : DeviceResult
    {
        public GenerateRandomNumberResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                          string ErrorDescription,
                                          GenerateRandomCompletion.PayloadData.ErrorCodeEnum? ErrorCode)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.RandomNumber = null;
        }

        public GenerateRandomNumberResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                          List<byte> RandomNumber)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.RandomNumber = RandomNumber;
        }
        public GenerateRandomCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        public List<byte> RandomNumber { get; init; }
    }

    public abstract class RequestBase
    {
        public RequestBase(string KeyName,
                           int KeySlot,
                           List<byte> Data,
                           byte Padding)
        {
            this.KeyName = KeyName;
            this.KeySlot = KeySlot;
            this.Data = Data;
            this.Padding = Padding;
        }

        /// <summary>
        /// Specifies the name of the stored key
        /// </summary>
        public string KeyName { get; init; }

        /// <summary>
        /// Key slot to use
        /// </summary>
        public int KeySlot { get; init; }

        /// <summary>
        /// Data to encrypt or decrypt
        /// </summary>
        public List<byte> Data { get; init; }

        /// <summary>
        /// Padding data
        /// </summary>
        public byte Padding { get; init; }
    }

    public sealed class CryptoDataRequest : RequestBase
    {
        public enum CryptoModeEnum
        {
            Encrypt,
            Decrypt,
        }

        public CryptoDataRequest(CryptoModeEnum Mode,
                                 string KeyName,
                                 int KeySlot,
                                 List<byte> Data,
                                 byte Padding,
                                 string IVKey = null,
                                 int IVKeySlot = -1,
                                 List<byte> IVData = null)
            : base(KeyName, KeySlot, Data, Padding)
        {
            this.Mode = Mode;
            this.IVKey = IVKey;
            this.IVKeySlot = IVKeySlot;
            this.IVData = IVData;
        }

        /// <summary>
        /// Data to encrypt or decrypt
        /// </summary>
        public CryptoModeEnum Mode { get; init; }

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
    }

    public sealed class CryptoDataResult : DeviceResult
    {
        public CryptoDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                string ErrorDescription,
                                CryptoDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.CryptoData = null;
        }

        public CryptoDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                List<byte> CryptoData)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.CryptoData = CryptoData;
        }

        public CryptoDataCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        public List<byte> CryptoData { get; init; }
    }

    public sealed class GenerateSignatureRequest : RequestBase
    {
        public GenerateSignatureRequest(string KeyName,
                                        int KeySlot,
                                        List<byte> Data,
                                        byte Padding,
                                        CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum SignatureAlgorithm)
            : base(KeyName, KeySlot, Data, Padding)
        {
            this.SignatureAlgorithm = SignatureAlgorithm;
        }

        /// <summary>
        /// Signature algorithm
        /// </summary>
        public CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum SignatureAlgorithm { get; init; }

    }

    public sealed class GenerateMACRequest : RequestBase
    {
        public GenerateMACRequest(string KeyName,
                                  int KeySlot,
                                  List<byte> Data,
                                  byte Padding,
                                  string IVKey = null,
                                  int IVKeySlot = -1,
                                  List<byte> IVData = null)
            : base(KeyName, KeySlot, Data, Padding)
        {
            this.IVKey = IVKey;
            this.IVKeySlot = IVKeySlot;
            this.IVData = IVData;
        }

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
    }

    public sealed class GenerateAuthenticationDataResult : DeviceResult
    {
        public GenerateAuthenticationDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                          string ErrorDescription,
                                          GenerateAuthenticationCompletion.PayloadData.ErrorCodeEnum? ErrorCode)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.AuthenticationData = null;
        }

        public GenerateAuthenticationDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                                List<byte> AuthenticationData)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.AuthenticationData = AuthenticationData;
        }
        public GenerateAuthenticationCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        public List<byte> AuthenticationData { get; init; }
    }

    public sealed class VerifySignatureRequest : RequestBase
    {
        public VerifySignatureRequest(string KeyName,
                                      int KeySlot,
                                      List<byte> Data,
                                      List<byte> VerificationData,
                                      CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum SignatureAlgorithm,
                                      byte Padding)
            : base(KeyName, KeySlot, Data, Padding)
        {
            this.SignatureAlgorithm = SignatureAlgorithm;
            this.VerificationData = VerificationData;
        }

        /// <summary>
        /// Signature algorithm
        /// </summary>
        public CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum SignatureAlgorithm { get; init; }

        /// <summary>
        /// Data to verify signature
        /// </summary>
        public List<byte> VerificationData { get; init; }
    }

    public sealed class VerifyMACRequest : RequestBase
    {
        public VerifyMACRequest(string KeyName,
                                int KeySlot,
                                List<byte> Data,
                                List<byte> VerificationData,
                                byte Padding,
                                string IVKey = null,
                                int IVKeySlot = -1,
                                List<byte> IVData = null)
            : base(KeyName, KeySlot, Data, Padding)
        {
            this.VerificationData = VerificationData;
            this.IVKey = IVKey;
            this.IVKeySlot = IVKeySlot;
            this.IVData = IVData;
        }

        /// <summary>
        /// Data to verify MAC
        /// </summary>
        public List<byte> VerificationData { get; init; }

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
    }

    public sealed class VerifyAuthenticationDataResult : DeviceResult
    {
        public VerifyAuthenticationDataResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                              string ErrorDescription = null,
                                              VerifyAuthenticationCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public VerifyAuthenticationCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }
    }

    public sealed class GenerateDigestRequest
    {
        public GenerateDigestRequest(HashAlgorithmEnum Hash,
                                     List<byte> DataToHash)
        {
            this.Hash = Hash;
            this.DataToHash = DataToHash;
        }

        public HashAlgorithmEnum Hash { get; init; }

        public List<byte> DataToHash { get; init; }
    }

    public sealed class GenerateDigestResult : DeviceResult
    {
        public GenerateDigestResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                          string ErrorDescription,
                                          DigestCompletion.PayloadData.ErrorCodeEnum? ErrorCode)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.Digest = null;
        }

        public GenerateDigestResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    List<byte> Digest)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.Digest = Digest;
        }
        public DigestCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        public List<byte> Digest { get; init; }
    }
}
