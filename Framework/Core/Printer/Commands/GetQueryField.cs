/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryField.cs uses automatically generated parts. 
 * GetQueryField.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = GetQueryField
	[DataContract]
	[Command(Name = "Printer.GetQueryField")]
	public sealed class GetQueryField : Command<GetQueryFieldPayload>
	{

		public GetQueryField(string RequestId, GetQueryFieldPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryFieldPayload : MessagePayload
	{


		public GetQueryFieldPayload(int Timeout, string FormName = null, string FieldName = null)
			: base(Timeout)
		{
			this.FormName = FormName;
			this.FieldName = FieldName;
		}

		/// <summary>
		///The form name.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///The name of the field about which to retrieve details. If omitted, then details are retrieved for all fields on the form.
		/// </summary>
		[DataMember(Name = "fieldName")] 
		public string FieldName { get; private set; }
	}

}
