/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetQueryForm.cs uses automatically generated parts. 
 * GetQueryForm.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.GetQueryForm")]
	public sealed class GetQueryForm : Response<GetQueryFormPayload>
	{

		public GetQueryForm(string RequestId, GetQueryFormPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryFormPayload : MessagePayload
	{

		/// <summary>
		///Specifies the null-terminated name of the form.
		/// </summary>
		public class FormNameClass
		{

			public FormNameClass ()
			{
			}


		}

		public enum CharSupportEnum
		{
			Ascii,
			Unicode,
		}


		public GetQueryFormPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, FormNameClass FormName = null, string Width = null, string Height = null, string VersionMajor = null, string VersionMinor = null, CharSupportEnum? CharSupport = null, List<string> Fields = null, string LanguageId = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetQueryFormPayload)}");

			this.FormName = FormName;
			this.Width = Width;
			this.Height = Height;
			this.VersionMajor = VersionMajor;
			this.VersionMinor = VersionMinor;
			this.CharSupport = CharSupport;
			this.Fields = Fields;
			this.LanguageId = LanguageId;
		}

		/// <summary>
		///Specifies the null-terminated name of the form.
		/// </summary>
		[DataMember(Name = "formName")] 
		public FormNameClass FormName { get; private set; }
		/// <summary>
		///Specifies the width of the form in columns.
		/// </summary>
		[DataMember(Name = "width")] 
		public string Width { get; private set; }
		/// <summary>
		///Specifies the height of the form in rows. 
		/// </summary>
		[DataMember(Name = "height")] 
		public string Height { get; private set; }
		/// <summary>
		///Specifies the major version.If version is not specifies in the form then zero is returned.
		/// </summary>
		[DataMember(Name = "versionMajor")] 
		public string VersionMajor { get; private set; }
		/// <summary>
		///Specifies the minor version. If the version is not specified in the form then zero is returned.
		/// </summary>
		[DataMember(Name = "versionMinor")] 
		public string VersionMinor { get; private set; }
		/// <summary>
		///A single flag indicating whether the form is encoded in ascii or unicode.
		/// </summary>
		[DataMember(Name = "charSupport")] 
		public CharSupportEnum? CharSupport { get; private set; }
		/// <summary>
		///Object to a list of the field names.
		/// </summary>
		[DataMember(Name = "fields")] 
		public List<string> Fields{ get; private set; }
		/// <summary>
		///Specifies the language identifier for the form.
		/// </summary>
		[DataMember(Name = "languageId")] 
		public string LanguageId { get; private set; }
	}

}
