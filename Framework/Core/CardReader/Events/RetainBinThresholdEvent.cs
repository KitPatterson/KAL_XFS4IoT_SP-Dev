/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainBinThresholdEvent.cs uses automatically generated parts. 
 * RetainBinThresholdEvent.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{


	[DataContract]
	[Event(Name = "CardReader.RetainBinThresholdEvent")]
	public sealed class RetainBinThresholdEvent : Event<RetainBinThresholdEventPayload>
	{

		public RetainBinThresholdEvent(string RequestId, RetainBinThresholdEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RetainBinThresholdEventPayload : MessagePayloadBase
	{

		public enum StateEnum
		{
			Ok,
			Full,
			High,
		}


		public RetainBinThresholdEventPayload(StateEnum? State = null)
			: base()
		{
			this.State = State;
		}

		/// <summary>
		///Specifies the state of the ID card unit retain bin as one of the following values: **ok**
		////The retain bin of the ID card unit was emptied.**full**
		////The retain bin of the ID card unit is full. **high**
		////The retain bin of the ID card unit is nearly full.\
		/// </summary>
		[DataMember(Name = "state")] 
		public StateEnum? State { get; private set; }
	}

}
