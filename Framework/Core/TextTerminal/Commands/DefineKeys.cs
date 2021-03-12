/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DefineKeys.cs uses automatically generated parts. 
 * DefineKeys.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = DefineKeys
	[DataContract]
	[Command(Name = "TextTerminal.DefineKeys")]
	public sealed class DefineKeys : Command<DefineKeysPayload>
	{

		public DefineKeys(string RequestId, DefineKeysPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DefineKeysPayload : MessagePayload
	{


		public DefineKeysPayload(int Timeout, string ActiveKeys = null, List<string> ActiveCommandKeys = null, List<string> TerminateCommandKeys = null)
			: base(Timeout)
		{
			this.ActiveKeys = ActiveKeys;
			this.ActiveCommandKeys = ActiveCommandKeys;
			this.TerminateCommandKeys = TerminateCommandKeys;
		}

		/// <summary>
		///String which specifies the alphanumeric keys on the Text Terminal Unit, e.g. \"12ABab\", to be active during the execution of the next ReadForm command. Devices having a shift key interpret this parameter differently from those that do not have a shift key. For devices having a shift key, specifying only the upper case of a particular letter enables both upper and lower case of that key, but the device converts lower case letters to upper case in the output parameter. To enable both upper and lower case keys, and have both upper and lower case letters returned, specify both the upper and lower case of the letter (e.g. \"12AaBb\"). For devices not having a shift key, specifying either the upper case only (e.g. \"12AB\"), or specifying both the upper and lower case of a particular letter (e.g. \"12AaBb\"), enables that key and causes the device to return the upper case of the letter in the output parameter. For both types of device, specifying only lower case letters (e.g. \"12ab\") produces a key invalid error.
		/// </summary>
		[DataMember(Name = "activeKeys")] 
		public string ActiveKeys { get; private set; }
		/// <summary>
		///Array specifying the command keys which are active during the execution of the next ReadForm command.                       
		/// </summary>
		[DataMember(Name = "activeCommandKeys")] 
		public List<string> ActiveCommandKeys{ get; private set; }
		/// <summary>
		///Array specifying the command keys which must terminate the execution of the next ReadForm command.
		/// </summary>
		[DataMember(Name = "terminateCommandKeys")] 
		public List<string> TerminateCommandKeys{ get; private set; }
	}

}
