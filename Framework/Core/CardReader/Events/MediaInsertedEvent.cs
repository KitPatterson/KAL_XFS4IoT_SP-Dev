/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * MediaInsertedEvent.cs uses automatically generated parts. 
 * MediaInsertedEvent.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{


	[DataContract]
	[Event(Name = "CardReader.MediaInsertedEvent")]
	public sealed class MediaInsertedEvent : Event<MessagePayloadBase>
	{

		public MediaInsertedEvent(string RequestId)
			: base(RequestId)
		{ }

	}
}
