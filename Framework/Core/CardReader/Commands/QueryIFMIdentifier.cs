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
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = QueryIFMIdentifier
	[DataContract]
	[Command(Name = "CardReader.QueryIFMIdentifier")]
	public sealed class QueryIFMIdentifier : Command<QueryIFMIdentifierPayload>
	{

		public QueryIFMIdentifier(string RequestId, QueryIFMIdentifierPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class QueryIFMIdentifierPayload : MessagePayload
	{


		public QueryIFMIdentifierPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
