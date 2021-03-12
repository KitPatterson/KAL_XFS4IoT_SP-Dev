/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetQueryField.cs uses automatically generated parts. 
 * GetQueryField.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.TextTerminal.Responses
{


	[DataContract]
	[Response(Name = "TextTerminal.GetQueryField")]
	public sealed class GetQueryField : Response<GetQueryFieldPayload>
	{

		public GetQueryField(string RequestId, GetQueryFieldPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryFieldPayload : MessagePayload
	{

		[DataContract]
		public sealed class FieldsClass
		{
			/// <summary>
			///Specifies the type of field.
			/// </summary>
			public enum TypeEnum
			{
				Text,
				Invisible,
				Password,
			}

			/// <summary>
			///Specifies the class of the field.
			/// </summary>
			public enum ClassEnum
			{
				Static,
				Optional,
				Required,
			}

			/// <summary>
			///Specifies how an overflow of field data should be handled.
			/// </summary>
			public enum OverflowEnum
			{
				Terminate,
				Truncate,
				Overwrite,
			}

			public FieldsClass(string FieldName = null, TypeEnum? Type = null, ClassEnum? Class = null, string Access = null, OverflowEnum? Overflow = null, string Format = null, string LanguageId = null)
				: base()
			{
				this.FieldName = FieldName;
				this.Type = Type;
				this.Class = Class;
				this.Access = Access;
				this.Overflow = Overflow;
				this.Format = Format;
				this.LanguageId = LanguageId;
			}

			/// <summary>
			///Specifies the field name.
			/// </summary>
			[DataMember(Name = "fieldName")] 
			public string FieldName { get; private set; }

			/// <summary>
			///Specifies the type of field.
			/// </summary>
			[DataMember(Name = "type")] 
			public TypeEnum? Type { get; private set; }

			/// <summary>
			///Specifies the class of the field.
			/// </summary>
			[DataMember(Name = "class")] 
			public ClassEnum? Class { get; private set; }

			/// <summary>
			///Specifies whether the field is to be used for input, output or both.
			/// </summary>
			[DataMember(Name = "access")] 
			public string Access { get; private set; }

			/// <summary>
			///Specifies how an overflow of field data should be handled.
			/// </summary>
			[DataMember(Name = "overflow")] 
			public OverflowEnum? Overflow { get; private set; }

			/// <summary>
			///Format string as defined in the form for this field.
			/// </summary>
			[DataMember(Name = "format")] 
			public string Format { get; private set; }

			/// <summary>
			///Specifies the language identifier for the field.
			/// </summary>
			[DataMember(Name = "languageId")] 
			public string LanguageId { get; private set; }

		}


		public GetQueryFieldPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, List<FieldsClass> Fields = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetQueryFieldPayload)}");

			this.Fields = Fields;
		}

		/// <summary>
		///Array of Fields.
		/// </summary>
		[DataMember(Name = "fields")] 
		public List<FieldsClass> Fields{ get; private set; }
	}

}
