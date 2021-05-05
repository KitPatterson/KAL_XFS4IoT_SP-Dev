/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * CountHandler.cs uses automatically generated parts.
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
    public partial class CountHandler
    {

        private Task<CountCompletion.PayloadData> HandleCount(ICountEvents events, CountCommand count, CancellationToken cancel)
        {
            //ToDo: Implement HandleCount for Dispenser.
            
            #if DEBUG
                throw new NotImplementedException("HandleCount for Dispenser is not implemented in CountHandler.cs");
            #else
                #error HandleCount for Dispenser is not implemented in CountHandler.cs
            #endif
        }

    }
}
