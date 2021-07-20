/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GenerateRSAKeyPairHandler.cs uses automatically generated parts.
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
    public partial class GenerateRSAKeyPairHandler
    {

        private Task<GenerateRSAKeyPairCompletion.PayloadData> HandleGenerateRSAKeyPair(IGenerateRSAKeyPairEvents events, GenerateRSAKeyPairCommand generateRSAKeyPair, CancellationToken cancel)
        {
            //ToDo: Implement HandleGenerateRSAKeyPair for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleGenerateRSAKeyPair for KeyManagement is not implemented in GenerateRSAKeyPairHandler.cs");
            #else
                #error HandleGenerateRSAKeyPair for KeyManagement is not implemented in GenerateRSAKeyPairHandler.cs
            #endif
        }

    }
}
