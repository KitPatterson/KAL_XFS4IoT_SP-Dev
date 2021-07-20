/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * GenerateRandomHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Crypto.Commands;
using XFS4IoT.Crypto.Completions;

namespace XFS4IoTFramework.Crypto
{
    public partial class GenerateRandomHandler
    {

        private Task<GenerateRandomCompletion.PayloadData> HandleGenerateRandom(IGenerateRandomEvents events, GenerateRandomCommand generateRandom, CancellationToken cancel)
        {
            //ToDo: Implement HandleGenerateRandom for Crypto.
            
            #if DEBUG
                throw new NotImplementedException("HandleGenerateRandom for Crypto is not implemented in GenerateRandomHandler.cs");
            #else
                #error HandleGenerateRandom for Crypto is not implemented in GenerateRandomHandler.cs
            #endif
        }

    }
}
