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
    public class EMVContactlessTransactionDataOutput
    {
        public enum TransactionOutcomeEnum
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

        /// <summary>
        /// Specifies the contactless transaction outcome
        /// </summary>
        public TransactionOutcomeEnum TransactionOutcome { get; private set; }

        public enum CardholderActionEnum
        {
            None,
            Retap,
            HoldCard,
        }

        /// <summary>
        ///  Specifies the cardholder action
        /// </summary>
        public CardholderActionEnum CardholderAction { get; private set; }

        /// <summary>
        /// The data read from the chip after a contactless transaction has been completed successfully.
        /// If the data source is chip, the BER-TLV formatted data contains cryptogram tag (9F26) after a contactless chip transaction has been completed successfully.
        /// if the data source is track1, track2 or track3, the data read from the chip, i.e the value returned by the card reader device and no cryptogram tag (9F26). 
        /// </summary>
        public List<byte> DataRead { get; private set; }

        /// <summary>
        /// The Entry Point Outcome specified in EMVCo Specifications for Contactless Payment Systems (Book A and B).
        /// This can be omitted for contactless chip card readers that do not follow EMVCo Entry Point Specifications.
        /// </summary>
        public class EMVContactlessOutcome
        {
            public enum CvmEnum
            {
                OnlinePIN,
                ConfirmationCodeVerified,
                Sign,
                NoCVM,
                NoCVMPreference,
            }

            /// <summary>
            ///  Specifies the cardholder verification method (CVM) to be performed
            /// </summary>
            public CvmEnum Cvm { get; private set; }

            public enum AlternateInterfaceEnum
            {
                Contact,
                MagneticStripe,
            }

            /// <summary>
            /// If the TransactionOutcome property is not TryAnotherInterface, this should be ignored.
            /// If the TransactionOutcome property is TryAnotherInterface, this specifies the alternative interface to be used to complete a transaction
            /// </summary>
            public AlternateInterfaceEnum AlternateInterface { get; private set; }

            public bool Receipt { get; private set; }

            /// <summary>
            /// The user interface details required to be displayed to the cardholder after processing the outcome of a
            /// contactless transaction. If no user interface details are required, this will be omitted. Please refer
            /// to EMVCo Contactless Specifications for Payment Systems Book A, Section 6.2 for details of the data
            /// within this object.
            /// </summary>
            public class EMVContactlessUI
            {
                /// <summary>
                /// Represents the EMVCo defined message identifier that indicates the text string to be displayed, e.g., 0x1B
                /// is the “Authorising Please Wait” message(see EMVCo Contactless Specifications for Payment Systems Book A, Section 9.4).
                /// </summary>
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
                /// <summary>
                /// Represents the EMVCo defined transaction status value to be indicated through the Beep/LEDs
                /// </summary>
                public StatusEnum Status { get; private set; }

                /// <summary>
                /// Represents the hold time in units of 100 milliseconds for which the application should display the message
                /// before processing the next user interface data.
                /// </summary>
                public int HoldTime { get; private set; }

                public enum ValueQualifierEnum
                {
                    Amount,
                    Balance,
                    NotApplicable,
                }

                /// <summary>
                /// This data is defined by EMVCo as either “Amount”, “Balance”, or "NotApplicable"
                /// </summary>
                public ValueQualifierEnum ValueQualifier { get; private set; }

                /// <summary>
                /// Represents the value of the amount or balance as specified by ValueQualifier to be displayed where appropriate.
                /// </summary>
                public string Value { get; private set; }

                /// <summary>
                /// Represents the numeric value of currency code as per ISO 4217.
                /// </summary>
                public string CurrencyCode { get; private set; }

                /// <summary>
                /// Represents the language preference (EMV Tag ‘5F2D’) if returned by the card. The application should use this
                /// data to display all messages in the specified language until the transaction concludes.
                /// </summary>
                public string LanguagePreferenceData { get; private set; }

                public EMVContactlessUI(int MessageId,
                                        StatusEnum Status,
                                        int HoldTime,
                                        ValueQualifierEnum ValueQualifier,
                                        string Value,
                                        string CurrencyCode,
                                        string LanguagePreferenceData)
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

            /// <summary>
            /// The user interface details required to be displayed to the cardholder after processing the outcome of a
            /// contactless transaction.If no user interface details are required, this will be omitted.Please refer
            /// to EMVCo Contactless Specifications for Payment Systems Book A, Section 6.2 for details of the data within this object.
            /// </summary>
            public EMVContactlessUI UiOutcome { get; private set; }

            /// <summary>
            /// The user interface details required to be displayed to the cardholder when a transaction needs to be
            /// completed with a re-tap.If no user interface details are required, this will be omitted.
            /// </summary>
            public EMVContactlessUI UiRestart { get; private set; }

            /// <summary>
            /// The application should wait for this specific hold time in units of 100 milliseconds, before re-enabling
            /// the contactless card reader by issuing either the CardReader.EMVClessPerformTransaction or CardReader.EMVClessIssuerUpdate command depending on the value of
            /// For intelligent contactless card readers, the completion of this command ensures that the contactless chip card reader field is automatically turned off, so there is no need for the application to disable the field.
            /// </summary>
            public int FieldOffHoldTime { get; private set; }

            /// <summary>
            /// Specifies a timeout value in units of 100 milliseconds for prompting the user to remove the card.
            /// </summary>
            public int CardRemovalTimeout { get; private set; }

            /// <summary>
            /// The payment system's specific discretionary data read from the chip, in a BER-TLV format, 
            /// after a contactless transaction has been completed.If discretionary data is not present, this will be omitted.
            /// </summary>
            public List<byte> DiscretionaryData { get; private set; }

            public EMVContactlessOutcome(CvmEnum Cvm,
                                         AlternateInterfaceEnum AlternateInterface,
                                         bool Receipt,
                                         EMVContactlessUI UiOutcome,
                                         EMVContactlessUI UiRestart,
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

        public EMVContactlessOutcome ClessOutcome { get; private set; }

        public EMVContactlessTransactionDataOutput(TransactionOutcomeEnum TransactionOutcome,
                                                   CardholderActionEnum CardholderAction,
                                                   List<byte> DataRead,
                                                   EMVContactlessOutcome ClessOutcome)
        {
            this.TransactionOutcome = TransactionOutcome;
            this.CardholderAction = CardholderAction;
            this.DataRead = DataRead;
            this.ClessOutcome = ClessOutcome;
        }
    }

    /// <summary>
    /// EMVContactlessPerformTransactionRequest
    /// Provide an information to perform EMV transaction
    /// </summary>
    public sealed class EMVContactlessPerformTransactionRequest
    {
        /// <summary>
        /// EMVClessPerformTransactionRequest
        /// </summary>
        /// <param name="TerminalData"></param>
        /// <param name="Timeout"></param>
        public EMVContactlessPerformTransactionRequest(List<byte> TerminalData, int Timeout)
        {
            this.TerminalData = TerminalData;
            this.Timeout = Timeout;
        }

        public List<byte> TerminalData { get; private set; }
        public int Timeout { get; private set; }
    }

    /// <summary>
    /// EMVContactlessPerformTransactionResult
    /// Return result of EMV transaction
    /// </summary>
    public sealed class EMVContactlessPerformTransactionResult : DeviceResult
    {

        public EMVContactlessPerformTransactionResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                                      EMVClessPerformTransactionCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                                      string ErrorDescription = null,
                                                      Dictionary<DataSourceTypeEnum, EMVContactlessTransactionDataOutput> TransactionResults = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.TransactionResults = TransactionResults;
        }

        public EMVContactlessPerformTransactionResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                                      Dictionary<DataSourceTypeEnum, EMVContactlessTransactionDataOutput> TransactionResults = null)
           : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.TransactionResults = TransactionResults;
        }

        public EMVClessPerformTransactionCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }

        /// <summary>
        /// Transaction type completed in a mag-stripe mode or an EMV mode
        /// </summary>
        public enum DataSourceTypeEnum
        {
            Track1,
            Track2,
            Track3,
            Chip,
        }

        public Dictionary<DataSourceTypeEnum, EMVContactlessTransactionDataOutput> TransactionResults { get; private set; }
    }

    public partial class EMVClessPerformTransactionHandler
    {
        private async Task<EMVClessPerformTransactionCompletion.PayloadData> HandleEMVClessPerformTransaction(IEMVClessPerformTransactionEvents events, EMVClessPerformTransactionCommand eMVClessPerformTransaction, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVContactlessPerformTransactionAsync()");
            var result = await Device.EMVContactlessPerformTransactionAsync(events,
                                                                            new EMVContactlessPerformTransactionRequest(string.IsNullOrEmpty(eMVClessPerformTransaction.Payload.Data) ? null : new List<byte>(Convert.FromBase64String(eMVClessPerformTransaction.Payload.Data)), eMVClessPerformTransaction.Payload.Timeout),
                                                                            cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVContactlessPerformTransactionAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success &&
                result.TransactionResults is not null &&
                result.TransactionResults.Count > 0)
            {
                EMVClessPerformTransactionCompletion.PayloadData.ChipClass Chip = null;
                EMVClessPerformTransactionCompletion.PayloadData.Track1Class Track1 = null;
                EMVClessPerformTransactionCompletion.PayloadData.Track2Class Track2 = null;
                EMVClessPerformTransactionCompletion.PayloadData.Track3Class Track3 = null;

                if (result.TransactionResults.ContainsKey(EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track1) &&
                    result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track1] is not null)
                {
                    var track1Result = result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track1];
                    // Build transaction output data
                    Track1 = new((EMVClessPerformTransactionCompletion.PayloadData.Track1Class.TxOutcomeEnum)track1Result.TransactionOutcome,
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

                if (result.TransactionResults.ContainsKey(EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track2) &&
                    result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track2] is not null)
                {
                    var track2Result = result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track2];
                    // Build transaction output data
                    Track2 = new((EMVClessPerformTransactionCompletion.PayloadData.Track2Class.TxOutcomeEnum)track2Result.TransactionOutcome,
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

                if (result.TransactionResults.ContainsKey(EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track3) &&
                    result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track3] is not null)
                {
                    var track3Result = result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Track3];
                    // Build transaction output data
                    Track3 = new((EMVClessPerformTransactionCompletion.PayloadData.Track3Class.TxOutcomeEnum)track3Result.TransactionOutcome,
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

                if (result.TransactionResults.ContainsKey(EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Chip) &&
                    result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Chip] is not null)
                {
                    var chipResult = result.TransactionResults[EMVContactlessPerformTransactionResult.DataSourceTypeEnum.Chip];
                    // Build transaction output data
                    Chip = new((EMVClessPerformTransactionCompletion.PayloadData.ChipClass.TxOutcomeEnum)chipResult.TransactionOutcome,
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
