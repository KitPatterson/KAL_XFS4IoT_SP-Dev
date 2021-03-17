/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ControlPassbook.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{
    //Original name = ControlPassbook
    [DataContract]
    [Command(Name = "Printer.ControlPassbook")]
    public sealed class ControlPassbookCommand : Command<ControlPassbookCommand.PayloadData>
    {
        public ControlPassbookCommand(string RequestId, ControlPassbookCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ActionEnum
            {
                Forward,
                Backward,
                CloseForward,
                CloseBackward,
            }


            public PayloadData(int Timeout, ActionEnum? Action = null, int? Count = null)
                : base(Timeout)
            {
                this.Action = Action;
                this.Count = Count;
            }

            /// <summary>
            ///Specifies the direction of the page turn as one of the following values:**forward**
            ////  Turns forward the pages of the passbook.**backward**
            ////  Turns backward the pages of the passbook.**closeForward**
            ////  Close the passbook forward.**closeBackward**
            ////  Close the passbook backward.
            /// </summary>
            [DataMember(Name = "action")] 
            public ActionEnum? Action { get; private set; }
            /// <summary>
            ///Specifies the number of pages to be turned. In the case where *action* is closeForward or closeBackward, this field will be ignored.
            /// </summary>
            [DataMember(Name = "count")] 
            public int? Count { get; private set; }

        }
    }
}
