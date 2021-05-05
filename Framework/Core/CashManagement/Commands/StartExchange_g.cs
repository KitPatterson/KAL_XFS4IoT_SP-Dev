/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * StartExchange_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashManagement.Commands
{
    //Original name = StartExchange
    [DataContract]
    [Command(Name = "CashManagement.StartExchange")]
    public sealed class StartExchangeCommand : Command<StartExchangeCommand.PayloadData>
    {
        public StartExchangeCommand(string RequestId, StartExchangeCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ExchangeTypeEnum
            {
                ByHand,
                ToCassettes,
                ClearRecycler,
                DepositInto,
            }

            /// <summary>
            /// This field is used when the [exchangeType](#cashmanagement.startexchange.command.properties.exchangetype) 
            /// is ```clearRecycler```, i.e. a recycle cash unit is to be emptied.
            /// </summary>
            public class OutputClass
            {
                [DataMember(Name = "cashunit")] 
                public string Cashunit { get; private set; }
                
                /// <summary>
                /// Determines to which position the cash should be moved as a combination of the following flags:
                /// </summary>
                public class PositionClass 
                {
                    [DataMember(Name = "default")] 
                    public bool? Default { get; private set; }
                    [DataMember(Name = "left")] 
                    public bool? Left { get; private set; }
                    [DataMember(Name = "right")] 
                    public bool? Right { get; private set; }
                    [DataMember(Name = "center")] 
                    public bool? Center { get; private set; }
                    [DataMember(Name = "top")] 
                    public bool? Top { get; private set; }
                    [DataMember(Name = "bottom")] 
                    public bool? Bottom { get; private set; }
                    [DataMember(Name = "front")] 
                    public bool? Front { get; private set; }
                    [DataMember(Name = "rear")] 
                    public bool? Rear { get; private set; }

                    public PositionClass (bool? Default, bool? Left, bool? Right, bool? Center, bool? Top, bool? Bottom, bool? Front, bool? Rear)
                    {
                        this.Default = Default;
                        this.Left = Left;
                        this.Right = Right;
                        this.Center = Center;
                        this.Top = Top;
                        this.Bottom = Bottom;
                        this.Front = Front;
                        this.Rear = Rear;
                    }
                }
                [DataMember(Name = "position")] 
                public PositionClass Position { get; private set; }
                [DataMember(Name = "targetCashunit")] 
                public string TargetCashunit { get; private set; }

                public OutputClass (string Cashunit, PositionClass Position, string TargetCashunit)
                {
                    this.Cashunit = Cashunit;
                    this.Position = Position;
                    this.TargetCashunit = TargetCashunit;
                }


            }


            public PayloadData(int Timeout, ExchangeTypeEnum? ExchangeType = null, int? TellerID = null, List<string> CashunitList = null, object Output = null)
                : base(Timeout)
            {
                this.ExchangeType = ExchangeType;
                this.TellerID = TellerID;
                this.CashunitList = CashunitList;
                this.Output = Output;
            }

            /// <summary>
            /// Specifies the type of cash unit exchange operation. Following values are possible:
            /// 
            /// * ```byHand``` - The cash units will be replenished manually either by filling or emptying the cash unit by 
            /// hand or by replacing the cash unit.
            /// * ```toCassettes``` - Items will be moved from the replenishment container to the bill cash units.
            /// * ```clearRecycler``` - Items will be moved from a recycle cash unit to a cash unit or output position.
            /// * ```depositInto``` - Items will be moved from the deposit entrance to the bill cash units. See section x.y 
            /// (TODO) for an example flow.
            /// </summary>
            [DataMember(Name = "exchangeType")] 
            public ExchangeTypeEnum? ExchangeType { get; private set; }
            /// <summary>
            /// Identifies the teller. If the device is a Self-Service ATM this field is ignored.
            /// </summary>
            [DataMember(Name = "tellerID")] 
            public int? TellerID { get; private set; }
            /// <summary>
            /// Array of strings containing the object names of the cash units to be exchanged as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command.
            /// </summary>
            [DataMember(Name = "cashunitList")] 
            public List<string> CashunitList{ get; private set; }
            /// <summary>
            /// This field is used when the [exchangeType](#cashmanagement.startexchange.command.properties.exchangetype) 
            /// is ```clearRecycler```, i.e. a recycle cash unit is to be emptied.
            /// </summary>
            [DataMember(Name = "output")] 
            public object Output { get; private set; }

        }
    }
}
