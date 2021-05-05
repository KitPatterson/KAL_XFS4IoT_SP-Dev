/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * GetCashUnitCountStatus_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashAcceptor.Completions
{
    [DataContract]
    [Completion(Name = "CashAcceptor.GetCashUnitCountStatus")]
    public sealed class GetCashUnitCountStatusCompletion : Completion<GetCashUnitCountStatusCompletion.PayloadData>
    {
        public GetCashUnitCountStatusCompletion(string RequestId, GetCashUnitCountStatusCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
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


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, CashUnitCountStatusClass CashUnitCountStatus = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.CashUnitCountStatus = CashUnitCountStatus;
            }

            /// <summary>
            /// Object containing cashUnitCountStatus objects. cashUnitCountStatus objects use the same names 
            /// as used in [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo).
            /// </summary>
            [DataMember(Name = "cashUnitCountStatus")] 
            public CashUnitCountStatusClass CashUnitCountStatus { get; private set; }

        }
    }
}
