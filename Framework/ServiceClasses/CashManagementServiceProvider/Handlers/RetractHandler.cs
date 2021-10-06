/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * RetractHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CashManagement.Commands;
using XFS4IoT.CashManagement.Completions;

namespace XFS4IoTFramework.CashManagement
{
    public partial class RetractHandler
    {

        private Task<RetractCompletion.PayloadData> HandleRetract(IRetractEvents events, RetractCommand retract, CancellationToken cancel)
        {
            //ToDo: Implement HandleRetract for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleRetract for CashManagement is not implemented in RetractHandler.cs");
            #else
                #error HandleRetract for CashManagement is not implemented in RetractHandler.cs
            #endif
        }

    }
}
