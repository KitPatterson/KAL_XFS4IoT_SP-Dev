/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipIO.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CardReader.Completions
{
    [DataContract]
    [Completion(Name = "CardReader.ChipIO")]
    public sealed class ChipIOCompletion : Completion<ChipIOCompletion.PayloadData>
    {
        public ChipIOCompletion(string RequestId, ChipIOCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, string ChipProtocol = null, string ChipData = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ChipIOCompletion.PayloadData)}");

                this.ChipProtocol = ChipProtocol;
                this.ChipData = ChipData;
            }

            /// <summary>
            ///Identifies the protocol that is used to communicate with the chip. This field contains the same value as the corresponding field in the payload. This field should be ignored in Memory Card dialogs and will contain WFS_IDC_NOTSUPP when returned for any Memory Card dialog.
            /// </summary>
            [DataMember(Name = "chipProtocol")] 
            public string ChipProtocol { get; private set; }
            /// <summary>
            ///The Base64 encoded data received from the chip.
            /// </summary>
            [DataMember(Name = "chipData")] 
            public string ChipData { get; private set; }

        }
    }
}
