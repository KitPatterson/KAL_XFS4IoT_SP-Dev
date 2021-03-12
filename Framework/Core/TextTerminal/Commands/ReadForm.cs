/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ReadForm.cs uses automatically generated parts. 
 * ReadForm.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = ReadForm
	[DataContract]
	[Command(Name = "TextTerminal.ReadForm")]
	public sealed class ReadForm : Command<ReadFormPayload>
	{

		public ReadForm(string RequestId, ReadFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ReadFormPayload : MessagePayload
	{


		public ReadFormPayload(int Timeout, string FormName = null, List<string> FieldNames = null)
			: base(Timeout)
		{
			this.FormName = FormName;
			this.FieldNames = FieldNames;
		}

		/// <summary>
		///Specifies the null-terminated name of the form
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///Specifies the field names from which to read input data. The fields are edited by the user in the order that the fields are specified within this parameter. If fieldNames value is not set, then data is read from all input fields on the form in the order they appear in the form file (independent of the field screen position).
		/// </summary>
		[DataMember(Name = "fieldNames")] 
		public List<string> FieldNames{ get; private set; }
	}

}
