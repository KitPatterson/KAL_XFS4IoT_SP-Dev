/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * CryptoData.cs uses automatically generated parts. 
 * CryptoData.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Crypto.Commands
{


	//Original name = CryptoData
	[DataContract]
	[Command(Name = "Crypto.CryptoData")]
	public sealed class CryptoData : Command<CryptoDataPayload>
	{

		public CryptoData(string RequestId, CryptoDataPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class CryptoDataPayload : MessagePayload
	{

		/// <summary>
		///This parameter specifies the encryption algorithm, cryptographic method, and mode to be used for this command.For a list of valid values see the Attributes capability field. The values specified must be compatible with the key identified by Key.
		/// </summary>
		public class CryptoAttributesClass
		{
			public enum KeyUsageEnum
			{
				D0,
				D1,
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
				D,
				E,
			}
			[DataMember(Name = "modeOfUse")] 
			public ModeOfUseEnum? ModeOfUse { get; private set; }
			public enum CryptoMethodEnum
			{
				Ecb,
				Cbc,
				Cfb,
				Ofb,
				Otr,
				Xts,
				RsaesPkcs1V15,
				RsaesOaep,
			}
			[DataMember(Name = "cryptoMethod")] 
			public CryptoMethodEnum? CryptoMethod { get; private set; }

			public CryptoAttributesClass (KeyUsageEnum? KeyUsage, AlgorithmEnum? Algorithm, ModeOfUseEnum? ModeOfUse, CryptoMethodEnum? CryptoMethod)
			{
				this.KeyUsage = KeyUsage;
				this.Algorithm = Algorithm;
				this.ModeOfUse = ModeOfUse;
				this.CryptoMethod = CryptoMethod;
			}


		}


		public CryptoDataPayload(int Timeout, string Key = null, string StartValueKey = null, string StartValue = null, int? Padding = null, bool? Compression = null, string CryptData = null, object CryptoAttributes = null)
			: base(Timeout)
		{
			this.Key = Key;
			this.StartValueKey = StartValueKey;
			this.StartValue = StartValue;
			this.Padding = Padding;
			this.Compression = Compression;
			this.CryptData = CryptData;
			this.CryptoAttributes = CryptoAttributes;
		}

		/// <summary>
		///Specifies the name of the stored key. This field is not required, if mode equals random. 
		/// </summary>
		[DataMember(Name = "key")] 
		public string Key { get; private set; }
		/// <summary>
		///Specifies the name of the stored key used to decrypt the startValue to obtain the initialization vector.If this field is not set, startValue is used as the initialization vector.This field is not required, if mode equals random.
		/// </summary>
		[DataMember(Name = "startValueKey")] 
		public string StartValueKey { get; private set; }
		/// <summary>
		///DES and Triple DES initialization vector for cbc / cfb encryption and macing.This value is not required, if mode equals random.
		/// </summary>
		[DataMember(Name = "startValue")] 
		public string StartValue { get; private set; }
		/// <summary>
		///Commonly used padding data
		/// </summary>
		[DataMember(Name = "padding")] 
		public int? Padding { get; private set; }
		/// <summary>
		///Specifies whether data is to be compressed (blanks removed) before building the mac.If compression is 0x00 no compression is selected, otherwise compression holds the representation of the blank character (e.g. 0x20 in ASCII or 0x40 in EBCDIC).This field is not required, if mode equals random.
		/// </summary>
		[DataMember(Name = "compression")] 
		public bool? Compression { get; private set; }
		/// <summary>
		///The data to be encrypted, decrypted, or maced formatted in base64. This value is ignored, if mode equals random. 
		/// </summary>
		[DataMember(Name = "cryptData")] 
		public string CryptData { get; private set; }
		/// <summary>
		///This parameter specifies the encryption algorithm, cryptographic method, and mode to be used for this command.For a list of valid values see the Attributes capability field. The values specified must be compatible with the key identified by Key.
		/// </summary>
		[DataMember(Name = "cryptoAttributes")] 
		public object CryptoAttributes { get; private set; }
	}

}
