/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * DeleteKeyHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class DeleteKeyHandler
    {

        private Task<DeleteKeyCompletion.PayloadData> HandleDeleteKey(IDeleteKeyEvents events, DeleteKeyCommand deleteKey, CancellationToken cancel)
        {
            //ToDo: Implement HandleDeleteKey for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleDeleteKey for KeyManagement is not implemented in DeleteKeyHandler.cs");
            #else
                #error HandleDeleteKey for KeyManagement is not implemented in DeleteKeyHandler.cs
            #endif
        }

    }
}
