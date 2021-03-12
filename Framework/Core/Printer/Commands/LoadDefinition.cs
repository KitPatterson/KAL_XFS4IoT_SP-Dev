/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * LoadDefinition.cs uses automatically generated parts. 
 * LoadDefinition.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = LoadDefinition
	[DataContract]
	[Command(Name = "Printer.LoadDefinition")]
	public sealed class LoadDefinition : Command<LoadDefinitionPayload>
	{

		public LoadDefinition(string RequestId, LoadDefinitionPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class LoadDefinitionPayload : MessagePayload
	{


		public LoadDefinitionPayload(int Timeout, string FileName = null, bool? Overwrite = null)
			: base(Timeout)
		{
			this.FileName = FileName;
			this.Overwrite = Overwrite;
		}

		/// <summary>
		///This is the full path and file name of the file to be loaded. This value cannot contain UNICODE characters. The file contains the form (including sub-forms and frames) or media definition in text format as described in [Form, Sub-Form, Field, Frame, Table and Media Definitions](#section-FormAndMediaDefinitions). Only  one form or media definition can be defined in the file.
		/// </summary>
		[DataMember(Name = "fileName")] 
		public string FileName { get; private set; }
		/// <summary>
		///Specifies if an existing form or media definition with the same name is to be replaced. If this flag is true then an existing form or media definition with the same name will be replaced, unless the command fails with an error, where the definition will remain unchanged. If this flag is false this command will fail with an error if the form or media definition already exists.
		/// </summary>
		[DataMember(Name = "overwrite")] 
		public bool? Overwrite { get; private set; }
	}

}
