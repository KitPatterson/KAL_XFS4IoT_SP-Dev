/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetMediaList.cs uses automatically generated parts. 
 * GetMediaList.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = GetMediaList
	[DataContract]
	[Command(Name = "Printer.GetMediaList")]
	public sealed class GetMediaList : Command<GetMediaListPayload>
	{

		public GetMediaList(string RequestId, GetMediaListPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetMediaListPayload : MessagePayload
	{


		public GetMediaListPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
