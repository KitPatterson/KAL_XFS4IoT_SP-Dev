/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryForm.cs uses automatically generated parts. 
 * QueryForm.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = QueryForm
	[DataContract]
	[Command(Name = "CardReader.QueryForm")]
	public sealed class QueryForm : Command<QueryFormPayload>
	{

		public QueryForm(string RequestId, QueryFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class QueryFormPayload : MessagePayload
	{


		public QueryFormPayload(int Timeout, string FormName = null)
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
