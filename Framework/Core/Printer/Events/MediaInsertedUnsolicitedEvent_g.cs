/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaInsertedUnsolicitedEvent_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{

    [DataContract]
    [Event(Name = "Printer.MediaInsertedUnsolicitedEvent")]
    public sealed class MediaInsertedUnsolicitedEvent : Event<MessagePayloadBase>
    {

        public MediaInsertedUnsolicitedEvent(string RequestId)
            : base(RequestId)
        { }

    }
}
