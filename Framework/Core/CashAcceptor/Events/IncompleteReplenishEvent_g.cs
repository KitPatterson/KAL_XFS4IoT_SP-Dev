/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * IncompleteReplenishEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CashAcceptor.Events
{

    [DataContract]
    [Event(Name = "CashAcceptor.IncompleteReplenishEvent")]
    public sealed class IncompleteReplenishEvent : Event<IncompleteReplenishEvent.PayloadData>
    {

        public IncompleteReplenishEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            /// <summary>
            /// Note that in this case the values in this structure report the amount and number of each denomination that have actually been moved during the replenishment command.
            /// </summary>
            public class ReplenishClass
            {
                [DataMember(Name = "numberOfItemsRemoved")] 
                public int? NumberOfItemsRemoved { get; private set; }
                [DataMember(Name = "numberOfItemsRejected")] 
                public int? NumberOfItemsRejected { get; private set; }
                
                /// <summary>
                /// Array of replenishTargetResult structures. In the case where one note type has several releases and these are moved, 
                /// or where items are moved from a multi denomination cash unit to a multi denomination cash unit, each target can receive several *noteID* note types. 
                /// For example: If one single target was specified with the *replenishTargets* input structure, and this target received two different *noteID* note types, 
                /// then the *replenishTargetResults* array will have two elements. Or if two targets were specified and the first 
                /// target received two different *noteID* note types and the second target received three different *noteID* note types, then the *replenishTargetResults* array will have five elements.
                /// </summary>
                public class ReplenishTargetResultsClass 
                {
                    [DataMember(Name = "cashunitTarget")] 
                    public string CashunitTarget { get; private set; }
                    [DataMember(Name = "noteID")] 
                    public int? NoteID { get; private set; }
                    [DataMember(Name = "numberOfItemsReceived")] 
                    public int? NumberOfItemsReceived { get; private set; }

                    public ReplenishTargetResultsClass (string CashunitTarget, int? NoteID, int? NumberOfItemsReceived)
                    {
                        this.CashunitTarget = CashunitTarget;
                        this.NoteID = NoteID;
                        this.NumberOfItemsReceived = NumberOfItemsReceived;
                    }
                }
                [DataMember(Name = "replenishTargetResults")] 
                public ReplenishTargetResultsClass ReplenishTargetResults { get; private set; }

                public ReplenishClass (int? NumberOfItemsRemoved, int? NumberOfItemsRejected, ReplenishTargetResultsClass ReplenishTargetResults)
                {
                    this.NumberOfItemsRemoved = NumberOfItemsRemoved;
                    this.NumberOfItemsRejected = NumberOfItemsRejected;
                    this.ReplenishTargetResults = ReplenishTargetResults;
                }


            }


            public PayloadData(object Replenish = null)
                : base()
            {
                this.Replenish = Replenish;
            }

            /// <summary>
            /// Note that in this case the values in this structure report the amount and number of each denomination that have actually been moved during the replenishment command.
            /// </summary>
            [DataMember(Name = "replenish")] 
            public object Replenish { get; private set; }
        }

    }
}
