/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrintRawFile.cs uses automatically generated parts. 
 * PrintRawFile.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.PrintRawFile")]
	public sealed class PrintRawFile : Response<PrintRawFilePayload>
	{

		public PrintRawFile(string RequestId, PrintRawFilePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class PrintRawFilePayload : MessagePayload
	{


		public PrintRawFilePayload(CompletionCodeEnum CompletionCode, string ErrorDescription)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(PrintRawFilePayload)}");

		}

	}

}
