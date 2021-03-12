/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EjectCard.cs uses automatically generated parts. 
 * EjectCard.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.EjectCard")]
	public sealed class EjectCard : Response<EjectCardPayload>
	{

		public EjectCard(string RequestId, EjectCardPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class EjectCardPayload : MessagePayload
	{


		public EjectCardPayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(EjectCardPayload)}");

		}

	}

}
