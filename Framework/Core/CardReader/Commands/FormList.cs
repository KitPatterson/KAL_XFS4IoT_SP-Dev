/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * FormList.cs uses automatically generated parts. 
 * FormList.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = FormList
	[DataContract]
	[Command(Name = "CardReader.FormList")]
	public sealed class FormList : Command<FormListPayload>
	{

		public FormList(string RequestId, FormListPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class FormListPayload : MessagePayload
	{


		public FormListPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
