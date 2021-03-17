/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * MediaDetectedEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{

    [DataContract]
    [Event(Name = "CardReader.MediaDetectedEvent")]
    public sealed class MediaDetectedEvent : Event<MediaDetectedEvent.PayloadData>
    {

        public MediaDetectedEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public enum ResetOutEnum
            {
                Ejected,
                Retained,
                ReadPosition,
                Jammed,
            }


            public PayloadData(ResetOutEnum? ResetOut = null)
                : base()
            {
                this.ResetOut = ResetOut;
            }

            /// <summary>
            ///Specifies the action that was performed on any card found within the IDC as one of the following:**ejected**
            ////The card was ejected.**retained**
            ////The card was retained.**readPosition**
            ////The card is in read position.**jammed**
            ////The card is jammed in the device.
            /// </summary>
            [DataMember(Name = "resetOut")] 
            public ResetOutEnum? ResetOut { get; private set; }
        }

    }
}
