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
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.ReadTrack")]
	public sealed class ReadTrack : Response<ReadTrackPayload>
	{

		public ReadTrack(string RequestId, ReadTrackPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ReadTrackPayload : MessagePayload
	{


		public ReadTrackPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string TrackData = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ReadTrackPayload)}");

			this.TrackData = TrackData;
		}

		/// <summary>
		///The data read successfully from the selected tracks (and value of security module if available).
		/// </summary>
		[DataMember(Name = "trackData")] 
		public string TrackData { get; private set; }
	}

}
