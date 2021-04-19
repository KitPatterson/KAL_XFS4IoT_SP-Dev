/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetLedHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
    public partial class SetLedHandler
    {

        private Task HandleSetLed(IConnection connection, SetLedCommand setLed, CancellationToken cancel)
        {
            ISetLedEvents events = new SetLedEvents(connection, setLed.Headers.RequestId);
            //ToDo: Implement HandleSetLed for TextTerminal.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetLed for TextTerminal is not implemented in SetLedHandler.cs");
            #else
                #error HandleSetLed for TextTerminal is not implemented in SetLedHandler.cs
            #endif
        }

    }
}
