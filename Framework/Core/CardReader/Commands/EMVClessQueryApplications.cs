/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessQueryApplications.cs uses automatically generated parts. 
 * EMVClessQueryApplications.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = EMVClessQueryApplications
	[DataContract]
	[Command(Name = "CardReader.EMVClessQueryApplications")]
	public sealed class EMVClessQueryApplications : Command<EMVClessQueryApplicationsPayload>
	{

		public EMVClessQueryApplications(string RequestId, EMVClessQueryApplicationsPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class EMVClessQueryApplicationsPayload : MessagePayload
	{


		public EMVClessQueryApplicationsPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
