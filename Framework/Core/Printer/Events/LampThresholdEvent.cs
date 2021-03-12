/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * LampThresholdEvent.cs uses automatically generated parts. 
 * LampThresholdEvent.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.LampThresholdEvent")]
	public sealed class LampThresholdEvent : Event<LampThresholdEventPayload>
	{

		public LampThresholdEvent(string RequestId, LampThresholdEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class LampThresholdEventPayload : MessagePayloadBase
	{

		public enum StateEnum
		{
			Ok,
			Fading,
			Inop,
		}


		public LampThresholdEventPayload(StateEnum? State = null)
			: base()
		{
			this.State = State;
		}

		/// <summary>
		///Specifies the current state of the imaging lamp as one of the following values:**ok**
		////  The imaging lamp is in a good state.**fading**
		////  The imaging lamp is fading and should be changed.**inop**
		////  The imaging lamp is inoperative.
		/// </summary>
		[DataMember(Name = "state")] 
		public StateEnum? State { get; private set; }
	}

}
