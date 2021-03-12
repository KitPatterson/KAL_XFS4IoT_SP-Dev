/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetKeyDetail.cs uses automatically generated parts. 
 * GetKeyDetail.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = GetKeyDetail
	[DataContract]
	[Command(Name = "TextTerminal.GetKeyDetail")]
	public sealed class GetKeyDetail : Command<GetKeyDetailPayload>
	{

		public GetKeyDetail(string RequestId, GetKeyDetailPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetKeyDetailPayload : MessagePayload
	{


		public GetKeyDetailPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
