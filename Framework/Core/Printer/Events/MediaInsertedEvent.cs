/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaInsertedEvent.cs uses automatically generated parts. 
 * MediaInsertedEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.MediaInsertedEvent")]
	public sealed class MediaInsertedEvent : Event<MessagePayloadBase>
	{

		public MediaInsertedEvent(string RequestId)
			: base(RequestId)
		{ }

	}
}
