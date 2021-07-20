/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * Initialization_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.KeyManagement.Commands
{
    //Original name = Initialization
    [DataContract]
    [Command(Name = "KeyManagement.Initialization")]
    public sealed class InitializationCommand : Command<InitializationCommand.PayloadData>
    {
        public InitializationCommand(int RequestId, InitializationCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, string Ident = null, string Key = null)
                : base(Timeout)
            {
                this.Ident = Ident;
                this.Key = Key;
            }

            /// <summary>
            /// The value of the ID key. this field is not required if an indent is not required.
            /// </summary>
            [DataMember(Name = "ident")]
            public string Ident { get; init; }

            /// <summary>
            /// The value of the encryption key formatted in base64. this field is not required if no specific key name required. 
            /// </summary>
            [DataMember(Name = "key")]
            public string Key { get; init; }

        }
    }
}
