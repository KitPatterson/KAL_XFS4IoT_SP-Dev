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
using XFS4IoT.Completions;

namespace XFS4IoT.Dispenser.Completions
{
    [DataContract]
    [Completion(Name = "Dispenser.Denominate")]
    public sealed class DenominateCompletion : Completion<DenominateCompletion.PayloadData>
    {
        public DenominateCompletion(string RequestId, DenominateCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                InvalidCurrency,
                InvalidTellerID,
                CashUnitError,
                InvalidDenomination,
                InvalidMixNumber,
                NoCurrencyMix,
                NotDispensable,
                TooManyItems,
                ExchangeActive,
                NoCashBoxPresent,
                AmountNotInMixTable,
            }

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


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, CurrenciesClass Currencies = null, ValuesClass Values = null, int? CashBox = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Currencies = Currencies;
                this.Values = Values;
                this.CashBox = CashBox;
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```invalidCurrency``` - There are no cash units in the device of the currency specified in one of the *currencyID* fields of the input structure.
            /// * ```invalidTellerID``` - Invalid teller ID. This error will never be generated by a Self-Service device.
            /// * ```cashUnitError``` - There is a problem with a cash unit. A CashManagement.CashUnitErrorEvent will be posted with the details.
            /// * ```invalidDenomination``` - The *mixNumer* is ```individual``` and the sum of the values for *cashBox* and the items 
            /// specified by *values* does not match the non-zero amount specified. This error code is not used when the amount specified is zero.
            /// * ```invalidMixNumber``` - Unknown mix algorithm.
            /// * ```noCurrencyMix``` - The cash units specified in the denomination were not all of the same currency and this device does not support multiple currencies.
            /// * ```notDispensable``` - The amount is not dispensable by the device. This error code is also returned if the *mixNumber* 
            /// is specified as ```individual```, but a cash unit is specified in the *values* list which is not a dispensing cash unit, e.g., a retract/reject cash unit.
            /// * ```tooManyItems``` - The request requires too many items to be dispensed.
            /// * ```exchangeActive``` - The device is in an exchange state (see CashManagement.StartExchange).
            /// * ```noCashBoxPresent``` - Cash box amount needed, however teller is not assigned a cash box.
            /// * ```amountNotInMixTable``` - A mix table is being used to determine the denomination but the amount specified for the denomination is not in the mix table.
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }
            /// <summary>
            /// \"List of currency and amount combinations for denomination. There will be one entry for each currency
            /// in the denomination. The property name is the currency name in ISO format (e.g. \"EUR\").
            /// </summary>
            [DataMember(Name = "currencies")] 
            public CurrenciesClass Currencies { get; private set; }
            /// <summary>
            /// This list specifies the number of items to take from the cash units. 
            /// Each entry uses a cashunit object name as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command. The value of the entry is the 
            /// number of items to take from that unit.
            /// If the application does not wish to specify a denomination, it should omit the values property.
            /// </summary>
            [DataMember(Name = "values")] 
            public ValuesClass Values { get; private set; }
            /// <summary>
            /// Only applies to Teller Dispensers. Amount to be paid from the teller’s cash box.
            /// </summary>
            [DataMember(Name = "cashBox")] 
            public int? CashBox { get; private set; }

        }
    }
}
