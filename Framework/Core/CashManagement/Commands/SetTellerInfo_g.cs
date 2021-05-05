/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * SetTellerInfo_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashManagement.Commands
{
    //Original name = SetTellerInfo
    [DataContract]
    [Command(Name = "CashManagement.SetTellerInfo")]
    public sealed class SetTellerInfoCommand : Command<SetTellerInfoCommand.PayloadData>
    {
        public SetTellerInfoCommand(string RequestId, SetTellerInfoCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ActionEnum
            {
                CreateTeller,
                ModifyTeller,
                DeleteTeller,
            }

            /// <summary>
            /// Teller details object.
            /// </summary>
            public class TellerDetailsClass
            {
                [DataMember(Name = "tellerID")] 
                public int? TellerID { get; private set; }
                public enum InputPositionEnum
                {
                    None,
                    Left,
                    Right,
                    Center,
                    Top,
                    Bottom,
                    Front,
                    Rear,
                }
                [DataMember(Name = "inputPosition")] 
                public InputPositionEnum? InputPosition { get; private set; }
                public enum OutputPositionEnum
                {
                    None,
                    Left,
                    Right,
                    Center,
                    Top,
                    Bottom,
                    Front,
                    Rear,
                }
                [DataMember(Name = "outputPosition")] 
                public OutputPositionEnum? OutputPosition { get; private set; }
                
                /// <summary>
                /// List of teller total objects. There is one object per currency.
                /// </summary>
                public class TellerTotalsClass 
                {
                    
                    /// <summary>
                    /// The object name is the three 
                    /// character ISO format currency identifier [Ref 2].
                    /// </summary>
                    public class AdditionalPropertiesClass 
                    {
                        [DataMember(Name = "itemsReceived")] 
                        public double? ItemsReceived { get; private set; }
                        [DataMember(Name = "itemsDispensed")] 
                        public double? ItemsDispensed { get; private set; }
                        [DataMember(Name = "coinsReceived")] 
                        public double? CoinsReceived { get; private set; }
                        [DataMember(Name = "coinsDispensed")] 
                        public double? CoinsDispensed { get; private set; }
                        [DataMember(Name = "cashBoxReceived")] 
                        public double? CashBoxReceived { get; private set; }
                        [DataMember(Name = "cashBoxDispensed")] 
                        public double? CashBoxDispensed { get; private set; }

                        public AdditionalPropertiesClass (double? ItemsReceived, double? ItemsDispensed, double? CoinsReceived, double? CoinsDispensed, double? CashBoxReceived, double? CashBoxDispensed)
                        {
                            this.ItemsReceived = ItemsReceived;
                            this.ItemsDispensed = ItemsDispensed;
                            this.CoinsReceived = CoinsReceived;
                            this.CoinsDispensed = CoinsDispensed;
                            this.CashBoxReceived = CashBoxReceived;
                            this.CashBoxDispensed = CashBoxDispensed;
                        }
                    }
                    [DataMember(Name = "additionalProperties")] 
                    public AdditionalPropertiesClass AdditionalProperties { get; private set; }

                    public TellerTotalsClass (AdditionalPropertiesClass AdditionalProperties)
                    {
                        this.AdditionalProperties = AdditionalProperties;
                    }
                }
                [DataMember(Name = "tellerTotals")] 
                public TellerTotalsClass TellerTotals { get; private set; }

                public TellerDetailsClass (int? TellerID, InputPositionEnum? InputPosition, OutputPositionEnum? OutputPosition, TellerTotalsClass TellerTotals)
                {
                    this.TellerID = TellerID;
                    this.InputPosition = InputPosition;
                    this.OutputPosition = OutputPosition;
                    this.TellerTotals = TellerTotals;
                }


            }


            public PayloadData(int Timeout, ActionEnum? Action = null, object TellerDetails = null)
                : base(Timeout)
            {
                this.Action = Action;
                this.TellerDetails = TellerDetails;
            }

            /// <summary>
            /// The action to be performed. Following values are possible:
            /// 
            /// * ```createTeller``` - A teller is to be added.
            /// * ```modifyTeller``` - Information about an existing teller is to be modified.
            /// * ```deleteTeller``` - A teller is to be removed.
            /// </summary>
            [DataMember(Name = "action")] 
            public ActionEnum? Action { get; private set; }
            /// <summary>
            /// Teller details object.
            /// </summary>
            [DataMember(Name = "tellerDetails")] 
            public object TellerDetails { get; private set; }

        }
    }
}
