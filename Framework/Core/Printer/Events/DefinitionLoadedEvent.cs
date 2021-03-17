/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * DefinitionLoadedEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{

    [DataContract]
    [Event(Name = "Printer.DefinitionLoadedEvent")]
    public sealed class DefinitionLoadedEvent : Event<DefinitionLoadedEvent.PayloadData>
    {

        public DefinitionLoadedEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public enum TypeEnum
            {
                Form,
                Name,
            }


            public PayloadData(string Name = null, TypeEnum? Type = null)
                : base()
            {
                this.Name = Name;
                this.Type = Type;
            }

            /// <summary>
            ///Specifies the name of the form or media just loaded.
            /// </summary>
            [DataMember(Name = "name")] 
            public string Name { get; private set; }
            /// <summary>
            ///Specifies the type of definition loaded. This field can be one of the following values:**form**
            ////   The form identified by *name* has been loaded.**media**
            ////  The media identified by *name* has been loaded.
            /// </summary>
            [DataMember(Name = "type")] 
            public TypeEnum? Type { get; private set; }
        }

    }
}
