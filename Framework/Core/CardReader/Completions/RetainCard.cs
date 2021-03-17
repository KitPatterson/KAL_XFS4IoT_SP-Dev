/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCard.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CardReader.Completions
{
    [DataContract]
    [Completion(Name = "CardReader.RetainCard")]
    public sealed class RetainCardCompletion : Completion<RetainCardCompletion.PayloadData>
    {
        public RetainCardCompletion(string RequestId, RetainCardCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum PositionEnum
            {
                Unknown,
                Present,
                Entering,
            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, int? Count = null, PositionEnum? Position = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(RetainCardCompletion.PayloadData)}");

                this.Count = Count;
                this.Position = Position;
            }

            /// <summary>
            ///Total number of ID cards retained up to and including this operation, since the last ResetCount command was executed.
            /// </summary>
            [DataMember(Name = "count")] 
            public int? Count { get; private set; }
            /// <summary>
            ///Position of card; only relevant if card could not be retained. Possible positions:**unknown**
            ////The position of the card cannot be determined with the device in its current state.**present**
            ////The card is present in the reader.**entering**
            ////The card is in the entering position (shutter).
            /// </summary>
            [DataMember(Name = "position")] 
            public PositionEnum? Position { get; private set; }

        }
    }
}
