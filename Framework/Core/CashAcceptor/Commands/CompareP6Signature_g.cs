/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * CompareP6Signature_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashAcceptor.Commands
{
    //Original name = CompareP6Signature
    [DataContract]
    [Command(Name = "CashAcceptor.CompareP6Signature")]
    public sealed class CompareP6SignatureCommand : Command<CompareP6SignatureCommand.PayloadData>
    {
        public CompareP6SignatureCommand(string RequestId, CompareP6SignatureCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            [DataContract]
            public sealed class P6ReferenceSignaturesClass
            {
                public P6ReferenceSignaturesClass(int? NoteId = null, object Orientation = null, string Signature = null)
                    : base()
                {
                    this.NoteId = NoteId;
                    this.Orientation = Orientation;
                    this.Signature = Signature;
                }

                /// <summary>
                /// Identification of note type.
                /// </summary>
                [DataMember(Name = "noteId")] 
                public int? NoteId { get; private set; }

                /// <summary>
                /// Orientation of the entered banknote.
                /// </summary>
                [DataMember(Name = "orientation")] 
                public object Orientation { get; private set; }

                /// <summary>
                /// Base64 encoded signature data.
                /// </summary>
                [DataMember(Name = "signature")] 
                public string Signature { get; private set; }

            }

            [DataContract]
            public sealed class P6SignaturesClass
            {
                public P6SignaturesClass(int? NoteId = null, object Orientation = null, string Signature = null)
                    : base()
                {
                    this.NoteId = NoteId;
                    this.Orientation = Orientation;
                    this.Signature = Signature;
                }

                /// <summary>
                /// Identification of note type.
                /// </summary>
                [DataMember(Name = "noteId")] 
                public int? NoteId { get; private set; }

                /// <summary>
                /// Orientation of the entered banknote.
                /// </summary>
                [DataMember(Name = "orientation")] 
                public object Orientation { get; private set; }

                /// <summary>
                /// Base64 encoded signature data.
                /// </summary>
                [DataMember(Name = "signature")] 
                public string Signature { get; private set; }

            }


            public PayloadData(int Timeout, List<P6ReferenceSignaturesClass> P6ReferenceSignatures = null, List<P6SignaturesClass> P6Signatures = null)
                : base(Timeout)
            {
                this.P6ReferenceSignatures = P6ReferenceSignatures;
                this.P6Signatures = P6Signatures;
            }

            /// <summary>
            /// Array of P6Signature structures.
            /// Each structure represents the signature corresponding to one orientation of a single reference banknote.
            /// At least one orientation must be provided. If no orientations are provided (this array is missing or empty) 
            /// the command returns an invalidData error.
            /// </summary>
            [DataMember(Name = "p6ReferenceSignatures")] 
            public List<P6ReferenceSignaturesClass> P6ReferenceSignatures{ get; private set; }
            /// <summary>
            /// Array of P6Signature structures. Each structure represents a level 2/3 signature, from the cash-in transactions, to be compared with the reference signatures in *p6ReferenceSignature*.
            /// At least one signature must be provided. If there are no signatures provided (this array is missing or emtpy) the command returns an invalidData error.
            /// </summary>
            [DataMember(Name = "p6Signatures")] 
            public List<P6SignaturesClass> P6Signatures{ get; private set; }

        }
    }
}
