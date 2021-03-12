/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * GenerateAuthetication.cs uses automatically generated parts. 
 * GenerateAuthetication.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Crypto.Commands
{


	//Original name = GenerateAuthetication
	[DataContract]
	[Command(Name = "Crypto.GenerateAuthetication")]
	public sealed class GenerateAuthetication : Command<GenerateAutheticationPayload>
	{

		public GenerateAuthetication(string RequestId, GenerateAutheticationPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GenerateAutheticationPayload : MessagePayload
	{

		/// <summary>
		///This parameter specifies the encryption algorithm, cryptographic method, and mode to be used for this command.For a list of valid values see the Attributes capability field. The values specified must be compatible with the key identified by Key.
		/// </summary>
		public class SignatureAttributesClass
		{
			public enum KeyUsageEnum
			{
				M0,
				M1,
				M2,
				M3,
				M4,
				M5,
				M6,
				M7,
				M8,
				S0,
				S1,
				S2,
			}
			[DataMember(Name = "keyUsage")] 
			public KeyUsageEnum? KeyUsage { get; private set; }
			public enum AlgorithmEnum
			{
				A,
				D,
				R,
				T,
			}
			[DataMember(Name = "algorithm")] 
			public AlgorithmEnum? Algorithm { get; private set; }
			public enum ModeOfUseEnum
			{
				G,
				S,
				V,
			}
			[DataMember(Name = "modeOfUse")] 
			public ModeOfUseEnum? ModeOfUse { get; private set; }
			public enum CryptoMethodEnum
			{
				RsassaPkcs1V15,
				RsassaPss,
			}
			[DataMember(Name = "cryptoMethod")] 
			public CryptoMethodEnum? CryptoMethod { get; private set; }
			
			/// <summary>
			///For asymmetric signature verification methods (keyUsage is ‘S0’, ‘S1’, or ‘S2’), this can be one of the following values to be used.If keyUsage is specified as any of the MAC usages (i.e. ‘m1’), then this proeprty should not be not set.
			/// </summary>
			public class HashAlgorithmClass 
			{
				[DataMember(Name = "sha1")] 
				public bool? Sha1 { get; private set; }
				[DataMember(Name = "sha256")] 
				public bool? Sha256 { get; private set; }

				public HashAlgorithmClass (bool? Sha1, bool? Sha256)
				{
					this.Sha1 = Sha1;
					this.Sha256 = Sha256;
				}
			}
			[DataMember(Name = "hashAlgorithm")] 
			public HashAlgorithmClass HashAlgorithm { get; private set; }

			public SignatureAttributesClass (KeyUsageEnum? KeyUsage, AlgorithmEnum? Algorithm, ModeOfUseEnum? ModeOfUse, CryptoMethodEnum? CryptoMethod, HashAlgorithmClass HashAlgorithm)
			{
				this.KeyUsage = KeyUsage;
				this.Algorithm = Algorithm;
				this.ModeOfUse = ModeOfUse;
				this.CryptoMethod = CryptoMethod;
				this.HashAlgorithm = HashAlgorithm;
			}


		}


		public GenerateAutheticationPayload(int Timeout, string Key = null, string StartValueKey = null, string StartValue = null, int? Padding = null, bool? Compression = null, string CryptData = null, string VerifyData = null, object SignatureAttributes = null)
			: base(Timeout)
		{
			this.Key = Key;
			this.StartValueKey = StartValueKey;
			this.StartValue = StartValue;
			this.Padding = Padding;
			this.Compression = Compression;
			this.CryptData = CryptData;
			this.VerifyData = VerifyData;
			this.SignatureAttributes = SignatureAttributes;
		}

		/// <summary>
		///Specifies the name of the stored key.
		/// </summary>
		[DataMember(Name = "key")] 
		public string Key { get; private set; }
		/// <summary>
		///If startValue specifies an Initialization Vector (IV), then this property specifies the name of the stored key used to decrypt the startValue to obtain the IV.If startValue is not set and this property is also not set, then this property specifies the name of the IV that has been previously imported via TR-31.If this property is not set, startValue is used as the Initialization Vector.
		/// </summary>
		[DataMember(Name = "startValueKey")] 
		public string StartValueKey { get; private set; }
		/// <summary>
		///DES and Triple DES initialization vector for cbc / cfb encryption and macing.
		/// </summary>
		[DataMember(Name = "startValue")] 
		public string StartValue { get; private set; }
		/// <summary>
		///Commonly used padding data
		/// </summary>
		[DataMember(Name = "padding")] 
		public int? Padding { get; private set; }
		/// <summary>
		///Specifies whether data is to be compressed (blanks removed) before building the mac.If compression is 0x00 no compression is selected, otherwise compression holds the representation of the blank character (e.g. 0x20 in ASCII or 0x40 in EBCDIC).
		/// </summary>
		[DataMember(Name = "compression")] 
		public bool? Compression { get; private set; }
		/// <summary>
		///The data to be encrypted, maced formatted in base64.
		/// </summary>
		[DataMember(Name = "cryptData")] 
		public string CryptData { get; private set; }
		/// <summary>
		///If the modeOfUse is 'd', 'g', or 's', then this property can be omitted.This value is set when the modeOfUse is 'v'.
		/// </summary>
		[DataMember(Name = "verifyData")] 
		public string VerifyData { get; private set; }
		/// <summary>
		///This parameter specifies the encryption algorithm, cryptographic method, and mode to be used for this command.For a list of valid values see the Attributes capability field. The values specified must be compatible with the key identified by Key.
		/// </summary>
		[DataMember(Name = "signatureAttributes")] 
		public object SignatureAttributes { get; private set; }
	}

}
