/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetTransactionState.cs uses automatically generated parts. 
 * GetTransactionState.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Common.Responses
{


	[DataContract]
	[Response(Name = "Common.GetTransactionState")]
	public sealed class GetTransactionState : Response<GetTransactionStatePayload>
	{

		public GetTransactionState(string RequestId, GetTransactionStatePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetTransactionStatePayload : MessagePayload
	{

		public enum StateEnum
		{
			Active,
			Inactive,
		}


		public GetTransactionStatePayload(CompletionCodeEnum CompletionCode, string ErrorDescription, StateEnum? State = null, string TransactionID = null, List<string> Extra = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetTransactionStatePayload)}");

			this.State = State;
			this.TransactionID = TransactionID;
			this.Extra = Extra;
		}

		/// <summary>
		///Specifies the transaction state. Following values are possible:\"active\": A customer transaction is in progress.\"inactive\": No customer transaction is in progress.
		/// </summary>
		[DataMember(Name = "state")] 
		public StateEnum? State { get; private set; }
		/// <summary>
		///Specifies a string which identifies the transaction ID. The value returned in this parameter is an application defined customer transaction identifier, which was previously set in the Common.SetTransactionState command
		/// </summary>
		[DataMember(Name = "transactionID")] 
		public string TransactionID { get; private set; }
		/// <summary>
		///A list of vendor-specific, or any other extended, transaction information. The information is set as a series of \"key=value\" strings. Each string is null-terminated, with the final string terminating with two null characters. An empty list may be indicated by either a NULL pointer or a pointer to two consecutive null characters
		/// </summary>
		[DataMember(Name = "extra")] 
		public List<string> Extra{ get; private set; }
	}

}
