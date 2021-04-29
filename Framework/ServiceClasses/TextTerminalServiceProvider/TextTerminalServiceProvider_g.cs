/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * TextTerminalServiceProvider.cs.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:06
\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;

using XFS4IoT;

namespace XFS4IoTServer
{
    public partial class TextTerminalServiceClass : ServiceProvider, ITextTerminalServiceClass
    {
        public TextTerminalServiceClass(EndpointDetails endPoint, string ServiceName, IEnumerable<XFSConstants.ServiceClass> serviceClasses, IDevice device, ILogger logger)
            : base(endPoint, ServiceName, serviceClasses, device, logger)
        {
        }
        public async Task FieldErrorEvent(XFS4IoT.TextTerminal.Events.FieldErrorEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.TextTerminal.Events.FieldErrorEvent(Payload));

        public async Task FieldWarningEvent()
            => await BroadcastEvent(new XFS4IoT.TextTerminal.Events.FieldWarningEvent());

        public async Task KeyEvent(XFS4IoT.TextTerminal.Events.KeyEvent.PayloadData Payload)
            => await BroadcastEvent(new XFS4IoT.TextTerminal.Events.KeyEvent(Payload));

    }
}
