/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * FieldErrorEvent.cs uses automatically generated parts. 
 * FieldErrorEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.FieldErrorEvent")]
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
			StaticOverwrite,
			Overflow,
			NotFound,
			NotRead,
			NotWrite,
			Hwerror,
			NotSupported,
			Graphic,
		}


		public FieldErrorEventPayload(string FormName = null, string FieldName = null, FailureEnum? Failure = null)
			: base()
		{
			this.FormName = FormName;
			this.FieldName = FieldName;
			this.Failure = Failure;
		}

		/// <summary>
		///The form name.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///The field name.
		/// </summary>
		[DataMember(Name = "fieldName")] 
		public string FieldName { get; private set; }
		/// <summary>
		///Specifies the type of failure as one of the following:**required**
		////  The specified field must be supplied by the application.**staticOverwrite**
		////  The specified field is static and thus cannot be overwritten by the application.**overflow**
		////  The value supplied for the specified fields is too long.**notFound**
		////  The specified field does not exist.**notRead**
		////  The specified field is not an input field.**notWrite**
		////  An attempt was made to write to an input field.**hwerror**
		////  The specified field uses special hardware (e.g. OCR, Low/High coercivity, etc) and an error occurred.**notSupported**
		////  The form field type is not supported with device.**graphic**
		////  The specified graphic image could not be printed.
		/// </summary>
		[DataMember(Name = "failure")] 
		public FailureEnum? Failure { get; private set; }
	}

}
