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
using XFS4IoT.Commands;

namespace XFS4IoT.Keyboard.Commands
{
    //Original name = GetFuncKeyDetail
    [DataContract]
    [Command(Name = "Keyboard.GetFuncKeyDetail")]
    public sealed class GetFuncKeyDetailCommand : Command<GetFuncKeyDetailCommand.PayloadData>
    {
        public GetFuncKeyDetailCommand(int RequestId, GetFuncKeyDetailCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, FdkMaskEnum? FdkMask = null)
                : base(Timeout)
            {
                this.FdkMask = FdkMask;
            }

            public enum FdkMaskEnum
            {
                FunctionKeys,
                AllKeys
            }

            /// <summary>
            /// Mask for the fdks for which additional information is requested.
            /// The following values are possible:
            /// 
            /// * ```functionKeys``` - Only information about function keys is returned.
            /// * ```allKeys``` - Information about all the supported FDKs is returned.
            /// </summary>
            [DataMember(Name = "fdkMask")]
            public FdkMaskEnum? FdkMask { get; init; }

        }
    }
}
