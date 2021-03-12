/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaExtents.cs uses automatically generated parts. 
 * MediaExtents.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.MediaExtents")]
	public sealed class MediaExtents : Response<MediaExtentsPayload>
	{

		public MediaExtents(string RequestId, MediaExtentsPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class MediaExtentsPayload : MessagePayload
	{


		public MediaExtentsPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, int? SizeX = null, int? SizeY = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(MediaExtentsPayload)}");

			this.SizeX = SizeX;
			this.SizeY = SizeY;
		}

		/// <summary>
		///Specifies the width of the media in terms of the base horizontal resolution.
		/// </summary>
		[DataMember(Name = "sizeX")] 
		public int? SizeX { get; private set; }
		/// <summary>
		///Specifies the height of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "sizeY")] 
		public int? SizeY { get; private set; }
	}

}
