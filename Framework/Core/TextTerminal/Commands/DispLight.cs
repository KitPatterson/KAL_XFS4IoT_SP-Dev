/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DispLight.cs uses automatically generated parts. 
 * DispLight.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = DispLight
	[DataContract]
	[Command(Name = "TextTerminal.DispLight")]
	public sealed class DispLight : Command<DispLightPayload>
	{

		public DispLight(string RequestId, DispLightPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DispLightPayload : MessagePayload
	{


		public DispLightPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
