/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaExtents.cs uses automatically generated parts. 
 * MediaExtents.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = MediaExtents
	[DataContract]
	[Command(Name = "Printer.MediaExtents")]
	public sealed class MediaExtents : Command<MediaExtentsPayload>
	{

		public MediaExtents(string RequestId, MediaExtentsPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class MediaExtentsPayload : MessagePayload
	{

		public enum BaseEnum
		{
			Inches,
			Mm,
			RowColumn,
		}


		public MediaExtentsPayload(int Timeout, BaseEnum? Base = null, int? UnitX = null, int? UnitY = null)
			: base(Timeout)
		{
			this.Base = Base;
			this.UnitX = UnitX;
			this.UnitY = UnitY;
		}

		/// <summary>
		///Specifies the base unit of measurement of the media and can be one of the following values:**inches**
		////  The base unit is inches.**mm**
		////  The base unit is millimeters.**rowColumn**
		////  The base unit is rows and columns.
		/// </summary>
		[DataMember(Name = "base")] 
		public BaseEnum? Base { get; private set; }
		/// <summary>
		///Specifies the horizontal resolution of the base units as a fraction of the base value. For example, a value of 16 applied to the base unit, inches, means that the base horizontal resolution is 1/16.
		/// </summary>
		[DataMember(Name = "unitX")] 
		public int? UnitX { get; private set; }
		/// <summary>
		///Specifies the vertical resolution of the base units as a fraction of the base value. For example, a value of 10 applied to the base unit, mm, means that the base vertical resolution is 0.1 mm.
		/// </summary>
		[DataMember(Name = "unitY")] 
		public int? UnitY { get; private set; }
	}

}
