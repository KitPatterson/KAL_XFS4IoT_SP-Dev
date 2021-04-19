/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SetBlackMarkModeHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;

namespace XFS4IoTFramework.Printer
{
    public partial class SetBlackMarkModeHandler
    {

        private Task HandleSetBlackMarkMode(IConnection connection, SetBlackMarkModeCommand setBlackMarkMode, CancellationToken cancel)
        {
            ISetBlackMarkModeEvents events = new SetBlackMarkModeEvents(connection, setBlackMarkMode.Headers.RequestId);
            //ToDo: Implement HandleSetBlackMarkMode for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetBlackMarkMode for Printer is not implemented in SetBlackMarkModeHandler.cs");
            #else
                #error HandleSetBlackMarkMode for Printer is not implemented in SetBlackMarkModeHandler.cs
            #endif
        }

    }
}
