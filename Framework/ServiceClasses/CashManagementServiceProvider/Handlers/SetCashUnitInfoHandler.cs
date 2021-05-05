/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * SetCashUnitInfoHandler.cs uses automatically generated parts.
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
    public partial class SetCashUnitInfoHandler
    {

        private Task<SetCashUnitInfoCompletion.PayloadData> HandleSetCashUnitInfo(ISetCashUnitInfoEvents events, SetCashUnitInfoCommand setCashUnitInfo, CancellationToken cancel)
        {
            //ToDo: Implement HandleSetCashUnitInfo for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetCashUnitInfo for CashManagement is not implemented in SetCashUnitInfoHandler.cs");
            #else
                #error HandleSetCashUnitInfo for CashManagement is not implemented in SetCashUnitInfoHandler.cs
            #endif
        }

    }
}
