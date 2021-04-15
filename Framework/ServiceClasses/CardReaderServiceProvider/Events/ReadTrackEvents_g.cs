/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ReadTrackEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:41:31
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.CardReader
{
    internal class ReadTrackEvents : CardReaderEvents, IReadTrackEvents
    {

        public ReadTrackEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

        public void InsertCardEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InsertCardEvent(requestId));

        public void MediaInsertedEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.MediaInsertedEvent(requestId));

        public void InvalidTrackDataEvent(XFS4IoT.CardReader.Events.InvalidTrackDataEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InvalidTrackDataEvent(requestId, Payload));

        public void InvalidMediaEvent() => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.InvalidMediaEvent(requestId));

        public void TrackDetectedEvent(XFS4IoT.CardReader.Events.TrackDetectedEvent.PayloadData Payload) => connection.SendMessageAsync(new XFS4IoT.CardReader.Events.TrackDetectedEvent(requestId, Payload));

    }
}
