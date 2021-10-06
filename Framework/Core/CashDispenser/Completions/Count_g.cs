/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * Count_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashDispenser.Completions
{
    [DataContract]
    [Completion(Name = "CashDispenser.Count")]
    public sealed class CountCompletion : Completion<CountCompletion.PayloadData>
    {
        public CountCompletion(int RequestId, CountCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, Dictionary<string, CountedCashUnitsClass> CountedCashUnits = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.CountedCashUnits = CountedCashUnits;
            }

            public enum ErrorCodeEnum
            {
                CashUnitError,
                UnsupportedPosition,
                SafeDoorOpen,
                ExchangeActive
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```cashUnitError``` - A cash unit caused a problem. A [Storage.StorageErrorEvent](#storage.storageerrorevent) will be posted with the details.
            /// * ```unsupportedPosition``` - The position specified is not supported.
            /// * ```safeDoorOpen``` - The safe door is open. This device requires the safe door to be closed in order to perform this operation.
            /// * ```exchangeActive``` - The device is in an exchange state (see 
            /// [CashManagement.StartExchange](#cashmanagement.startexchange)).
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            [DataContract]
            public sealed class CountedCashUnitsClass
            {
                public CountedCashUnitsClass(int? Dispensed = null, int? Counted = null, ReplenishmentStatusEnum? ReplenishmentStatus = null, StatusEnum? Status = null)
                {
                    this.Dispensed = Dispensed;
                    this.Counted = Counted;
                    this.ReplenishmentStatus = ReplenishmentStatus;
                    this.Status = Status;
                }

                /// <summary>
                /// The number of items that were dispensed during the emptying of the storage unit.
                /// <example>100</example>
                /// </summary>
                [DataMember(Name = "dispensed")]
                [DataTypes(Minimum = 1)]
                public int? Dispensed { get; init; }

                /// <summary>
                /// The number of items that were counted during the emptying of the storage unit.
                /// <example>100</example>
                /// </summary>
                [DataMember(Name = "counted")]
                [DataTypes(Minimum = 1)]
                public int? Counted { get; init; }

                public enum ReplenishmentStatusEnum
                {
                    Ok,
                    Full,
                    High,
                    Low,
                    Empty
                }

                /// <summary>
                /// The state of the media in the unit if it can be determined. Note that overall 
                /// [status](#storage.getstorage.completion.properties.storage.unit1.status) of the storage unit must
                /// be taken into account when deciding whether the storage unit is usable and whether replenishment status
                /// is applicable. In particular, if the overall status is _missing_ this will not be reported.
                /// The following values are possible:
                /// 
                /// * ```ok``` - The storage unit media is in a good state.
                /// * ```full``` - The storage unit is full.
                /// * ```high``` - The storage unit is almost full (either sensor based or exceeded the 
                /// [highThreshold](#storage.getstorage.completion.properties.storage.unit1.cash.configuration.highthreshold).
                /// * ```low``` - The storage unit is almost empty (either sensor based or below the 
                /// [lowThreshold](#storage.getstorage.completion.properties.storage.unit1.cash.configuration.lowthreshold)). 
                /// * ```empty``` - The storage unit is empty, or insufficient items in the storage unit are preventing further 
                /// dispense operations.
                /// </summary>
                [DataMember(Name = "replenishmentStatus")]
                public ReplenishmentStatusEnum? ReplenishmentStatus { get; init; }

                public enum StatusEnum
                {
                    Ok,
                    Inoperative,
                    Missing,
                    NotConfigured,
                    Manipulated
                }

                /// <summary>
                /// The state of the unit. The following values are possible:
                /// 
                /// * ```ok``` - The storage unit is in a good state.
                /// * ```inoperative``` - The storage unit is inoperative.
                /// * ```missing``` - The storage unit is missing.
                /// * ```notConfigured``` - The storage unit has not been configured for use.
                /// * ```manipulated``` - The storage unit has been inserted (including removal followed by a reinsertion) when 
                /// the device was not in the exchange state - see [Storage.StartExchange](#storage.startexchange). This storage 
                /// unit cannot be used. Only applies to services which support the exchange state.
                /// </summary>
                [DataMember(Name = "status")]
                public StatusEnum? Status { get; init; }

            }

            /// <summary>
            /// List of counted cash unit objects.
            /// </summary>
            [DataMember(Name = "countedCashUnits")]
            public Dictionary<string, CountedCashUnitsClass> CountedCashUnits { get; init; }

        }
    }
}
