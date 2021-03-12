/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipPower.cs uses automatically generated parts. 
 * ChipPower.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.ChipPower")]
	public sealed class ChipPower : Response<ChipPowerPayload>
	{

		public ChipPower(string RequestId, ChipPowerPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ChipPowerPayload : MessagePayload
	{


		public ChipPowerPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string ChipData = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ChipPowerPayload)}");

			this.ChipData = ChipData;
		}

		/// <summary>
		///The Base64 encoded data received from the chip.
		/// </summary>
		[DataMember(Name = "chipData")] 
		public string ChipData { get; private set; }
	}

}
