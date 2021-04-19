/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryFieldHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;

namespace XFS4IoTFramework.Printer
{
    public partial class GetQueryFieldHandler
    {

        private Task HandleGetQueryField(IConnection connection, GetQueryFieldCommand getQueryField, CancellationToken cancel)
        {
            IGetQueryFieldEvents events = new GetQueryFieldEvents(connection, getQueryField.Headers.RequestId);
            //ToDo: Implement HandleGetQueryField for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetQueryField for Printer is not implemented in GetQueryFieldHandler.cs");
            #else
                #error HandleGetQueryField for Printer is not implemented in GetQueryFieldHandler.cs
            #endif
        }

    }
}
