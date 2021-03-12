/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetTransactionState.cs uses automatically generated parts. 
 * GetTransactionState.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Common.Commands
{


	//Original name = GetTransactionState
	[DataContract]
	[Command(Name = "Common.GetTransactionState")]
	public sealed class GetTransactionState : Command<GetTransactionStatePayload>
	{

		public GetTransactionState(string RequestId, GetTransactionStatePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetTransactionStatePayload : MessagePayload
	{


		public GetTransactionStatePayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
