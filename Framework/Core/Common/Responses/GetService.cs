// (C) KAL ATM Software GmbH, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XFS4IoT.Responses;

namespace XFS4IoT.Common.Responses
{
    [DataContract]
    [Response(Name = "Common.GetService")]
    public sealed class GetService : Response<GetServicePayload>
    {
        public GetService(string RequestId, GetServicePayload Payload) :
            base(RequestId, Payload)
        { }
    }

    [DataContract]
    public sealed class GetServicePayload : MessagePayload
    {
        public GetServicePayload(CompletionCodeEnum CompletionCode, string ErrorDescription, string VendorName = null, IEnumerable<ServiceUriDetails> Services = null) : 
            base(CompletionCode, ErrorDescription)
        {
            Services.IsNotNull($"The service URI information must not null even services are not available for {nameof(GetService)}. must be an empty array. {nameof(Services)}");

            this.VendorName = VendorName;
            this.Services = Services;
        }

        [DataMember(Name = "vendorName")]
        public string VendorName { get; private set; }

        [DataMember(Name = "services", IsRequired = true)]
        public IEnumerable<ServiceUriDetails> Services { get; private set; }


        /// <summary>
        /// Details on a single service endpoint's URIs. 
        /// </summary>
        [DataContract]
        public sealed class ServiceUriDetails
        {
            public ServiceUriDetails(string ServiceURI)
            {
                Contracts.IsNotNullOrWhitespace(ServiceURI, $"{nameof(ServiceURI)} are null or an empty value for {nameof(ServiceUriDetails)}");
            
                this.ServiceUri = ServiceURI;
            }

            /// <summary>
            /// The URI which can be used to contact this individual service
            /// </summary>
            /// <example>wss://ATM1:123/xfs4iot/v1.0/CardReader</example>
            [DataMember(Name = "serviceUri", IsRequired = true)]
            public string ServiceUri { get; private set; }//e.g. wss://machineid:port/XFS4IoT/v1.0/<ServiceName1> 
        }
    }
}