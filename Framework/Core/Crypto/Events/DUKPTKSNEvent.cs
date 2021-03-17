/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * DUKPTKSNEvent.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.Crypto.Events
{

    [DataContract]
    [Event(Name = "Crypto.DUKPTKSNEvent")]
    public sealed class DUKPTKSNEvent : Event<DUKPTKSNEvent.PayloadData>
    {

        public DUKPTKSNEvent(string RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {


            public PayloadData(string Key = null, string Ksn = null)
                : base()
            {
                this.Key = Key;
                this.Ksn = Ksn;
            }

            /// <summary>
            ///Specifies the name of the DUKPT Key derivation key. 
            /// </summary>
            [DataMember(Name = "key")] 
            public string Key { get; private set; }
            /// <summary>
            ///structure that contains the KSN formatted in base64.
            /// </summary>
            [DataMember(Name = "ksn")] 
            public string Ksn { get; private set; }
        }

    }
}
