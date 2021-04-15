/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ResetHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.TextTerminal.Commands;
using XFS4IoT.TextTerminal.Completions;

namespace XFS4IoTFramework.TextTerminal
{
    public partial class ResetHandler
    {

        private Task HandleReset(IConnection connection, ResetCommand reset, CancellationToken cancel)
        {
            IResetEvents events = new ResetEvents(connection, reset.Headers.RequestId);
            //ToDo: Implement HandleReset for TextTerminal.
            
            #if DEBUG
                throw new NotImplementedException("HandleReset for TextTerminal is not implemented in ResetHandler.cs");
            #else
                #error HandleReset for TextTerminal is not implemented in ResetHandler.cs
            #endif
        }

    }
}
