/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * InvalidTrackDataEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{

    [DataContract]
    [Event(Name = "CardReader.InvalidTrackDataEvent")]
    public sealed class InvalidTrackDataEvent : Event<InvalidTrackDataEvent.PayloadData>
    {

        public InvalidTrackDataEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public enum StatusEnum
            {
                Missing,
                Invalid,
                TooLong,
                TooShort,
            }


            public PayloadData(StatusEnum? Status = null, string Track = null, string Data = null)
                : base()
            {
                this.Status = Status;
                this.Track = Track;
                this.Data = Data;
            }

            /// <summary>
            ///Status of reading the track as one of the following:**missing**
            ////The track is blank.**invalid**
            ////The data contained on the track is invalid.**tooLong**
            ////The data contained on the track is too long.**tooShort**
            ////The data contained on the track is too short.
            /// </summary>
            [DataMember(Name = "status")] 
            public StatusEnum? Status { get; private set; }
            /// <summary>
            ///The keyword of the track on which the error occurred.
            /// </summary>
            [DataMember(Name = "track")] 
            public string Track { get; private set; }
            /// <summary>
            ///Any data from the track that could be read.
            /// </summary>
            [DataMember(Name = "data")] 
            public string Data { get; private set; }
        }

    }
}
