/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaAutoRetractedEvent.cs uses automatically generated parts. 
 * MediaAutoRetractedEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.MediaAutoRetractedEvent")]
	public sealed class MediaAutoRetractedEvent : Event<MediaAutoRetractedEventPayload>
	{

		public MediaAutoRetractedEvent(string RequestId, MediaAutoRetractedEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class MediaAutoRetractedEventPayload : MessagePayloadBase
	{

		public enum RetractResultEnum
		{
			Ok,
			Jammed,
		}


		public MediaAutoRetractedEventPayload(RetractResultEnum? RetractResult = null, int? BinNumber = null)
			: base()
		{
			this.RetractResult = RetractResult;
			this.BinNumber = BinNumber;
		}

		/// <summary>
		///Specifies the result of the automatic retraction, as one of the following values:**ok**
		////  The media was retracted successfully.**jammed**
		////  The media is jammed.
		/// </summary>
		[DataMember(Name = "retractResult")] 
		public RetractResultEnum? RetractResult { get; private set; }
		/// <summary>
		///Number of the retract bin the media was retracted to or zero if the media is retracted to the transport. This number has to be between zero and the number of bins supported by this device. This value is also zero if *retractResult* is *jammed*.
		/// </summary>
		[DataMember(Name = "binNumber")] 
		public int? BinNumber { get; private set; }
	}

}
