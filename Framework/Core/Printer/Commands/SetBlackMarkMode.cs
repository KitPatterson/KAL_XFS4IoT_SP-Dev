/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SetBlackMarkMode.cs uses automatically generated parts. 
 * SetBlackMarkMode.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = SetBlackMarkMode
	[DataContract]
	[Command(Name = "Printer.SetBlackMarkMode")]
	public sealed class SetBlackMarkMode : Command<SetBlackMarkModePayload>
	{

		public SetBlackMarkMode(string RequestId, SetBlackMarkModePayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SetBlackMarkModePayload : MessagePayload
	{

		public enum BlackMarkModeEnum
		{
			On,
			Off,
		}


		public SetBlackMarkModePayload(int Timeout, BlackMarkModeEnum? BlackMarkMode = null)
			: base(Timeout)
		{
			this.BlackMarkMode = BlackMarkMode;
		}

		/// <summary>
		///Specifies the desired black mark detection mode as one of the following:**on**
		////  Turns the black mark detection and associated functionality on.**off**
		////  Turns the black mark detection and associated functionality off.
		/// </summary>
		[DataMember(Name = "blackMarkMode")] 
		public BlackMarkModeEnum? BlackMarkMode { get; private set; }
	}

}
