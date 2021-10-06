/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * Reset_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{
    //Original name = Reset
    [DataContract]
    [Command(Name = "CardReader.Reset")]
    public sealed class ResetCommand : Command<ResetCommand.PayloadData>
    {
        public ResetCommand(int RequestId, ResetCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, ToEnum? To = null, string StorageId = null)
                : base(Timeout)
            {
                this.To = To;
                this.StorageId = StorageId;
            }

            public enum ToEnum
            {
                Exit,
                Retain,
                Transport
            }

            /// <summary>
            /// Specifies where any user card found within the card reader should be moved to as one of the following:
            /// 
            /// * ```exit``` - Move the card to the exit position. If the card is already at the exit, it may be moved
            ///   to ensure it is in the correct position to be taken.
            /// * ```retain``` - Move the card to a retain storage unit.
            /// * ```transport``` - Move the card to the transport or keep the card in the transport. If the card is 
            ///   already in the transport, it may be moved to verify it is not jammed.
            /// <example>retain</example>
            /// </summary>
            [DataMember(Name = "to")]
            public ToEnum? To { get; init; }

            /// <summary>
            /// If the card is to be moved to a retain storage unit, this indicates the retain storage unit to which
            /// the card should be be moved. If omitted, the service will select the retain storage unit based on the
            /// number of retain storage units available and service specific configuration.
            /// <example>retn1</example>
            /// </summary>
            [DataMember(Name = "storageId")]
            [DataTypes(Pattern = @"^.{1,5}$")]
            public string StorageId { get; init; }

        }
    }
}
