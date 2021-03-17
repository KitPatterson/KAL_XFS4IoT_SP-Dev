/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * Digest.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Crypto.Commands
{
    //Original name = Digest
    [DataContract]
    [Command(Name = "Crypto.Digest")]
    public sealed class DigestCommand : Command<DigestCommand.PayloadData>
    {
        public DigestCommand(string RequestId, DigestCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum HashAlgorithmEnum
            {
                Sha1,
                Sha256,
            }


            public PayloadData(int Timeout, HashAlgorithmEnum? HashAlgorithm = null, string DigestInput = null)
                : base(Timeout)
            {
                this.HashAlgorithm = HashAlgorithm;
                this.DigestInput = DigestInput;
            }

            /// <summary>
            ///Specifies which hash algorithm should be used to calculate the hash. See the Capabilities section for valid algorithms.
            /// </summary>
            [DataMember(Name = "hashAlgorithm")] 
            public HashAlgorithmEnum? HashAlgorithm { get; private set; }
            /// <summary>
            ///Contains the length and the data to be hashed formatted in base64.
            /// </summary>
            [DataMember(Name = "digestInput")] 
            public string DigestInput { get; private set; }

        }
    }
}
