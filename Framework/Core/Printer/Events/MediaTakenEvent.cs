/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaTakenEvent.cs uses automatically generated parts. 
 * MediaTakenEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.MediaTakenEvent")]
	public sealed class MediaTakenEvent : Event<MessagePayloadBase>
	{

		public MediaTakenEvent(string RequestId)
			: base(RequestId)
		{ }

	}
}
