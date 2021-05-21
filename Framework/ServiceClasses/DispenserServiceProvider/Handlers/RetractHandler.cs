/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;

namespace XFS4IoTFramework.Dispenser
{
    public partial class RetractHandler
    {

        private Task<RetractCompletion.PayloadData> HandleRetract(IRetractEvents events, RetractCommand retract, CancellationToken cancel)
        {
            //ToDo: Implement HandleRetract for Dispenser.
            
            #if DEBUG
                throw new NotImplementedException("HandleRetract for Dispenser is not implemented in RetractHandler.cs");
            #else
                #error HandleRetract for Dispenser is not implemented in RetractHandler.cs
            #endif
        }

    }
}
