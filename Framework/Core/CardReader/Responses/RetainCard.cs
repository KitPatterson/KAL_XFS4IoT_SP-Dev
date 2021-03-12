/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCard.cs uses automatically generated parts. 
 * RetainCard.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.RetainCard")]
	public sealed class RetainCard : Response<RetainCardPayload>
	{

		public RetainCard(string RequestId, RetainCardPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class RetainCardPayload : MessagePayload
	{

		public enum PositionEnum
		{
			Unknown,
			Present,
			Entering,
		}


		public RetainCardPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, int? Count = null, PositionEnum? Position = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(RetainCardPayload)}");

			this.Count = Count;
			this.Position = Position;
		}

		/// <summary>
		///Total number of ID cards retained up to and including this operation, since the last ResetCount command was executed.
		/// </summary>
		[DataMember(Name = "count")] 
		public int? Count { get; private set; }
		/// <summary>
		///Position of card; only relevant if card could not be retained. Possible positions:**unknown**
		////The position of the card cannot be determined with the device in its current state.**present**
		////The card is present in the reader.**entering**
		////The card is in the entering position (shutter).
		/// </summary>
		[DataMember(Name = "position")] 
		public PositionEnum? Position { get; private set; }
	}

}
