/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessIssuerUpdateHandler.cs uses automatically generated parts. 
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
    public partial class EMVClessIssuerUpdateHandler
    {
        /// <summary>
        /// EMVClessIssuerUpdateRequest
        /// Provide an information to perform EMV transaction
        /// </summary>
        public sealed class EMVClessIssuerUpdateRequest
        {
            /// <summary>
            /// EMVClessIssuerUpdateRequest
            /// </summary>
            /// <param name="TerminalData"></param>
            /// <param name="Timeout"></param>
            public EMVClessIssuerUpdateRequest(List<byte> TerminalData, int Timeout)
            {
                this.TerminalData = TerminalData;
                this.Timeout = Timeout;
            }

            public List<byte> TerminalData { get; private set; }
            public int Timeout { get; private set; }
        }

        /// <summary>
        /// EMVClessIssuerUpdateResult
        /// Return result of EMV transaction
        /// </summary>
        public sealed class EMVClessIssuerUpdateResult : DeviceResult
        {

            public EMVClessIssuerUpdateResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                              EMVClessIssuerUpdateCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                              string ErrorDescription = null,
                                              EMVClessTransactionDataOutput TransactionResult = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.TransactionResult = TransactionResult;
            }

            public EMVClessIssuerUpdateCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
            public EMVClessTransactionDataOutput TransactionResult { get; private set; }
        }

        private async Task<EMVClessIssuerUpdateCompletion.PayloadData> HandleEMVClessIssuerUpdate(IEMVClessIssuerUpdateEvents events, EMVClessIssuerUpdateCommand eMVClessIssuerUpdate, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessIssuerUpdate()");
            var result = await Device.EMVClessIssuerUpdate(events,
                                                           new EMVClessIssuerUpdateRequest(string.IsNullOrEmpty(eMVClessIssuerUpdate.Payload.Data) ? null : new List<byte>(Convert.FromBase64String(eMVClessIssuerUpdate.Payload.Data)), eMVClessIssuerUpdate.Payload.Timeout),
                                                           cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessIssuerUpdate() -> {result.CompletionCode}, {result.ErrorCode}");

            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success &&
                result.TransactionResult is not null)
            {
                // Build transaction output data
                EMVClessIssuerUpdateCompletion.PayloadData.ChipClass chip = new ((EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.TxOutcomeEnum)result.TransactionResult.TxOutcome,
                                                                                 (EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.CardholderActionEnum)result.TransactionResult.CardholderAction,
                                                                                 result.TransactionResult.DataRead.Count == 0 ? null : Convert.ToBase64String(result.TransactionResult.DataRead.ToArray()),
                                                                                 new EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass((EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.CvmEnum)result.TransactionResult.ClessOutcome.Cvm,
                                                                                                                                                            (EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.AlternateInterfaceEnum)result.TransactionResult.ClessOutcome.AlternateInterface,
                                                                                                                                                            result.TransactionResult.ClessOutcome.Receipt,
                                                                                                                                                            new EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiOutcomeClass(result.TransactionResult.ClessOutcome.UiOutcome.MessageId,
                                                                                                                                                                                                                                                      (EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiOutcomeClass.StatusEnum)result.TransactionResult.ClessOutcome.UiOutcome.Status,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiOutcome.HoldTime,
                                                                                                                                                                                                                                                      (EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiOutcomeClass.ValueQualifierEnum)result.TransactionResult.ClessOutcome.UiOutcome.ValueQualifier,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiOutcome.Value,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiOutcome.CurrencyCode,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiOutcome.LanguagePreferenceData),
                                                                                                                                                            new EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiRestartClass(result.TransactionResult.ClessOutcome.UiRestart.MessageId,
                                                                                                                                                                                                                                                      (EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiRestartClass.StatusEnum)result.TransactionResult.ClessOutcome.UiRestart.Status,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiRestart.HoldTime,
                                                                                                                                                                                                                                                      (EMVClessIssuerUpdateCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiRestartClass.ValueQualifierEnum)result.TransactionResult.ClessOutcome.UiRestart.ValueQualifier,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiRestart.Value,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiRestart.CurrencyCode,
                                                                                                                                                                                                                                                      result.TransactionResult.ClessOutcome.UiRestart.LanguagePreferenceData),
                                                                                                                                                            result.TransactionResult.ClessOutcome.FieldOffHoldTime,
                                                                                                                                                            result.TransactionResult.ClessOutcome.CardRemovalTimeout,
                                                                                                                                                            result.TransactionResult.ClessOutcome.DiscretionaryData.Count == 0 ? null : Convert.ToBase64String(result.TransactionResult.ClessOutcome.DiscretionaryData.ToArray())));
                return new EMVClessIssuerUpdateCompletion.PayloadData(result.CompletionCode, 
                                                                      result.ErrorDescription,
                                                                      result.ErrorCode,
                                                                      chip);
            }
            else
            {
                return new EMVClessIssuerUpdateCompletion.PayloadData(result.CompletionCode,
                                                                      result.ErrorDescription,
                                                                      result.ErrorCode);
            }
        }
    }
}
