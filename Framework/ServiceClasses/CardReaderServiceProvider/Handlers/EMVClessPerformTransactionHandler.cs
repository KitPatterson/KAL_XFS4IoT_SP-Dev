/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessPerformTransactionHandler.cs uses automatically generated parts. 
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
    /// <summary>
    /// Contains the chip returned data formatted in as track 2. This value is set after the contactless
    /// transaction has been completed with mag-stripe mode.
    /// </summary>
    public class EMVClessTransactionDataOutput
    {
        public enum TxOutcomeEnum
        {
            MultipleCards,
            Approve,
            Decline,
            OnlineRequest,
            OnlineRequestCompletionRequired,
            TryAgain,
            TryAnotherInterface,
            EndApplication,
            ConfirmationRequired,
        }

        public TxOutcomeEnum TxOutcome { get; private set; }

        public enum CardholderActionEnum
        {
            None,
            Retap,
            HoldCard,
        }

        public CardholderActionEnum CardholderAction { get; private set; }

        public List<byte> DataRead { get; private set; }

        /// <summary>
        /// The Entry Point Outcome specified in EMVCo Specifications for Contactless Payment Systems (Book A and B).
        /// This can be omitted for contactless chip card readers that do not follow EMVCo Entry Point Specifications.
        /// </summary>
        public class EMVClessOutcome
        {
            public enum CvmEnum
            {
                OnlinePIN,
                ConfirmationCodeVerified,
                Sign,
                NoCVM,
                NoCVMPreference,
            }

            public CvmEnum Cvm { get; private set; }

            public enum AlternateInterfaceEnum
            {
                Contact,
                MagneticStripe,
            }

            public AlternateInterfaceEnum AlternateInterface { get; private set; }

            public bool Receipt { get; private set; }

            /// <summary>
            /// The user interface details required to be displayed to the cardholder after processing the outcome of a
            /// contactless transaction. If no user interface details are required, this will be omitted. Please refer
            /// to EMVCo Contactless Specifications for Payment Systems Book A, Section 6.2 for details of the data
            /// within this object.
            /// </summary>
            public class EMVClessUI
            {
                public int MessageId { get; private set; }
                public enum StatusEnum
                {
                    NotReady,
                    Idle,
                    ReadyToRead,
                    Processing,
                    CardReadOk,
                    ProcessingError,
                }
                public StatusEnum Status { get; private set; }

                public int HoldTime { get; private set; }

                public enum ValueQualifierEnum
                {
                    Amount,
                    Balance,
                    NotApplicable,
                }

                public ValueQualifierEnum ValueQualifier { get; private set; }

                public string Value { get; private set; }

                public string CurrencyCode { get; private set; }

                public string LanguagePreferenceData { get; private set; }

                public EMVClessUI(int MessageId, StatusEnum Status, int HoldTime, ValueQualifierEnum ValueQualifier, string Value, string CurrencyCode, string LanguagePreferenceData)
                {
                    this.MessageId = MessageId;
                    this.Status = Status;
                    this.HoldTime = HoldTime;
                    this.ValueQualifier = ValueQualifier;
                    this.Value = Value;
                    this.CurrencyCode = CurrencyCode;
                    this.LanguagePreferenceData = LanguagePreferenceData;
                }
            }

            public EMVClessUI UiOutcome { get; private set; }

            public EMVClessUI UiRestart { get; private set; }

            public int FieldOffHoldTime { get; private set; }

            public int CardRemovalTimeout { get; private set; }

            public List<byte> DiscretionaryData { get; private set; }

            public EMVClessOutcome(CvmEnum Cvm,
                                   AlternateInterfaceEnum AlternateInterface,
                                   bool Receipt,
                                   EMVClessUI UiOutcome,
                                   EMVClessUI UiRestart,
                                   int FieldOffHoldTime,
                                   int CardRemovalTimeout,
                                   List<byte> DiscretionaryData)
            {
                this.Cvm = Cvm;
                this.AlternateInterface = AlternateInterface;
                this.Receipt = Receipt;
                this.UiOutcome = UiOutcome;
                this.UiRestart = UiRestart;
                this.FieldOffHoldTime = FieldOffHoldTime;
                this.CardRemovalTimeout = CardRemovalTimeout;
                this.DiscretionaryData = DiscretionaryData;
            }
        }

        public EMVClessOutcome ClessOutcome { get; private set; }

        public EMVClessTransactionDataOutput(TxOutcomeEnum TxOutcome,
                                             CardholderActionEnum CardholderAction,
                                             List<byte> DataRead,
                                             EMVClessOutcome ClessOutcome)
        {
            this.TxOutcome = TxOutcome;
            this.CardholderAction = CardholderAction;
            this.DataRead = DataRead;
            this.ClessOutcome = ClessOutcome;
        }
    }

    public partial class EMVClessPerformTransactionHandler
    {
        /// <summary>
        /// EMVClessPerformTransactionRequest
        /// Provide an information to perform EMV transaction
        /// </summary>
        public sealed class EMVClessPerformTransactionRequest
        {
            /// <summary>
            /// EMVClessPerformTransactionRequest
            /// </summary>
            /// <param name="TerminalData"></param>
            /// <param name="Timeout"></param>
            public EMVClessPerformTransactionRequest(List<byte> TerminalData, int Timeout)
            {
                this.TerminalData = TerminalData;
                this.Timeout = Timeout;
            }

            public List<byte> TerminalData { get; private set; }
            public int Timeout { get; private set; }
        }

        /// <summary>
        /// EMVClessPerformTransactionResult
        /// Return result of EMV transaction
        /// </summary>
        public sealed class EMVClessPerformTransactionResult : DeviceResult
        {

            public EMVClessPerformTransactionResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                                    EMVClessPerformTransactionCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                                    string ErrorDescription = null,
                                                    Dictionary<DataSourceTypeEnum, EMVClessTransactionDataOutput> TransactionResults = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.TransactionResults = TransactionResults;
            }

            public EMVClessPerformTransactionCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }

            public enum DataSourceTypeEnum
            {
                Track1,
                Track2,
                Track3,
                Chip,
            }

            public Dictionary<DataSourceTypeEnum, EMVClessTransactionDataOutput> TransactionResults { get; private set; }
        }

        private async Task<EMVClessPerformTransactionCompletion.PayloadData> HandleEMVClessPerformTransaction(IEMVClessPerformTransactionEvents events, EMVClessPerformTransactionCommand eMVClessPerformTransaction, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessPerformTransaction()");
            var result = await Device.EMVClessPerformTransaction(events, 
                                                                 new EMVClessPerformTransactionRequest(string.IsNullOrEmpty(eMVClessPerformTransaction.Payload.Data) ? null : new List<byte>(Convert.FromBase64String(eMVClessPerformTransaction.Payload.Data)), eMVClessPerformTransaction.Payload.Timeout), 
                                                                 cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessPerformTransaction() -> {result.CompletionCode}, {result.ErrorCode}");

            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success &&
                result.TransactionResults is not null &&
                result.TransactionResults.Count > 0)
            {
                EMVClessPerformTransactionCompletion.PayloadData.ChipClass Chip = null;
                EMVClessPerformTransactionCompletion.PayloadData.Track1Class Track1 = null;
                EMVClessPerformTransactionCompletion.PayloadData.Track2Class Track2 = null;
                EMVClessPerformTransactionCompletion.PayloadData.Track3Class Track3 = null;

                if (result.TransactionResults.ContainsKey(EMVClessPerformTransactionResult.DataSourceTypeEnum.Track1) &&
                    result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Track1] is not null)
                {
                    var track1Result = result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Track1];
                    // Build transaction output data
                    Track1 = new((EMVClessPerformTransactionCompletion.PayloadData.Track1Class.TxOutcomeEnum)track1Result.TxOutcome,
                                 (EMVClessPerformTransactionCompletion.PayloadData.Track1Class.CardholderActionEnum)track1Result.CardholderAction,
                                 track1Result.DataRead.Count == 0 ? null : Convert.ToBase64String(track1Result.DataRead.ToArray()),
                                 new EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass((EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.CvmEnum)track1Result.ClessOutcome.Cvm,
                                                                                                                    (EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.AlternateInterfaceEnum)track1Result.ClessOutcome.AlternateInterface,
                                                                                                                    track1Result.ClessOutcome.Receipt,
                                                                                                                    new EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.UiOutcomeClass(track1Result.ClessOutcome.UiOutcome.MessageId,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.UiOutcomeClass.StatusEnum)track1Result.ClessOutcome.UiOutcome.Status,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiOutcome.HoldTime,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.UiOutcomeClass.ValueQualifierEnum)track1Result.ClessOutcome.UiOutcome.ValueQualifier,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiOutcome.Value,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiOutcome.CurrencyCode,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiOutcome.LanguagePreferenceData),
                                                                                                                    new EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.UiRestartClass(track1Result.ClessOutcome.UiRestart.MessageId,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.UiRestartClass.StatusEnum)track1Result.ClessOutcome.UiRestart.Status,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiRestart.HoldTime,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track1Class.ClessOutcomeClass.UiRestartClass.ValueQualifierEnum)track1Result.ClessOutcome.UiRestart.ValueQualifier,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiRestart.Value,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiRestart.CurrencyCode,
                                                                                                                                                                                                                      track1Result.ClessOutcome.UiRestart.LanguagePreferenceData),
                                                                                                        track1Result.ClessOutcome.FieldOffHoldTime,
                                                                                                                    track1Result.ClessOutcome.CardRemovalTimeout,
                                                                                                                    track1Result.ClessOutcome.DiscretionaryData.Count == 0 ? null : Convert.ToBase64String(track1Result.ClessOutcome.DiscretionaryData.ToArray())));
                }

                if (result.TransactionResults.ContainsKey(EMVClessPerformTransactionResult.DataSourceTypeEnum.Track2) &&
                    result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Track2] is not null)
                {
                    var track2Result = result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Track2];
                    // Build transaction output data
                    Track2 = new((EMVClessPerformTransactionCompletion.PayloadData.Track2Class.TxOutcomeEnum)track2Result.TxOutcome,
                                 (EMVClessPerformTransactionCompletion.PayloadData.Track2Class.CardholderActionEnum)track2Result.CardholderAction,
                                 track2Result.DataRead.Count == 0 ? null : Convert.ToBase64String(track2Result.DataRead.ToArray()),
                                 new EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass((EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.CvmEnum)track2Result.ClessOutcome.Cvm,
                                                                                                                    (EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.AlternateInterfaceEnum)track2Result.ClessOutcome.AlternateInterface,
                                                                                                                    track2Result.ClessOutcome.Receipt,
                                                                                                                    new EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.UiOutcomeClass(track2Result.ClessOutcome.UiOutcome.MessageId,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.UiOutcomeClass.StatusEnum)track2Result.ClessOutcome.UiOutcome.Status,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiOutcome.HoldTime,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.UiOutcomeClass.ValueQualifierEnum)track2Result.ClessOutcome.UiOutcome.ValueQualifier,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiOutcome.Value,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiOutcome.CurrencyCode,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiOutcome.LanguagePreferenceData),
                                                                                                                    new EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.UiRestartClass(track2Result.ClessOutcome.UiRestart.MessageId,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.UiRestartClass.StatusEnum)track2Result.ClessOutcome.UiRestart.Status,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiRestart.HoldTime,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track2Class.ClessOutcomeClass.UiRestartClass.ValueQualifierEnum)track2Result.ClessOutcome.UiRestart.ValueQualifier,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiRestart.Value,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiRestart.CurrencyCode,
                                                                                                                                                                                                                      track2Result.ClessOutcome.UiRestart.LanguagePreferenceData),
                                                                                                                    track2Result.ClessOutcome.FieldOffHoldTime,
                                                                                                                    track2Result.ClessOutcome.CardRemovalTimeout,
                                                                                                                    track2Result.ClessOutcome.DiscretionaryData.Count == 0 ? null : Convert.ToBase64String(track2Result.ClessOutcome.DiscretionaryData.ToArray())));
                }

                if (result.TransactionResults.ContainsKey(EMVClessPerformTransactionResult.DataSourceTypeEnum.Track3) &&
                    result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Track3] is not null)
                {
                    var track3Result = result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Track3];
                    // Build transaction output data
                    Track3 = new((EMVClessPerformTransactionCompletion.PayloadData.Track3Class.TxOutcomeEnum)track3Result.TxOutcome,
                                 (EMVClessPerformTransactionCompletion.PayloadData.Track3Class.CardholderActionEnum)track3Result.CardholderAction,
                                 track3Result.DataRead.Count == 0 ? null : Convert.ToBase64String(track3Result.DataRead.ToArray()),
                                 new EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass((EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.CvmEnum)track3Result.ClessOutcome.Cvm,
                                                                                                                    (EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.AlternateInterfaceEnum)track3Result.ClessOutcome.AlternateInterface,
                                                                                                                    track3Result.ClessOutcome.Receipt,
                                                                                                                    new EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.UiOutcomeClass(track3Result.ClessOutcome.UiOutcome.MessageId,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.UiOutcomeClass.StatusEnum)track3Result.ClessOutcome.UiOutcome.Status,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiOutcome.HoldTime,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.UiOutcomeClass.ValueQualifierEnum)track3Result.ClessOutcome.UiOutcome.ValueQualifier,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiOutcome.Value,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiOutcome.CurrencyCode,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiOutcome.LanguagePreferenceData),
                                                                                                                    new EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.UiRestartClass(track3Result.ClessOutcome.UiRestart.MessageId,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.UiRestartClass.StatusEnum)track3Result.ClessOutcome.UiRestart.Status,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiRestart.HoldTime,
                                                                                                                                                                                                                      (EMVClessPerformTransactionCompletion.PayloadData.Track3Class.ClessOutcomeClass.UiRestartClass.ValueQualifierEnum)track3Result.ClessOutcome.UiRestart.ValueQualifier,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiRestart.Value,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiRestart.CurrencyCode,
                                                                                                                                                                                                                      track3Result.ClessOutcome.UiRestart.LanguagePreferenceData),
                                                                                                                    track3Result.ClessOutcome.FieldOffHoldTime,
                                                                                                                    track3Result.ClessOutcome.CardRemovalTimeout,
                                                                                                                    track3Result.ClessOutcome.DiscretionaryData.Count == 0 ? null : Convert.ToBase64String(track3Result.ClessOutcome.DiscretionaryData.ToArray())));
                }

                if (result.TransactionResults.ContainsKey(EMVClessPerformTransactionResult.DataSourceTypeEnum.Chip) &&
                    result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Chip] is not null)
                {
                    var chipResult = result.TransactionResults[EMVClessPerformTransactionResult.DataSourceTypeEnum.Chip];
                    // Build transaction output data
                    Chip = new((EMVClessPerformTransactionCompletion.PayloadData.ChipClass.TxOutcomeEnum)chipResult.TxOutcome,
                               (EMVClessPerformTransactionCompletion.PayloadData.ChipClass.CardholderActionEnum)chipResult.CardholderAction,
                               chipResult.DataRead.Count == 0 ? null : Convert.ToBase64String(chipResult.DataRead.ToArray()),
                               new EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass((EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.CvmEnum)chipResult.ClessOutcome.Cvm,
                                                                                                                (EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.AlternateInterfaceEnum)chipResult.ClessOutcome.AlternateInterface,
                                                                                                                chipResult.ClessOutcome.Receipt,
                                                                                                                new EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiOutcomeClass(chipResult.ClessOutcome.UiOutcome.MessageId,
                                                                                                                                                                                                                (EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiOutcomeClass.StatusEnum)chipResult.ClessOutcome.UiOutcome.Status,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiOutcome.HoldTime,
                                                                                                                                                                                                                (EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiOutcomeClass.ValueQualifierEnum)chipResult.ClessOutcome.UiOutcome.ValueQualifier,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiOutcome.Value,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiOutcome.CurrencyCode,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiOutcome.LanguagePreferenceData),
                                                                                                                new EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiRestartClass(chipResult.ClessOutcome.UiRestart.MessageId,
                                                                                                                                                                                                                (EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiRestartClass.StatusEnum)chipResult.ClessOutcome.UiRestart.Status,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiRestart.HoldTime,
                                                                                                                                                                                                                (EMVClessPerformTransactionCompletion.PayloadData.ChipClass.ClessOutcomeClass.UiRestartClass.ValueQualifierEnum)chipResult.ClessOutcome.UiRestart.ValueQualifier,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiRestart.Value,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiRestart.CurrencyCode,
                                                                                                                                                                                                                chipResult.ClessOutcome.UiRestart.LanguagePreferenceData),
                                                                                                                chipResult.ClessOutcome.FieldOffHoldTime,
                                                                                                                chipResult.ClessOutcome.CardRemovalTimeout,
                                                                                                                chipResult.ClessOutcome.DiscretionaryData.Count == 0 ? null : Convert.ToBase64String(chipResult.ClessOutcome.DiscretionaryData.ToArray())));
                }

                return new EMVClessPerformTransactionCompletion.PayloadData(result.CompletionCode,
                                                                            result.ErrorDescription,
                                                                            result.ErrorCode,
                                                                            Track1,
                                                                            Track2,
                                                                            Track3,
                                                                            Chip);
            }
            else
            {
                return new EMVClessPerformTransactionCompletion.PayloadData(result.CompletionCode,
                                                                            result.ErrorDescription,
                                                                            result.ErrorCode);
            }
        }
    }
}
