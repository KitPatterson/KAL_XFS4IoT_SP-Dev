/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetMediaList.cs uses automatically generated parts. 
 * GetMediaList.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.GetMediaList")]
	public sealed class GetMediaList : Response<GetMediaListPayload>
	{

		public GetMediaList(string RequestId, GetMediaListPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetMediaListPayload : MessagePayload
	{


		public GetMediaListPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, List<string> MediaList = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetMediaListPayload)}");

			this.MediaList = MediaList;
		}

		/// <summary>
		///The list of media names.
		/// </summary>
		[DataMember(Name = "mediaList")] 
		public List<string> MediaList{ get; private set; }
	}

}
