/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * GetItemInfoHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CashManagement.Commands;
using XFS4IoT.CashManagement.Completions;

namespace XFS4IoTFramework.CashManagement
{
    public partial class GetItemInfoHandler
    {

        private Task<GetItemInfoCompletion.PayloadData> HandleGetItemInfo(IGetItemInfoEvents events, GetItemInfoCommand getItemInfo, CancellationToken cancel)
        {
            //ToDo: Implement HandleGetItemInfo for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetItemInfo for CashManagement is not implemented in GetItemInfoHandler.cs");
            #else
                #error HandleGetItemInfo for CashManagement is not implemented in GetItemInfoHandler.cs
            #endif
        }

    }
}
