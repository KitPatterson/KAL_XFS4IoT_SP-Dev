/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ParkCard.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CardReader.Completions
{
    [DataContract]
    [Completion(Name = "CardReader.ParkCard")]
    public sealed class ParkCardCompletion : Completion<ParkCardCompletion.PayloadData>
    {
        public ParkCardCompletion(string RequestId, ParkCardCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, int Timeout)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ParkCardCompletion.PayloadData)}");

                this.Timeout = Timeout;
            }

            /// <summary>
            ///Timeout in milliseconds for the command to complete. If set to zero, the command will not timeout but can be cancelled.
            /// </summary>
            [DataMember(Name = "timeout")] 
            public int? Timeout { get; private set; }

        }
    }
}
