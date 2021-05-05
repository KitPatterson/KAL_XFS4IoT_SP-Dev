/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * GetTellerInfo_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashManagement.Completions
{
    [DataContract]
    [Completion(Name = "CashManagement.GetTellerInfo")]
    public sealed class GetTellerInfoCompletion : Completion<GetTellerInfoCompletion.PayloadData>
    {
        public GetTellerInfoCompletion(string RequestId, GetTellerInfoCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                InvalidCurrency,
                InvalidTellerId,
            }

            [DataContract]
            public sealed class TellerDetailsClass
            {
                /// <summary>
                /// The input position assigned to the teller for cash entry. Following values are possible:
                /// 
                /// * ```none``` - No position is assigned to the teller.
                /// * ```left``` - Left position is assigned to the teller.
                /// * ```right``` - Right position is assigned to the teller.
                /// * ```center``` - Center position is assigned to the teller.
                /// * ```top``` - Top position is assigned to the teller.
                /// * ```bottom``` - Bottom position is assigned to the teller.
                /// * ```front``` - Front position is assigned to the teller.
                /// * ```rear``` - Rear position is assigned to the teller.
                /// </summary>
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

                /// <summary>
                /// The output position from which cash is presented to the teller. Following values are possible:
                /// 
                /// * ```none``` - No position is assigned to the teller.
                /// * ```left``` - Left position is assigned to the teller.
                /// * ```right``` - Right position is assigned to the teller.
                /// * ```center``` - Center position is assigned to the teller.
                /// * ```top``` - Top position is assigned to the teller.
                /// * ```bottom``` - Bottom position is assigned to the teller.
                /// * ```front``` - Front position is assigned to the teller.
                /// * ```rear``` - Rear position is assigned to the teller.
                /// </summary>
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

                public TellerDetailsClass(int? TellerID = null, InputPositionEnum? InputPosition = null, OutputPositionEnum? OutputPosition = null, object TellerTotals = null)
                    : base()
                {
                    this.TellerID = TellerID;
                    this.InputPosition = InputPosition;
                    this.OutputPosition = OutputPosition;
                    this.TellerTotals = TellerTotals;
                }

                /// <summary>
                /// Identification of the teller.
                /// </summary>
                [DataMember(Name = "tellerID")] 
                public int? TellerID { get; private set; }

                /// <summary>
                /// The input position assigned to the teller for cash entry. Following values are possible:
                /// 
                /// * ```none``` - No position is assigned to the teller.
                /// * ```left``` - Left position is assigned to the teller.
                /// * ```right``` - Right position is assigned to the teller.
                /// * ```center``` - Center position is assigned to the teller.
                /// * ```top``` - Top position is assigned to the teller.
                /// * ```bottom``` - Bottom position is assigned to the teller.
                /// * ```front``` - Front position is assigned to the teller.
                /// * ```rear``` - Rear position is assigned to the teller.
                /// </summary>
                [DataMember(Name = "inputPosition")] 
                public InputPositionEnum? InputPosition { get; private set; }

                /// <summary>
                /// The output position from which cash is presented to the teller. Following values are possible:
                /// 
                /// * ```none``` - No position is assigned to the teller.
                /// * ```left``` - Left position is assigned to the teller.
                /// * ```right``` - Right position is assigned to the teller.
                /// * ```center``` - Center position is assigned to the teller.
                /// * ```top``` - Top position is assigned to the teller.
                /// * ```bottom``` - Bottom position is assigned to the teller.
                /// * ```front``` - Front position is assigned to the teller.
                /// * ```rear``` - Rear position is assigned to the teller.
                /// </summary>
                [DataMember(Name = "outputPosition")] 
                public OutputPositionEnum? OutputPosition { get; private set; }

                /// <summary>
                /// List of teller total objects. There is one object per currency.
                /// </summary>
                [DataMember(Name = "tellerTotals")] 
                public object TellerTotals { get; private set; }

            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, List<TellerDetailsClass> TellerDetails = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.TellerDetails = TellerDetails;
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```invalidCurrency``` - Specified currency not currently available.
            /// * ```invalidTellerId``` - Invalid teller ID.
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }
            /// <summary>
            /// Array of teller detail objects.
            /// </summary>
            [DataMember(Name = "tellerDetails")] 
            public List<TellerDetailsClass> TellerDetails{ get; private set; }

        }
    }
}
