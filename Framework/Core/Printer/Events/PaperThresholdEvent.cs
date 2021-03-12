/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PaperThresholdEvent.cs uses automatically generated parts. 
 * PaperThresholdEvent.cs was created at 03/03/2021 05:09:26 PM
\***********************************************************************************************/

using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Printer.Events
{


	[DataContract]
	[Event(Name = "Printer.PaperThresholdEvent")]
	public sealed class PaperThresholdEvent : Event<PaperThresholdEventPayload>
	{

		public PaperThresholdEvent(string RequestId, PaperThresholdEventPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class PaperThresholdEventPayload : MessagePayloadBase
	{

		public enum PaperSourceEnum
		{
			Upper,
			Lower,
			External,
			Aux,
			Aux2,
		}

		public enum ThresholdEnum
		{
			Full,
			Low,
			Out,
		}


		public PaperThresholdEventPayload(PaperSourceEnum? PaperSource = null, ThresholdEnum? Threshold = null)
			: base()
		{
			this.PaperSource = PaperSource;
			this.Threshold = Threshold;
		}

		/// <summary>
		///Specifies the paper source as one of the following:**upper**
		////  The only paper source or the upper paper source, if there is more than one paper supply.**lower**
		////  The lower paper source.**external**
		////  The external paper source (such as envelope tray or single sheet feed).**aux**
		////  The auxiliary paper source.**aux2**
		////  The second auxiliary paper source.
		/// </summary>
		[DataMember(Name = "paperSource")] 
		public PaperSourceEnum? PaperSource { get; private set; }
		/// <summary>
		///Specifies the current state of the paper source as one of the following:**full**
		////   The paper in the paper source is in a good state.**low**
		////  The paper in the paper source is low.**out**
		////  The paper in the paper source is out.
		/// </summary>
		[DataMember(Name = "threshold")] 
		public ThresholdEnum? Threshold { get; private set; }
	}

}
