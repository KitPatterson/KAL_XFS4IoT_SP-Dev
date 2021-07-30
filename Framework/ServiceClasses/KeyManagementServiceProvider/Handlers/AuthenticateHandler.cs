/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * AuthenticateHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class AuthenticateHandler
    {

        private Task<AuthenticateCompletion.PayloadData> HandleAuthenticate(IAuthenticateEvents events, AuthenticateCommand authenticate, CancellationToken cancel)
        {
            //ToDo: Implement HandleAuthenticate for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleAuthenticate for KeyManagement is not implemented in AuthenticateHandler.cs");
            #else
                #error HandleAuthenticate for KeyManagement is not implemented in AuthenticateHandler.cs
            #endif
        }

    }
}
