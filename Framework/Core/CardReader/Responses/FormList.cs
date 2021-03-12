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
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.FormList")]
	public sealed class FormList : Response<FormListPayload>
	{

		public FormList(string RequestId, FormListPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class FormListPayload : MessagePayload
	{


		public FormListPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, List<string> Forms = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(FormListPayload)}");

			this.Forms = Forms;
		}

		/// <summary>
		///The available forms
		/// </summary>
		[DataMember(Name = "forms")] 
		public List<string> Forms{ get; private set; }
	}

}
