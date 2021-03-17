/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * FieldWarningEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.TextTerminal.Events
{

    [DataContract]
    [Event(Name = "TextTerminal.FieldWarningEvent")]
    public sealed class FieldWarningEvent : Event<MessagePayloadBase>
    {

        public FieldWarningEvent(string RequestId)
            : base(RequestId)
        { }

    }
}
