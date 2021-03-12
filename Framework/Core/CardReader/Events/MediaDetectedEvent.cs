/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * MediaDetectedEvent.cs uses automatically generated parts. 
 * MediaDetectedEvent.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CardReader.Events
{


	[DataContract]
	[Event(Name = "CardReader.MediaDetectedEvent")]
	public sealed class MediaDetectedEvent : Event<MediaDetectedEventPayload>
	{

		public MediaDetectedEvent(string RequestId, MediaDetectedEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class MediaDetectedEventPayload : MessagePayloadBase
	{

		public enum ResetOutEnum
		{
			Ejected,
			Retained,
			ReadPosition,
			Jammed,
		}


		public MediaDetectedEventPayload(ResetOutEnum? ResetOut = null)
			: base()
		{
			this.ResetOut = ResetOut;
		}

		/// <summary>
		///Specifies the action that was performed on any card found within the IDC as one of the following:**ejected**
		////The card was ejected.**retained**
		////The card was retained.**readPosition**
		////The card is in read position.**jammed**
		////The card is jammed in the device.
		/// </summary>
		[DataMember(Name = "resetOut")] 
		public ResetOutEnum? ResetOut { get; private set; }
	}

}
