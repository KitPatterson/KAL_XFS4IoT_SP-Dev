/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaPresentedUnsolicitedEvent.cs uses automatically generated parts. 
 * MediaPresentedUnsolicitedEvent.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.MediaPresentedUnsolicitedEvent")]
	public sealed class MediaPresentedUnsolicitedEvent : Event<MediaPresentedUnsolicitedEventPayload>
	{

		public MediaPresentedUnsolicitedEvent(string RequestId, MediaPresentedUnsolicitedEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class MediaPresentedUnsolicitedEventPayload : MessagePayloadBase
	{


		public MediaPresentedUnsolicitedEventPayload(int? WadIndex = null, int? TotalWads = null)
			: base()
		{
			this.WadIndex = WadIndex;
			this.TotalWads = TotalWads;
		}

		/// <summary>
		///Specifies the index (starting from one) of the presented wad, where a Wad is a bunch of one or more pages presented as a bunch.
		/// </summary>
		[DataMember(Name = "wadIndex")] 
		public int? WadIndex { get; private set; }
		/// <summary>
		///Specifies the total number of wads in the print job, zero if the total number of wads is not known.
		/// </summary>
		[DataMember(Name = "totalWads")] 
		public int? TotalWads { get; private set; }
	}

}
