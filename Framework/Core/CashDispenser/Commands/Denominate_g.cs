/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * Denominate_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashDispenser.Commands
{
    //Original name = Denominate
    [DataContract]
    [Command(Name = "CashDispenser.Denominate")]
    public sealed class DenominateCommand : Command<DenominateCommand.PayloadData>
    {
        public DenominateCommand(int RequestId, DenominateCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, DenominationClass Denomination = null, string Mix = null, int? TellerID = null)
                : base(Timeout)
            {
                this.Denomination = Denomination;
                this.Mix = Mix;
                this.TellerID = TellerID;
            }

            [DataContract]
            public sealed class DenominationClass
            {
                public DenominationClass(Dictionary<string, double> Currencies = null, Dictionary<string, double> Values = null, Dictionary<string, double> CashBox = null)
                {
                    this.Currencies = Currencies;
                    this.Values = Values;
                    this.CashBox = CashBox;
                }

                /// <summary>
                /// List of currency and amount combinations for denomination requests or output. There will be one entry for 
                /// each currency in the denomination. The property name is the ISO 4217 currency identifier. This list can be 
                /// omitted on a request if _values_ specifies the entire request.
                /// </summary>
                [DataMember(Name = "currencies")]
                public Dictionary<string, double> Currencies { get; init; }

                /// <summary>
                /// This list specifies the number of items to take from the cash units. If specified in a request, the output 
                /// denomination must include these items.
                /// 
                /// The property name is storage unit object name as stated by the [Storage.GetStorage](#storage.getstorage)
                /// command. The value of the entry is the number of items to take from that unit.
                /// </summary>
                [DataMember(Name = "values")]
                public Dictionary<string, double> Values { get; init; }

                /// <summary>
                /// List of currency and amount combinations for denomination requests or output. There will be one entry for 
                /// each currency in the denomination. The property name is the ISO 4217 currency identifier. This list can be 
                /// omitted on a request if _values_ specifies the entire request.
                /// </summary>
                [DataMember(Name = "cashBox")]
                public Dictionary<string, double> CashBox { get; init; }

            }

            /// <summary>
            /// Specifies a denomination or a denomination request.
            /// </summary>
            [DataMember(Name = "denomination")]
            public DenominationClass Denomination { get; init; }

            /// <summary>
            /// Mix algorithm or house mix table to be used as defined by mixes reported by
            /// [CashDispenser.GetMixTypes](#cashdispenser.getmixtypes). May be omitted if the request is entirely specified
            /// by _counts_.
            /// <example>mix1</example>
            /// </summary>
            [DataMember(Name = "mix")]
            public string Mix { get; init; }

            /// <summary>
            /// Only applies to Teller Dispensers. Identification of teller.
            /// </summary>
            [DataMember(Name = "tellerID")]
            public int? TellerID { get; init; }

        }
    }
}
