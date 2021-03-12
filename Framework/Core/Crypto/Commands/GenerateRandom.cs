/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * GenerateRandom.cs uses automatically generated parts. 
 * GenerateRandom.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Crypto.Commands
{


	//Original name = GenerateRandom
	[DataContract]
	[Command(Name = "Crypto.GenerateRandom")]
	public sealed class GenerateRandom : Command<GenerateRandomPayload>
	{

		public GenerateRandom(string RequestId, GenerateRandomPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GenerateRandomPayload : MessagePayload
	{


		public GenerateRandomPayload(int Timeout)
			: base(Timeout)
		{
		}

	}

}
