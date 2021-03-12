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
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = GetCodelineMapping
	[DataContract]
	[Command(Name = "Printer.GetCodelineMapping")]
	public sealed class GetCodelineMapping : Command<GetCodelineMappingPayload>
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


		public GetCodelineMappingPayload(int Timeout, CodelineFormatEnum? CodelineFormat = null)
			: base(Timeout)
		{
			this.CodelineFormat = CodelineFormat;
		}

		/// <summary>
		///Specifies the code-line format that the mapping for the special characters is required for. This field can be one of the following values:**cmc7**
		////  Report the CMC7 mapping.**e13b**
		////  Report the E13B mapping.
		/// </summary>
		[DataMember(Name = "codelineFormat")] 
		public CodelineFormatEnum? CodelineFormat { get; private set; }
	}

}
