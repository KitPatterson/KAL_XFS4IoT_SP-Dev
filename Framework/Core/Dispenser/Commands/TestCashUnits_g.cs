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
using XFS4IoT.Commands;

namespace XFS4IoT.Dispenser.Commands
{
    //Original name = TestCashUnits
    [DataContract]
    [Command(Name = "Dispenser.TestCashUnits")]
    public sealed class TestCashUnitsCommand : Command<TestCashUnitsCommand.PayloadData>
    {
        public TestCashUnitsCommand(string RequestId, TestCashUnitsCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
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


            public PayloadData(int Timeout, string Cashunit = null, object RetractArea = null, OutputPositionEnum? OutputPosition = null)
                : base(Timeout)
            {
                this.Cashunit = Cashunit;
                this.RetractArea = RetractArea;
                this.OutputPosition = OutputPosition;
            }

            /// <summary>
            /// If defined, this value specifies the object name (as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command) of the single cash unit to 
            /// be used for the storage of any items found.
            /// </summary>
            [DataMember(Name = "cashunit")] 
            public string Cashunit { get; private set; }
            /// <summary>
            /// This field is used if items are to be moved to internal areas of the device, including cash units, the intermediate stacker, or the transport.
            /// </summary>
            [DataMember(Name = "retractArea")] 
            public object RetractArea { get; private set; }
            /// <summary>
            /// The output position to which items are to be moved. This field is only used if *number* is zero and retractArea is omitted.
            /// Following values are possible:
            /// 
            /// * ```default``` - The default configuration.
            /// * ```left``` - The left output position.
            /// * ```right``` - The right output position.
            /// * ```center``` - The center output position.
            /// * ```top``` - The top output position.
            /// * ```bottom``` - The bottom output position.
            /// * ```front``` - The front output position.
            /// * ```rear``` - The rear output position.
            /// </summary>
            [DataMember(Name = "outputPosition")] 
            public OutputPositionEnum? OutputPosition { get; private set; }

        }
    }
}
