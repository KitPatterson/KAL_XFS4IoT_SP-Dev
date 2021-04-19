/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessQueryApplicationsHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class EMVClessQueryApplicationsHandler
    {

        private Task HandleEMVClessQueryApplications(IConnection connection, EMVClessQueryApplicationsCommand eMVClessQueryApplications, CancellationToken cancel)
        {
            IEMVClessQueryApplicationsEvents events = new EMVClessQueryApplicationsEvents(connection, eMVClessQueryApplications.Headers.RequestId);
            //ToDo: Implement HandleEMVClessQueryApplications for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleEMVClessQueryApplications for CardReader is not implemented in EMVClessQueryApplicationsHandler.cs");
            #else
                #error HandleEMVClessQueryApplications for CardReader is not implemented in EMVClessQueryApplicationsHandler.cs
            #endif
        }

    }
}
