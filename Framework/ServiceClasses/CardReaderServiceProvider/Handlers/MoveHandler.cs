/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * MoveHandler.cs uses automatically generated parts.
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
    public partial class MoveHandler
    {

        private Task<MoveCompletion.PayloadData> HandleMove(IMoveEvents events, MoveCommand move, CancellationToken cancel)
        {
            //ToDo: Implement HandleMove for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleMove for CardReader is not implemented in MoveHandler.cs");
            #else
                #error HandleMove for CardReader is not implemented in MoveHandler.cs
            #endif
        }

    }
}
