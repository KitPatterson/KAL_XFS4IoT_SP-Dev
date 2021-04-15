/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryFormEvents_g.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.Printer
{
    internal class GetQueryFormEvents : PrinterEvents, IGetQueryFormEvents
    {

        public GetQueryFormEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

    }
}
