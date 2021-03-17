/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipPower.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CardReader.Completions
{
    [DataContract]
    [Completion(Name = "CardReader.ChipPower")]
    public sealed class ChipPowerCompletion : Completion<ChipPowerCompletion.PayloadData>
    {
        public ChipPowerCompletion(string RequestId, ChipPowerCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, string ChipData = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ChipPowerCompletion.PayloadData)}");

                this.ChipData = ChipData;
            }

            /// <summary>
            ///The Base64 encoded data received from the chip.
            /// </summary>
            [DataMember(Name = "chipData")] 
            public string ChipData { get; private set; }

        }
    }
}
