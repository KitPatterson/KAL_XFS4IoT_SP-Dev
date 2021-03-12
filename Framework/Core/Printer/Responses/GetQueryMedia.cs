/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryMedia.cs uses automatically generated parts. 
 * GetQueryMedia.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Printer.Responses
{


	[DataContract]
	[Response(Name = "Printer.GetQueryMedia")]
	public sealed class GetQueryMedia : Response<GetQueryMediaPayload>
	{

		public GetQueryMedia(string RequestId, GetQueryMediaPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class GetQueryMediaPayload : MessagePayload
	{

		public enum MediaTypeEnum
		{
			Generic,
			Passbook,
			Multipart,
		}

		public enum BaseEnum
		{
			Inch,
			Mm,
			Rowcolumn,
		}

		public enum FoldTypeEnum
		{
			None,
			Horizontal,
			Vertical,
		}

		/// <summary>
		///Specifies the Paper sources to use when printing forms using this media as a combination of the following flags
		/// </summary>
		public class PaperSourcesClass
		{
			[DataMember(Name = "any")] 
			public bool? Any { get; private set; }
			[DataMember(Name = "upper")] 
			public bool? Upper { get; private set; }
			[DataMember(Name = "lower")] 
			public bool? Lower { get; private set; }
			[DataMember(Name = "external")] 
			public bool? External { get; private set; }
			[DataMember(Name = "aux")] 
			public bool? Aux { get; private set; }
			[DataMember(Name = "aux2")] 
			public bool? Aux2 { get; private set; }
			[DataMember(Name = "park")] 
			public bool? Park { get; private set; }

			public PaperSourcesClass (bool? Any, bool? Upper, bool? Lower, bool? External, bool? Aux, bool? Aux2, bool? Park)
			{
				this.Any = Any;
				this.Upper = Upper;
				this.Lower = Lower;
				this.External = External;
				this.Aux = Aux;
				this.Aux2 = Aux2;
				this.Park = Park;
			}


		}


		public GetQueryMediaPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, MediaTypeEnum? MediaType = null, BaseEnum? Base = null, int? UnitX = null, int? UnitY = null, int? SizeWidth = null, int? SizeHeight = null, int? PageCount = null, int? LineCount = null, int? PrintAreaX = null, int? PrintAreaY = null, int? PrintAreaWidth = null, int? PrintAreaHeight = null, int? RestrictedAreaX = null, int? RestrictedAreaY = null, int? RestrictedAreaWidth = null, int? RestrictedAreaHeight = null, int? Stagger = null, FoldTypeEnum? FoldType = null, PaperSourcesClass PaperSources = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(GetQueryMediaPayload)}");

			this.MediaType = MediaType;
			this.Base = Base;
			this.UnitX = UnitX;
			this.UnitY = UnitY;
			this.SizeWidth = SizeWidth;
			this.SizeHeight = SizeHeight;
			this.PageCount = PageCount;
			this.LineCount = LineCount;
			this.PrintAreaX = PrintAreaX;
			this.PrintAreaY = PrintAreaY;
			this.PrintAreaWidth = PrintAreaWidth;
			this.PrintAreaHeight = PrintAreaHeight;
			this.RestrictedAreaX = RestrictedAreaX;
			this.RestrictedAreaY = RestrictedAreaY;
			this.RestrictedAreaWidth = RestrictedAreaWidth;
			this.RestrictedAreaHeight = RestrictedAreaHeight;
			this.Stagger = Stagger;
			this.FoldType = FoldType;
			this.PaperSources = PaperSources;
		}

		/// <summary>
		///Specifies the type of media as one of the following:**generic**
		////  The media is a generic media, i.e. a single sheet.**passbook**
		////  The media is a passbook media.**multipart**
		////  The media is a multi part media.
		/// </summary>
		[DataMember(Name = "mediaType")] 
		public MediaTypeEnum? MediaType { get; private set; }
		/// <summary>
		///Specifies the base unit of measurement of the form and can be one of the following values:**inch**
		////  The base unit is inches.**mm**
		////  The base unit is millimeters.**rowcolumn**
		////  The base unit is rows and columns.
		/// </summary>
		[DataMember(Name = "base")] 
		public BaseEnum? Base { get; private set; }
		/// <summary>
		///Specifies the horizontal resolution of the base units as a fraction of the *base* value. For example, a value of 16 applied to the base unit *inch* means that the base horizontal resolution is 1/16th inch.
		/// </summary>
		[DataMember(Name = "unitX")] 
		public int? UnitX { get; private set; }
		/// <summary>
		///Specifies the vertical resolution of the base units as a fraction of the *base* value. For example, a value of 10 applied to the base unit *mm* means that the base vertical resolution is 0.1 mm.
		/// </summary>
		[DataMember(Name = "unitY")] 
		public int? UnitY { get; private set; }
		/// <summary>
		///Specifies the width of the media in terms of the base horizontal resolution.
		/// </summary>
		[DataMember(Name = "sizeWidth")] 
		public int? SizeWidth { get; private set; }
		/// <summary>
		///Specifies the height of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "sizeHeight")] 
		public int? SizeHeight { get; private set; }
		/// <summary>
		///Specifies the number of pages in a media of type *passbook*.
		/// </summary>
		[DataMember(Name = "pageCount")] 
		public int? PageCount { get; private set; }
		/// <summary>
		///Specifies the number of lines on a page for a media of type *passbook*.
		/// </summary>
		[DataMember(Name = "lineCount")] 
		public int? LineCount { get; private set; }
		/// <summary>
		///Specifies the horizontal offset of the printable area relative to the top left corner of the media in terms of the base horizontal resolution.
		/// </summary>
		[DataMember(Name = "printAreaX")] 
		public int? PrintAreaX { get; private set; }
		/// <summary>
		///Specifies the vertical offset of the printable area relative to the top left corner of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "printAreaY")] 
		public int? PrintAreaY { get; private set; }
		/// <summary>
		///Specifies the printable area width of the media in terms of the base horizontal resolution.
		/// </summary>
		[DataMember(Name = "printAreaWidth")] 
		public int? PrintAreaWidth { get; private set; }
		/// <summary>
		///Specifies the printable area height of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "printAreaHeight")] 
		public int? PrintAreaHeight { get; private set; }
		/// <summary>
		///Specifies the horizontal offset of the restricted area relative to the top left corner of the media in terms of the base horizontal resolution.
		/// </summary>
		[DataMember(Name = "restrictedAreaX")] 
		public int? RestrictedAreaX { get; private set; }
		/// <summary>
		///Specifies the vertical offset of the restricted area relative to the top left corner of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "restrictedAreaY")] 
		public int? RestrictedAreaY { get; private set; }
		/// <summary>
		///Specifies the restricted area width of the media in terms of the base horizontal resolution.
		/// </summary>
		[DataMember(Name = "restrictedAreaWidth")] 
		public int? RestrictedAreaWidth { get; private set; }
		/// <summary>
		///Specifies the restricted area height of the media in terms of the base vertical resolution.
		/// </summary>
		[DataMember(Name = "restrictedAreaHeight")] 
		public int? RestrictedAreaHeight { get; private set; }
		/// <summary>
		///Specifies the staggering from the top in terms of the base vertical resolution for a media of type *passbook*.
		/// </summary>
		[DataMember(Name = "stagger")] 
		public int? Stagger { get; private set; }
		/// <summary>
		///Specified the type of fold for a media of type *passbook* as one of the following:**none**
		////  Passbook has no fold.**horizontal**
		////  Passbook has a horizontal fold.**vertical**
		////  Passbook has a vertical fold.
		/// </summary>
		[DataMember(Name = "foldType")] 
		public FoldTypeEnum? FoldType { get; private set; }
		/// <summary>
		///Specifies the Paper sources to use when printing forms using this media as a combination of the following flags
		/// </summary>
		[DataMember(Name = "paperSources")] 
		public PaperSourcesClass PaperSources { get; private set; }
	}

}
