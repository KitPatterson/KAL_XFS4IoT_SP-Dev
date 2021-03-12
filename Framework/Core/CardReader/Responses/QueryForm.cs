/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryForm.cs uses automatically generated parts. 
 * QueryForm.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.QueryForm")]
	public sealed class QueryForm : Response<QueryFormPayload>
	{

		public QueryForm(string RequestId, QueryFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class QueryFormPayload : MessagePayload
	{

		public enum ActionEnum
		{
			Read,
			Write,
		}


		public QueryFormPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string FormName = null, string FieldSeparatorTrack1 = null, string FieldSeparatorTrack2 = null, string FieldSeparatorTrack3 = null, string FieldSeparatorFrontTrack1 = null, string FieldSeparatorJISITrack1 = null, string FieldSeparatorJISITrack3 = null, ActionEnum? Action = null, string ActionDescription = null, bool? Secure = null, List<string> Track1Fields = null, List<string> Track2Fields = null, List<string> Track3Fields = null, List<string> JisItrack1Fields = null, List<string> JisItrack3Fields = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(QueryFormPayload)}");

			this.FormName = FormName;
			this.FieldSeparatorTrack1 = FieldSeparatorTrack1;
			this.FieldSeparatorTrack2 = FieldSeparatorTrack2;
			this.FieldSeparatorTrack3 = FieldSeparatorTrack3;
			this.FieldSeparatorFrontTrack1 = FieldSeparatorFrontTrack1;
			this.FieldSeparatorJISITrack1 = FieldSeparatorJISITrack1;
			this.FieldSeparatorJISITrack3 = FieldSeparatorJISITrack3;
			this.Action = Action;
			this.ActionDescription = ActionDescription;
			this.Secure = Secure;
			this.Track1Fields = Track1Fields;
			this.Track2Fields = Track2Fields;
			this.Track3Fields = Track3Fields;
			this.JisItrack1Fields = JisItrack1Fields;
			this.JisItrack3Fields = JisItrack3Fields;
		}

		/// <summary>
		///The form name.
		/// </summary>
		[DataMember(Name = "formName")] 
		public string FormName { get; private set; }
		/// <summary>
		///The value of the field separator of track 1.
		/// </summary>
		[DataMember(Name = "fieldSeparatorTrack1")] 
		public string FieldSeparatorTrack1 { get; private set; }
		/// <summary>
		///The value of the field separator of track 2.
		/// </summary>
		[DataMember(Name = "fieldSeparatorTrack2")] 
		public string FieldSeparatorTrack2 { get; private set; }
		/// <summary>
		///The value of the field separator of track 3.
		/// </summary>
		[DataMember(Name = "fieldSeparatorTrack3")] 
		public string FieldSeparatorTrack3 { get; private set; }
		/// <summary>
		///The value of the field separator of front track 1.
		/// </summary>
		[DataMember(Name = "fieldSeparatorFrontTrack1")] 
		public string FieldSeparatorFrontTrack1 { get; private set; }
		/// <summary>
		///The value of the field separator of JIS I track 1.
		/// </summary>
		[DataMember(Name = "fieldSeparatorJISITrack1")] 
		public string FieldSeparatorJISITrack1 { get; private set; }
		/// <summary>
		///The value of the field separator of JIS I track 3.
		/// </summary>
		[DataMember(Name = "fieldSeparatorJISITrack3")] 
		public string FieldSeparatorJISITrack3 { get; private set; }
		/// <summary>
		///The form action as one of the following:**read**
		////The form reads the card.**write**
		////The form writes the card.
		/// </summary>
		[DataMember(Name = "action")] 
		public ActionEnum? Action { get; private set; }
		/// <summary>
		///The description of the READ or WRITE action.
		/// </summary>
		[DataMember(Name = "actionDescription")] 
		public string ActionDescription { get; private set; }
		/// <summary>
		///Whether or not to do a security check.
		/// </summary>
		[DataMember(Name = "secure")] 
		public bool? Secure { get; private set; }
		/// <summary>
		///The field names for track 1.
		/// </summary>
		[DataMember(Name = "track1Fields")] 
		public List<string> Track1Fields{ get; private set; }
		/// <summary>
		///The field names for track 2.
		/// </summary>
		[DataMember(Name = "track2Fields")] 
		public List<string> Track2Fields{ get; private set; }
		/// <summary>
		///The field names for track 3.
		/// </summary>
		[DataMember(Name = "track3Fields")] 
		public List<string> Track3Fields{ get; private set; }
		/// <summary>
		///The field names for JIS I track 1.
		/// </summary>
		[DataMember(Name = "jisItrack1Fields")] 
		public List<string> JisItrack1Fields{ get; private set; }
		/// <summary>
		///The field names for JIS I track 3.
		/// </summary>
		[DataMember(Name = "jisItrack3Fields")] 
		public List<string> JisItrack3Fields{ get; private set; }
	}

}
