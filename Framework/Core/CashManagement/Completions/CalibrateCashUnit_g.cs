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
using XFS4IoT.Completions;

namespace XFS4IoT.CashManagement.Completions
{
    [DataContract]
    [Completion(Name = "CashManagement.CalibrateCashUnit")]
    public sealed class CalibrateCashUnitCompletion : Completion<CalibrateCashUnitCompletion.PayloadData>
    {
        public CalibrateCashUnitCompletion(string RequestId, CalibrateCashUnitCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                CashUnitError,
                UnsupportedPosition,
                ExchangeActive,
                InvalidCashUnit,
            }

            /// <summary>
            /// Specifies where the items were moved to during the calibration process.
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


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string Cashunit = null, int? NumOfBills = null, PositionClass Position = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Cashunit = Cashunit;
                this.NumOfBills = NumOfBills;
                this.Position = Position;
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```cashUnitError``` - A cash unit caused an error. A CashManagement.CashUnitErrorEvent will be sent with the details.
            /// * ```unsupportedPosition``` - The position specified is not valid.
            /// * ```exchangeActive``` - The device is in an exchange state.
            /// * ```invalidCashUnit``` - The cash unit number specified is not valid.
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }
            /// <summary>
            /// The object name of the cash unit which has been calibrated as stated by the 
            /// [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) command.
            /// </summary>
            [DataMember(Name = "cashunit")] 
            public string Cashunit { get; private set; }
            /// <summary>
            /// Number of items that were actually dispensed during the calibration process. This value may be different from that 
            /// passed in using the input structure if the cash dispenser always dispenses a default number of bills. 
            /// When bills are presented to an output position this is the count of notes presented to the output position, 
            /// any other notes rejected during the calibration process are not included in this count as they will be accounted for within the cash unit counts.
            /// </summary>
            [DataMember(Name = "numOfBills")] 
            public int? NumOfBills { get; private set; }
            /// <summary>
            /// Specifies where the items were moved to during the calibration process.
            /// </summary>
            [DataMember(Name = "position")] 
            public PositionClass Position { get; private set; }

        }
    }
}
