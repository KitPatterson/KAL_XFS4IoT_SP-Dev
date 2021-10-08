/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Lights interface.
 * SetLightHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Lights.Commands;
using XFS4IoT.Lights.Completions;

namespace XFS4IoTFramework.Lights
{
    public partial class SetLightHandler
    {

        private Task<SetLightCompletion.PayloadData> HandleSetLight(ISetLightEvents events, SetLightCommand setLight, CancellationToken cancel)
        {
            //ToDo: Implement HandleSetLight for Lights.
            
            #if DEBUG
                throw new NotImplementedException("HandleSetLight for Lights is not implemented in SetLightHandler.cs");
            #else
                #error HandleSetLight for Lights is not implemented in SetLightHandler.cs
            #endif
        }

    }
}
