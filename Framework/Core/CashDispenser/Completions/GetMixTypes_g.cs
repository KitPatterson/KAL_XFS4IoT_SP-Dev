/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * GetMixTypes_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashDispenser.Completions
{
    [DataContract]
    [Completion(Name = "CashDispenser.GetMixTypes")]
    public sealed class GetMixTypesCompletion : Completion<GetMixTypesCompletion.PayloadData>
    {
        public GetMixTypesCompletion(int RequestId, GetMixTypesCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, Dictionary<string, MixesClass> Mixes = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.Mixes = Mixes;
            }

            [DataContract]
            public sealed class MixesClass
            {
                public MixesClass(TypeEnum? Type = null, string Algorithm = null, string Name = null)
                {
                    this.Type = Type;
                    this.Algorithm = Algorithm;
                    this.Name = Name;
                }

                public enum TypeEnum
                {
                    Individual,
                    Algorithm,
                    Table
                }

                /// <summary>
                /// Specifies the mix type as one of the following:
                /// 
                /// * ```individual``` - the mix is not calculated by the service, completely specified by the application.
                /// * ```algorithm``` - the mix is calculated using one of the algorithms specified by _algorithm_.
                /// * ```table``` - the mix is calculated using a mix table - see
                /// [CashDispenser.GetMixTable](#cashdispenser.getmixtable)
                /// <example>algorithm</example>
                /// </summary>
                [DataMember(Name = "type")]
                public TypeEnum? Type { get; init; }

                /// <summary>
                /// If _type_ is _algorithm_, specifies the algorithm type as one of the following. There are three pre-defined
                /// algorithms, additional vendor-defined algorithms can also be defined. Omitted if the mix is not an algorithm.
                /// 
                /// * ```minimumBills``` - Select a mix requiring the minimum possible number of items.
                /// * ```equalEmptying``` - The denomination is selected based upon criteria which ensure that over the course 
                /// of its operation the storage units will empty as far as possible at the same rate and will therefore go 
                /// low and then empty at approximately the same time.
                /// * ```maxCashUnits``` - The denomination will be selected based upon criteria which ensures the maximum 
                /// number of storage units are used.
                /// * ```&lt;vendor-defined mix&gt;``` - A vendor defined mix algorithm
                /// <example>minimumBills</example>
                /// </summary>
                [DataMember(Name = "algorithm")]
                [DataTypes(Pattern = @"^minimumBills$|^equalEmptying$|^maxCashUnits$|^[A-Za-z0-9]*$")]
                public string Algorithm { get; init; }

                /// <summary>
                /// Name of the table or algorithm used. May be omitted.
                /// <example>Minimum Bills</example>
                /// </summary>
                [DataMember(Name = "name")]
                public string Name { get; init; }

            }

            /// <summary>
            /// Object containing mix specifications. The property name of each mix can be used as the _mix_ in the 
            /// [CashDispenser.GetMixTable](#cashdispenser.getmixtable), [CashDispenser.Dispense](#cashdispenser.dispense)
            /// and [CashDispenser.Denominate](#cashdispenser.denominate) commands.
            /// </summary>
            [DataMember(Name = "mixes")]
            public Dictionary<string, MixesClass> Mixes { get; init; }

        }
    }
}
