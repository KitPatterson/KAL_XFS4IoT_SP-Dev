/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetGuidanceLightHandler.cs uses automatically generated parts. 
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
    public partial class SetGuidanceLightHandler
    {

        private Task<SetGuidanceLightCompletion.PayloadData> HandleSetGuidanceLight(ISetGuidanceLightEvents events, SetGuidanceLightCommand setGuidanceLight, CancellationToken cancel)
        {
            //NOTE: GuideLights doesn't support in common interface. All guidelight control is done by the SIU
            //This command exists in the XFS4IoT common interface spec for the April 2021 preview
            
            #if DEBUG
                throw new NotImplementedException("SetGuidanceLight is obsolete in the common interface.");
            #else
                #error HandleSetGuidanceLight for Common is not implemented in SetGuidanceLightHandler.cs
            #endif
        }

    }
}