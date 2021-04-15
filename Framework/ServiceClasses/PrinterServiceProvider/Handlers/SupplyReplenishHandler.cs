/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SupplyReplenishHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
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
    public partial class SupplyReplenishHandler
    {

        private Task HandleSupplyReplenish(IConnection connection, SupplyReplenishCommand supplyReplenish, CancellationToken cancel)
        {
            ISupplyReplenishEvents events = new SupplyReplenishEvents(connection, supplyReplenish.Headers.RequestId);
            //ToDo: Implement HandleSupplyReplenish for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleSupplyReplenish for Printer is not implemented in SupplyReplenishHandler.cs");
            #else
                #error HandleSupplyReplenish for Printer is not implemented in SupplyReplenishHandler.cs
            #endif
        }

    }
}
