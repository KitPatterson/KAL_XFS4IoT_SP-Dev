/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * DefinitionLoadedEvent.cs uses automatically generated parts. 
 * DefinitionLoadedEvent.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.DefinitionLoadedEvent")]
	public sealed class DefinitionLoadedEvent : Event<DefinitionLoadedEventPayload>
	{

		public DefinitionLoadedEvent(string RequestId, DefinitionLoadedEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DefinitionLoadedEventPayload : MessagePayloadBase
	{

		public enum TypeEnum
		{
			Form,
			Name,
		}


		public DefinitionLoadedEventPayload(string Name = null, TypeEnum? Type = null)
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
