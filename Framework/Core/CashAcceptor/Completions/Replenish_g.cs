/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * Replenish_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashAcceptor.Completions
{
    [DataContract]
    [Completion(Name = "CashAcceptor.Replenish")]
    public sealed class ReplenishCompletion : Completion<ReplenishCompletion.PayloadData>
    {
        public ReplenishCompletion(int RequestId, ReplenishCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, int? NumberOfItemsRemoved = null, int? NumberOfItemsRejected = null, List<ReplenishTargetResultsClass> ReplenishTargetResults = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.NumberOfItemsRemoved = NumberOfItemsRemoved;
                this.NumberOfItemsRejected = NumberOfItemsRejected;
                this.ReplenishTargetResults = ReplenishTargetResults;
            }

            public enum ErrorCodeEnum
            {
                CashUnitError,
                InvalidCashUnit,
                ExchangeActive,
                CashInActive
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```cashUnitError``` - A problem occurred with a cash unit. A 
            /// [Storage.StorageErrorEvent](#storage.storageerrorevent) will be sent with the details. If appropriate 
            /// a [CashAcceptor.IncompleteReplenishEvent](#cashacceptor.incompletereplenishevent) will also be sent.
            /// * ```invalidCashUnit``` - The source or target cash unit specified is invalid for this operation. 
            /// The [CashAcceptor.ReplenishTarget](#cashacceptor.replenishtarget) command can be used to determine which source or target is valid.
            /// * ```exchangeActive``` - The device is in the exchange state.
            /// * ```cashInActive``` - A cash-in transaction is active.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            /// <summary>
            /// Total number of items removed from the source storage unit including rejected items during execution of this
            /// command. Not specified if no items were removed.
            /// <example>20</example>
            /// </summary>
            [DataMember(Name = "numberOfItemsRemoved")]
            [DataTypes(Minimum = 1)]
            public int? NumberOfItemsRemoved { get; init; }

            /// <summary>
            /// Total number of items rejected during execution of this command. Not specified if no items were rejected.
            /// <example>2</example>
            /// </summary>
            [DataMember(Name = "numberOfItemsRejected")]
            [DataTypes(Minimum = 1)]
            public int? NumberOfItemsRejected { get; init; }

            [DataContract]
            public sealed class ReplenishTargetResultsClass
            {
                public ReplenishTargetResultsClass(string Target = null, NoteIdClass NoteId = null, int? NumberOfItemsReceived = null)
                {
                    this.Target = Target;
                    this.NoteId = NoteId;
                    this.NumberOfItemsReceived = NumberOfItemsReceived;
                }

                /// <summary>
                /// Object name of the cash unit (as stated by the [Storage.GetStorage](#storage.getstorage) 
                /// command) to which items have been moved.
                /// <example>unit1</example>
                /// </summary>
                [DataMember(Name = "target")]
                public string Target { get; init; }

                [DataContract]
                public sealed class NoteIdClass
                {
                    public NoteIdClass(int? NoteID = null, string Currency = null, double? Value = null, int? Release = null)
                    {
                        this.NoteID = NoteID;
                        this.Currency = Currency;
                        this.Value = Value;
                        this.Release = Release;
                    }

                    /// <summary>
                    /// Assigned by the XFS4IoT service. A unique number identifying a single cash item. 
                    /// Each unique combination of the other properties will have a different noteID. 
                    /// Can be used for migration of _usNoteID_ from XFS 3.x.
                    /// <example>25</example>
                    /// </summary>
                    [DataMember(Name = "noteID")]
                    [DataTypes(Minimum = 1)]
                    public int? NoteID { get; init; }

                    /// <summary>
                    /// ISO 4217 currency.
                    /// <example>USD</example>
                    /// </summary>
                    [DataMember(Name = "currency")]
                    public string Currency { get; init; }

                    /// <summary>
                    /// Absolute value of all contents, 0 if mixed. May only be modified in an exchange state if applicable. May be 
                    /// a floating point value to allow for coins and notes which have a value which is not a whole multiple 
                    /// of the currency unit.
                    /// <example>20</example>
                    /// </summary>
                    [DataMember(Name = "value")]
                    public double? Value { get; init; }

                    /// <summary>
                    /// The release of the cash item. The higher this number is, the newer the release.
                    /// 
                    /// If zero or not reported, there is only one release of that cash item or the device is not
                    /// capable of distinguishing different release of the item, for example in a simple cash dispenser.
                    /// 
                    /// An example of how this can be used is being able to sort different releases of the same denomination 
                    /// note to different storage units to take older notes out of circulation.
                    /// 
                    /// This value is device, banknote reader and currency dependent, therefore a release number of the 
                    /// same cash item will not necessarily have the same value in different systems and any such usage 
                    /// would be specific to a specific device's configuration.
                    /// <example>1</example>
                    /// </summary>
                    [DataMember(Name = "release")]
                    [DataTypes(Minimum = 0)]
                    public int? Release { get; init; }

                }

                /// <summary>
                /// An object containing information about a single cash item supported by the device.
                /// </summary>
                [DataMember(Name = "noteId")]
                public NoteIdClass NoteId { get; init; }

                /// <summary>
                /// Total number of items received in this target cash unit of the _noteId_ note type.
                /// <example>20</example>
                /// </summary>
                [DataMember(Name = "numberOfItemsReceived")]
                [DataTypes(Minimum = 1)]
                public int? NumberOfItemsReceived { get; init; }

            }

            /// <summary>
            /// Breakdown of which notes moved where. In the case where one note type has several releases and these 
            /// are moved, or where items are moved from a multi denomination cash unit to a multi denomination cash unit, 
            /// each target can receive several note types. 
            /// 
            /// For example:
            /// * If one single target was specified with the _replenishTargets_ input structure, and this target received 
            /// two different note types, then this property will have two elements.
            /// * If two targets were specified and the first target received two different note types and the second target 
            /// received three different note types, then this property will have five elements.
            /// </summary>
            [DataMember(Name = "replenishTargetResults")]
            public List<ReplenishTargetResultsClass> ReplenishTargetResults { get; init; }

        }
    }
}
