/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * Authenticate_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.KeyManagement.Completions
{
    [DataContract]
    [Completion(Name = "KeyManagement.Authenticate")]
    public sealed class AuthenticateCompletion : Completion<AuthenticateCompletion.PayloadData>
    {
        public AuthenticateCompletion(int RequestId, AuthenticateCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string InternalCmdResult = null, CommandNameEnum? CommandName = null, object CommandParameters = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.InternalCmdResult = InternalCmdResult;
                this.CommandName = CommandName;
                this.CommandParameters = CommandParameters;
            }

            public enum ErrorCodeEnum
            {
                AccessDenied,
                KeyNotFound,
                RandomInvalid,
                MacInvalid,
                SignatureInvalid,
                InvalidId
            }

            /// <summary>
            /// Specifies the error code if applicable. The following values are possible:
            /// * ```accessDenied``` - The encryption module is either not initialized or not ready for any vendor specific reason.
            /// * ```keyNotFound``` - The supplied key name cannot be found.
            /// * ```randomInvalid``` - The random number is either incorrect or no random number has been generated prior to this command.
            /// * ```macInvalid``` - The MAC calculated by the KeyManagement device does not match the MAC supplied in signedData
            /// * ```signatureInvalid``` - The signature in the input data is invalid.
            /// * ```invalidId``` - The data that was signed was not valid.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            /// <summary>
            /// Result from the command referenced by execution command. If the data within payload is invalid or cannot be used for 
            /// some reason, then internalCmdResult will return an error but the result of this command will be ok. 
            /// </summary>
            [DataMember(Name = "internalCmdResult")]
            public string InternalCmdResult { get; init; }

            public enum CommandNameEnum
            {
                DeleteKey,
                ImportKey,
                Initialization,
                ReplaceCertificate,
                DeriveKey
            }

            /// <summary>
            /// The XFS command name of the command to which authentication was applied.
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
            /// A payload to the output parameters of the command referred to by commandName property.
            /// If the specified commandName doesn't have an output parameter, this property can be omitted.
            /// </summary>
            [DataMember(Name = "commandParameters")]
            public object CommandParameters { get; init; }

        }
    }
}
