/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * EndExchangeHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Storage.Commands;
using XFS4IoT.Storage.Completions;

namespace XFS4IoTFramework.Storage
{
    public partial class EndExchangeHandler
    {

        private Task<EndExchangeCompletion.PayloadData> HandleEndExchange(IEndExchangeEvents events, EndExchangeCommand endExchange, CancellationToken cancel)
        {
            //ToDo: Implement HandleEndExchange for Storage.
            
            #if DEBUG
                throw new NotImplementedException("HandleEndExchange for Storage is not implemented in EndExchangeHandler.cs");
            #else
                #error HandleEndExchange for Storage is not implemented in EndExchangeHandler.cs
            #endif
        }

    }
}
