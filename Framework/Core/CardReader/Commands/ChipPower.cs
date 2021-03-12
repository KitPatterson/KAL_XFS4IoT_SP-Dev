/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipPower.cs uses automatically generated parts. 
 * ChipPower.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{


	//Original name = ChipPower
	[DataContract]
	[Command(Name = "CardReader.ChipPower")]
	public sealed class ChipPower : Command<ChipPowerPayload>
	{

		public ChipPower(string RequestId, ChipPowerPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ChipPowerPayload : MessagePayload
	{

		public enum ChipPowerEnum
		{
			Cold,
			Warm,
			Off,
		}


		public ChipPowerPayload(int Timeout, ChipPowerEnum? ChipPower = null)
			: base(Timeout)
		{
			this.ChipPower = ChipPower;
		}

		/// <summary>
		///Specifies the action to perform as one of the following:**cold*
		////The chip is powered on and reset.**warm*
		////The chip is reset.**off**
		////The chip is powered off.
		/// </summary>
		[DataMember(Name = "chipPower")] 
		public ChipPowerEnum? ChipPower { get; private set; }
	}

}
