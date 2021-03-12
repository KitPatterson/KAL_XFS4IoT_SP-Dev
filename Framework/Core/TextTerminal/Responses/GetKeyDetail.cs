/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetKeyDetail.cs uses automatically generated parts. 
 * GetKeyDetail.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.GetKeyDetail")]
	public sealed class GetKeyDetail : Response<GetKeyDetailPayload>
	{

		public GetKeyDetail(string RequestId, GetKeyDetailPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetKeyDetailPayload : MessagePayload
	{


		public GetKeyDetailPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string Keys = null, List<string> CommandKeys = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetKeyDetailPayload)}");

			this.Keys = Keys;
			this.CommandKeys = CommandKeys;
		}

		/// <summary>
		///String which holds the printable characters (numeric and alphanumeric keys) on the Text Terminal Unit, e.g. “0123456789ABCabc” if those text terminal input keys are present. This field is not set if no keys of this type are present on the device.
		/// </summary>
		[DataMember(Name = "keys")] 
		public string Keys { get; private set; }
		/// <summary>
		///Array of command keys on the Text Terminal Unit.
		/// </summary>
		[DataMember(Name = "commandKeys")] 
		public List<string> CommandKeys{ get; private set; }
	}

}
