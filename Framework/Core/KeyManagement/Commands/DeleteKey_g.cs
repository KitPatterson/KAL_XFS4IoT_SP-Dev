/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * DeleteKey_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.KeyManagement.Commands
{
    //Original name = DeleteKey
    [DataContract]
    [Command(Name = "KeyManagement.DeleteKey")]
    public sealed class DeleteKeyCommand : Command<DeleteKeyCommand.PayloadData>
    {
        public DeleteKeyCommand(int RequestId, DeleteKeyCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, string Key = null)
                : base(Timeout)
            {
                this.Key = Key;
            }

            /// <summary>
            /// Specifies the name of key being deleted. if this property is omitted. all keys are deleted.
            /// </summary>
            [DataMember(Name = "key")]
            public string Key { get; init; }

        }
    }
}
