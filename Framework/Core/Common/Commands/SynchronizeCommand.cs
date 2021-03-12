/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SynchronizeCommand.cs uses automatically generated parts. 
 * SynchronizeCommand.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Common.Commands
{


	//Original name = SynchronizeCommand
	[DataContract]
	[Command(Name = "Common.SynchronizeCommand")]
	public sealed class SynchronizeCommand : Command<SynchronizeCommandPayload>
	{

		public SynchronizeCommand(string RequestId, SynchronizeCommandPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SynchronizeCommandPayload : MessagePayload
	{

		/// <summary>
		///A payload that represents the parameter that is normally associated with the command.
		/// </summary>
		public class CmdDataClass
		{

			public CmdDataClass ()
			{
			}


		}


		public SynchronizeCommandPayload(int Timeout, string Command = null, object CmdData = null)
			: base(Timeout)
		{
			this.Command = Command;
			this.CmdData = CmdData;
		}

		/// <summary>
		///The command name to be synchronized and executed next. 
		/// </summary>
		[DataMember(Name = "command")] 
		public string Command { get; private set; }
		/// <summary>
		///A payload that represents the parameter that is normally associated with the command.
		/// </summary>
		[DataMember(Name = "cmdData")] 
		public object CmdData { get; private set; }
	}

}
