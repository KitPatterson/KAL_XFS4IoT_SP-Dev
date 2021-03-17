/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * PowerSaveChangeEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Common.Events
{

    [DataContract]
    [Event(Name = "Common.PowerSaveChangeEvent")]
    public sealed class PowerSaveChangeEvent : Event<PowerSaveChangeEvent.PayloadData>
    {

        public PowerSaveChangeEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {


            public PayloadData(int? PowerSaveRecoveryTime = null)
                : base()
            {
                this.PowerSaveRecoveryTime = PowerSaveRecoveryTime;
            }

            /// <summary>
            ///Specifies the actual number of seconds required by the device to resume its normal operational state. This value is zero if the device exited the power saving mode
            /// </summary>
            [DataMember(Name = "powerSaveRecoveryTime")] 
            public int? PowerSaveRecoveryTime { get; private set; }
        }

    }
}
