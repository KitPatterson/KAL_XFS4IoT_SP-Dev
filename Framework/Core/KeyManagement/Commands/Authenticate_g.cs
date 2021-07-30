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
using XFS4IoT.Commands;

namespace XFS4IoT.KeyManagement.Commands
{
    //Original name = Authenticate
    [DataContract]
    [Command(Name = "KeyManagement.Authenticate")]
    public sealed class AuthenticateCommand : Command<AuthenticateCommand.PayloadData>
    {
        public AuthenticateCommand(int RequestId, AuthenticateCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, SignersClass Signer = null, string SignatureKey = null, string SignedData = null, CommandNameEnum? CommandName = null, object CommandParameters = null)
                : base(Timeout)
            {
                this.Signer = Signer;
                this.SignatureKey = SignatureKey;
                this.SignedData = SignedData;
                this.CommandName = CommandName;
                this.CommandParameters = CommandParameters;
            }


            [DataMember(Name = "signer")]
            public SignersClass Signer { get; init; }

            /// <summary>
            /// If the *signer* is cbcmac or mac are specified, then this _signatureKey_ property is the name of a key with the key usage of key attribute is M0 to M8.
            /// If sigHost is specified for signer property, then this property signatureKey specifies the name of a previously loaded asymmetric key(i.e. an RSA Public Key).
            /// The default Signature Issuer public key (installed in a secure environment during manufacture) will be used, 
            /// if this signatureKey propery is omitted or contains the name of the default Signature Issuer as defined in the document [Default keys and securitry item loaded during manufacture](#keymanagement.generalinformation.rklprocess.defaultkeyandsecurity).
            /// Otherwise, this property should be omitted.
            /// </summary>
            [DataMember(Name = "signatureKey")]
            public string SignatureKey { get; init; }

            /// <summary>
            /// This property contains the signed version of the data that was provided by the KeyManagement device during the previous call to the StartExchange command.
            /// The signer specified by *signer* property is used to do the signing.
            /// Both the signature and the data that was signed must be verified before the operation is performed.
            /// If certHost, ca, or hl are specified for *signer* property, then _signedData_ is a PKCS #7 structure which includes the data that was returned by the StartAuthenticate command.
            /// The optional CRL field may or may not be included in the PKCS #7 _signedData_ structure.
            /// If the signer is certHostTr34, crTr34 or hlTr34, please refer to the X9 TR34-2012 [Ref. 42] for more details.
            /// If sigHost is specified for the *signer* property specified, then s is a PKCS #7 structure which includes the data that was returned by the StartAuthenticate command.
            /// If cmcmac or cmac are specified for the *signer* property specified, then _signatureKey_ must refer to a key loaded with the key usage of key attribute is M0 to M8.
            /// Base64 encoded data.
            /// </summary>
            [DataMember(Name = "signedData")]
            public string SignedData { get; init; }

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
            /// A payload to the input parameters of the command referred to by the commandName property.
            /// If the specified commandName doesn't require an input parameter, this property can be omitted.
            /// </summary>
            [DataMember(Name = "commandParameters")]
            public object CommandParameters { get; init; }

        }
    }
}
