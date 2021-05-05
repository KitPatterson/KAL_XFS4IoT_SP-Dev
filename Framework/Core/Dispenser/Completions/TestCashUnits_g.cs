/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * TestCashUnits_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Dispenser.Completions
{
    [DataContract]
    [Completion(Name = "Dispenser.TestCashUnits")]
    public sealed class TestCashUnitsCompletion : Completion<TestCashUnitsCompletion.PayloadData>
    {
        public TestCashUnitsCompletion(string RequestId, TestCashUnitsCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                CashUnitError,
                UnsupportedPosition,
                ShutterNotOpen,
                ShutterOpen,
                InvalidCashUnit,
                ExchangeActive,
                PresentErrorNoItems,
                PresentErrorItems,
                PresentErrorUnknown,
            }

            /// <summary>
            /// Object containing cash unit information.
            /// </summary>
            public class CashunitsClass
            {
                
                /// <summary>
                /// Cash unit information.
                /// </summary>
                public class AdditionalPropertiesClass 
                {
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
                    public enum TypeEnum
                    {
                        BillCassette,
                        NotApplicable,
                        RejectCassette,
                        CoinCylinder,
                        CoinDispenser,
                        RetractCassette,
                        Coupon,
                        Document,
                        ReplenishmentContainer,
                        Recycling,
                        CashIn,
                    }
                    [DataMember(Name = "type")] 
                    public TypeEnum? Type { get; private set; }
                    [DataMember(Name = "currencyID")] 
                    public string CurrencyID { get; private set; }
                    [DataMember(Name = "value")] 
                    public double? Value { get; private set; }
                    [DataMember(Name = "logicalCount")] 
                    public int? LogicalCount { get; private set; }
                    [DataMember(Name = "maximum")] 
                    public int? Maximum { get; private set; }
                    [DataMember(Name = "appLock")] 
                    public bool? AppLock { get; private set; }
                    [DataMember(Name = "cashUnitName")] 
                    public string CashUnitName { get; private set; }
                    [DataMember(Name = "initialCount")] 
                    public int? InitialCount { get; private set; }
                    [DataMember(Name = "dispensedCount")] 
                    public int? DispensedCount { get; private set; }
                    [DataMember(Name = "presentedCount")] 
                    public int? PresentedCount { get; private set; }
                    [DataMember(Name = "retractedCount")] 
                    public int? RetractedCount { get; private set; }
                    [DataMember(Name = "rejectCount")] 
                    public int? RejectCount { get; private set; }
                    [DataMember(Name = "minimum")] 
                    public int? Minimum { get; private set; }
                    [DataMember(Name = "physicalPositionName")] 
                    public string PhysicalPositionName { get; private set; }
                    [DataMember(Name = "unitID")] 
                    public string UnitID { get; private set; }
                    [DataMember(Name = "count")] 
                    public int? Count { get; private set; }
                    [DataMember(Name = "maximumCapacity")] 
                    public int? MaximumCapacity { get; private set; }
                    [DataMember(Name = "hardwareSensor")] 
                    public bool? HardwareSensor { get; private set; }
                    
                    /// <summary>
                    /// Specifies the type of items the cash unit takes as a combination of the following flags. 
                    /// The table in the Comments section of this command defines how to interpret the combination of these flags (TODO: include Table)
                    /// </summary>
                    public class ItemTypeClass 
                    {
                        [DataMember(Name = "all")] 
                        public bool? All { get; private set; }
                        [DataMember(Name = "unfit")] 
                        public bool? Unfit { get; private set; }
                        [DataMember(Name = "individual")] 
                        public bool? Individual { get; private set; }
                        [DataMember(Name = "level1")] 
                        public bool? Level1 { get; private set; }
                        [DataMember(Name = "level2")] 
                        public bool? Level2 { get; private set; }
                        [DataMember(Name = "level3")] 
                        public bool? Level3 { get; private set; }
                        [DataMember(Name = "itemProcessor")] 
                        public bool? ItemProcessor { get; private set; }
                        [DataMember(Name = "unfitIndividual")] 
                        public bool? UnfitIndividual { get; private set; }

                        public ItemTypeClass (bool? All, bool? Unfit, bool? Individual, bool? Level1, bool? Level2, bool? Level3, bool? ItemProcessor, bool? UnfitIndividual)
                        {
                            this.All = All;
                            this.Unfit = Unfit;
                            this.Individual = Individual;
                            this.Level1 = Level1;
                            this.Level2 = Level2;
                            this.Level3 = Level3;
                            this.ItemProcessor = ItemProcessor;
                            this.UnfitIndividual = UnfitIndividual;
                        }
                    }
                    [DataMember(Name = "itemType")] 
                    public ItemTypeClass ItemType { get; private set; }
                    [DataMember(Name = "cashInCount")] 
                    public int? CashInCount { get; private set; }
                    
                    /// <summary>
                    /// Array of cash items inside the cash unit. The content of this structure is persistent. 
                    /// If the cash unit is Dispenser specific cash unit with *type* *billCassette* or the contents of the cash unit are not known
                    /// this structure will be omitted.
                    /// If the cash unit is of *type* *retractCassette* this pointer will be omitted except for the following cases:
                    /// 
                    /// •\tIf the retract cash unit is configured to accept level 2 notes then the number and type of level 2 notes is returned in 
                    /// the *noteNumberList* and *count* contains the number of retract operations. *cashInCount* contains the actual number of level 2 notes.
                    /// 
                    /// •\tIf items are recognized during retract operations then the number and type of notes retracted is returned in *noteNumberList*
                    /// and *count* contains the number of retract operations. *cashInCount* contains the actual number of retracted items.
                    /// </summary>
                    public class NoteNumberListClass 
                    {
                        
                        /// <summary>
                        /// Array of banknote numbers the cash unit contains.
                        /// </summary>
                        public class NoteNumberClass 
                        {
                            [DataMember(Name = "noteID")] 
                            public int? NoteID { get; private set; }
                            [DataMember(Name = "count")] 
                            public int? Count { get; private set; }

                            public NoteNumberClass (int? NoteID, int? Count)
                            {
                                this.NoteID = NoteID;
                                this.Count = Count;
                            }
                        }
                        [DataMember(Name = "noteNumber")] 
                        public NoteNumberClass NoteNumber { get; private set; }

                        public NoteNumberListClass (NoteNumberClass NoteNumber)
                        {
                            this.NoteNumber = NoteNumber;
                        }
                    }
                    [DataMember(Name = "noteNumberList")] 
                    public NoteNumberListClass NoteNumberList { get; private set; }
                    [DataMember(Name = "noteIDs")] 
                    public List<int?> NoteIDs { get; private set; }

                    public AdditionalPropertiesClass (StatusEnum? Status, TypeEnum? Type, string CurrencyID, double? Value, int? LogicalCount, int? Maximum, bool? AppLock, string CashUnitName, int? InitialCount, int? DispensedCount, int? PresentedCount, int? RetractedCount, int? RejectCount, int? Minimum, string PhysicalPositionName, string UnitID, int? Count, int? MaximumCapacity, bool? HardwareSensor, ItemTypeClass ItemType, int? CashInCount, NoteNumberListClass NoteNumberList, List<int?> NoteIDs)
                    {
                        this.Status = Status;
                        this.Type = Type;
                        this.CurrencyID = CurrencyID;
                        this.Value = Value;
                        this.LogicalCount = LogicalCount;
                        this.Maximum = Maximum;
                        this.AppLock = AppLock;
                        this.CashUnitName = CashUnitName;
                        this.InitialCount = InitialCount;
                        this.DispensedCount = DispensedCount;
                        this.PresentedCount = PresentedCount;
                        this.RetractedCount = RetractedCount;
                        this.RejectCount = RejectCount;
                        this.Minimum = Minimum;
                        this.PhysicalPositionName = PhysicalPositionName;
                        this.UnitID = UnitID;
                        this.Count = Count;
                        this.MaximumCapacity = MaximumCapacity;
                        this.HardwareSensor = HardwareSensor;
                        this.ItemType = ItemType;
                        this.CashInCount = CashInCount;
                        this.NoteNumberList = NoteNumberList;
                        this.NoteIDs = NoteIDs;
                    }
                }
                [DataMember(Name = "additionalProperties")] 
                public AdditionalPropertiesClass AdditionalProperties { get; private set; }

                public CashunitsClass (AdditionalPropertiesClass AdditionalProperties)
                {
                    this.AdditionalProperties = AdditionalProperties;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, CashunitsClass Cashunits = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Cashunits = Cashunits;
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```cashUnitError``` - A cash unit caused a problem that meant all cash units could not be tested or 
            /// no cash units were testable. One or more CashManagement.CashUnitErrorEvent will be posted with the details.
            /// * ```unsupportedPosition``` - The position specified is not supported.
            /// * ```shutterNotOpen``` - The shutter is not open or did not open when it should have. No items presented.
            /// * ```shutterOpen``` - The shutter is open when it should be closed. No items presented.
            /// * ```invalidCashUnit``` - The cash unit number specified is not valid.
            /// * ```exchangeActive``` - The device is in an exchange state (see CashManagement.StartExchange).
            /// * ```presentErrorNoItems``` - There was an error during the present operation - no items were presented.
            /// * ```presentErrorItems``` - There was an error during the present operation - at least some of the items 
            /// were presented.
            /// * ```presentErrorUnknown``` - There was an error during the present operation - the position of the items 
            /// is unknown. Intervention may be required to reconcile the cash amount totals.
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }
            /// <summary>
            /// Object containing cash unit information.
            /// </summary>
            [DataMember(Name = "cashunits")] 
            public CashunitsClass Cashunits { get; private set; }

        }
    }
}
