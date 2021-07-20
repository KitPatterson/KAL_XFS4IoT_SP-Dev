/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * ExportRSAEPPSignedItemHandler.cs uses automatically generated parts.
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
    public partial class ExportRSAEPPSignedItemHandler
    {

        private Task<ExportRSAEPPSignedItemCompletion.PayloadData> HandleExportRSAEPPSignedItem(IExportRSAEPPSignedItemEvents events, ExportRSAEPPSignedItemCommand exportRSAEPPSignedItem, CancellationToken cancel)
        {
            //ToDo: Implement HandleExportRSAEPPSignedItem for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleExportRSAEPPSignedItem for KeyManagement is not implemented in ExportRSAEPPSignedItemHandler.cs");
            #else
                #error HandleExportRSAEPPSignedItem for KeyManagement is not implemented in ExportRSAEPPSignedItemHandler.cs
            #endif
        }

    }
}
