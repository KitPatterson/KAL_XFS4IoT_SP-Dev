/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * Present_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashDispenser.Commands
{
    //Original name = Present
    [DataContract]
    [Command(Name = "CashDispenser.Present")]
    public sealed class PresentCommand : Command<PresentCommand.PayloadData>
    {
        public PresentCommand(int RequestId, PresentCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, PositionEnum? Position = null)
                : base(Timeout)
            {
                this.Position = Position;
            }

            public enum PositionEnum
            {
                OutDefault,
                OutLeft,
                OutRight,
                OutCenter,
                OutTop,
                OutBottom,
                OutFront,
                OutRear
            }

            /// <summary>
            /// Supplies the output position as one of the following values:
            /// 
            /// * ```outDefault``` - Default output position.
            /// * ```outLeft``` - Left output position.
            /// * ```outRight``` - Right output position.
            /// * ```outCenter``` - Center output position.
            /// * ```outTop``` - Top output position.
            /// * ```outBottom``` - Bottom output position.
            /// * ```outFront``` - Front output position.
            /// * ```outRear``` - Rear output position.
            /// </summary>
            [DataMember(Name = "position")]
            public PositionEnum? Position { get; init; }

        }
    }
}
