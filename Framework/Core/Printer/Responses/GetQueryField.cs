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
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.GetQueryField")]
	public sealed class GetQueryField : Response<GetQueryFieldPayload>
	{

		public GetQueryField(string RequestId, GetQueryFieldPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryFieldPayload : MessagePayload
	{

		/// <summary>
		///Details of the field(s) requested. For each object, the key is the field name.
		/// </summary>
		public class FieldsClass
		{

			public FieldsClass ()
			{
			}


		}


		public GetQueryFieldPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, FieldsClass Fields = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetQueryFieldPayload)}");

			this.Fields = Fields;
		}

		/// <summary>
		///Details of the field(s) requested. For each object, the key is the field name.
		/// </summary>
		[DataMember(Name = "fields")] 
		public FieldsClass Fields { get; private set; }
	}

}
