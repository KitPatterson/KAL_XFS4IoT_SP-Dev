/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetFormList.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.TextTerminal.Completions
{
    [DataContract]
    [Completion(Name = "TextTerminal.GetFormList")]
    public sealed class GetFormListCompletion : Completion<GetFormListCompletion.PayloadData>
    {
        public GetFormListCompletion(string RequestId, GetFormListCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, List<string> FormList = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetFormListCompletion.PayloadData)}");

                this.FormList = FormList;
            }

            /// <summary>
            ///Array of the form names.
            /// </summary>
            [DataMember(Name = "formList")] 
            public List<string> FormList{ get; private set; }

        }
    }
}
