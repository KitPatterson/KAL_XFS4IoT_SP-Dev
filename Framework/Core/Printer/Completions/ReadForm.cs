/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ReadForm.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Printer.Completions
{
    [DataContract]
    [Completion(Name = "Printer.ReadForm")]
    public sealed class ReadFormCompletion : Completion<ReadFormCompletion.PayloadData>
    {
        public ReadFormCompletion(string RequestId, ReadFormCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            ///An object containing one or more key/value pairs where the key is a field name and the value is the field value. If the field is an index field, the key must be specified as **fieldname[index]** where index specifies the zero-based element of the index field. The field names and values can contain UNICODE if supported by the service.
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
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ReadFormCompletion.PayloadData)}");

                this.Fields = Fields;
            }

            /// <summary>
            ///An object containing one or more key/value pairs where the key is a field name and the value is the field value. If the field is an index field, the key must be specified as **fieldname[index]** where index specifies the zero-based element of the index field. The field names and values can contain UNICODE if supported by the service.
            /// </summary>
            [DataMember(Name = "fields")] 
            public FieldsClass Fields { get; private set; }

        }
    }
}
