/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetFormList.cs uses automatically generated parts. 
 * GetFormList.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = GetFormList
	[DataContract]
	[Command(Name = "Printer.GetFormList")]
	public sealed class GetFormList : Command<GetFormListPayload>
	{

		public GetFormList(string RequestId, GetFormListPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetFormListPayload : MessagePayload
	{


		public GetFormListPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
