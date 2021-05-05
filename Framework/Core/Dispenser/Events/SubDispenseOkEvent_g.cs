/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * SubDispenseOkEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Dispenser.Events
{

    [DataContract]
    [Event(Name = "Dispenser.SubDispenseOkEvent")]
    public sealed class SubDispenseOkEvent : Event<SubDispenseOkEvent.PayloadData>
    {

        public SubDispenseOkEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            /// <summary>
            /// \"List of currency and amount combinations for denomination. There will be one entry for each currency
            /// in the denomination. The property name is the currency name in ISO format (e.g. \"EUR\").
            /// </summary>
            public class CurrenciesClass
            {
                [DataMember(Name = "additionalProperties")] 
                public double? AdditionalProperties { get; private set; }

                public CurrenciesClass (double? AdditionalProperties)
                {
                    this.AdditionalProperties = AdditionalProperties;
                }


            }

            /// <summary>
            /// This list specifies the number of items to take from the cash units. 
            /// Each entry uses a cashunit object name as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command. The value of the entry is the 
            /// number of items to take from that unit.
            /// If the application does not wish to specify a denomination, it should omit the values property.
            /// </summary>
            public class ValuesClass
            {
                [DataMember(Name = "additionalProperties")] 
                public int? AdditionalProperties { get; private set; }

                public ValuesClass (int? AdditionalProperties)
                {
                    this.AdditionalProperties = AdditionalProperties;
                }


            }


            public PayloadData(object Currencies = null, object Values = null, int? CashBox = null)
                : base()
            {
                this.Currencies = Currencies;
                this.Values = Values;
                this.CashBox = CashBox;
            }

            /// <summary>
            /// \"List of currency and amount combinations for denomination. There will be one entry for each currency
            /// in the denomination. The property name is the currency name in ISO format (e.g. \"EUR\").
            /// </summary>
            [DataMember(Name = "currencies")] 
            public object Currencies { get; private set; }
            /// <summary>
            /// This list specifies the number of items to take from the cash units. 
            /// Each entry uses a cashunit object name as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command. The value of the entry is the 
            /// number of items to take from that unit.
            /// If the application does not wish to specify a denomination, it should omit the values property.
            /// </summary>
            [DataMember(Name = "values")] 
            public object Values { get; private set; }
            /// <summary>
            /// Only applies to Teller Dispensers. Amount to be paid from the teller’s cash box.
            /// </summary>
            [DataMember(Name = "cashBox")] 
            public int? CashBox { get; private set; }
        }

    }
}
