/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryFormHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
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
    public partial class QueryFormHandler
    {

        private Task HandleQueryForm(IConnection connection, QueryFormCommand queryForm, CancellationToken cancel)
        {
            IQueryFormEvents events = new QueryFormEvents(connection, queryForm.Headers.RequestId);
            //ToDo: Implement HandleQueryForm for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleQueryForm for CardReader is not implemented in QueryFormHandler.cs");
            #else
                #error HandleQueryForm for CardReader is not implemented in QueryFormHandler.cs
            #endif
        }

    }
}
