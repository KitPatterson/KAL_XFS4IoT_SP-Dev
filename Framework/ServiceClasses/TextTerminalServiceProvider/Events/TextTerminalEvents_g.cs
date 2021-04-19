/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * TextTerminalEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.TextTerminal
{
    internal class TextTerminalEvents : ITextTerminalEvents
    {
        protected readonly IConnection connection;
        protected readonly string requestId;

        public TextTerminalEvents(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public async Task FieldErrorEvent(XFS4IoT.TextTerminal.Events.FieldErrorEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.TextTerminal.Events.FieldErrorEvent(requestId, Payload));

        public async Task FieldWarningEvent() => await connection.SendMessageAsync(new XFS4IoT.TextTerminal.Events.FieldWarningEvent(requestId));

        public async Task KeyEvent(XFS4IoT.TextTerminal.Events.KeyEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.TextTerminal.Events.KeyEvent(requestId, Payload));

    }
}
