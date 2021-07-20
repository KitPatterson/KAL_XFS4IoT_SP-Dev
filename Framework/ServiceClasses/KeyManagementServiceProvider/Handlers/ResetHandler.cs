/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * ResetHandler.cs uses automatically generated parts.
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
    public partial class ResetHandler
    {

        private Task<ResetCompletion.PayloadData> HandleReset(IResetEvents events, ResetCommand reset, CancellationToken cancel)
        {
            //ToDo: Implement HandleReset for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleReset for KeyManagement is not implemented in ResetHandler.cs");
            #else
                #error HandleReset for KeyManagement is not implemented in ResetHandler.cs
            #endif
        }

    }
}
