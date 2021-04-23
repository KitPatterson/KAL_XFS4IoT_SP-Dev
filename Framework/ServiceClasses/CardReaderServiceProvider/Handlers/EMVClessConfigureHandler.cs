/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessConfigureHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class EMVClessConfigureHandler
    {
        public sealed class AIDInfo
        {
            public AIDInfo(List<byte> AID, bool PartialSelection, int TransactionType, List<byte> KernelIdentifier, List<byte> ConfigData)
            {
                this.AID = AID;
                this.PartialSelection = PartialSelection;
                this.TransactionType = TransactionType;
                this.KernelIdentifier = KernelIdentifier;
                this.ConfigData = ConfigData;
            }

            /// <summary>
            /// The application identifier to be accepted by the contactless chip card reader. The
            /// [CardReader.EMVClessQueryApplications](#cardreader.emvclessqueryapplications) command will
            /// return the list of supported application identifiers.
            /// </summary>
            public List<byte> AID { get; private set; }

            /// <summary>
            /// If PartialSelection is true, partial name selection of the specified AID is enabled. If
            /// PartialSelection is false, partial name selection is disabled. A detailed explanation for
            /// partial name selection is given in EMV 4.3 Book 1, Section 11.3.5.
            /// </summary>
            public bool PartialSelection { get; private set; }

            /// <summary>
            /// The transaction type supported by the AID. This indicates the type of financial transaction
            /// represented by the first two digits of the ISO 8583:1987 Processing Code.
            /// </summary>
            public int TransactionType { get; private set; }

            /// <summary>
            /// The EMVCo defined kernel identifier associated with the AID.
            /// This field will be ignored if the reader does not support kernel identifiers.
            /// </summary>
            public List<byte> KernelIdentifier { get; private set; }

            /// <summary>
            /// The list of BER-TLV formatted configuration data, applicable to
            /// the specific AID-Kernel ID-Transaction Type combination. The appropriate payment systems
            /// specifications define the BER-TLV tags to be configured.
            /// </summary>

            public List<byte> ConfigData { get; private set; }

        }

        public sealed class PublicKey
        {
            /// <summary>
            /// The algorithm used in the calculation of the CA Public Key checksum.A detailed
            /// description of secure hash algorithm values is given in EMV Book 2, Annex B3; see reference
            /// [2]. For example, if the EMV specification indicates the algorithm is ‘01’, the value of the
            ///  algorithm is coded as 0x01.
            /// </summary>
            public int AlgorithmIndicator { get; private set; }

            /// <summary>
            /// The CA Public Key Exponent for the specific RID.This value
            /// is represented by the minimum number of bytes required.A detailed description of public key
            /// exponent values is given in EMV Book 2, Annex B2; see reference[2]. For example,
            /// representing value ‘216 + 1’ requires 3 bytes in hexadecimal(0x01, 0x00, 0x01), while value
            /// ‘3’ is coded as 0x03.
            /// </summary>
            public List<byte> Exponent;

            /// <summary>
            /// The CA Public Key Modulus for the specific RID.
            /// </summary>
            public List<byte> Modulus;

            /// <summary>
            /// The 20 byte checksum value for the CA Public Key
            /// </summary>
            public List<byte> Checksum;
        }

        public sealed class PublicKeyInfo
        {
            public PublicKeyInfo(List<byte> RID, List<PublicKeyInfo> CAPublicKey)
            {
                this.RID = RID;
                this.CAPublicKey = CAPublicKey;
            }

            /// <summary>
            /// Specifies the payment system's Registered Identifier (RID). RID is the first 5 bytes of the AID
            /// and identifies the payments system.
            /// </summary>
            public List<byte> RID { get; private set; }

            /// <summary>
            /// CA Public Key information for the specified RID
            /// </summary>
            public List<PublicKeyInfo> CAPublicKey { get; private set; }

        }

        /// <summary>
        /// EMVClessConfigureRequest
        /// Provide EMV terminal configuration to be set
        /// </summary>
        public sealed class EMVClessConfigureRequest
        {
            /// <summary>
            /// EMVClessConfigureRequest
            /// </summary>
            /// <param name="TerminalData">Terminal configuration data formatted in TLV.</param>
            /// <param name="AIDs">List of AIDs</param>
            /// <param name="PublicKeys">List of the CA publc keys</param>
            public EMVClessConfigureRequest(List<byte> TerminalData, List<AIDInfo> AIDs, List<PublicKeyInfo> PublicKeys)
            {
                this.TerminalData = TerminalData;
                this.AIDs = AIDs;
                this.PublicKeys = PublicKeys;
            }

            /// <summary>
            /// Base64 encoded representation of the BER-TLV formatted data for the terminal e.g. Terminal Type,
            /// Transaction Category Code, Merchant Name & Location etc. Any terminal based data elements referenced
            /// in the Payment Systems Specifications or EMVCo Contactless Payment Systems Specifications Books may be
            /// included (see References [2] to [14] section for more details).
            /// </summary>
            public List<byte> TerminalData { get; private set; }

            /// <summary>
            /// Specifies the list of acceptable payment system applications. For EMVCo approved contactless card
            /// readers each AID is associated with a Kernel Identifier and a Transaction Type. Legacy approved
            /// contactless readers may use only the AID.
            /// 
            /// Each AID-Transaction Type or each AID-Kernel-Transaction Type combination will have its own unique set
            /// of configuration data. See References [2] and [3] for more details.
            /// </summary>
            public List<AIDInfo> AIDs { get; private set; }

            /// <summary>
            /// Specifies the encryption key information required by an intelligent contactless chip card reader for
            /// offline data authentication.
            /// </summary>
            public List<PublicKeyInfo> PublicKeys { get; private set; }
        }

        /// <summary>
        /// EMVClessConfigureResult
        /// Return result of terminal configuration setup.
        /// </summary>
        public sealed class EMVClessConfigureResult : DeviceResult
        {
            public EMVClessConfigureResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                           EMVClessConfigureCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                           string ErrorDescription = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
            }

            public EMVClessConfigureCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
        }

        private async Task<EMVClessConfigureCompletion.PayloadData> HandleEMVClessConfigure(IEMVClessConfigureEvents events, EMVClessConfigureCommand eMVClessConfigure, CancellationToken cancel)
        {
            /// Data check
            if ((eMVClessConfigure.Payload.AidData is null || eMVClessConfigure.Payload.AidData.Count == 0) &&
                eMVClessConfigure.Payload.TerminalData is null &&
                (eMVClessConfigure.Payload.KeyData is null || eMVClessConfigure.Payload.KeyData.Count == 0))
            {
                return new EMVClessConfigureCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                   "No terminal configuration data supplied.");
            }

            List<AIDInfo> AIDs = new(); 
            foreach (EMVClessConfigureCommand.PayloadData.AidDataClass AID in eMVClessConfigure.Payload.AidData)
            {
                if (string.IsNullOrEmpty(AID.Aid))
                {
                    return new EMVClessConfigureCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                       "No AID is supplied.");
                }
                if (AID.PartialSelection is null)
                {
                    return new EMVClessConfigureCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                       "No PartialSelection is supplied.");
                }
                if (AID.TransactionType is null)
                {
                    return new EMVClessConfigureCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                       "No TransactionType is supplied.");
                }
                AIDs.Add(new AIDInfo(new List<byte>(Convert.FromBase64String(AID.Aid)), 
                         (bool)AID.PartialSelection, 
                         (int)AID.TransactionType, 
                         string.IsNullOrEmpty(AID.KernelIdentifier) ? null : new List<byte>(Convert.FromBase64String(AID.KernelIdentifier)),
                         string.IsNullOrEmpty(AID.ConfigData) ? null : new List<byte>(Convert.FromBase64String(AID.ConfigData))));
            }

            List<PublicKeyInfo> PublicKeys = new();
            foreach (EMVClessConfigureCommand.PayloadData.KeyDataClass PKs in eMVClessConfigure.Payload.KeyData)
            {
                if (string.IsNullOrEmpty(PKs.Rid))
                {
                    return new EMVClessConfigureCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                       "No RID is supplied.");
                }
                if (PKs.CaPublicKey is null)
                {
                    return new EMVClessConfigureCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                       "No CA Public Key is supplied.");
                }

                /// MISSING PKs.CaPublicKey structure
                PublicKeys.Add(new PublicKeyInfo(new List<byte>(Convert.FromBase64String(PKs.Rid)),
                                                 null));
            }

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessConfigure()");
            var result = await Device.EMVClessConfigure(new EMVClessConfigureRequest(string.IsNullOrEmpty(eMVClessConfigure.Payload.TerminalData) ? null : new List<byte>(Convert.FromBase64String(eMVClessConfigure.Payload.TerminalData)), 
                                                        AIDs, 
                                                        PublicKeys));
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessConfigure() -> {result.CompletionCode}, {result.ErrorCode}");

            return new EMVClessConfigureCompletion.PayloadData(result.CompletionCode,
                                                               result.ErrorDescription,
                                                               result.ErrorCode);
        }
    }
}
