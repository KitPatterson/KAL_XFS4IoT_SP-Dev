/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteTrackHandler.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class WriteTrackHandler
    {

        private Task HandleWriteTrack(IConnection connection, WriteTrackCommand writeTrack, CancellationToken cancel)
        {
            IWriteTrackEvents events = new WriteTrackEvents(connection, writeTrack.Headers.RequestId);
            //ToDo: Implement HandleWriteTrack for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleWriteTrack for CardReader is not implemented in WriteTrackHandler.cs");
            #else
                #error HandleWriteTrack for CardReader is not implemented in WriteTrackHandler.cs
            #endif
        }

    }
}
