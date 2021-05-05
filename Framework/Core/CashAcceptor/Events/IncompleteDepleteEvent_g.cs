/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * IncompleteDepleteEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CashAcceptor.Events
{

    [DataContract]
    [Event(Name = "CashAcceptor.IncompleteDepleteEvent")]
    public sealed class IncompleteDepleteEvent : Event<IncompleteDepleteEvent.PayloadData>
    {

        public IncompleteDepleteEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            /// <summary>
            /// Note that in this case the values in this structure report the amount and number of each denomination that have actually been moved during the depletion command.
            /// </summary>
            public class DepleteClass
            {
                [DataMember(Name = "numberOfItemsReceived")] 
                public int? NumberOfItemsReceived { get; private set; }
                [DataMember(Name = "numberOfItemsRejected")] 
                public int? NumberOfItemsRejected { get; private set; }
                
                /// <summary>
                /// Array of DepleteSpourceResult structures. In the case where one item type has several releases and these are moved, 
                /// or where items are moved from a multi denomination cash unit to a multi denomination cash unit, each source can move several *noteID* item types. 
                /// 
                /// For example: If one single source was specified with the *depleteSources* input structure, and this source moved two different *noteID* item types, 
                /// then the *depleteSourceResults* array will have two elements. Or if two sources were specified and the 
                /// first source moved two different *noteID* item types and the second source moved three different *noteID* item types, then the *depleteSourceResults* array will have five elements.
                /// </summary>
                public class DepleteSourceResultsClass 
                {
                    [DataMember(Name = "cashunitSource")] 
                    public string CashunitSource { get; private set; }
                    [DataMember(Name = "noteID")] 
                    public int? NoteID { get; private set; }
                    [DataMember(Name = "numberOfItemsRemoved")] 
                    public int? NumberOfItemsRemoved { get; private set; }

                    public DepleteSourceResultsClass (string CashunitSource, int? NoteID, int? NumberOfItemsRemoved)
                    {
                        this.CashunitSource = CashunitSource;
                        this.NoteID = NoteID;
                        this.NumberOfItemsRemoved = NumberOfItemsRemoved;
                    }
                }
                [DataMember(Name = "depleteSourceResults")] 
                public DepleteSourceResultsClass DepleteSourceResults { get; private set; }

                public DepleteClass (int? NumberOfItemsReceived, int? NumberOfItemsRejected, DepleteSourceResultsClass DepleteSourceResults)
                {
                    this.NumberOfItemsReceived = NumberOfItemsReceived;
                    this.NumberOfItemsRejected = NumberOfItemsRejected;
                    this.DepleteSourceResults = DepleteSourceResults;
                }


            }


            public PayloadData(object Deplete = null)
                : base()
            {
                this.Deplete = Deplete;
            }

            /// <summary>
            /// Note that in this case the values in this structure report the amount and number of each denomination that have actually been moved during the depletion command.
            /// </summary>
            [DataMember(Name = "deplete")] 
            public object Deplete { get; private set; }
        }

    }
}
