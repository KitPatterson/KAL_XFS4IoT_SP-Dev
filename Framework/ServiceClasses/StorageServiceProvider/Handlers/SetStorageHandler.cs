/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * SetStorageHandler.cs uses automatically generated parts.
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
    public partial class SetStorageHandler
    {

        private Task<SetStorageCompletion.PayloadData> HandleSetStorage(ISetStorageEvents events, SetStorageCommand setStorage, CancellationToken cancel)
        {
            //ToDo: Implement HandleSetStorage for Storage.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetStorage for Storage is not implemented in SetStorageHandler.cs");
            #else
                #error HandleSetStorage for Storage is not implemented in SetStorageHandler.cs
            #endif
        }

    }
}
