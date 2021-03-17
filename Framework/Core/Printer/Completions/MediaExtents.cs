/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaExtents.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Printer.Completions
{
    [DataContract]
    [Completion(Name = "Printer.MediaExtents")]
    public sealed class MediaExtentsCompletion : Completion<MediaExtentsCompletion.PayloadData>
    {
        public MediaExtentsCompletion(string RequestId, MediaExtentsCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, int? SizeX = null, int? SizeY = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(MediaExtentsCompletion.PayloadData)}");

                this.SizeX = SizeX;
                this.SizeY = SizeY;
            }

            /// <summary>
            ///Specifies the width of the media in terms of the base horizontal resolution.
            /// </summary>
            [DataMember(Name = "sizeX")] 
            public int? SizeX { get; private set; }
            /// <summary>
            ///Specifies the height of the media in terms of the base vertical resolution.
            /// </summary>
            [DataMember(Name = "sizeY")] 
            public int? SizeY { get; private set; }

        }
    }
}
