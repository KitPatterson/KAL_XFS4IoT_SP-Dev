/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * InkThresholdEvent.cs uses automatically generated parts. 
 * InkThresholdEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.InkThresholdEvent")]
	public sealed class InkThresholdEvent : Event<InkThresholdEventPayload>
	{

		public InkThresholdEvent(string RequestId, InkThresholdEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class InkThresholdEventPayload : MessagePayloadBase
	{

		public enum StateEnum
		{
			Full,
			Low,
			Out,
		}


		public InkThresholdEventPayload(StateEnum? State = null)
			: base()
		{
			this.State = State;
		}

		/// <summary>
		///Specifies the current state of the stamping ink as one of the following:**full**
		////  The stamping ink in the printer is in a good state.**low**
		////  The stamping ink in the printer is low.**out**
		////  The stamping ink in the printer is out.
		/// </summary>
		[DataMember(Name = "state")] 
		public StateEnum? State { get; private set; }
	}

}
