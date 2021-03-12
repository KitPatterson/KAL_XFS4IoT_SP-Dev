/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCard.cs uses automatically generated parts. 
 * RetainCard.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = RetainCard
	[DataContract]
	[Command(Name = "CardReader.RetainCard")]
	public sealed class RetainCard : Command<RetainCardPayload>
	{

		public RetainCard(string RequestId, RetainCardPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RetainCardPayload : MessagePayload
	{


		public RetainCardPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
