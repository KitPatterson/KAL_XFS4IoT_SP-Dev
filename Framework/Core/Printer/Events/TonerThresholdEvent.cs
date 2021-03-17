/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * TonerThresholdEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{

    [DataContract]
    [Event(Name = "Printer.TonerThresholdEvent")]
    public sealed class TonerThresholdEvent : Event<TonerThresholdEvent.PayloadData>
    {

        public TonerThresholdEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public enum StateEnum
            {
                Full,
                Low,
                Out,
            }


            public PayloadData(StateEnum? State = null)
                : base()
            {
                this.State = State;
            }

            /// <summary>
            ///Specifies the current state of the toner (or ink) as one of the following:**full**
            ////  The toner (or ink) in the printer is in a good state.**low**
            ////  The toner (or ink) in the printer is low.**out**
            ////  The toner (or ink) in the printer is out.
            /// </summary>
            [DataMember(Name = "state")] 
            public StateEnum? State { get; private set; }
        }

    }
}
