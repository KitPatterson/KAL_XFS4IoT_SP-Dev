/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ClearScreenHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
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
    public partial class ClearScreenHandler
    {

        private Task HandleClearScreen(IConnection connection, ClearScreenCommand clearScreen, CancellationToken cancel)
        {
            IClearScreenEvents events = new ClearScreenEvents(connection, clearScreen.Headers.RequestId);
            //ToDo: Implement HandleClearScreen for TextTerminal.
            
            #if DEBUG
                throw new NotImplementedException("HandleClearScreen for TextTerminal is not implemented in ClearScreenHandler.cs");
            #else
                #error HandleClearScreen for TextTerminal is not implemented in ClearScreenHandler.cs
            #endif
        }

    }
}
