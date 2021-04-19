/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * PowerSaveControlHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    public partial class PowerSaveControlHandler
    {

        private Task HandlePowerSaveControl(IConnection connection, PowerSaveControlCommand powerSaveControl, CancellationToken cancel)
        {
            IPowerSaveControlEvents events = new PowerSaveControlEvents(connection, powerSaveControl.Headers.RequestId);
            //ToDo: Implement HandlePowerSaveControl for Common.
            
            #if DEBUG
                throw new NotImplementedException("HandlePowerSaveControl for Common is not implemented in PowerSaveControlHandler.cs");
            #else
                #error HandlePowerSaveControl for Common is not implemented in PowerSaveControlHandler.cs
            #endif
        }

    }
}
