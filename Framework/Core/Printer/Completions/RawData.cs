/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RawData.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Printer.Completions
{
    [DataContract]
    [Completion(Name = "Printer.RawData")]
    public sealed class RawDataCompletion : Completion<RawDataCompletion.PayloadData>
    {
        public RawDataCompletion(string RequestId, RawDataCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, string Data = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(RawDataCompletion.PayloadData)}");

                this.Data = Data;
            }

            /// <summary>
            ///BASE64 encoded device dependent data received from the device.
            /// </summary>
            [DataMember(Name = "data")] 
            public string Data { get; private set; }

        }
    }
}
