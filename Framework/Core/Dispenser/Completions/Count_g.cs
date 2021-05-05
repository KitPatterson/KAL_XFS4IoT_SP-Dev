/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * Count_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Dispenser.Completions
{
    [DataContract]
    [Completion(Name = "Dispenser.Count")]
    public sealed class CountCompletion : Completion<CountCompletion.PayloadData>
    {
        public CountCompletion(string RequestId, CountCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                CashUnitError,
                UnsupportedPosition,
                SafeDoorOpen,
                ExchangeActive,
            }

            /// <summary>
            /// List of counted cash unit objects.
            /// </summary>
            public class CountedCashUnitsClass
            {
                
                /// <summary>
                /// Counted cash unit object. Object name is the same as used in [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo).
                /// </summary>
                public class AdditionalPropertiesClass 
                {
                    [DataMember(Name = "physicalPositionName")] 
                    public string PhysicalPositionName { get; private set; }
                    [DataMember(Name = "unitId")] 
                    public string UnitId { get; private set; }
                    [DataMember(Name = "dispensed")] 
                    public int? Dispensed { get; private set; }
                    [DataMember(Name = "counted")] 
                    public int? Counted { get; private set; }
                    public enum StatusEnum
                    {
                        Ok,
                        Full,
                        High,
                        Low,
                        Empty,
                        Inoperative,
                        Missing,
                        NoValue,
                        NoReference,
                        Manipulated,
                    }
                    [DataMember(Name = "status")] 
                    public StatusEnum? Status { get; private set; }

                    public AdditionalPropertiesClass (string PhysicalPositionName, string UnitId, int? Dispensed, int? Counted, StatusEnum? Status)
                    {
                        this.PhysicalPositionName = PhysicalPositionName;
                        this.UnitId = UnitId;
                        this.Dispensed = Dispensed;
                        this.Counted = Counted;
                        this.Status = Status;
                    }
                }
                [DataMember(Name = "additionalProperties")] 
                public AdditionalPropertiesClass AdditionalProperties { get; private set; }

                public CountedCashUnitsClass (AdditionalPropertiesClass AdditionalProperties)
                {
                    this.AdditionalProperties = AdditionalProperties;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, CountedCashUnitsClass CountedCashUnits = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.CountedCashUnits = CountedCashUnits;
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```cashUnitError``` - A cash unit caused a problem. A CashManagement.CashUnitErrorEvent will be posted with the details.
            /// * ```unsupportedPosition``` - The position specified is not supported.
            /// * ```safeDoorOpen``` - The safe door is open. This device requires the safe door to be closed in order to perform this operation.
            /// * ```exchangeActive``` - The device is in an exchange state (see 
            /// [CashManagement.StartExchange](#cashmanagement.startexchange)).
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }
            /// <summary>
            /// List of counted cash unit objects.
            /// </summary>
            [DataMember(Name = "countedCashUnits")] 
            public CountedCashUnitsClass CountedCashUnits { get; private set; }

        }
    }
}
