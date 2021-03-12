/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * Status.cs uses automatically generated parts. 
 * Status.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Common.Commands
{


	//Original name = Status
	[DataContract]
	[Command(Name = "Common.Status")]
	public sealed class Status : Command<StatusPayload>
	{

		public Status(string RequestId, StatusPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class StatusPayload : MessagePayload
	{


		public StatusPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
