/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetCodelineMapping.cs uses automatically generated parts. 
 * GetCodelineMapping.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.GetCodelineMapping")]
	public sealed class GetCodelineMapping : Response<GetCodelineMappingPayload>
	{

		public GetCodelineMapping(string RequestId, GetCodelineMappingPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetCodelineMappingPayload : MessagePayload
	{

		public enum CodelineFormatEnum
		{
			Cmc7,
			E13b,
		}


		public GetCodelineMappingPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, CodelineFormatEnum? CodelineFormat = null, string CharMapping = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetCodelineMappingPayload)}");

			this.CodelineFormat = CodelineFormat;
			this.CharMapping = CharMapping;
		}

		/// <summary>
		///Specifies the code-line format that is being reported.
		/// </summary>
		[DataMember(Name = "codelineFormat")] 
		public CodelineFormatEnum? CodelineFormat { get; private set; }
		/// <summary>
		///Defines the mapping of the font specific symbols to byte values. These byte values are used torepresent the font specific characters when the code line is read through the Printer.ReadImagecommand. The font specific meaning of each index is defined in the following tables.**E13B**
		////| Index | Symbol | Meaning || :-- | :-- |:-- || 0 | ![Transit](../../Assets/e13b-transit.png \"Transit\") | Transit || 1 | ![Amount](../../Assets/e13b-amount.png \"Amount\") | Amount || 2 | ![On Us](../../Assets/e13b-onus.png \"On Us\") | On Us || 3 | ![Dash](../../Assets/e13b-dash.png \"Dash\") | Dash || 4 | N/A | Reject / Unreadable |**CMC7**
		////| Index | Symbol | Meaning || :-- | :-- |:-- || 0 | ![S1](../../Assets/cmc7-s1.png \"S1\") | S1 - Start of Bank Account || 1 | ![S2](../../Assets/cmc7-s2.png \"S2\") | S2 - Start of the Amount field || 2 | ![S3](../../Assets/cmc7-s3.png \"S3\") | S3 - Terminate Routing || 3 | ![S4](../../Assets/cmc7-s4.png \"S4\") | S4 - Unused || 4 | ![S5](../../Assets/cmc7-s5.png \"S5\") | S5 - Transit / Routing || 5 | N/A | Reject / Unreadable |
		/// </summary>
		[DataMember(Name = "charMapping")] 
		public string CharMapping { get; private set; }
	}

}
