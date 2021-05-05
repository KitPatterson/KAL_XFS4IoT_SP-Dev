/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * SetClassificationListHandler.cs uses automatically generated parts.
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
    public partial class SetClassificationListHandler
    {

        private Task<SetClassificationListCompletion.PayloadData> HandleSetClassificationList(ISetClassificationListEvents events, SetClassificationListCommand setClassificationList, CancellationToken cancel)
        {
            //ToDo: Implement HandleSetClassificationList for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetClassificationList for CashManagement is not implemented in SetClassificationListHandler.cs");
            #else
                #error HandleSetClassificationList for CashManagement is not implemented in SetClassificationListHandler.cs
            #endif
        }

    }
}
