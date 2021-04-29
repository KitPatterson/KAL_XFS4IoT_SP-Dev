/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetLedEvents_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:05
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.TextTerminal
{
    internal class SetLedEvents : TextTerminalEvents, ISetLedEvents
    {

        public SetLedEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

    }
}
