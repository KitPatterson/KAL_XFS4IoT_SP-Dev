/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ResetCount.cs uses automatically generated parts. 
 * ResetCount.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = ResetCount
	[DataContract]
	[Command(Name = "CardReader.ResetCount")]
	public sealed class ResetCount : Command<ResetCountPayload>
	{

		public ResetCount(string RequestId, ResetCountPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ResetCountPayload : MessagePayload
	{


		public ResetCountPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
