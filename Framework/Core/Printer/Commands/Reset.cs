/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * Reset.cs uses automatically generated parts. 
 * Reset.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = Reset
	[DataContract]
	[Command(Name = "Printer.Reset")]
	public sealed class Reset : Command<ResetPayload>
	{

		public Reset(string RequestId, ResetPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ResetPayload : MessagePayload
	{

		public enum MediaControlEnum
		{
			Eject,
			Retract,
			Expel,
		}


		public ResetPayload(int Timeout, MediaControlEnum? MediaControl = null, int? RetractBinNumber = null)
			: base(Timeout)
		{
			this.MediaControl = MediaControl;
			this.RetractBinNumber = RetractBinNumber;
		}

		/// <summary>
		///Specifies the manner in which the media should be handled, as one of the following:**eject**
		////  Eject the media.**retract**
		////  Retract the media to retract bin number specified.**expel**
		////  Throw the media out of the exit slot.
		/// </summary>
		[DataMember(Name = "mediaControl")] 
		public MediaControlEnum? MediaControl { get; private set; }
		/// <summary>
		///Number of the retract bin the media is retracted to. This number has to be between one and the  [number of bins](#printer-capability-retractbins) supported by this device. It is only relevant if  *mediaControl* is *retract*.
		/// </summary>
		[DataMember(Name = "retractBinNumber")] 
		public int? RetractBinNumber { get; private set; }
	}

}
