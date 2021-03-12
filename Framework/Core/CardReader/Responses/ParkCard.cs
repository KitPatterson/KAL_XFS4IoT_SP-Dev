/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ParkCard.cs uses automatically generated parts. 
 * ParkCard.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.ParkCard")]
	public sealed class ParkCard : Response<ParkCardPayload>
	{

		public ParkCard(string RequestId, ParkCardPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ParkCardPayload : MessagePayload
	{


		public ParkCardPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, int Timeout)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ParkCardPayload)}");

			this.Timeout = Timeout;
		}

		/// <summary>
		///Timeout in milliseconds for the command to complete. If set to zero, the command will not timeout but can be cancelled.
		/// </summary>
		[DataMember(Name = "timeout")] 
		public int? Timeout { get; private set; }
	}

}
