/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryIFMIdentifierHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
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
    public partial class QueryIFMIdentifierHandler
    {

        private Task HandleQueryIFMIdentifier(IConnection connection, QueryIFMIdentifierCommand queryIFMIdentifier, CancellationToken cancel)
        {
            IQueryIFMIdentifierEvents events = new QueryIFMIdentifierEvents(connection, queryIFMIdentifier.Headers.RequestId);
            //ToDo: Implement HandleQueryIFMIdentifier for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleQueryIFMIdentifier for CardReader is not implemented in QueryIFMIdentifierHandler.cs");
            #else
                #error HandleQueryIFMIdentifier for CardReader is not implemented in QueryIFMIdentifierHandler.cs
            #endif
        }

    }
}
