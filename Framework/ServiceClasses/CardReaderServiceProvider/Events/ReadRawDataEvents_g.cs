/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ReadRawDataEvents_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.CardReader
{
    internal class ReadRawDataEvents : CardReaderEvents, IReadRawDataEvents
    {

        public ReadRawDataEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

        public void InsertCardEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InsertCardEvent(requestId));

        public void MediaInsertedEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaInsertedEvent(requestId));

        public void InvalidMediaEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InvalidMediaEvent(requestId));

        public void TrackDetectedEvent(XFS4IoT.CardReader.Events.TrackDetectedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.TrackDetectedEvent(requestId, Payload));

    }
}
