/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetQueryForm.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{
    //Original name = GetQueryForm
    [DataContract]
    [Command(Name = "TextTerminal.GetQueryForm")]
    public sealed class GetQueryFormCommand : Command<GetQueryFormCommand.PayloadData>
    {
        public GetQueryFormCommand(string RequestId, GetQueryFormCommand.PayloadData Payload)
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
            ///Contains the form name on which to retrieve details.
            /// </summary>
            [DataMember(Name = "formName")] 
            public string FormName { get; private set; }

        }
    }
}
