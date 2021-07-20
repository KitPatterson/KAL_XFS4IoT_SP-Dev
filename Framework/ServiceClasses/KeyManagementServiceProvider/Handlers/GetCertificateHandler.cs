/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GetCertificateHandler.cs uses automatically generated parts.
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
    public partial class GetCertificateHandler
    {

        private Task<GetCertificateCompletion.PayloadData> HandleGetCertificate(IGetCertificateEvents events, GetCertificateCommand getCertificate, CancellationToken cancel)
        {
            //ToDo: Implement HandleGetCertificate for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetCertificate for KeyManagement is not implemented in GetCertificateHandler.cs");
            #else
                #error HandleGetCertificate for KeyManagement is not implemented in GetCertificateHandler.cs
            #endif
        }

    }
}
