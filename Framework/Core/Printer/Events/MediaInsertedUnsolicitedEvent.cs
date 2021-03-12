/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaInsertedUnsolicitedEvent.cs uses automatically generated parts. 
 * MediaInsertedUnsolicitedEvent.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

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
