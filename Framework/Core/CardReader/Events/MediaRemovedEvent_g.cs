/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * MediaRemovedEvent_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{

    [DataContract]
    [Event(Name = "CardReader.MediaRemovedEvent")]
    public sealed class MediaRemovedEvent : Event<MessagePayloadBase>
    {

        public MediaRemovedEvent(string RequestId)
            : base(RequestId)
        { }

    }
}
