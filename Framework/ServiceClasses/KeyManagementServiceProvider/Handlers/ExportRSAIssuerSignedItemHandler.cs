/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
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
    public partial class ExportRSAIssuerSignedItemHandler
    {
        private Task<ExportRSAIssuerSignedItemCompletion.PayloadData> HandleExportRSAIssuerSignedItem(IExportRSAIssuerSignedItemEvents events, ExportRSAIssuerSignedItemCommand exportRSAIssuerSignedItem, CancellationToken cancel)
        {
            /// TODO: This command to be deleted from YAML
            throw new NotImplementedException("HandleExportRSAIssuerSignedItem for KeyManagement is not implemented in ExportRSAIssuerSignedItemHandler.cs");
        }
    }
}
