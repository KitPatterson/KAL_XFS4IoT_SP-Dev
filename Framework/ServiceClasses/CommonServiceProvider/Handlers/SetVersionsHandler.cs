/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetVersionsHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    public partial class SetVersionsHandler
    {

        private Task<SetVersionsCompletion.PayloadData> HandleSetVersions(ISetVersionsEvents events, SetVersionsCommand setVersions, CancellationToken cancel)
        {
            //ToDo: Implement HandleSetVersions for Common.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetVersions for Common is not implemented in SetVersionsHandler.cs");
            #else
                #error HandleSetVersions for Common is not implemented in SetVersionsHandler.cs
            #endif
        }

    }
}
