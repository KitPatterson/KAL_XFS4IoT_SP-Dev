/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * DispensePaper.cs uses automatically generated parts. 
 * DispensePaper.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = DispensePaper
	[DataContract]
	[Command(Name = "Printer.DispensePaper")]
	public sealed class DispensePaper : Command<DispensePaperPayload>
	{

		public DispensePaper(string RequestId, DispensePaperPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DispensePaperPayload : MessagePayload
	{

		public enum PaperSourceEnum
		{
			Any,
			Upper,
			Lower,
			External,
			Aux,
			Aux2,
			Park,
		}


		public DispensePaperPayload(int Timeout, PaperSourceEnum? PaperSource = null)
			: base(Timeout)
		{
			this.PaperSource = PaperSource;
		}

		/// <summary>
		///The paper source to dispense from. It can be one of the following:**any**
		////  Any paper source can be used; it is determined by the service.**upper**
		////  Use the only paper source or the upper paper source, if there is more than one paper supply.**lower**
		////  Use the lower paper source.**internal**
		////  Use the external paper.**aux**
		////  Use the auxiliary paper source.**aux2**
		////  Use the second auxiliary paper source.**park**
		////  Use the parking station paper source.
		/// </summary>
		[DataMember(Name = "paperSource")] 
		public PaperSourceEnum? PaperSource { get; private set; }
	}

}
