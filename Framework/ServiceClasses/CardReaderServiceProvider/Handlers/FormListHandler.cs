/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * FormListHandler.cs uses automatically generated parts. 
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
    public partial class FormListHandler
    {

        private Task HandleFormList(IConnection connection, FormListCommand formList, CancellationToken cancel)
        {
            IFormListEvents events = new FormListEvents(connection, formList.Headers.RequestId);
            //ToDo: Implement HandleFormList for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleFormList for CardReader is not implemented in FormListHandler.cs");
            #else
                #error HandleFormList for CardReader is not implemented in FormListHandler.cs
            #endif
        }

    }
}
