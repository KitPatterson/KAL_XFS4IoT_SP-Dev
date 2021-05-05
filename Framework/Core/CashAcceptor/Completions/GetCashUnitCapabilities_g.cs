/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * GetCashUnitCapabilities_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashAcceptor.Completions
{
    [DataContract]
    [Completion(Name = "CashAcceptor.GetCashUnitCapabilities")]
    public sealed class GetCashUnitCapabilitiesCompletion : Completion<GetCashUnitCapabilitiesCompletion.PayloadData>
    {
        public GetCashUnitCapabilitiesCompletion(string RequestId, GetCashUnitCapabilitiesCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            /// Object containing additional cash unit capapabilities. Cash Unit capabiity objects use the same names 
            /// as used in [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo).
            /// </summary>
            public class CashUnitCapsClass
            {
                
                /// <summary>
                /// Cash unit capabilities.
                /// </summary>
                public class AdditionalPropertiesClass 
                {
                    [DataMember(Name = "physicalPositionName")] 
                    public string PhysicalPositionName { get; private set; }
                    [DataMember(Name = "maximumCapacity")] 
                    public int? MaximumCapacity { get; private set; }
                    [DataMember(Name = "hardwareSensors")] 
                    public bool? HardwareSensors { get; private set; }
                    [DataMember(Name = "retractNoteCountThresholds")] 
                    public bool? RetractNoteCountThresholds { get; private set; }
                    
                    /// <summary>
                    /// Specifies the type of items the cash unit can be configured to accept as a combination of flags. The flags 
                    /// are defined as the same values listed in the *itemType* field of the CashManagement.CashUnitInfo output 
                    /// structure. The CashManagement.CashUnitInfo command describes the item types currently configured for a cash 
                    /// unit. This field provides the possible item types values that can be configured for a cash unit using the 
                    /// CashManagement.SetCashUnitInfo command.
                    /// </summary>
                    public class PossibleItemTypesClass 
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

                        public PossibleItemTypesClass (bool? All, bool? Unfit, bool? Individual, bool? Level1, bool? Level2, bool? Level3, bool? ItemProcessor, bool? UnfitIndividual)
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
                    [DataMember(Name = "possibleItemTypes")] 
                    public PossibleItemTypesClass PossibleItemTypes { get; private set; }

                    public AdditionalPropertiesClass (string PhysicalPositionName, int? MaximumCapacity, bool? HardwareSensors, bool? RetractNoteCountThresholds, PossibleItemTypesClass PossibleItemTypes)
                    {
                        this.PhysicalPositionName = PhysicalPositionName;
                        this.MaximumCapacity = MaximumCapacity;
                        this.HardwareSensors = HardwareSensors;
                        this.RetractNoteCountThresholds = RetractNoteCountThresholds;
                        this.PossibleItemTypes = PossibleItemTypes;
                    }
                }
                [DataMember(Name = "additionalProperties")] 
                public AdditionalPropertiesClass AdditionalProperties { get; private set; }

                public CashUnitCapsClass (AdditionalPropertiesClass AdditionalProperties)
                {
                    this.AdditionalProperties = AdditionalProperties;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, CashUnitCapsClass CashUnitCaps = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.CashUnitCaps = CashUnitCaps;
            }

            /// <summary>
            /// Object containing additional cash unit capapabilities. Cash Unit capabiity objects use the same names 
            /// as used in [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo).
            /// </summary>
            [DataMember(Name = "cashUnitCaps")] 
            public CashUnitCapsClass CashUnitCaps { get; private set; }

        }
    }
}
