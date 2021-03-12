/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RetractBinThresholdEvent.cs uses automatically generated parts. 
 * RetractBinThresholdEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.RetractBinThresholdEvent")]
	public sealed class RetractBinThresholdEvent : Event<RetractBinThresholdEventPayload>
	{

		public RetractBinThresholdEvent(string RequestId, RetractBinThresholdEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RetractBinThresholdEventPayload : MessagePayloadBase
	{

		public enum StateEnum
		{
			Ok,
			Full,
			High,
		}


		public RetractBinThresholdEventPayload(int? BinNumber = null, StateEnum? State = null)
			: base()
		{
			this.BinNumber = BinNumber;
			this.State = State;
		}

		/// <summary>
		///Number of the retract bin for which the status has changed.
		/// </summary>
		[DataMember(Name = "binNumber")] 
		public int? BinNumber { get; private set; }
		/// <summary>
		///Specifies the current state of the retract bin as one of the following:**ok**
		////  The retract bin of the printer is in a good state.**full**
		////  The retract bin of the printer is full.**high**
		////  The retract bin of the printer is high.
		/// </summary>
		[DataMember(Name = "state")] 
		public StateEnum? State { get; private set; }
	}

}
