/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * SetMixTableHandler.cs uses automatically generated parts.
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
    public partial class SetMixTableHandler
    {

        private Task<SetMixTableCompletion.PayloadData> HandleSetMixTable(ISetMixTableEvents events, SetMixTableCommand setMixTable, CancellationToken cancel)
        {
            //ToDo: Implement HandleSetMixTable for Dispenser.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetMixTable for Dispenser is not implemented in SetMixTableHandler.cs");
            #else
                #error HandleSetMixTable for Dispenser is not implemented in SetMixTableHandler.cs
            #endif
        }

    }
}
