/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Lights interface.
 * SetLightEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Lights
{
    internal class SetLightEvents : LightsEvents, ISetLightEvents
    {

        public SetLightEvents(IConnection connection, int requestId)
            : base(connection, requestId)
        { }

    }
}
