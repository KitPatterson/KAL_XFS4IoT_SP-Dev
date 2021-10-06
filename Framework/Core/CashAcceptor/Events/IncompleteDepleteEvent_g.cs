/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * IncompleteDepleteEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CashAcceptor.Events
{

    [DataContract]
    [Event(Name = "CashAcceptor.IncompleteDepleteEvent")]
    public sealed class IncompleteDepleteEvent : Event<IncompleteDepleteEvent.PayloadData>
    {

        public IncompleteDepleteEvent(int RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public PayloadData(DepleteClass Deplete = null)
                : base()
            {
                this.Deplete = Deplete;
            }

            [DataContract]
            public sealed class DepleteClass
            {
                public DepleteClass(int? NumberOfItemsReceived = null, int? NumberOfItemsRejected = null, List<DepleteSourceResultsClass> DepleteSourceResults = null)
                {
                    this.NumberOfItemsReceived = NumberOfItemsReceived;
                    this.NumberOfItemsRejected = NumberOfItemsRejected;
                    this.DepleteSourceResults = DepleteSourceResults;
                }

                /// <summary>
                /// Total number of items received in the target cash unit during execution of this command.
                /// <example>100</example>
                /// </summary>
                [DataMember(Name = "numberOfItemsReceived")]
                [DataTypes(Minimum = 0)]
                public int? NumberOfItemsReceived { get; init; }

                /// <summary>
                /// Total number of items rejected during execution of this command.
                /// <example>10</example>
                /// </summary>
                [DataMember(Name = "numberOfItemsRejected")]
                [DataTypes(Minimum = 0)]
                public int? NumberOfItemsRejected { get; init; }

                [DataContract]
                public sealed class DepleteSourceResultsClass
                {
                    public DepleteSourceResultsClass(string CashUnitSource = null, NoteIdClass NoteId = null, int? NumberOfItemsRemoved = null)
                    {
                        this.CashUnitSource = CashUnitSource;
                        this.NoteId = NoteId;
                        this.NumberOfItemsRemoved = NumberOfItemsRemoved;
                    }

                    /// <summary>
                    /// Object name of the cash unit (as stated by the [Storage.GetStorage](#storage.getstorage) 
                    /// command) from which items have been removed.
                    /// <example>unit1</example>
                    /// </summary>
                    [DataMember(Name = "cashUnitSource")]
                    public string CashUnitSource { get; init; }

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
                    /// Total number of items removed from this source cash unit of the _noteId_ item type. 
                    /// Zero will be returned if this source cash unit did not move any items of this item type, 
                    /// for example due to a cash unit or transport jam.
                    /// </summary>
                    [DataMember(Name = "numberOfItemsRemoved")]
                    [DataTypes(Minimum = 0)]
                    public int? NumberOfItemsRemoved { get; init; }

                }

                /// <summary>
                /// Breakdown of which notes moved where. In the case where one item type has several releases and these are moved, 
                /// or where items are moved from a multi denomination cash unit to a multi denomination cash unit, each source 
                /// can move several note types. 
                /// 
                /// For example:
                /// * If one single source was specified with the input structure, and this source moved two different 
                /// note types, then this will have two elements. 
                /// * If two sources were specified and the first source moved two different note types and the second source 
                /// moved three different note types, then this will have five elements.
                /// </summary>
                [DataMember(Name = "depleteSourceResults")]
                public List<DepleteSourceResultsClass> DepleteSourceResults { get; init; }

            }

            /// <summary>
            /// Note that in this case the values in this structure report the amount and number of each denomination that 
            /// have actually been moved during the depletion command.
            /// </summary>
            [DataMember(Name = "deplete")]
            public DepleteClass Deplete { get; init; }

        }

    }
}
