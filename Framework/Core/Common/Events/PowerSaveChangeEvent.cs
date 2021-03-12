/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * PowerSaveChangeEvent.cs uses automatically generated parts. 
 * PowerSaveChangeEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Common.Events
{


	[DataContract]
	[Event(Name = "Common.PowerSaveChangeEvent")]
	public sealed class PowerSaveChangeEvent : Event<PowerSaveChangeEventPayload>
	{

		public PowerSaveChangeEvent(string RequestId, PowerSaveChangeEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class PowerSaveChangeEventPayload : MessagePayloadBase
	{


		public PowerSaveChangeEventPayload(int? PowerSaveRecoveryTime = null)
			: base()
		{
			this.PowerSaveRecoveryTime = PowerSaveRecoveryTime;
		}

		/// <summary>
		///Specifies the actual number of seconds required by the device to resume its normal operational state. This value is zero if the device exited the power saving mode
		/// </summary>
		[DataMember(Name = "powerSaveRecoveryTime")] 
		public int? PowerSaveRecoveryTime { get; private set; }
	}

}
