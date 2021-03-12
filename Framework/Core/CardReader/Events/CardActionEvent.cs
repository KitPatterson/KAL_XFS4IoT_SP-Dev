/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * CardActionEvent.cs uses automatically generated parts. 
 * CardActionEvent.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{


	[DataContract]
	[Event(Name = "CardReader.CardActionEvent")]
	public sealed class CardActionEvent : Event<CardActionEventPayload>
	{

		public CardActionEvent(string RequestId, CardActionEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class CardActionEventPayload : MessagePayloadBase
	{

		public enum ActionEnum
		{
			Retained,
			Ejected,
			ReadPosition,
		}

		public enum PositionEnum
		{
			Unknown,
			Present,
			Entering,
		}


		public CardActionEventPayload(ActionEnum? Action = null, PositionEnum? Position = null)
			: base()
		{
			this.Action = Action;
			this.Position = Position;
		}

		/// <summary>
		///Specifies which action has been performed with the card. Possible values are:**retained**
		////The card has been retained.**ejected**
		////The card has been ejected.**readPosition**
		////The card has been moved to the read position.
		/// </summary>
		[DataMember(Name = "action")] 
		public ActionEnum? Action { get; private set; }
		/// <summary>
		///Position of card before being retained or ejected. Possible values are:**unknown**
		////The position of the card cannot be determined.**present**
		////The card was present in the reader.**entering**
		////The card was entering the reader.
		/// </summary>
		[DataMember(Name = "position")] 
		public PositionEnum? Position { get; private set; }
	}

}
