/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaDetectedEvent.cs uses automatically generated parts. 
 * MediaDetectedEvent.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.MediaDetectedEvent")]
	public sealed class MediaDetectedEvent : Event<MediaDetectedEventPayload>
	{

		public MediaDetectedEvent(string RequestId, MediaDetectedEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class MediaDetectedEventPayload : MessagePayloadBase
	{

		public enum PositionEnum
		{
			Retracted,
			Present,
			Entering,
			Jammed,
			Unknown,
			Expelled,
		}


		public MediaDetectedEventPayload(PositionEnum? Position = null, int? RetractBinNumber = null)
			: base()
		{
			this.Position = Position;
			this.RetractBinNumber = RetractBinNumber;
		}

		/// <summary>
		///Specifies the media position after the reset operation, as one of the following values:**retracted**
		////  The media was retracted during the reset operation.**present**
		////  The media is in the print position or on the stacker.**entering**
		////  The media is in the exit slot.**jammed**
		////  The media is jammed in the device.**unknown**
		////  The media is in an unknown position.**expelled**
		////  The media was expelled during the reset operation.
		/// </summary>
		[DataMember(Name = "position")] 
		public PositionEnum? Position { get; private set; }
		/// <summary>
		///Number of the retract bin the media was retracted to. This number has to be between one and the  [number of bins](#printer-capability-retractbins) supported by this device. It is only relevant if  *position* is *retracted*.
		/// </summary>
		[DataMember(Name = "retractBinNumber")] 
		public int? RetractBinNumber { get; private set; }
	}

}
