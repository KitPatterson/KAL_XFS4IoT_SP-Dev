/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Keyboard interface.
 * GetLayout_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Keyboard.Commands
{
    //Original name = GetLayout
    [DataContract]
    [Command(Name = "Keyboard.GetLayout")]
    public sealed class GetLayoutCommand : Command<GetLayoutCommand.PayloadData>
    {
        public GetLayoutCommand(int RequestId, GetLayoutCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, EntryModeEnum? EntryMode = null)
                : base(Timeout)
            {
                this.EntryMode = EntryMode;
            }

            public enum EntryModeEnum
            {
                Data,
                Pin,
                Secure,
                All
            }

            /// <summary>
            /// Specifies entry mode to be returned. The following values are possible:
            /// 
            /// * ```data``` - The layout is to be applied to the DataEntry command.
            /// * ```pin``` - The layout is to be applied to the PinEntry command.
            /// * ```secure``` - The layout is to be applied to the SecureKeyEntry command.
            /// * ```all``` - The layout for all supported entry commands.
            /// </summary>
            [DataMember(Name = "entryMode")]
            public EntryModeEnum? EntryMode { get; init; }

        }
    }
}
