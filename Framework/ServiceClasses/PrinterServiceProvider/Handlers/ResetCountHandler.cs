/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ResetCountHandler.cs uses automatically generated parts. 
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
    public partial class ResetCountHandler
    {

        private Task HandleResetCount(IConnection connection, ResetCountCommand resetCount, CancellationToken cancel)
        {
            IResetCountEvents events = new ResetCountEvents(connection, resetCount.Headers.RequestId);
            //ToDo: Implement HandleResetCount for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleResetCount for Printer is not implemented in ResetCountHandler.cs");
            #else
                #error HandleResetCount for Printer is not implemented in ResetCountHandler.cs
            #endif
        }

    }
}
