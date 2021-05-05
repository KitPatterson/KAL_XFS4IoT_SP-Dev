/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * StartExchangeHandler.cs uses automatically generated parts.
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
    public partial class StartExchangeHandler
    {

        private Task<StartExchangeCompletion.PayloadData> HandleStartExchange(IStartExchangeEvents events, StartExchangeCommand startExchange, CancellationToken cancel)
        {
            //ToDo: Implement HandleStartExchange for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleStartExchange for CashManagement is not implemented in StartExchangeHandler.cs");
            #else
                #error HandleStartExchange for CashManagement is not implemented in StartExchangeHandler.cs
            #endif
        }

    }
}
