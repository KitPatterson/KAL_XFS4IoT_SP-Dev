/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * Capabilities.cs uses automatically generated parts. 
 * Capabilities.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Common.Commands
{


	//Original name = Capabilities
	[DataContract]
	[Command(Name = "Common.Capabilities")]
	public sealed class Capabilities : Command<CapabilitiesPayload>
	{

		public Capabilities(string RequestId, CapabilitiesPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class CapabilitiesPayload : MessagePayload
	{


		public CapabilitiesPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
