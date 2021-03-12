/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * FieldErrorEvent.cs uses automatically generated parts. 
 * FieldErrorEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.TextTerminal.Events
{


	[DataContract]
	[Event(Name = "TextTerminal.FieldErrorEvent")]
	public sealed class FieldErrorEvent : Event<FieldErrorEventPayload>
	{

		public FieldErrorEvent(string RequestId, FieldErrorEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class FieldErrorEventPayload : MessagePayloadBase
	{

		public enum FailureEnum
		{
			Required,
			StaticOvwr,
			Overflow,
			NotRead,
			NotWrite,
			TypeNotSupported,
			CharSetForm,
		}


		public FieldErrorEventPayload(string FormName = null, string FieldName = null, FailureEnum? Failure = null)
			: base()
		{
			this.FormName = FormName;
			this.FieldName = FieldName;
			this.Failure = Failure;
		}

		/// <summary>
		///Specifies the form name.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///Specifies the field name.
		/// </summary>
		[DataMember(Name = "fieldName")] 
		public string FieldName { get; private set; }
		/// <summary>
		///Specifies the type of failure.
		/// </summary>
		[DataMember(Name = "failure")] 
		public FailureEnum? Failure { get; private set; }
	}

}
