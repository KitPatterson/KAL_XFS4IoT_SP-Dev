/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * StartAuthenticate_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.KeyManagement.Commands
{
    //Original name = StartAuthenticate
    [DataContract]
    [Command(Name = "KeyManagement.StartAuthenticate")]
    public sealed class StartAuthenticateCommand : Command<StartAuthenticateCommand.PayloadData>
    {
        public StartAuthenticateCommand(int RequestId, StartAuthenticateCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, CommandNameEnum? CommandName = null, object CommandParameters = null)
                : base(Timeout)
            {
                this.CommandName = CommandName;
                this.CommandParameters = CommandParameters;
            }

            public enum CommandNameEnum
            {
                DeleteKey,
                ImportKey,
                Initialization,
                ReplaceCertificate,
                DeriveKey
            }

            /// <summary>
            /// The command name to which authentication is being applied.
            /// The possible variables are:
            /// 
            /// * ```DeleteKey``` - Details of [KeyManagement.DeleteKey](#keymanagement.deletekey) command.
            /// * ```ImportKey``` - Details of [KeyManagement.ImportKey](#keymanagement.importkey) command.
            /// * ```Initialization``` - Details of [KeyManagement.Initialization](#keymanagement.initialization) command.
            /// * ```ReplaceCertificate``` - Details of [KeyManagement.ReplaceCertificate](#keymanagement.replacecertificate) command.
            /// * ```DeriveKey``` - Details of [KeyManagement.DeriveKey](#keymanagement.derivekey) command.
            /// </summary>
            [DataMember(Name = "commandName")]
            public CommandNameEnum? CommandName { get; init; }

            /// <summary>
            /// A payload to the input parameters of the command referred to by commandName property.
            /// If the specified commandName doesn't require an input parameter, this property can be omitted.
            /// </summary>
            [DataMember(Name = "commandParameters")]
            public object CommandParameters { get; init; }

        }
    }
}
