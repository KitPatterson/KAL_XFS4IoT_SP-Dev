/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaPresentedEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{

    [DataContract]
    [Event(Name = "Printer.MediaPresentedEvent")]
    public sealed class MediaPresentedEvent : Event<MediaPresentedEvent.PayloadData>
    {

        public MediaPresentedEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {


            public PayloadData(int? WadIndex = null, int? TotalWads = null)
                : base()
            {
                this.WadIndex = WadIndex;
                this.TotalWads = TotalWads;
            }

            /// <summary>
            ///Specifies the index (starting from one) of the presented wad, where a Wad is a bunch of one or more pages presented as a bunch.
            /// </summary>
            [DataMember(Name = "wadIndex")] 
            public int? WadIndex { get; private set; }
            /// <summary>
            ///Specifies the total number of wads in the print job, zero if the total number of wads is not known.
            /// </summary>
            [DataMember(Name = "totalWads")] 
            public int? TotalWads { get; private set; }
        }

    }
}
