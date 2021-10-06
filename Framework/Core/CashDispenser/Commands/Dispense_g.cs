/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * Dispense_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashDispenser.Commands
{
    //Original name = Dispense
    [DataContract]
    [Command(Name = "CashDispenser.Dispense")]
    public sealed class DispenseCommand : Command<DispenseCommand.PayloadData>
    {
        public DispenseCommand(int RequestId, DispenseCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, DenominationClass Denomination = null, PositionEnum? Position = null, string Token = null)
                : base(Timeout)
            {
                this.Denomination = Denomination;
                this.Position = Position;
                this.Token = Token;
            }

            [DataContract]
            public sealed class DenominationClass
            {
                public DenominationClass(DenominationClassClass Denomination = null, string Mix = null, int? TellerID = null)
                {
                    this.Denomination = Denomination;
                    this.Mix = Mix;
                    this.TellerID = TellerID;
                }

                [DataContract]
                public sealed class DenominationClassClass
                {
                    public DenominationClassClass(Dictionary<string, double> Currencies = null, Dictionary<string, double> Values = null, Dictionary<string, double> CashBox = null)
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
                public DenominationClassClass Denomination { get; init; }

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

            /// <summary>
            /// Denomination object describing the contents of the denomination operation.
            /// </summary>
            [DataMember(Name = "denomination")]
            public DenominationClass Denomination { get; init; }

            public enum PositionEnum
            {
                OutDefault,
                OutLeft,
                OutRight,
                OutCenter,
                OutTop,
                OutBottom,
                OutFront,
                OutRear
            }

            /// <summary>
            /// Supplies the output position as one of the following values:
            /// 
            /// * ```outDefault``` - Default output position.
            /// * ```outLeft``` - Left output position.
            /// * ```outRight``` - Right output position.
            /// * ```outCenter``` - Center output position.
            /// * ```outTop``` - Top output position.
            /// * ```outBottom``` - Bottom output position.
            /// * ```outFront``` - Front output position.
            /// * ```outRear``` - Rear output position.
            /// </summary>
            [DataMember(Name = "position")]
            public PositionEnum? Position { get; init; }

            /// <summary>
            /// The dispense token that authorizes the dispense operation, as created by the authorizing host. See 
            /// the section on [end to end security](#api.generalinformation.e2esecurity) for more information. 
            /// 
            /// The same token may be used multiple times with multiple calls to the CashDispenser.Dispense command as long 
            /// as the total value stacked does not exceed the value given in the token. The hardware will track the value 
            /// of the cash that has been dispensed and will raise an invalidToken error for any attempt to dispense more 
            /// cash than authorized by the token. 
            /// 
            /// The token contains a nonce returned by [Common.GetCommandNonce](#common.getcommandnonce) which must match 
            /// the nonce stored in the hardware. The nonce value stored in the hardware will be cleared when cash is 
            /// presented meaning that all tokens will become invalid after cash is presented. 
            /// 
            /// The dispense token will follow the standard token format, and will contain the following key: 
            /// 
            /// ```DISPENSE1```: The maximum value to be dispensed. This will be a number string that may contain a 
            /// fractional part. The decimal character will be ".". The value, including the fractional part, will be 
            /// defined by the ISO currency. The number will be followed by the ISO currency code. The currency 
            /// code will be upper case. 
            /// 
            /// For example, "123.45EUR" will be €123 and 45 cents.
            /// 
            /// The "DISPENSE" key may appear multiple times with a number suffix. For example, DISPENSE1, DISPENSE2, 
            /// DISPENSE3. The number will start at 1 and increment. Each key can only be given once. Each key must 
            /// have a value in a different currency. For example, DISPENSE1=100.00EUR,DISPENSE2=200.00USD   
            /// 
            /// The actual amount dispensed will be given by the denomination. The value in the token MUST be
            /// greater or equal to the amount in the denomination parameter. If the Token has a lower value, 
            /// or the Token is invalid for any reason, then the command will fail with an invalid data error code.
            /// <example>NONCE=254611E63B2531576314E86527338D61,TOKENFORMAT=1,TOKENLENGTH=0164,DISPENSE1=50.00EUR,HMACSHA256=CB735612FD6141213C2827FB5A6A4F4846D7A7347B15434916FEA6AC16F3D2F2</example>
            /// </summary>
            [DataMember(Name = "token")]
            public string Token { get; init; }

        }
    }
}
