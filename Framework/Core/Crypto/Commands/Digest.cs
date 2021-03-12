/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * Digest.cs uses automatically generated parts. 
 * Digest.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Crypto.Commands
{


	//Original name = Digest
	[DataContract]
	[Command(Name = "Crypto.Digest")]
	public sealed class Digest : Command<DigestPayload>
	{

		public Digest(string RequestId, DigestPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DigestPayload : MessagePayload
	{

		public enum HashAlgorithmEnum
		{
			Sha1,
			Sha256,
		}


		public DigestPayload(int Timeout, HashAlgorithmEnum? HashAlgorithm = null, string DigestInput = null)
			: base(Timeout)
		{
			this.HashAlgorithm = HashAlgorithm;
			this.DigestInput = DigestInput;
		}

		/// <summary>
		///Specifies which hash algorithm should be used to calculate the hash. See the Capabilities section for valid algorithms.
		/// </summary>
		[DataMember(Name = "hashAlgorithm")] 
		public HashAlgorithmEnum? HashAlgorithm { get; private set; }
		/// <summary>
		///Contains the length and the data to be hashed formatted in base64.
		/// </summary>
		[DataMember(Name = "digestInput")] 
		public string DigestInput { get; private set; }
	}

}
