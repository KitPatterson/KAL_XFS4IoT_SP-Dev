/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * ReplaceCertificateHandler.cs uses automatically generated parts.
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
    public partial class ReplaceCertificateHandler
    {

        private Task<ReplaceCertificateCompletion.PayloadData> HandleReplaceCertificate(IReplaceCertificateEvents events, ReplaceCertificateCommand replaceCertificate, CancellationToken cancel)
        {
            //ToDo: Implement HandleReplaceCertificate for KeyManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleReplaceCertificate for KeyManagement is not implemented in ReplaceCertificateHandler.cs");
            #else
                #error HandleReplaceCertificate for KeyManagement is not implemented in ReplaceCertificateHandler.cs
            #endif
        }

    }
}
