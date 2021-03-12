/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EjectCard.cs uses automatically generated parts. 
 * EjectCard.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = EjectCard
	[DataContract]
	[Command(Name = "CardReader.EjectCard")]
	public sealed class EjectCard : Command<EjectCardPayload>
	{

		public EjectCard(string RequestId, EjectCardPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class EjectCardPayload : MessagePayload
	{

		public enum EjectPositionEnum
		{
			ExitPosition,
			TransportPosition,
		}


		public EjectCardPayload(int Timeout, EjectPositionEnum? EjectPosition = null)
			: base(Timeout)
		{
			this.EjectPosition = EjectPosition;
		}

		/// <summary>
		///Specifies the destination of the card ejection for motorized card readers. Possible values are one of the following:**exitPosition**
		//// The card will be transferred to the exit slot from where the  user can remove it. In the case of a latched dip the card will be unlatched, enabling removal.**transportPosition**
		////The card will be transferred to the transport just  behind the exit slot. If a card is already at this position then ok will be  returned. Another EjectCard command is required with the wEjectPosition set to exitPosition in order to present the card to the user for removal.
		/// </summary>
		[DataMember(Name = "ejectPosition")] 
		public EjectPositionEnum? EjectPosition { get; private set; }
	}

}
