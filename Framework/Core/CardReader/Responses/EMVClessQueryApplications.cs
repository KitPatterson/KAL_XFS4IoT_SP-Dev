/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessQueryApplications.cs uses automatically generated parts. 
 * EMVClessQueryApplications.cs was created at 03/03/2021 05:09:25 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.CardReader.Responses
{


	[DataContract]
	[Response(Name = "CardReader.EMVClessQueryApplications")]
	public sealed class EMVClessQueryApplications : Response<EMVClessQueryApplicationsPayload>
	{

		public EMVClessQueryApplications(string RequestId, EMVClessQueryApplicationsPayload Payload)
			: base(RequestId, Payload)
		{ }

	}

	[DataContract]
	public sealed class EMVClessQueryApplicationsPayload : MessagePayload
	{

		[DataContract]
		public sealed class AppDataClass
		{
			public AppDataClass(string Aid = null, string KernelIdentifier = null)
				: base()
			{
				this.Aid = Aid;
				this.KernelIdentifier = KernelIdentifier;
			}

			/// <summary>
			///Contains the Base64 encoded payment system application identifier (AID) supported by the intelligent contactless card unit.
			/// </summary>
			[DataMember(Name = "aid")] 
			public string Aid { get; private set; }

			/// <summary>
			///Contains the Base64 encoded Kernel Identifier associated with the *aid*. This data may be empty if the reader does not support Kernel Identifiers for example in the case of legacy approved contactless readers.
			/// </summary>
			[DataMember(Name = "kernelIdentifier")] 
			public string KernelIdentifier { get; private set; }

		}


		public EMVClessQueryApplicationsPayload(CompletionCodeEnum CompletionCode, string ErrorDescription, List<AppDataClass> AppData = null)
			: base(CompletionCode, ErrorDescription)
		{
			Contracts.IsNotNullOrWhitespace(ErrorDescription, $"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(EMVClessQueryApplicationsPayload)}");

			this.AppData = AppData;
		}

		/// <summary>
		///An array of application data objects which specifies a supported identifier (AID) and associated Kernel Identifier.
		/// </summary>
		[DataMember(Name = "appData")] 
		public List<AppDataClass> AppData{ get; private set; }
	}

}
