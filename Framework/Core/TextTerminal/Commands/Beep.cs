/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * Beep.cs uses automatically generated parts. 
 * Beep.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{


	//Original name = Beep
	[DataContract]
	[Command(Name = "TextTerminal.Beep")]
	public sealed class Beep : Command<BeepPayload>
	{

		public Beep(string RequestId, BeepPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class BeepPayload : MessagePayload
	{

		/// <summary>
		///Specifies whether the beeper should be turned on or off.
		/// </summary>
		public class BeepClass
		{
			[DataMember(Name = "off")] 
			public bool? Off { get; private set; }
			[DataMember(Name = "keyPress")] 
			public bool? KeyPress { get; private set; }
			[DataMember(Name = "exclamation")] 
			public bool? Exclamation { get; private set; }
			[DataMember(Name = "warning")] 
			public bool? Warning { get; private set; }
			[DataMember(Name = "error")] 
			public bool? Error { get; private set; }
			[DataMember(Name = "critical")] 
			public bool? Critical { get; private set; }
			[DataMember(Name = "continuous")] 
			public bool? Continuous { get; private set; }

			public BeepClass (bool? Off, bool? KeyPress, bool? Exclamation, bool? Warning, bool? Error, bool? Critical, bool? Continuous)
			{
				this.Off = Off;
				this.KeyPress = KeyPress;
				this.Exclamation = Exclamation;
				this.Warning = Warning;
				this.Error = Error;
				this.Critical = Critical;
				this.Continuous = Continuous;
			}


		}


		public BeepPayload(int Timeout, object Beep = null)
			: base(Timeout)
		{
			this.Beep = Beep;
		}

		/// <summary>
		///Specifies whether the beeper should be turned on or off.
		/// </summary>
		[DataMember(Name = "beep")] 
		public object Beep { get; private set; }
	}

}
