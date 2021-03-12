/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * DUKPTKSNEvent.cs uses automatically generated parts. 
 * DUKPTKSNEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Crypto.Events
{


	[DataContract]
	[Event(Name = "Crypto.DUKPTKSNEvent")]
	public sealed class DUKPTKSNEvent : Event<DUKPTKSNEventPayload>
	{

		public DUKPTKSNEvent(string RequestId, DUKPTKSNEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class DUKPTKSNEventPayload : MessagePayloadBase
	{


		public DUKPTKSNEventPayload(string Key = null, string Ksn = null)
			: base()
		{
			this.Key = Key;
			this.Ksn = Ksn;
		}

		/// <summary>
		///Specifies the name of the DUKPT Key derivation key. 
		/// </summary>
		[DataMember(Name = "key")] 
		public string Key { get; private set; }
		/// <summary>
		///structure that contains the KSN formatted in base64.
		/// </summary>
		[DataMember(Name = "ksn")] 
		public string Ksn { get; private set; }
	}

}
