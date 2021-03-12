/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RetractMedia.cs uses automatically generated parts. 
 * RetractMedia.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = RetractMedia
	[DataContract]
	[Command(Name = "Printer.RetractMedia")]
	public sealed class RetractMedia : Command<RetractMediaPayload>
	{

		public RetractMedia(string RequestId, RetractMediaPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RetractMediaPayload : MessagePayload
	{


		public RetractMediaPayload(int Timeout, int? BinNumber = null)
			: base(Timeout)
		{
			this.BinNumber = BinNumber;
		}

		/// <summary>
		///This number has to be between one and the number of bins supported by this device. If omitted, the media will be retracted to the transport. After it has been retracted to the transport, in a subsequent operation the media can be ejected again, or retracted to one of the retract bins.
		/// </summary>
		[DataMember(Name = "binNumber")] 
		public int? BinNumber { get; private set; }
	}

}
