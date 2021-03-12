/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ClearScreen.cs uses automatically generated parts. 
 * ClearScreen.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = ClearScreen
	[DataContract]
	[Command(Name = "TextTerminal.ClearScreen")]
	public sealed class ClearScreen : Command<ClearScreenPayload>
	{

		public ClearScreen(string RequestId, ClearScreenPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class ClearScreenPayload : MessagePayload
	{


		public ClearScreenPayload(int Timeout, int? PositionX = null, int? PositionY = null, int? Width = null, int? Height = null)
			: base(Timeout)
		{
			this.PositionX = PositionX;
			this.PositionY = PositionY;
			this.Width = Width;
			this.Height = Height;
		}

		/// <summary>
		///Specifies the horizontal position of the area to be cleared.
		/// </summary>
		[DataMember(Name = "positionX")] 
		public int? PositionX { get; private set; }
		/// <summary>
		///Specifies the vertical position of the area to be cleared.
		/// </summary>
		[DataMember(Name = "positionY")] 
		public int? PositionY { get; private set; }
		/// <summary>
		///Specifies the width position of the area to be cleared.
		/// </summary>
		[DataMember(Name = "width")] 
		public int? Width { get; private set; }
		/// <summary>
		///Specifies the height position of the area to be cleared.
		/// </summary>
		[DataMember(Name = "height")] 
		public int? Height { get; private set; }
	}

}
