/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipIO.cs uses automatically generated parts. 
 * ChipIO.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.ChipIO")]
	public sealed class ChipIO : Response<ChipIOPayload>
	{

		public ChipIO(string RequestId, ChipIOPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ChipIOPayload : MessagePayload
	{


		public ChipIOPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string ChipProtocol = null, string ChipData = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ChipIOPayload)}");

			this.ChipProtocol = ChipProtocol;
			this.ChipData = ChipData;
		}

		/// <summary>
		///Identifies the protocol that is used to communicate with the chip. This field contains the same value as the corresponding field in the payload. This field should be ignored in Memory Card dialogs and will contain WFS_IDC_NOTSUPP when returned for any Memory Card dialog.
		/// </summary>
		[DataMember(Name = "chipProtocol")] 
		public string ChipProtocol { get; private set; }
		/// <summary>
		///The Base64 encoded data received from the chip.
		/// </summary>
		[DataMember(Name = "chipData")] 
		public string ChipData { get; private set; }
	}

}
