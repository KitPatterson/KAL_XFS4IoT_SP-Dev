/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * PrepareDispenseHandler.cs uses automatically generated parts.
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
    public partial class PrepareDispenseHandler
    {

        private Task<PrepareDispenseCompletion.PayloadData> HandlePrepareDispense(IPrepareDispenseEvents events, PrepareDispenseCommand prepareDispense, CancellationToken cancel)
        {
            //ToDo: Implement HandlePrepareDispense for Dispenser.
            
            #if DEBUG
                throw new NotImplementedException("HandlePrepareDispense for Dispenser is not implemented in PrepareDispenseHandler.cs");
            #else
                #error HandlePrepareDispense for Dispenser is not implemented in PrepareDispenseHandler.cs
            #endif
        }

    }
}
