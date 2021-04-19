/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ControlMediaHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
    public partial class ControlMediaHandler
    {

        private Task HandleControlMedia(IConnection connection, ControlMediaCommand controlMedia, CancellationToken cancel)
        {
            IControlMediaEvents events = new ControlMediaEvents(connection, controlMedia.Headers.RequestId);
            //ToDo: Implement HandleControlMedia for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleControlMedia for Printer is not implemented in ControlMediaHandler.cs");
            #else
                #error HandleControlMedia for Printer is not implemented in ControlMediaHandler.cs
            #endif
        }

    }
}
