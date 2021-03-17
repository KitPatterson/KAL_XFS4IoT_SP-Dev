/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryField.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Printer.Completions
{
    [DataContract]
    [Completion(Name = "Printer.GetQueryField")]
    public sealed class GetQueryFieldCompletion : Completion<GetQueryFieldCompletion.PayloadData>
    {
        public GetQueryFieldCompletion(string RequestId, GetQueryFieldCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            ///Details of the field(s) requested. For each object, the key is the field name.
            /// </summary>
            public class FieldsClass
            {

                public FieldsClass ()
                {
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, FieldsClass Fields = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetQueryFieldCompletion.PayloadData)}");

                this.Fields = Fields;
            }

            /// <summary>
            ///Details of the field(s) requested. For each object, the key is the field name.
            /// </summary>
            [DataMember(Name = "fields")] 
            public FieldsClass Fields { get; private set; }

        }
    }
}
