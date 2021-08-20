/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using XFS4IoT.Completions;
using XFS4IoTFramework.Common;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class ExportRSAIssuerSignedItemHandler
    {
        private async Task<ExportRSAIssuerSignedItemCompletion.PayloadData> HandleExportRSAIssuerSignedItem(IExportRSAIssuerSignedItemEvents events, ExportRSAIssuerSignedItemCommand exportRSAIssuerSignedItem, CancellationToken cancel)
        {
            if (exportRSAIssuerSignedItem.Payload.ExportItemType is null)
            {
                return new ExportRSAIssuerSignedItemCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                           $"No item type specified to export.");
            }


            RSAIssuerSignedItemResult result;
            if (exportRSAIssuerSignedItem.Payload.ExportItemType == XFS4IoT.KeyManagement.TypeDataItemToExportEnum.EppId)
            {
                Logger.Log(Constants.DeviceClass, "KeyManagement.ExportEPPIdIssuerSigned()");

                result = await Device.ExportEPPIdIssuerSigned(cancel);

                Logger.Log(Constants.DeviceClass, $"KeyManagement.ExportEPPIdIssuerSigned() -> {result.CompletionCode}");
            }
            else
            {
                Logger.Log(Constants.DeviceClass, "KeyManagement.ExportRSAPublicKeyIssuerSigned()");

                result = await Device.ExportRSAPublicKeyIssuerSigned(exportRSAIssuerSignedItem.Payload.Name, cancel);

                Logger.Log(Constants.DeviceClass, $"KeyManagement.ExportRSAPublicKeyIssuerSigned() -> {result.CompletionCode}, {result.ErrorCode}");
            }

            return new ExportRSAIssuerSignedItemCompletion.PayloadData(result.CompletionCode,
                                                                       result.ErrorDescription,
                                                                       result.ErrorCode,
                                                                       result.Data is not null && result.Data.Count > 0 ? Convert.ToBase64String(result.Data.ToArray()) : null,
                                                                       result.SignatureAlgorithm switch
                                                                       {
                                                                           RSAIssuerSignedItemResult.RSASignatureAlgorithmEnum.RSASSA_PKCS1_V1_5 => XFS4IoT.KeyManagement.RSASignatureAlgorithmEnum.RsassaPkcs1V15,
                                                                           RSAIssuerSignedItemResult.RSASignatureAlgorithmEnum.RSASSA_PSS => XFS4IoT.KeyManagement.RSASignatureAlgorithmEnum.RsassaPss,
                                                                           _ => XFS4IoT.KeyManagement.RSASignatureAlgorithmEnum.Na,
                                                                       },
                                                                       result.Signature is not null && result.Signature.Count > 0 ? Convert.ToBase64String(result.Signature.ToArray()) : null);
        }
    }
}
