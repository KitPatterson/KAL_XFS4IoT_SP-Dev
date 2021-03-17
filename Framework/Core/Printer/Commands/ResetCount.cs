/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ResetCount.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{
    //Original name = ResetCount
    [DataContract]
    [Command(Name = "Printer.ResetCount")]
    public sealed class ResetCountCommand : Command<ResetCountCommand.PayloadData>
    {
        public ResetCountCommand(string RequestId, ResetCountCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, int? BinNumber = null)
                : base(Timeout)
            {
                this.BinNumber = BinNumber;
            }

            /// <summary>
            ///Specifies the height of the media in terms of the base vertical resolution.
            /// </summary>
            [DataMember(Name = "binNumber")] 
            public int? BinNumber { get; private set; }

        }
    }
}
