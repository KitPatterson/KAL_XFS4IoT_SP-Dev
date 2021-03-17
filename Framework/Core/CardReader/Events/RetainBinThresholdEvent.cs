/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainBinThresholdEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{

    [DataContract]
    [Event(Name = "CardReader.RetainBinThresholdEvent")]
    public sealed class RetainBinThresholdEvent : Event<RetainBinThresholdEvent.PayloadData>
    {

        public RetainBinThresholdEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public enum StateEnum
            {
                Ok,
                Full,
                High,
            }


            public PayloadData(StateEnum? State = null)
                : base()
            {
                this.State = State;
            }

            /// <summary>
            ///Specifies the state of the ID card unit retain bin as one of the following values: **ok**
            ////The retain bin of the ID card unit was emptied.**full**
            ////The retain bin of the ID card unit is full. **high**
            ////The retain bin of the ID card unit is nearly full.\
            /// </summary>
            [DataMember(Name = "state")] 
            public StateEnum? State { get; private set; }
        }

    }
}
