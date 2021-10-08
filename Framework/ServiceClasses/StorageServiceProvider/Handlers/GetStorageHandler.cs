/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * GetStorageHandler.cs uses automatically generated parts.
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
    public partial class GetStorageHandler
    {

        private Task<GetStorageCompletion.PayloadData> HandleGetStorage(IGetStorageEvents events, GetStorageCommand getStorage, CancellationToken cancel)
        {
            //ToDo: Implement HandleGetStorage for Storage.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetStorage for Storage is not implemented in GetStorageHandler.cs");
            #else
                #error HandleGetStorage for Storage is not implemented in GetStorageHandler.cs
            #endif
        }

    }
}
