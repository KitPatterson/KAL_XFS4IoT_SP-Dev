/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * GetPresentStatus_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashAcceptor.Completions
{
    [DataContract]
    [Completion(Name = "CashAcceptor.GetPresentStatus")]
    public sealed class GetPresentStatusCompletion : Completion<GetPresentStatusCompletion.PayloadData>
    {
        public GetPresentStatusCompletion(string RequestId, GetPresentStatusCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum PositionEnum
            {
                Left,
                Right,
                Center,
                Top,
                Bottom,
                Front,
                Rear,
            }

            public enum PresentStateEnum
            {
                Presented,
                NotPresented,
                Unknown,
            }

            public enum AdditionalBunchesEnum
            {
                None,
                OneMore,
                Unknown,
            }

            /// <summary>
            /// Array holding a list of banknote numbers which have been moved to the output position as a result of the most recent operation.
            /// </summary>
            public class ReturnedItemsClass
            {
                
                /// <summary>
                /// Array of banknote numbers the cash unit contains.
                /// </summary>
                public class NoteNumberClass 
                {
                    [DataMember(Name = "noteID")] 
                    public int? NoteID { get; private set; }
                    [DataMember(Name = "count")] 
                    public int? Count { get; private set; }

                    public NoteNumberClass (int? NoteID, int? Count)
                    {
                        this.NoteID = NoteID;
                        this.Count = Count;
                    }
                }
                [DataMember(Name = "noteNumber")] 
                public NoteNumberClass NoteNumber { get; private set; }

                public ReturnedItemsClass (NoteNumberClass NoteNumber)
                {
                    this.NoteNumber = NoteNumber;
                }


            }

            /// <summary>
            /// Array of cumulative banknote numbers which have been moved to the output position. 
            /// This value will be reset when the CashInStart, CashIn, CashInEnd, Retract, Reset or CashInRollback command is executed.
            /// </summary>
            public class TotalReturnedItemsClass
            {
                
                /// <summary>
                /// Array of banknote numbers the cash unit contains.
                /// </summary>
                public class NoteNumberClass 
                {
                    [DataMember(Name = "noteID")] 
                    public int? NoteID { get; private set; }
                    [DataMember(Name = "count")] 
                    public int? Count { get; private set; }

                    public NoteNumberClass (int? NoteID, int? Count)
                    {
                        this.NoteID = NoteID;
                        this.Count = Count;
                    }
                }
                [DataMember(Name = "noteNumber")] 
                public NoteNumberClass NoteNumber { get; private set; }

                public TotalReturnedItemsClass (NoteNumberClass NoteNumber)
                {
                    this.NoteNumber = NoteNumber;
                }


            }

            /// <summary>
            /// Array of banknote numbers on the intermediate stacker or transport which have not been yet moved to the output position.
            /// </summary>
            public class RemainingItemsClass
            {
                
                /// <summary>
                /// Array of banknote numbers the cash unit contains.
                /// </summary>
                public class NoteNumberClass 
                {
                    [DataMember(Name = "noteID")] 
                    public int? NoteID { get; private set; }
                    [DataMember(Name = "count")] 
                    public int? Count { get; private set; }

                    public NoteNumberClass (int? NoteID, int? Count)
                    {
                        this.NoteID = NoteID;
                        this.Count = Count;
                    }
                }
                [DataMember(Name = "noteNumber")] 
                public NoteNumberClass NoteNumber { get; private set; }

                public RemainingItemsClass (NoteNumberClass NoteNumber)
                {
                    this.NoteNumber = NoteNumber;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, PositionEnum? Position = null, PresentStateEnum? PresentState = null, AdditionalBunchesEnum? AdditionalBunches = null, int? BunchesRemaining = null, ReturnedItemsClass ReturnedItems = null, TotalReturnedItemsClass TotalReturnedItems = null, RemainingItemsClass RemainingItems = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.Position = Position;
                this.PresentState = PresentState;
                this.AdditionalBunches = AdditionalBunches;
                this.BunchesRemaining = BunchesRemaining;
                this.ReturnedItems = ReturnedItems;
                this.TotalReturnedItems = TotalReturnedItems;
                this.RemainingItems = RemainingItems;
            }

            /// <summary>
            /// Specifies the output position. Following values are possible:
            /// 
            /// \"left\": Left output position.
            /// 
            /// \"right\": Right output position.
            /// 
            /// \"center\": Center output position.
            /// 
            /// \"top\": Top output position.
            /// 
            /// \"bottom\": Bottom output position.
            /// 
            /// \"front\": Front output position.
            /// 
            /// \"rear\": Rear output position.
            /// </summary>
            [DataMember(Name = "position")] 
            public PositionEnum? Position { get; private set; }
            /// <summary>
            /// Supplies the status of the items that were to be presented by the most recent attempt to present or return items to the customer. Following values are possible:
            /// 
            /// \"presented\": The items were presented. This status is set as soon as the customer has access to the items.
            /// 
            /// \"notPresented\": The customer has not had access to the items.
            /// 
            /// \"unknown\": It is not known if the customer had access to the items.
            /// </summary>
            [DataMember(Name = "presentState")] 
            public PresentStateEnum? PresentState { get; private set; }
            /// <summary>
            /// Specifies whether or not additional bunches of items are remaining to be presented as a result of the most recent operation. Following values are possible:
            /// 
            /// \"none\": No additional bunches remain.
            /// 
            /// \"oneMore\": At least one additional bunch remains.
            /// 
            /// \"unknown\": It is unknown whether additional bunches remain.
            /// </summary>
            [DataMember(Name = "additionalBunches")] 
            public AdditionalBunchesEnum? AdditionalBunches { get; private set; }
            /// <summary>
            /// If *additionalBunches* is \"oneMore\", specifies the number of additional bunches of items remaining to be presented as a result of the current operation. 
            /// If the number of additional bunches is at least one, but the precise number is unknown, *bunchesRemaining* will be 255 (TODO: Check if there is a better way to represent this state). 
            /// For any other value of *additionalBunches*, *bunchesRemaining* will be zero.
            /// </summary>
            [DataMember(Name = "bunchesRemaining")] 
            public int? BunchesRemaining { get; private set; }
            /// <summary>
            /// Array holding a list of banknote numbers which have been moved to the output position as a result of the most recent operation.
            /// </summary>
            [DataMember(Name = "returnedItems")] 
            public ReturnedItemsClass ReturnedItems { get; private set; }
            /// <summary>
            /// Array of cumulative banknote numbers which have been moved to the output position. 
            /// This value will be reset when the CashInStart, CashIn, CashInEnd, Retract, Reset or CashInRollback command is executed.
            /// </summary>
            [DataMember(Name = "totalReturnedItems")] 
            public TotalReturnedItemsClass TotalReturnedItems { get; private set; }
            /// <summary>
            /// Array of banknote numbers on the intermediate stacker or transport which have not been yet moved to the output position.
            /// </summary>
            [DataMember(Name = "remainingItems")] 
            public RemainingItemsClass RemainingItems { get; private set; }

        }
    }
}
