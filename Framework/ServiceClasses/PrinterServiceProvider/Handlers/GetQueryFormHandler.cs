/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryFormHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
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
    public partial class GetQueryFormHandler
    {

        private Task HandleGetQueryForm(IConnection connection, GetQueryFormCommand getQueryForm, CancellationToken cancel)
        {
            IGetQueryFormEvents events = new GetQueryFormEvents(connection, getQueryForm.Headers.RequestId);
            //ToDo: Implement HandleGetQueryForm for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetQueryForm for Printer is not implemented in GetQueryFormHandler.cs");
            #else
                #error HandleGetQueryForm for Printer is not implemented in GetQueryFormHandler.cs
            #endif
        }

    }
}
