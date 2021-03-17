/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * SetKey.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{
    //Original name = SetKey
    [DataContract]
    [Command(Name = "CardReader.SetKey")]
    public sealed class SetKeyCommand : Command<SetKeyCommand.PayloadData>
    {
        public SetKeyCommand(string RequestId, SetKeyCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
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


            public PayloadData(int Timeout, CompletionCodeEnum? CompletionCode = null, string ErrorDescription = null, string KeyValue = null)
                : base(Timeout)
            {
                this.CompletionCode = CompletionCode;
                this.ErrorDescription = ErrorDescription;
                this.KeyValue = KeyValue;
            }

            /// <summary>
            ///success if the commmand was successful otherwise error
            /// </summary>
            [DataMember(Name = "completionCode")] 
            public CompletionCodeEnum? CompletionCode { get; private set; }
            /// <summary>
            ///If not success, then this is optional vendor dependent information to provide additional information
            /// </summary>
            [DataMember(Name = "errorDescription")] 
            public string ErrorDescription { get; private set; }
            /// <summary>
            ///Contains the Base64 encoded payment containing the CIM86 DES key. This key is supplied by the vendor of the CIM86 module.
            /// </summary>
            [DataMember(Name = "keyValue")] 
            public string KeyValue { get; private set; }

        }
    }
}
