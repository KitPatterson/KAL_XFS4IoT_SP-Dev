/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * LoadDefinitionHandler.cs uses automatically generated parts. 
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
    public partial class LoadDefinitionHandler
    {

        private Task HandleLoadDefinition(IConnection connection, LoadDefinitionCommand loadDefinition, CancellationToken cancel)
        {
            ILoadDefinitionEvents events = new LoadDefinitionEvents(connection, loadDefinition.Headers.RequestId);
            //ToDo: Implement HandleLoadDefinition for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleLoadDefinition for Printer is not implemented in LoadDefinitionHandler.cs");
            #else
                #error HandleLoadDefinition for Printer is not implemented in LoadDefinitionHandler.cs
            #endif
        }

    }
}
