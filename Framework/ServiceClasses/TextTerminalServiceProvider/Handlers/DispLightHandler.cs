/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DispLightHandler.cs uses automatically generated parts. 
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
    public partial class DispLightHandler
    {

        private Task HandleDispLight(IConnection connection, DispLightCommand dispLight, CancellationToken cancel)
        {
            IDispLightEvents events = new DispLightEvents(connection, dispLight.Headers.RequestId);
            //ToDo: Implement HandleDispLight for TextTerminal.
            
            #if DEBUG
                throw new NotImplementedException("HandleDispLight for TextTerminal is not implemented in DispLightHandler.cs");
            #else
                #error HandleDispLight for TextTerminal is not implemented in DispLightHandler.cs
            #endif
        }

    }
}
