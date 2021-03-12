﻿// (C) KAL ATM Software GmbH, 2021

using System.Runtime.Serialization;

namespace XFS4IoT.Responses
{
    /// <summary>
    /// A message object representing payload on responses
    /// </summary>
    [DataContract]
    public class MessagePayload : MessagePayloadBase
    {
        /// <summary>
        /// success if the commmand was successful otherwise error
        /// </summary>
        public enum CompletionCodeEnum
        {
            Success,
            Canceled,
            DeviceNotready,
            HardwareError,
            InternalError,
            InvalidCommand,
            InvalidRequestID,
            TimeOut,
            UnsupportedCommand,
            InvalidData,
            ConnectionLost,
            UserError,
            UnsupportedData,
            FraudAttempt,
            SequenceError,
            AuthorisationRequired,
        }

        /// <summary>
        /// The original message name, for example "CardReader.Status"
        /// </summary>
        [DataMember(IsRequired = true, Name = "completionCode")]
        public CompletionCodeEnum CompletionCode { get; private set; } = default;

        /// <summary>
        ///  If not success, then this is optional vendor dependent information to provide additional information
        /// </summary>
        [DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; private set; } = string.Empty;


        /// <summary>
        /// Constructor of the common payload for response
        /// </summary>
        /// <param name="CompletionCode"></param>
        /// <param name="ErrorDescription"></param>
        public MessagePayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
        {
            this.CompletionCode = CompletionCode;
            this.ErrorDescription = ErrorDescription;
        }
    }
}
