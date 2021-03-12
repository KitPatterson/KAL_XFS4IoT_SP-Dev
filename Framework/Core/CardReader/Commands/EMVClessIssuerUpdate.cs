/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessIssuerUpdate.cs uses automatically generated parts. 
 * EMVClessIssuerUpdate.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = EMVClessIssuerUpdate
	[DataContract]
	[Command(Name = "CardReader.EMVClessIssuerUpdate")]
	public sealed class EMVClessIssuerUpdate : Command<EMVClessIssuerUpdatePayload>
	{

		public EMVClessIssuerUpdate(string RequestId, EMVClessIssuerUpdatePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class EMVClessIssuerUpdatePayload : MessagePayload
	{


		public EMVClessIssuerUpdatePayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
