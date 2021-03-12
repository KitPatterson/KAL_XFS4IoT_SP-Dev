/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteTrack.cs uses automatically generated parts. 
 * WriteTrack.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = WriteTrack
	[DataContract]
	[Command(Name = "CardReader.WriteTrack")]
	public sealed class WriteTrack : Command<WriteTrackPayload>
	{

		public WriteTrack(string RequestId, WriteTrackPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class WriteTrackPayload : MessagePayload
	{

		public enum WriteMethodEnum
		{
			Loco,
			Hico,
			Auto,
		}


		public WriteTrackPayload(int Timeout, string FormName = null, string TrackData = null, WriteMethodEnum? WriteMethod = null)
			: base(Timeout)
		{
			this.FormName = FormName;
			this.TrackData = TrackData;
			this.WriteMethod = WriteMethod;
		}

		/// <summary>
		///The name of the form to be used.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///The track data to be written.
		/// </summary>
		[DataMember(Name = "trackData")] 
		public string TrackData { get; private set; }
		/// <summary>
		///Indicates whether a low coercivity or high coercivity magnetic stripe is being written as one of the following:**loco**
		////Low coercivity magnetic stripe is being written.**hico**
		//// High coercivity magnetic stripe is being written.**auto**
		////Service Provider will determine whether low or high coercivity stripe is to be written.
		/// </summary>
		[DataMember(Name = "writeMethod")] 
		public WriteMethodEnum? WriteMethod { get; private set; }
	}

}
