/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ReadTrack.cs uses automatically generated parts. 
 * ReadTrack.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = ReadTrack
	[DataContract]
	[Command(Name = "CardReader.ReadTrack")]
	public sealed class ReadTrack : Command<ReadTrackPayload>
	{

		public ReadTrack(string RequestId, ReadTrackPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ReadTrackPayload : MessagePayload
	{


		public ReadTrackPayload(int Timeout, string FormName = null)
			: base(Timeout)
		{
			this.FormName = FormName;
		}

		/// <summary>
		///The name of the form that defines the behavior for the reading of tracks.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
	}

}
