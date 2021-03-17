/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ReadTrack.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CardReader.Completions
{
    [DataContract]
    [Completion(Name = "CardReader.ReadTrack")]
    public sealed class ReadTrackCompletion : Completion<ReadTrackCompletion.PayloadData>
    {
        public ReadTrackCompletion(string RequestId, ReadTrackCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, string TrackData = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ReadTrackCompletion.PayloadData)}");

                this.TrackData = TrackData;
            }

            /// <summary>
            ///The data read successfully from the selected tracks (and value of security module if available).
            /// </summary>
            [DataMember(Name = "trackData")] 
            public string TrackData { get; private set; }

        }
    }
}
