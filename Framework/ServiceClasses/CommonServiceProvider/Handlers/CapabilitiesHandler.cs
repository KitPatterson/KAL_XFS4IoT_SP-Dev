/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CapabilitiesHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 13:51:50
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
    public partial class CapabilitiesHandler
    {

        private async Task<CapabilitiesCompletion.PayloadData> HandleCapabilities(ICapabilitiesEvents events, CapabilitiesCommand capabilities, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CommonDev.Capabilities()");
            var result = await Device.Capabilities(events, capabilities.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CommonDev.Capabilities() -> {result.CompletionCode}");

            return result;
        }

    }
}
