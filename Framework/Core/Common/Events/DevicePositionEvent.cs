/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * DevicePositionEvent.cs uses automatically generated parts. 
 * DevicePositionEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Common.Events
{


	[DataContract]
	[Event(Name = "Common.DevicePositionEvent")]
	public sealed class DevicePositionEvent : Event<DevicePositionEventPayload>
	{

		public DevicePositionEvent(string RequestId, DevicePositionEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DevicePositionEventPayload : MessagePayloadBase
	{

		public enum PositionEnum
		{
			Inposition,
			Notinposition,
			Posunknown,
		}


		public DevicePositionEventPayload(PositionEnum? Position = null)
			: base()
		{
			this.Position = Position;
		}

		/// <summary>
		///Position of the device.
		/// </summary>
		[DataMember(Name = "position")] 
		public PositionEnum? Position { get; private set; }
	}

}
