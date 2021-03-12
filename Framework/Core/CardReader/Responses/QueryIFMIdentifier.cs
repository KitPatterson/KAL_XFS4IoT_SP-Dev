/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryIFMIdentifier.cs uses automatically generated parts. 
 * QueryIFMIdentifier.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.QueryIFMIdentifier")]
	public sealed class QueryIFMIdentifier : Response<QueryIFMIdentifierPayload>
	{

		public QueryIFMIdentifier(string RequestId, QueryIFMIdentifierPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class QueryIFMIdentifierPayload : MessagePayload
	{

		public enum IfmAuthorityEnum
		{
			Emv,
			Europay,
			Visa,
			Giecb,
		}


		public QueryIFMIdentifierPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, IfmAuthorityEnum? IfmAuthority = null, string IfmIdentifier = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(QueryIFMIdentifierPayload)}");

			this.IfmAuthority = IfmAuthority;
			this.IfmIdentifier = IfmIdentifier;
		}

		/// <summary>
		///Specifies the IFM authority that issued the IFM identifier:**emv**
		////The Level 1 Type Approval IFM identifier assigned by EMVCo.**europay**
		////The Level 1 Type Approval IFM identifier assigned by Europay.**visa**
		////The Level 1 Type Approval IFM identifier assigned by VISA.**giecb**
		////The IFM identifier assigned by GIE Cartes Bancaires.
		/// </summary>
		[DataMember(Name = "ifmAuthority")] 
		public IfmAuthorityEnum? IfmAuthority { get; private set; }
		/// <summary>
		///The IFM Identifier of the chip card reader (or IFM) as assigned by the specified authority.
		/// </summary>
		[DataMember(Name = "ifmIdentifier")] 
		public string IfmIdentifier { get; private set; }
	}

}
