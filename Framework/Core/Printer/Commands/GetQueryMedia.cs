/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryMedia.cs uses automatically generated parts. 
 * GetQueryMedia.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = GetQueryMedia
	[DataContract]
	[Command(Name = "Printer.GetQueryMedia")]
	public sealed class GetQueryMedia : Command<GetQueryMediaPayload>
	{

		public GetQueryMedia(string RequestId, GetQueryMediaPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryMediaPayload : MessagePayload
	{


		public GetQueryMediaPayload(int Timeout, string MediaName = null)
			: base(Timeout)
		{
			this.MediaName = MediaName;
		}

		/// <summary>
		///The media name for which to retrieve details.
		/// </summary>
		[DataMember(Name = "mediaName")] 
		public string MediaName { get; private set; }
	}

}
