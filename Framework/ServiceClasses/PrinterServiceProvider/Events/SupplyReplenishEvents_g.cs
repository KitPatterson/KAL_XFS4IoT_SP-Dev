/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SupplyReplenishEvents_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:07
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Printer
{
    internal class SupplyReplenishEvents : PrinterEvents, ISupplyReplenishEvents
    {

        public SupplyReplenishEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

    }
}
