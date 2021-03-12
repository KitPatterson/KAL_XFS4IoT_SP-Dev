/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ReadForm.cs uses automatically generated parts. 
 * ReadForm.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.ReadForm")]
	public sealed class ReadForm : Response<ReadFormPayload>
	{

		public ReadForm(string RequestId, ReadFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ReadFormPayload : MessagePayload
	{

		/// <summary>
		///An object containing one or more key/value pairs where the key is a field name and the value is the field value. If the field is an index field, the key must be specified as **fieldname[index]** where index specifies the zero-based element of the index field. The field names and values can contain UNICODE if supported by the service.
		/// </summary>
		public class FieldsClass
		{

			public FieldsClass ()
			{
			}


		}


		public ReadFormPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, FieldsClass Fields = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ReadFormPayload)}");

			this.Fields = Fields;
		}

		/// <summary>
		///An object containing one or more key/value pairs where the key is a field name and the value is the field value. If the field is an index field, the key must be specified as **fieldname[index]** where index specifies the zero-based element of the index field. The field names and values can contain UNICODE if supported by the service.
		/// </summary>
		[DataMember(Name = "fields")] 
		public FieldsClass Fields { get; private set; }
	}

}
