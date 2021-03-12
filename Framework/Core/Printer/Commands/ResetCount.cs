/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ResetCount.cs uses automatically generated parts. 
 * ResetCount.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = ResetCount
	[DataContract]
	[Command(Name = "Printer.ResetCount")]
	public sealed class ResetCount : Command<ResetCountPayload>
	{

		public ResetCount(string RequestId, ResetCountPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ResetCountPayload : MessagePayload
	{


		public ResetCountPayload(int Timeout, int? BinNumber = null)
			: base(Timeout)
		{
			this.BinNumber = BinNumber;
		}

		/// <summary>
		///Specifies the height of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "binNumber")] 
		public int? BinNumber { get; private set; }
	}

}
