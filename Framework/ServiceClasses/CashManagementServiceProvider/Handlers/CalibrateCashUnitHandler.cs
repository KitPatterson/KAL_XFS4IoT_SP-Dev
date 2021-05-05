/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * CalibrateCashUnitHandler.cs uses automatically generated parts.
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
    public partial class CalibrateCashUnitHandler
    {

        private Task<CalibrateCashUnitCompletion.PayloadData> HandleCalibrateCashUnit(ICalibrateCashUnitEvents events, CalibrateCashUnitCommand calibrateCashUnit, CancellationToken cancel)
        {
            //ToDo: Implement HandleCalibrateCashUnit for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleCalibrateCashUnit for CashManagement is not implemented in CalibrateCashUnitHandler.cs");
            #else
                #error HandleCalibrateCashUnit for CashManagement is not implemented in CalibrateCashUnitHandler.cs
            #endif
        }

    }
}
