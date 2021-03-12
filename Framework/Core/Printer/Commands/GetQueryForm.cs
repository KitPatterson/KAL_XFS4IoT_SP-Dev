/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryForm.cs uses automatically generated parts. 
 * GetQueryForm.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = GetQueryForm
	[DataContract]
	[Command(Name = "Printer.GetQueryForm")]
	public sealed class GetQueryForm : Command<GetQueryFormPayload>
	{

		public GetQueryForm(string RequestId, GetQueryFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryFormPayload : MessagePayload
	{


		public GetQueryFormPayload(int Timeout, string FormName = null)
			: base(Timeout)
		{
			this.FormName = FormName;
		}

		/// <summary>
		///The form name for which to retrieve details.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
	}

}
