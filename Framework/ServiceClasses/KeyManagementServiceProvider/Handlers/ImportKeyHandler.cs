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
using System.Text.RegularExpressions;
using XFS4IoT;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using XFS4IoT.Completions;
using XFS4IoTFramework.Common;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class ImportKeyHandler
    {
        private async Task<ImportKeyCompletion.PayloadData> HandleImportKey(IImportKeyEvents events, ImportKeyCommand importKey, CancellationToken cancel)
        {
            if (string.IsNullOrEmpty(importKey.Payload.Key))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"No key name specified.",
                                                           ImportKeyCompletion.PayloadData.ErrorCodeEnum.KeyNotFound);
            }

            if (importKey.Payload.KeyAttributes is null)
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"No key data specified.");
            }

            if (string.IsNullOrEmpty(importKey.Payload.KeyAttributes.KeyUsage))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"No key usage specified.");
            }
            if (!Regex.IsMatch(importKey.Payload.KeyAttributes.KeyUsage, KeyDetail.regxKeyUsage))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"Invalid key usage specified. {importKey.Payload.KeyAttributes.KeyUsage}");
            }

            if (string.IsNullOrEmpty(importKey.Payload.KeyAttributes.Algorithm))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"No key algorith specified.");
            }
            if (!Regex.IsMatch(importKey.Payload.KeyAttributes.Algorithm, KeyDetail.regxAlgorithm))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"Invalid algorith specified. {importKey.Payload.KeyAttributes.Algorithm}");
            }

            if (string.IsNullOrEmpty(importKey.Payload.KeyAttributes.ModeOfUse))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"No key mode specified.");
            }
            if (!Regex.IsMatch(importKey.Payload.KeyAttributes.ModeOfUse, KeyDetail.regxModeOfUse))
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                           $"Invalid key mode specified.");
            }

            // Check key attributes supported
            List<string> keyUsages = new() { importKey.Payload.KeyAttributes.KeyUsage };
            for (int i = 0; i < 100; i++)
                keyUsages.Add(i.ToString("00"));
            bool keyAttribSupported = false;
            foreach (string keyUsage in keyUsages)
            {
                if (KeyManagement.KeyManagementCapabilities.KeyAttributes.ContainsKey(keyUsage))
                {
                    List<string> algorithms = new() { importKey.Payload.KeyAttributes.Algorithm };
                    for (int i = 0; i < 10; i++)
                        algorithms.Add(i.ToString("0"));
                    foreach (string algorithm in algorithms)
                    {
                        if (KeyManagement.KeyManagementCapabilities.KeyAttributes[keyUsage].ContainsKey(algorithm))
                        {
                            List<string> modes = new() { importKey.Payload.KeyAttributes.ModeOfUse };
                            for (int i = 0; i < 10; i++)
                                modes.Add(i.ToString("0"));
                            foreach (string mode in modes)
                            {
                                keyAttribSupported = KeyManagement.KeyManagementCapabilities.KeyAttributes[keyUsage][algorithm].ContainsKey(mode);
                                if (keyAttribSupported)
                                    break;
                            }
                        }
                        if (keyAttribSupported)
                            break;
                    }
                }
                if (keyAttribSupported)
                    break;
            }

            if (!keyAttribSupported)
            {
                return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                           $"Specified key attribute is not supported.",
                                                           ImportKeyCompletion.PayloadData.ErrorCodeEnum.ModeNotSupported);
            }

            int keySlot = KeyManagement.FindKeySlot(importKey.Payload.Key);
            SecureKeyEntryStatusClass secureKeyEntryStatus = KeyManagement.GetSecureKeyEntryStatus();
            bool assemblyParts;

            if (importKey.Payload.Constructing is not null &&
                (bool)importKey.Payload.Constructing)
            {
                if (!KeyManagement.KeyManagementCapabilities.KeyImportThroughParts)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                               $"The constructing property is enabled, but the device doesn't support secure key entry.");
                }

                if (!secureKeyEntryStatus.KeyBuffered)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"There are not key component buffered.",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.KeyNoValue);
                }

                if (!string.IsNullOrEmpty(importKey.Payload.Value))
                {
                    Logger.Warning(Constants.Framework, "Key value is set to buffer key components. the key data is ignored.");
                }

                int componentNumber;
                Dictionary<SecureKeyEntryStatusClass.KeyPartEnum, KeyComponentStatus> keyStatus = secureKeyEntryStatus.GetKeyStatus();
                if (!keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].Stored)
                {
                    keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].Stored.IsFalse($"First part of key is not loaded, but the second part of key flag is enabled.");
                    componentNumber = (int)SecureKeyEntryStatusClass.KeyPartEnum.First;
                }
                else
                {
                    // first part of key component is already loaded
                    if (keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.Second].Stored)
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                   $"Both 2 parts of key components already stored.");
                    }

                    if (keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].KeyName != importKey.Payload.Key)
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                   $"Invalid key name specified. first part stored {keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].KeyName}, attempt to load {importKey.Payload.Key}");
                    }

                    componentNumber = (int)SecureKeyEntryStatusClass.KeyPartEnum.Second;
                }

                Logger.Log(Constants.DeviceClass, "KeyManagement.ImportKeyPart()");

                var importKeyPartResult = await Device.ImportKeyPart(new ImportKeyPartRequest(importKey.Payload.Key,
                                                                                              componentNumber,
                                                                                              importKey.Payload.KeyAttributes.KeyUsage,
                                                                                              importKey.Payload.KeyAttributes.Algorithm,
                                                                                              importKey.Payload.KeyAttributes.ModeOfUse,
                                                                                              importKey.Payload.KeyAttributes.Restricted),
                                                                     cancel);

                Logger.Log(Constants.DeviceClass, $"KeyManagement.ImportKeyPart() -> {importKeyPartResult.CompletionCode}, {importKeyPartResult.ErrorCode}");

                Dictionary<string, Dictionary<string, Dictionary<string, ImportKeyCompletion.PayloadData.VerifyAttributesClass>>> verifyAttribute = null;
                if (importKeyPartResult.CompletionCode == MessagePayload.CompletionCodeEnum.Success)
                {
                    secureKeyEntryStatus.KeyPartLoaded((SecureKeyEntryStatusClass.KeyPartEnum)componentNumber, importKey.Payload.Key);
                    // Successfully loaded and add key information managing internally
                    KeyManagement.AddKey(importKey.Payload.Key,
                                         keySlot,
                                         importKey.Payload.KeyAttributes.KeyUsage,
                                         importKey.Payload.KeyAttributes.Algorithm,
                                         importKey.Payload.KeyAttributes.ModeOfUse,
                                         importKeyPartResult.KeyLength,
                                         KeyDetail.KeyStatusEnum.Construct,
                                         false,
                                         importKey.Payload.KeyAttributes.Restricted);

                    if (importKeyPartResult.VerifyAttribute is not null)
                    {
                        ImportKeyCompletion.PayloadData.VerifyAttributesClass verifyAttributes = new(
                            new ImportKeyCompletion.PayloadData.VerifyAttributesClass.CryptoMethodClass(importKeyPartResult.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.KCVNone,
                                                                                                        importKeyPartResult.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.KCVSelf,
                                                                                                        importKeyPartResult.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.KCVZero,
                                                                                                        importKeyPartResult.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.SignatureNone,
                                                                                                        importKeyPartResult.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.RSASSA_PKCS1_V1_5,
                                                                                                        importKeyPartResult.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.RSASSA_PSS),
                            new ImportKeyCompletion.PayloadData.VerifyAttributesClass.HashAlgorithmClass(importKeyPartResult.VerifyAttribute.HashAlgorithm == ImportKeyRequest.VerifyAttributeClass.HashAlgorithmEnum.SHA1,
                                                                                                         importKeyPartResult.VerifyAttribute.HashAlgorithm == ImportKeyRequest.VerifyAttributeClass.HashAlgorithmEnum.SHA256));

                        Dictionary<string, ImportKeyCompletion.PayloadData.VerifyAttributesClass> verifyAttrib = new();
                        verifyAttrib.Add(importKey.Payload.KeyAttributes.ModeOfUse, verifyAttributes);
                        Dictionary<string, Dictionary<string, ImportKeyCompletion.PayloadData.VerifyAttributesClass>> algorithmAttrib = new();
                        algorithmAttrib.Add(importKey.Payload.KeyAttributes.Algorithm, verifyAttrib);
                        verifyAttribute = new();
                        verifyAttribute.Add(importKey.Payload.KeyAttributes.KeyUsage, algorithmAttrib);
                    }
                }

                return new ImportKeyCompletion.PayloadData(importKeyPartResult.CompletionCode,
                                                           importKeyPartResult.ErrorDescription,
                                                           importKeyPartResult.ErrorCode,
                                                           importKeyPartResult.VerificationData is not null && importKeyPartResult.VerificationData.Count > 0 ? Convert.ToBase64String(importKeyPartResult.VerificationData.ToArray()) : null,
                                                           verifyAttribute,
                                                           importKeyPartResult.KeyLength);
            }
            else
            {
                Dictionary<SecureKeyEntryStatusClass.KeyPartEnum, KeyComponentStatus> keyStatus = secureKeyEntryStatus.GetKeyStatus();
                assemblyParts = (keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].Stored && keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.Second].Stored);
                
                if (assemblyParts)
                {
                    if (!string.IsNullOrEmpty(importKey.Payload.Value))
                    {
                        Logger.Warning(Constants.Framework, "Key value is set for assembly buffered key components. the key data is ignored.");
                    }

                    Contracts.Assert(keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].KeyName == keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.Second].KeyName, $"Stored key component name should be identical. {keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].KeyName}, {keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.Second].KeyName}");


                    if (keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].KeyName != importKey.Payload.Key)
                    {
                        // Some of firmware allows to load different encryption key while secure key components are already loaded, however the SP framework returns error.
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.SequenceError,
                                                                   $"Invalid sequence for importing encryption key as the secure key entry process is enabled. the key name preserved for the key components different on assemblying key components.");
                    }
                }
                else
                {
                    if (keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.First].Stored ||
                        keyStatus[SecureKeyEntryStatusClass.KeyPartEnum.Second].Stored)
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.SequenceError,
                                                                   $"Invalid sequence for importing encryption key as the secure key entry process is enabled.");
                    }

                    if (string.IsNullOrEmpty(importKey.Payload.Value))
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                   $"No key value specified. {importKey.Payload.Key}");
                    }

                }
            }

            if (!string.IsNullOrEmpty(importKey.Payload.DecryptKey))
            {
                KeyDetail decryptKeyDetail = KeyManagement.GetKeyDetail(importKey.Payload.DecryptKey);
                if (decryptKeyDetail is null)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                           $"Specified decrypt key is not stored. {importKey.Payload.DecryptKey}",
                                                           ImportKeyCompletion.PayloadData.ErrorCodeEnum.KeyNotFound);
                }

                if (decryptKeyDetail.KeyStatus != KeyDetail.KeyStatusEnum.Loaded)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                           $"Specified decrypt key is not loaded. {importKey.Payload.DecryptKey}",
                                                           ImportKeyCompletion.PayloadData.ErrorCodeEnum.KeyNoValue);
                }

                if (!KeyManagement.KeyManagementCapabilities.DecryptAttributes.ContainsKey(decryptKeyDetail.Algorithm))
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"Specified decrypt key doesn't support required algorithm. {importKey.Payload.DecryptKey}",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.AlgorithmNotSupported);
                }

                if (importKey.Payload.DecryptMethod is null)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                               $"No decrypt method specified.");
                }
                
                KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum decryptMethod = KeyManagement.KeyManagementCapabilities.DecryptAttributes[decryptKeyDetail.Algorithm].DecryptMethods;
                if (decryptMethod == KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.NotSupported)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"No decrypt method is not supported. {importKey.Payload.DecryptMethod}",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.CryptoMethodNotSupported);
                }

                if (decryptKeyDetail.Algorithm != "R")
                {
                    if ((importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.Cbc &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.CBC)) ||
                        (importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.Cfb &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.CFB)) ||
                        (importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.Ctr &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.CTR)) ||
                        (importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.Ecb &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.ECB)) ||
                        (importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.Ofb &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.OFB)) ||
                        (importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.Xts &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.XTS)))
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                   $"No decrypt method is not supported. {importKey.Payload.DecryptMethod}",
                                                                   ImportKeyCompletion.PayloadData.ErrorCodeEnum.CryptoMethodNotSupported);
                    }
                }
                else
                {
                    if ((importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.RsaesOaep &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.RSAES_OAEP)) ||
                        (importKey.Payload.DecryptMethod == ImportKeyCommand.PayloadData.DecryptMethodEnum.RsaesPkcs1V15 &&
                         !decryptMethod.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.RSAES_PKCS1_V1_5)))
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                   $"No decrypt method is supported. {importKey.Payload.DecryptMethod}",
                                                                   ImportKeyCompletion.PayloadData.ErrorCodeEnum.CryptoMethodNotSupported);
                    }
                }
            }

            if (!string.IsNullOrEmpty(importKey.Payload.VerifyKey))
            {
                if (importKey.Payload.VerificationData is null)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                               $"No verification data specified.");
                }

                KeyDetail verifyKeyDetail = KeyManagement.GetKeyDetail(importKey.Payload.VerifyKey);
                if (verifyKeyDetail is null)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                           $"Specified verify key is not stored. {importKey.Payload.VerifyKey}",
                                                           ImportKeyCompletion.PayloadData.ErrorCodeEnum.KeyNotFound);
                }

                if (verifyKeyDetail.KeyStatus != KeyDetail.KeyStatusEnum.Loaded)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                           $"Specified verify key is not loaded. {importKey.Payload.VerifyKey}",
                                                           ImportKeyCompletion.PayloadData.ErrorCodeEnum.KeyNoValue);
                }

                if (!KeyManagement.KeyManagementCapabilities.VerifyAttributes.ContainsKey(verifyKeyDetail.KeyUsage))
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"Specified verify key doesn't support required key usage. {importKey.Payload.VerifyKey}",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.UseViolation);
                }

                if (!KeyManagement.KeyManagementCapabilities.VerifyAttributes[verifyKeyDetail.KeyUsage].ContainsKey(verifyKeyDetail.Algorithm))
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"Specified verify key doesn't support required algorithm. {importKey.Payload.VerifyKey}",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.AlgorithmNotSupported);
                }

                if (KeyManagement.KeyManagementCapabilities.VerifyAttributes[verifyKeyDetail.KeyUsage][verifyKeyDetail.Algorithm].ContainsKey("V"))
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"Specified verify key doesn't support required mode of use. {importKey.Payload.VerifyKey}",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.ModeNotSupported);
                }

                KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum cryptoMethod = KeyManagement.KeyManagementCapabilities.VerifyAttributes[verifyKeyDetail.KeyUsage][verifyKeyDetail.Algorithm]["V"].CryptoMethod;
                if (cryptoMethod == KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.NotSupported)
                {
                    return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                               $"Crypto method is not supported. {importKey.Payload.VerifyAttributes.CryptoMethod}",
                                                               ImportKeyCompletion.PayloadData.ErrorCodeEnum.CryptoMethodNotSupported);
                }

                if (verifyKeyDetail.Algorithm != "R")
                {
                    if ((importKey.Payload.VerifyAttributes.CryptoMethod == ImportKeyCommand.PayloadData.VerifyAttributesClass.CryptoMethodEnum.KcvNone &&
                         !cryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.KCVNone)) ||
                        (importKey.Payload.VerifyAttributes.CryptoMethod == ImportKeyCommand.PayloadData.VerifyAttributesClass.CryptoMethodEnum.KcvSelf &&
                         !cryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.KCVSelf)) ||
                        (importKey.Payload.VerifyAttributes.CryptoMethod == ImportKeyCommand.PayloadData.VerifyAttributesClass.CryptoMethodEnum.KcvZero &&
                         !cryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.KCVZero)))
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                   $"No crypto method is supported. {importKey.Payload.VerifyAttributes.CryptoMethod}",
                                                                   ImportKeyCompletion.PayloadData.ErrorCodeEnum.CryptoMethodNotSupported);
                    }
                }
                else
                {
                    if ((importKey.Payload.VerifyAttributes.CryptoMethod == ImportKeyCommand.PayloadData.VerifyAttributesClass.CryptoMethodEnum.SigNone &&
                         !cryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.SignatureNone)) ||
                        (importKey.Payload.VerifyAttributes.CryptoMethod == ImportKeyCommand.PayloadData.VerifyAttributesClass.CryptoMethodEnum.RsassaPkcs1V15 &&
                         !cryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.RSASSA_PKCS1_V1_5)) ||
                        (importKey.Payload.VerifyAttributes.CryptoMethod == ImportKeyCommand.PayloadData.VerifyAttributesClass.CryptoMethodEnum.RsassaPs &&
                         !cryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.RSASSA_PSS)))
                    {
                        return new ImportKeyCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                   $"No decrypt method is not supported. {importKey.Payload.VerifyAttributes.CryptoMethod}",
                                                                   ImportKeyCompletion.PayloadData.ErrorCodeEnum.CryptoMethodNotSupported);
                    }
                }
            }

            ImportKeyResult result;
            if (assemblyParts)
            {
                Logger.Log(Constants.DeviceClass, "KeyManagement.AssemblyKeyParts()");

                result = await Device.AssemblyKeyParts(new AssemblyKeyPartsRequest(importKey.Payload.Key,
                                                                                   keySlot,
                                                                                   importKey.Payload.KeyAttributes.KeyUsage,
                                                                                   importKey.Payload.KeyAttributes.Algorithm,
                                                                                   importKey.Payload.KeyAttributes.ModeOfUse,
                                                                                   importKey.Payload.KeyAttributes.Restricted),
                                                       cancel);

                Logger.Log(Constants.DeviceClass, $"KeyManagement.AssemblyKeyParts() -> {result.CompletionCode}, {result.ErrorCode}");
            }
            else
            {
                Logger.Log(Constants.DeviceClass, "KeyManagement.ImportKey()");

                result = await Device.ImportKey(new ImportKeyRequest(importKey.Payload.Key,
                                                                     keySlot,
                                                                     Convert.FromBase64String(importKey.Payload.Value).ToList(),
                                                                     importKey.Payload.KeyAttributes.KeyUsage,
                                                                     importKey.Payload.KeyAttributes.Algorithm,
                                                                     importKey.Payload.KeyAttributes.ModeOfUse,
                                                                     importKey.Payload.KeyAttributes.Restricted),
                                                cancel);

                Logger.Log(Constants.DeviceClass, $"KeyManagement.ImportKey() -> {result.CompletionCode}, {result.ErrorCode}");
            }

            Dictionary<string, Dictionary<string, Dictionary<string, ImportKeyCompletion.PayloadData.VerifyAttributesClass>>> importKeyVerifyAttib = null;
            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success)
            {
                secureKeyEntryStatus.Reset();

                if (result.VerifyAttribute is not null)
                {
                    ImportKeyCompletion.PayloadData.VerifyAttributesClass verifyAttributes = new (
                        new ImportKeyCompletion.PayloadData.VerifyAttributesClass.CryptoMethodClass(result.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.KCVNone,
                                                                                                    result.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.KCVSelf,
                                                                                                    result.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.KCVZero,
                                                                                                    result.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.SignatureNone,
                                                                                                    result.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.RSASSA_PKCS1_V1_5,
                                                                                                    result.VerifyAttribute.VerifyMethod == ImportKeyRequest.VerifyAttributeClass.VerifyMethodEnum.RSASSA_PSS),
                            new ImportKeyCompletion.PayloadData.VerifyAttributesClass.HashAlgorithmClass(result.VerifyAttribute.HashAlgorithm == ImportKeyRequest.VerifyAttributeClass.HashAlgorithmEnum.SHA1,
                                                                                                         result.VerifyAttribute.HashAlgorithm == ImportKeyRequest.VerifyAttributeClass.HashAlgorithmEnum.SHA256));

                    Dictionary<string, ImportKeyCompletion.PayloadData.VerifyAttributesClass> verifyAttrib = new();
                    verifyAttrib.Add(importKey.Payload.KeyAttributes.ModeOfUse, verifyAttributes);
                    Dictionary<string, Dictionary<string, ImportKeyCompletion.PayloadData.VerifyAttributesClass>> algorithmAttrib = new();
                    algorithmAttrib.Add(importKey.Payload.KeyAttributes.Algorithm, verifyAttrib);
                    importKeyVerifyAttib = new();
                    importKeyVerifyAttib.Add(importKey.Payload.KeyAttributes.KeyUsage, algorithmAttrib);
                }
                // Successfully loaded and add key information managing internally
                KeyManagement.AddKey(importKey.Payload.Key,
                                     result.UpdatedKeySlot is null ? keySlot : (int)result.UpdatedKeySlot,
                                     importKey.Payload.KeyAttributes.KeyUsage,
                                     importKey.Payload.KeyAttributes.Algorithm,
                                     importKey.Payload.KeyAttributes.ModeOfUse,
                                     result.KeyLength,
                                     KeyDetail.KeyStatusEnum.Loaded,
                                     false,
                                     importKey.Payload.KeyAttributes.Restricted,
                                     result.KeyInformation?.KeyVersionNumber,
                                     result.KeyInformation?.Exportability,
                                     result.KeyInformation?.OptionalKeyBlockHeader,
                                     result.KeyInformation?.Generation,
                                     result.KeyInformation?.ActivatingDate,
                                     result.KeyInformation?.ExpiryDate,
                                     result.KeyInformation?.Version);
            }

            return new ImportKeyCompletion.PayloadData(result.CompletionCode,
                                                       result.ErrorDescription,
                                                       result.ErrorCode,
                                                       result.VerificationData is not null && result.VerificationData.Count > 0 ? Convert.ToBase64String(result.VerificationData.ToArray()) : null,
                                                       importKeyVerifyAttib,
                                                       result.KeyLength);
        }
    }
}
