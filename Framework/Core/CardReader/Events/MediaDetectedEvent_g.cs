/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * MediaDetectedEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{

    [DataContract]
    [Event(Name = "CardReader.MediaDetectedEvent")]
    public sealed class MediaDetectedEvent : Event<MediaDetectedEvent.PayloadData>
    {

        public MediaDetectedEvent(int RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public PayloadData(string Position = null)
                : base()
            {
                this.Position = Position;
            }

            /// <summary>
            /// Specifies where the card was moved to or if it is jammed as one of the following:
            /// 
            /// * ```exit``` - The card was moved to the exit position.
            /// * ```transport``` - The card was moved to the transport position.
            /// * ```&lt;storage unit identifier&gt;``` - The card was moved to the storage unit with matching
            ///   [identifier](#storage.getstorage.completion.properties.storage.unit1). The storage unit type must be
            ///   either *retain*.
            /// * ```jammed``` - The card is jammed in the device.
            /// <example>retn1</example>
            /// </summary>
            [DataMember(Name = "position")]
            [DataTypes(Pattern = @"^exit$|^transport$|^.{1,5}$")]
            public string Position { get; init; }

        }

    }
}
