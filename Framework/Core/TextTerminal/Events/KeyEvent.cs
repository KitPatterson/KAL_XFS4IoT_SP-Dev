/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * KeyEvent.cs uses automatically generated parts. 
 * KeyEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.TextTerminal.Events
{


	[DataContract]
	[Event(Name = "TextTerminal.KeyEvent")]
	public sealed class KeyEvent : Event<KeyEventPayload>
	{

		public KeyEvent(string RequestId, KeyEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class KeyEventPayload : MessagePayloadBase
	{


		public KeyEventPayload(string Key = null, string CommandKey = null)
			: base()
		{
			this.Key = Key;
			this.CommandKey = CommandKey;
		}

		/// <summary>
		///On a numeric or alphanumeric key press this parameter holds the value of the key pressed. This value is not set if no numeric or alphanumeric key was pressed.
		/// </summary>
		[DataMember(Name = "key")] 
		public string Key { get; private set; }
		/// <summary>
		///On a Command key press this parameter holds the value of the Command key pressed, e.g. ckEnter.This value is not set when no command key was pressed.
		/// </summary>
		[DataMember(Name = "commandKey")] 
		public string CommandKey { get; private set; }
	}

}
