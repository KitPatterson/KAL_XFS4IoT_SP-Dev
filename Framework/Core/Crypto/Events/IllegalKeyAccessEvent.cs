/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * IllegalKeyAccessEvent.cs uses automatically generated parts. 
 * IllegalKeyAccessEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Crypto.Events
{


	[DataContract]
	[Event(Name = "Crypto.IllegalKeyAccessEvent")]
	public sealed class IllegalKeyAccessEvent : Event<IllegalKeyAccessEventPayload>
	{

		public IllegalKeyAccessEvent(string RequestId, IllegalKeyAccessEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class IllegalKeyAccessEventPayload : MessagePayloadBase
	{

		public enum ErrorCodeEnum
		{
			Keynotfound,
			Keynovalue,
			Useviolation,
			Algorithmnotsupp,
		}


		public IllegalKeyAccessEventPayload(string KeyName = null, ErrorCodeEnum? ErrorCode = null)
			: base()
		{
			this.KeyName = KeyName;
			this.ErrorCode = ErrorCode;
		}

		/// <summary>
		///Specifies the name of the key that caused the error. 
		/// </summary>
		[DataMember(Name = "keyName")] 
		public string KeyName { get; private set; }
		/// <summary>
		///Specifies the type of illegal key access that occurred
		/// </summary>
		[DataMember(Name = "errorCode")] 
		public ErrorCodeEnum? ErrorCode { get; private set; }
	}

}
