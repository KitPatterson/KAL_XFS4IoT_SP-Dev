/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * CalibrateCashUnit_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashManagement.Commands
{
    //Original name = CalibrateCashUnit
    [DataContract]
    [Command(Name = "CashManagement.CalibrateCashUnit")]
    public sealed class CalibrateCashUnitCommand : Command<CalibrateCashUnitCommand.PayloadData>
    {
        public CalibrateCashUnitCommand(string RequestId, CalibrateCashUnitCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            /// Specifies where the dispensed items should be moved to.
            /// </summary>
            public class PositionClass
            {
                [DataMember(Name = "cashunit")] 
                public string Cashunit { get; private set; }
                
                /// <summary>
                /// This field is used if items are to be moved to internal areas of the device, including cash units, the intermediate stacker, or the transport.
                /// </summary>
                public class RetractAreaClass 
                {
                    public enum OutputPositionEnum
                    {
                        Default,
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
                    public enum RetractAreaEnum
                    {
                        Retract,
                        Transport,
                        Stacker,
                        Reject,
                        ItemCassette,
                    }
                    [DataMember(Name = "retractArea")] 
                    public RetractAreaEnum? RetractArea { get; private set; }
                    [DataMember(Name = "index")] 
                    public int? Index { get; private set; }

                    public RetractAreaClass (OutputPositionEnum? OutputPosition, RetractAreaEnum? RetractArea, int? Index)
                    {
                        this.OutputPosition = OutputPosition;
                        this.RetractArea = RetractArea;
                        this.Index = Index;
                    }
                }
                [DataMember(Name = "retractArea")] 
                public RetractAreaClass RetractArea { get; private set; }
                public enum OutputPositionEnum
                {
                    Default,
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

                public PositionClass (string Cashunit, RetractAreaClass RetractArea, OutputPositionEnum? OutputPosition)
                {
                    this.Cashunit = Cashunit;
                    this.RetractArea = RetractArea;
                    this.OutputPosition = OutputPosition;
                }


            }


            public PayloadData(int Timeout, string Cashunit = null, int? NumOfBills = null, object Position = null)
                : base(Timeout)
            {
                this.Cashunit = Cashunit;
                this.NumOfBills = NumOfBills;
                this.Position = Position;
            }

            /// <summary>
            /// The object name of the cash unit as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command.
            /// </summary>
            [DataMember(Name = "cashunit")] 
            public string Cashunit { get; private set; }
            /// <summary>
            /// The number of bills to be dispensed during the calibration process.
            /// </summary>
            [DataMember(Name = "numOfBills")] 
            public int? NumOfBills { get; private set; }
            /// <summary>
            /// Specifies where the dispensed items should be moved to.
            /// </summary>
            [DataMember(Name = "position")] 
            public object Position { get; private set; }

        }
    }
}
