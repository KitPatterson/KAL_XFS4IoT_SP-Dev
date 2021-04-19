/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCardHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class RetainCardHandler
    {

        private Task HandleRetainCard(IConnection connection, RetainCardCommand retainCard, CancellationToken cancel)
        {
            IRetainCardEvents events = new RetainCardEvents(connection, retainCard.Headers.RequestId);
            //ToDo: Implement HandleRetainCard for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleRetainCard for CardReader is not implemented in RetainCardHandler.cs");
            #else
                #error HandleRetainCard for CardReader is not implemented in RetainCardHandler.cs
            #endif
        }

    }
}
