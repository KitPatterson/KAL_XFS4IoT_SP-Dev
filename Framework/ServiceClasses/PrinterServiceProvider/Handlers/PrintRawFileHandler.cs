/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrintRawFileHandler.cs uses automatically generated parts. 
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
    public partial class PrintRawFileHandler
    {

        private Task HandlePrintRawFile(IConnection connection, PrintRawFileCommand printRawFile, CancellationToken cancel)
        {
            IPrintRawFileEvents events = new PrintRawFileEvents(connection, printRawFile.Headers.RequestId);
            //ToDo: Implement HandlePrintRawFile for Printer.
            
            #if DEBUG
                throw new NotImplementedException("HandlePrintRawFile for Printer is not implemented in PrintRawFileHandler.cs");
            #else
                #error HandlePrintRawFile for Printer is not implemented in PrintRawFileHandler.cs
            #endif
        }

    }
}
