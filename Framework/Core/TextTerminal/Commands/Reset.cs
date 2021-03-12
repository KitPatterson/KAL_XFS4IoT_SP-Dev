/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * Reset.cs uses automatically generated parts. 
 * Reset.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = Reset
	[DataContract]
	[Command(Name = "TextTerminal.Reset")]
	public sealed class Reset : Command<ResetPayload>
	{

		public Reset(string RequestId, ResetPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ResetPayload : MessagePayload
	{


		public ResetPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
