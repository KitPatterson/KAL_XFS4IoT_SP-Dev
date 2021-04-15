/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * CardReaderEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:41:31
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.CardReader
{
    internal class CardReaderEvents : ICardReaderEvents
    {
        protected readonly IConnection connection;
        protected readonly string requestId;

        public CardReaderEvents(IConnection connection, string requestId)
        {
            this.connection = connection;
            Contracts.IsNotNullOrWhitespace(requestId, $"Unexpected request ID is received. {requestId}");
            this.requestId = requestId;
        }

        public void MediaRemovedEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaRemovedEvent(requestId));

        public void RetainBinThresholdEvent(XFS4IoT.CardReader.Events.RetainBinThresholdEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.RetainBinThresholdEvent(requestId, Payload));

        public void CardActionEvent(XFS4IoT.CardReader.Events.CardActionEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.CardActionEvent(requestId, Payload));

    }
}
