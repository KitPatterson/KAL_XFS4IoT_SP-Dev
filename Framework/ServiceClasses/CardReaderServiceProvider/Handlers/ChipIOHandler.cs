/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipIOHandler.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
    public partial class ChipIOHandler
    {

        private Task HandleChipIO(IConnection connection, ChipIOCommand chipIO, CancellationToken cancel)
        {
            IChipIOEvents events = new ChipIOEvents(connection, chipIO.Headers.RequestId);
            //ToDo: Implement HandleChipIO for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleChipIO for CardReader is not implemented in ChipIOHandler.cs");
            #else
                #error HandleChipIO for CardReader is not implemented in ChipIOHandler.cs
            #endif
        }

    }
}
