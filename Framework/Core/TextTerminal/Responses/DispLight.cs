/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DispLight.cs uses automatically generated parts. 
 * DispLight.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.DispLight")]
	public sealed class DispLight : Response<DispLightPayload>
	{

		public DispLight(string RequestId, DispLightPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DispLightPayload : MessagePayload
	{


		public DispLightPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, bool? Mode = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(DispLightPayload)}");

			this.Mode = Mode;
		}

		/// <summary>
		///Specifies whether the lighting of the text terminal unit is switched on (TRUE) or off (FALSE).
		/// </summary>
		[DataMember(Name = "mode")] 
		public bool? Mode { get; private set; }
	}

}
