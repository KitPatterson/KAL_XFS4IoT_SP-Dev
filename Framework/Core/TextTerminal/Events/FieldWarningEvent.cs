/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * FieldWarningEvent.cs uses automatically generated parts. 
 * FieldWarningEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

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
