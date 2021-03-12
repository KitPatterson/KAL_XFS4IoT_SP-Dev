/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SupplyReplenish.cs uses automatically generated parts. 
 * SupplyReplenish.cs was created at 03/03/2021 05:09:27 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{


	//Original name = SupplyReplenish
	[DataContract]
	[Command(Name = "Printer.SupplyReplenish")]
	public sealed class SupplyReplenish : Command<SupplyReplenishPayload>
	{

		public SupplyReplenish(string RequestId, SupplyReplenishPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class SupplyReplenishPayload : MessagePayload
	{


		public SupplyReplenishPayload(int Timeout, bool? Upper = null, bool? Lower = null, bool? Aux = null, bool? Aux2 = null, bool? Toner = null, bool? Ink = null, bool? Lamp = null)
			: base(Timeout)
		{
			this.Upper = Upper;
			this.Lower = Lower;
			this.Aux = Aux;
			this.Aux2 = Aux2;
			this.Toner = Toner;
			this.Ink = Ink;
			this.Lamp = Lamp;
		}

		/// <summary>
		///The only paper supply or the upper paper supply was replenished.
		/// </summary>
		[DataMember(Name = "upper")] 
		public bool? Upper { get; private set; }
		/// <summary>
		///The lower paper supply was replenished.
		/// </summary>
		[DataMember(Name = "lower")] 
		public bool? Lower { get; private set; }
		/// <summary>
		///The auxiliary paper supply was replenished.
		/// </summary>
		[DataMember(Name = "aux")] 
		public bool? Aux { get; private set; }
		/// <summary>
		///The second auxiliary paper supply was replenished.
		/// </summary>
		[DataMember(Name = "aux2")] 
		public bool? Aux2 { get; private set; }
		/// <summary>
		///The toner supply was replenished.
		/// </summary>
		[DataMember(Name = "toner")] 
		public bool? Toner { get; private set; }
		/// <summary>
		///The ink supply was replenished.
		/// </summary>
		[DataMember(Name = "ink")] 
		public bool? Ink { get; private set; }
		/// <summary>
		///The imaging lamp was replaced.
		/// </summary>
		[DataMember(Name = "lamp")] 
		public bool? Lamp { get; private set; }
	}

}
