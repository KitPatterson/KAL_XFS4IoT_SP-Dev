/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * PowerSaveControlHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
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

        private async Task<PowerSaveControlCompletion.PayloadData> HandlePowerSaveControl(IPowerSaveControlEvents events, PowerSaveControlCommand powerSaveControl, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CommonDev.PowerSaveControl()");
            var result = await Device.PowerSaveControl(powerSaveControl.Payload);
            Logger.Log(Constants.DeviceClass, $"CommonDev.PowerSaveControl() -> {result.CompletionCode}");

            return result;
        }
    }
}
