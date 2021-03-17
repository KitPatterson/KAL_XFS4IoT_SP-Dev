/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RetractBinStatusEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{

    [DataContract]
    [Event(Name = "Printer.RetractBinStatusEvent")]
    public sealed class RetractBinStatusEvent : Event<RetractBinStatusEvent.PayloadData>
    {

        public RetractBinStatusEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public enum StateEnum
            {
                Inserted,
                Removed,
            }


            public PayloadData(int? BinNumber = null, StateEnum? State = null)
                : base()
            {
                this.BinNumber = BinNumber;
                this.State = State;
            }

            /// <summary>
            ///Number of the retract bin for which the status has changed.
            /// </summary>
            [DataMember(Name = "binNumber")] 
            public int? BinNumber { get; private set; }
            /// <summary>
            ///Specifies the current state of the retract bin as one of the following values:**inserted**
            ////  The retract bin has been inserted.**removed**
            ////  The retract bin has been removed.
            /// </summary>
            [DataMember(Name = "state")] 
            public StateEnum? State { get; private set; }
        }

    }
}
