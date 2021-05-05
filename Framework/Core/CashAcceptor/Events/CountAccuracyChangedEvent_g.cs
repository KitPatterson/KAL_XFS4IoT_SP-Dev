/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * CountAccuracyChangedEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CashAcceptor.Events
{

    [DataContract]
    [Event(Name = "CashAcceptor.CountAccuracyChangedEvent")]
    public sealed class CountAccuracyChangedEvent : UnsolicitedEvent<CountAccuracyChangedEvent.PayloadData>
    {

        public CountAccuracyChangedEvent(PayloadData Payload)
            : base(Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            /// <summary>
            /// Object containing cashUnitCountStatus objects. cashUnitCountStatus objects use the same names 
            /// as used in [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo).
            /// </summary>
            public class CashUnitCountStatusClass
            {
                

                public class AdditionalPropertiesClass 
                {
                    [DataMember(Name = "physicalPositionName")] 
                    public string PhysicalPositionName { get; private set; }
                    public enum AccuracyEnum
                    {
                        NotSupported,
                        Accurate,
                        AccurateSet,
                        Inaccurate,
                        Unknown,
                    }
                    [DataMember(Name = "accuracy")] 
                    public AccuracyEnum? Accuracy { get; private set; }

                    public AdditionalPropertiesClass (string PhysicalPositionName, AccuracyEnum? Accuracy)
                    {
                        this.PhysicalPositionName = PhysicalPositionName;
                        this.Accuracy = Accuracy;
                    }
                }
                [DataMember(Name = "additionalProperties")] 
                public AdditionalPropertiesClass AdditionalProperties { get; private set; }

                public CashUnitCountStatusClass (AdditionalPropertiesClass AdditionalProperties)
                {
                    this.AdditionalProperties = AdditionalProperties;
                }


            }


            public PayloadData(object CashUnitCountStatus = null)
                : base()
            {
                this.CashUnitCountStatus = CashUnitCountStatus;
            }

            /// <summary>
            /// Object containing cashUnitCountStatus objects. cashUnitCountStatus objects use the same names 
            /// as used in [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo).
            /// </summary>
            [DataMember(Name = "cashUnitCountStatus")] 
            public object CashUnitCountStatus { get; private set; }
        }

    }
}
