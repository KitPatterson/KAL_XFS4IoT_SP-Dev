/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * IReadTrackEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:41:31
\***********************************************************************************************/


using XFS4IoTServer;

namespace XFS4IoTFramework.CardReader
{
    public interface IReadTrackEvents : ICardReaderEvents
    {

        void InsertCardEvent();

        void MediaInsertedEvent();

        void InvalidTrackDataEvent(XFS4IoT.CardReader.Events.InvalidTrackDataEvent.PayloadData Payload);

        void InvalidMediaEvent();

        void TrackDetectedEvent(XFS4IoT.CardReader.Events.TrackDetectedEvent.PayloadData Payload);

    }
}
