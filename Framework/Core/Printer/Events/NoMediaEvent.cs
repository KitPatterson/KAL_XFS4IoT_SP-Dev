/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * NoMediaEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{

    [DataContract]
    [Event(Name = "Printer.NoMediaEvent")]
    public sealed class NoMediaEvent : Event<NoMediaEvent.PayloadData>
    {

        public NoMediaEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {


            public PayloadData(string UserPrompt = null)
                : base()
            {
                this.UserPrompt = UserPrompt;
            }

            /// <summary>
            ///The user prompt from the form definition. This will be omitted if either a form does not define a value for the user prompt or the event is being generated as the result of a command that does not use forms.The application may use the this in any manner it sees fit, for example it might display the string to the operator, along with a message that the media should be inserted.
            /// </summary>
            [DataMember(Name = "userPrompt")] 
            public string UserPrompt { get; private set; }
        }

    }
}
