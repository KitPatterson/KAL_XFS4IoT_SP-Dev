/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GenerateKCVHandler.cs uses automatically generated parts.
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
    public partial class GenerateKCVHandler
    {

        private Task<GenerateKCVCompletion.PayloadData> HandleGenerateKCV(IGenerateKCVEvents events, GenerateKCVCommand generateKCV, CancellationToken cancel)
        {
            //ToDo: Implement HandleGenerateKCV for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleGenerateKCV for KeyManagement is not implemented in GenerateKCVHandler.cs");
            #else
                #error HandleGenerateKCV for KeyManagement is not implemented in GenerateKCVHandler.cs
            #endif
        }

    }
}
