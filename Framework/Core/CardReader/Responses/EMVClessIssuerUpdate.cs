/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessIssuerUpdate.cs uses automatically generated parts. 
 * EMVClessIssuerUpdate.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.EMVClessIssuerUpdate")]
	public sealed class EMVClessIssuerUpdate : Response<EMVClessIssuerUpdatePayload>
	{

		public EMVClessIssuerUpdate(string RequestId, EMVClessIssuerUpdatePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class EMVClessIssuerUpdatePayload : MessagePayload
	{


		public EMVClessIssuerUpdatePayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(EMVClessIssuerUpdatePayload)}");

		}

	}

}
