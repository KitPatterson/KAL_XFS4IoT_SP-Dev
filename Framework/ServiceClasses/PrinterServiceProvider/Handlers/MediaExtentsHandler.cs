/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaExtentsHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;

namespace XFS4IoTFramework.Printer
{
    public partial class MediaExtentsHandler
    {

        private Task HandleMediaExtents(IConnection connection, MediaExtentsCommand mediaExtents, CancellationToken cancel)
        {
            IMediaExtentsEvents events = new MediaExtentsEvents(connection, mediaExtents.Headers.RequestId);
            //ToDo: Implement HandleMediaExtents for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandleMediaExtents for Printer is not implemented in MediaExtentsHandler.cs");
            #else
                #error HandleMediaExtents for Printer is not implemented in MediaExtentsHandler.cs
            #endif
        }

    }
}
