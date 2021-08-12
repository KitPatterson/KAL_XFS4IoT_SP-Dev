/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Keyboard interface.
 * GetFuncKeyDetail_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Keyboard.Completions
{
    [DataContract]
    [Completion(Name = "Keyboard.GetFuncKeyDetail")]
    public sealed class GetFuncKeyDetailCompletion : Completion<GetFuncKeyDetailCompletion.PayloadData>
    {
        public GetFuncKeyDetailCompletion(int RequestId, GetFuncKeyDetailCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, FunctionKeysClass FuncMask = null, List<FdksClass> Fdks = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.FuncMask = FuncMask;
                this.Fdks = Fdks;
            }


            [DataMember(Name = "funcMask")]
            public FunctionKeysClass FuncMask { get; init; }

            [DataContract]
            public sealed class FdksClass
            {
                public FdksClass(string Fdk = null, int? XPosition = null, int? YPosition = null)
                {
                    this.Fdk = Fdk;
                    this.XPosition = XPosition;
                    this.YPosition = YPosition;
                }

                /// <summary>
                /// Specifies the code returned by this FDK. Possible FDK numbers are from 01 to 32.
                /// </summary>
                [DataMember(Name = "fdk")]
                [DataTypes(Pattern = "^fdk(0[1-9]|[12][0-9]|3[0-2])$")]
                public string Fdk { get; init; }

                /// <summary>
                /// For FDKs, specifies the screen position the FDK relates to.
                /// This position is relative to the top of the screen expressed as a percentage of the height of the screen.
                /// For FDKs above or below the screen this will be 0 (above) or 100 (below).
                /// </summary>
                [DataMember(Name = "xPosition")]
                public int? XPosition { get; init; }

                /// <summary>
                /// For FDKs, specifies the screen position the FDK relates to.
                /// This position is relative to the Left Hand side of the screen expressed as a percentage of the width of the screen.
                /// For FDKs along the side of the screen this will be 0 (left side) or 100 (right side, user’s view).
                /// </summary>
                [DataMember(Name = "yPosition")]
                public int? YPosition { get; init; }

            }

            /// <summary>
            /// It is the responsibility of the application to identify the mapping between the FDK code and the physical location of the FDK.
            /// An empty array if no FDKs are requested or supported.
            /// </summary>
            [DataMember(Name = "fdks")]
            public List<FdksClass> Fdks { get; init; }

        }
    }
}
