/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * Denominate_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Dispenser.Commands
{
    //Original name = Denominate
    [DataContract]
    [Command(Name = "Dispenser.Denominate")]
    public sealed class DenominateCommand : Command<DenominateCommand.PayloadData>
    {
        public DenominateCommand(string RequestId, DenominateCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            /// Denomination object describing the contents of the denomination operation.
            /// </summary>
            public class DenominationClass
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
                [DataMember(Name = "currencies")] 
                public CurrenciesClass Currencies { get; private set; }
                
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
                [DataMember(Name = "values")] 
                public ValuesClass Values { get; private set; }
                [DataMember(Name = "cashBox")] 
                public int? CashBox { get; private set; }

                public DenominationClass (CurrenciesClass Currencies, ValuesClass Values, int? CashBox)
                {
                    this.Currencies = Currencies;
                    this.Values = Values;
                    this.CashBox = CashBox;
                }


            }


            public PayloadData(int Timeout, int? TellerID = null, int? MixNumber = null, object Denomination = null)
                : base(Timeout)
            {
                this.TellerID = TellerID;
                this.MixNumber = MixNumber;
                this.Denomination = Denomination;
            }

            /// <summary>
            /// Identification of teller. This field is ignored if the device is a Self-Service Dispenser.
            /// </summary>
            [DataMember(Name = "tellerID")] 
            public int? TellerID { get; private set; }
            /// <summary>
            /// Mix algorithm or house mix table to be used.
            /// </summary>
            [DataMember(Name = "mixNumber")] 
            public int? MixNumber { get; private set; }
            /// <summary>
            /// Denomination object describing the contents of the denomination operation.
            /// </summary>
            [DataMember(Name = "denomination")] 
            public object Denomination { get; private set; }

        }
    }
}
