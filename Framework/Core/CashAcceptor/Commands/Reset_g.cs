/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * Reset_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashAcceptor.Commands
{
    //Original name = Reset
    [DataContract]
    [Command(Name = "CashAcceptor.Reset")]
    public sealed class ResetCommand : Command<ResetCommand.PayloadData>
    {
        public ResetCommand(string RequestId, ResetCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            /// This field is used if items are to be moved to internal areas of the device, including cash units, the intermediate stacker or the transport. 
            /// The field is only relevant if [cashunit](#cashacceptor.reset.command.properties.cashunit) is not defined.
            /// </summary>
            public class RetractAreaClass
            {
                public enum OutputPositionEnum
                {
                    Null,
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
                    Reject,
                    Transport,
                    Stacker,
                    BillCassettes,
                    CashIn,
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
                Null,
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
            /// 
            /// If items are to be moved to an output position, this value must be omitted, 
            /// [retractArea](#cashacceptor.reset.command.properties.retractarea) must be omitted and 
            /// [outputPosition](#cashacceptor.reset.command.properties.outputposition) specifies where items are to be 
            /// moved to.
            /// If this value is omitted and items are to be moved to internal areas of the device, *retractArea* specifies 
            /// where items are to be moved to or stored.
            /// </summary>
            [DataMember(Name = "cashunit")] 
            public string Cashunit { get; private set; }
            /// <summary>
            /// This field is used if items are to be moved to internal areas of the device, including cash units, the intermediate stacker or the transport. 
            /// The field is only relevant if [cashunit](#cashacceptor.reset.command.properties.cashunit) is not defined.
            /// </summary>
            [DataMember(Name = "retractArea")] 
            public object RetractArea { get; private set; }
            /// <summary>
            /// The output position to which items are to be moved. This field is only used if *number* is zero and *netractArea* is omitted. Following values are possible:
            /// 
            /// \"null\": Take the default configuration.
            /// 
            /// \"left\": Move items to the left output position.
            /// 
            /// \"right\": Move items to the right output position.
            /// 
            /// \"center\": Move items to the center output position.
            /// 
            /// \"top\": Move items to the top output position.
            /// 
            /// \"bottom\": Move items to the bottom output position.
            /// 
            /// \"front\": Move items to the front output position.
            /// 
            /// \"rear\": Move items to the rear output position.
            /// </summary>
            [DataMember(Name = "outputPosition")] 
            public OutputPositionEnum? OutputPosition { get; private set; }

        }
    }
}
