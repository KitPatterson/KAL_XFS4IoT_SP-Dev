/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * TonerThresholdEvent.cs uses automatically generated parts. 
 * TonerThresholdEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.TonerThresholdEvent")]
	public sealed class TonerThresholdEvent : Event<TonerThresholdEventPayload>
	{

		public TonerThresholdEvent(string RequestId, TonerThresholdEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class TonerThresholdEventPayload : MessagePayloadBase
	{

		public enum StateEnum
		{
			Full,
			Low,
			Out,
		}


		public TonerThresholdEventPayload(StateEnum? State = null)
			: base()
		{
			this.State = State;
		}

		/// <summary>
		///Specifies the current state of the toner (or ink) as one of the following:**full**
		////  The toner (or ink) in the printer is in a good state.**low**
		////  The toner (or ink) in the printer is low.**out**
		////  The toner (or ink) in the printer is out.
		/// </summary>
		[DataMember(Name = "state")] 
		public StateEnum? State { get; private set; }
	}

}
