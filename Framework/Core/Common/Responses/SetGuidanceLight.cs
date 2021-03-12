/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetGuidanceLight.cs uses automatically generated parts. 
 * SetGuidanceLight.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Common.Responses
{


	[DataContract]
	[Response(Name = "Common.SetGuidanceLight")]
	public sealed class SetGuidanceLight : Response<SetGuidanceLightPayload>
	{

		public SetGuidanceLight(string RequestId, SetGuidanceLightPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SetGuidanceLightPayload : MessagePayload
	{


		public SetGuidanceLightPayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(SetGuidanceLightPayload)}");

		}

	}

}
