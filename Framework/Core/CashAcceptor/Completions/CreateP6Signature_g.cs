/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * CreateP6Signature_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashAcceptor.Completions
{
    [DataContract]
    [Completion(Name = "CashAcceptor.CreateP6Signature")]
    public sealed class CreateP6SignatureCompletion : Completion<CreateP6SignatureCompletion.PayloadData>
    {
        public CreateP6SignatureCompletion(string RequestId, CreateP6SignatureCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                TooManyItems,
                NoItems,
                CashInActive,
                ExchangeActive,
                PositionNotEmpty,
                ShutterNotOpen,
                ShutterNotClosed,
                ForeignItemsDetected,
            }

            /// <summary>
            /// Orientation of the entered banknote.
            /// </summary>
            public class OrientationClass
            {
                [DataMember(Name = "frontTop")] 
                public bool? FrontTop { get; private set; }
                [DataMember(Name = "frontBottom")] 
                public bool? FrontBottom { get; private set; }
                [DataMember(Name = "backTop")] 
                public bool? BackTop { get; private set; }
                [DataMember(Name = "backBottom")] 
                public bool? BackBottom { get; private set; }
                [DataMember(Name = "unknown")] 
                public bool? Unknown { get; private set; }
                [DataMember(Name = "notSupported")] 
                public bool? NotSupported { get; private set; }

                public OrientationClass (bool? FrontTop, bool? FrontBottom, bool? BackTop, bool? BackBottom, bool? Unknown, bool? NotSupported)
                {
                    this.FrontTop = FrontTop;
                    this.FrontBottom = FrontBottom;
                    this.BackTop = BackTop;
                    this.BackBottom = BackBottom;
                    this.Unknown = Unknown;
                    this.NotSupported = NotSupported;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, int? NoteId = null, OrientationClass Orientation = null, string Signature = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.NoteId = NoteId;
                this.Orientation = Orientation;
                this.Signature = Signature;
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// \"tooManyItems\": There was more than one banknote inserted for creating a signature.
            /// 
            /// \"noItems\": There was no banknote to create a signature.
            /// 
            /// \"cashInActive\": A cash-in transaction is active.
            /// 
            /// \"exchangeActive\": The device is in the exchange state.
            /// 
            /// \"positionNotEmpty\": The output position is not empty so a banknote cannot be inserted.
            /// 
            /// \"shutterNotOpen\": Shutter failed to open.
            /// 
            /// \"shutterNotClosed\": Shutter failed to close.
            /// 
            /// \"foreignItemsDetected\": Foreign items have been detected in the input position.
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }
            /// <summary>
            /// Identification of note type.
            /// </summary>
            [DataMember(Name = "noteId")] 
            public int? NoteId { get; private set; }
            /// <summary>
            /// Orientation of the entered banknote.
            /// </summary>
            [DataMember(Name = "orientation")] 
            public OrientationClass Orientation { get; private set; }
            /// <summary>
            /// Base64 encoded signature data.
            /// </summary>
            [DataMember(Name = "signature")] 
            public string Signature { get; private set; }

        }
    }
}
