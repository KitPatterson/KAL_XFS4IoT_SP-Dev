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
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.GetFormList")]
	public sealed class GetFormList : Response<GetFormListPayload>
	{

		public GetFormList(string RequestId, GetFormListPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetFormListPayload : MessagePayload
	{


		public GetFormListPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, List<string> FormList = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetFormListPayload)}");

			this.FormList = FormList;
		}

		/// <summary>
		///The list of form names.
		/// </summary>
		[DataMember(Name = "formList")] 
		public List<string> FormList{ get; private set; }
	}

}
