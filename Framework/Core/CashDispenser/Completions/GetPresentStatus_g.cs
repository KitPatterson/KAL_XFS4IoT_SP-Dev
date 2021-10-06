/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * GetPresentStatus_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashDispenser.Completions
{
    [DataContract]
    [Completion(Name = "CashDispenser.GetPresentStatus")]
    public sealed class GetPresentStatusCompletion : Completion<GetPresentStatusCompletion.PayloadData>
    {
        public GetPresentStatusCompletion(int RequestId, GetPresentStatusCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, DenominationClass Denomination = null, PresentStateEnum? PresentState = null, string Token = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Denomination = Denomination;
                this.PresentState = PresentState;
                this.Token = Token;
            }

            public enum ErrorCodeEnum
            {
                UnsupportedPosition
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```unsupportedPosition``` - The specified output position is not supported.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

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

            public enum PresentStateEnum
            {
                Presented,
                NotPresented,
                Unknown
            }

            /// <summary>
            /// Supplies the status of the last dispense or present operation. Following values are possible:
            /// 
            /// * ```presented``` - The items were presented. This status is set as soon as the customer has access to the items.
            /// * ```notPresented``` - The customer has not had access to the items.
            /// * ```unknown``` - It is not known if the customer had access to the items.
            /// </summary>
            [DataMember(Name = "presentState")]
            public PresentStateEnum? PresentState { get; init; }

            /// <summary>
            /// The present status token that protects the present status. See 
            /// the generic end to end document for more information. 
            /// <example>NONCE=1414,TOKENFORMAT=1,TOKENLENGTH=0268,DISPENSEID=CB735612FD6141213C2827FB5A6A4F4846D7A7347B15434916FEA6AC16F3D2F2,DISPENSED1=50.00EUR,PRESENTED1=YES,PRESENTEDAMOUNT1=50.00EUR,RETRACTED1=NO,HMACSHA256=55D123E9EE64F0CC3D1CD4F953348B441E521BBACCD6998C6F51D645D71E6C83</example>
            /// </summary>
            [DataMember(Name = "token")]
            public string Token { get; init; }

        }
    }
}
