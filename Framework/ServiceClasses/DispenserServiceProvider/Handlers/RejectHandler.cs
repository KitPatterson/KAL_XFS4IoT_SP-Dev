/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * RejectHandler.cs uses automatically generated parts.
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
    public partial class RejectHandler
    {

        private Task<RejectCompletion.PayloadData> HandleReject(IRejectEvents events, RejectCommand reject, CancellationToken cancel)
        {
            //ToDo: Implement HandleReject for Dispenser.
            
            #if DEBUG
                throw new NotImplementedException("HandleReject for Dispenser is not implemented in RejectHandler.cs");
            #else
                #error HandleReject for Dispenser is not implemented in RejectHandler.cs
            #endif
        }

    }
}
