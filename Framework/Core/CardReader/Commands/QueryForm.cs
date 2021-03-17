/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryForm.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{
    //Original name = QueryForm
    [DataContract]
    [Command(Name = "CardReader.QueryForm")]
    public sealed class QueryFormCommand : Command<QueryFormCommand.PayloadData>
    {
        public QueryFormCommand(string RequestId, QueryFormCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, string FormName = null)
                : base(Timeout)
            {
                this.FormName = FormName;
            }

            /// <summary>
            ///The form name for which to retrieve details.
            /// </summary>
            [DataMember(Name = "formName")] 
            public string FormName { get; private set; }

        }
    }
}
