/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * IncompleteRetractEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Dispenser.Events
{

    [DataContract]
    [Event(Name = "Dispenser.IncompleteRetractEvent")]
    public sealed class IncompleteRetractEvent : Event<IncompleteRetractEvent.PayloadData>
    {

        public IncompleteRetractEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            /// <summary>
            /// The values in this structure report the amount and number of each denomination that were successfully moved during the command prior to the failure.
            /// </summary>
            public class ItemNumberListClass
            {
                
                /// <summary>
                /// Array of item number objects.
                /// </summary>
                public class ItemNumberClass 
                {
                    [DataMember(Name = "currencyID")] 
                    public string CurrencyID { get; private set; }
                    [DataMember(Name = "values")] 
                    public double? Values { get; private set; }
                    [DataMember(Name = "release")] 
                    public int? Release { get; private set; }
                    [DataMember(Name = "count")] 
                    public int? Count { get; private set; }
                    [DataMember(Name = "cashunit")] 
                    public string Cashunit { get; private set; }

                    public ItemNumberClass (string CurrencyID, double? Values, int? Release, int? Count, string Cashunit)
                    {
                        this.CurrencyID = CurrencyID;
                        this.Values = Values;
                        this.Release = Release;
                        this.Count = Count;
                        this.Cashunit = Cashunit;
                    }
                }
                [DataMember(Name = "itemNumber")] 
                public ItemNumberClass ItemNumber { get; private set; }

                public ItemNumberListClass (ItemNumberClass ItemNumber)
                {
                    this.ItemNumber = ItemNumber;
                }


            }

            public enum ReasonEnum
            {
                RetractFailure,
                RetractAreaFull,
                ForeignItemsDetected,
                InvalidBunch,
            }


            public PayloadData(object ItemNumberList = null, ReasonEnum? Reason = null)
                : base()
            {
                this.ItemNumberList = ItemNumberList;
                this.Reason = Reason;
            }

            /// <summary>
            /// The values in this structure report the amount and number of each denomination that were successfully moved during the command prior to the failure.
            /// </summary>
            [DataMember(Name = "itemNumberList")] 
            public object ItemNumberList { get; private set; }
            /// <summary>
            /// The reason for not having retracted items. Following values are possible:
            /// 
            /// * ```retractFailure``` - The retract has partially failed for a reason not covered by the other reasons 
            /// listed in this event, for example failing to pick an item to be retracted.
            /// * ```retractAreaFull``` - The specified retract area (see input parameter *retractArea*) has become full 
            /// during the retract operation.
            /// * ```foreignItemsDetected``` - Foreign items have been detected.
            /// * ```invalidBunch``` - An invalid bunch of items has been detected, e.g. it is too large or could not be processed.
            /// </summary>
            [DataMember(Name = "reason")] 
            public ReasonEnum? Reason { get; private set; }
        }

    }
}
