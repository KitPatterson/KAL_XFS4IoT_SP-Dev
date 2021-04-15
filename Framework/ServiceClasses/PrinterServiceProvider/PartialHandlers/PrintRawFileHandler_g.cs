/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrintRawFileHandler_g.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(PrintRawFileCommand))]
    public partial class PrintRawFileHandler : ICommandHandler
    {
        public PrintRawFileHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PrintRawFileHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(PrintRawFileHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(PrintRawFileHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as IPrinterDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(PrintRawFileHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(PrintRawFileHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            PrintRawFileCommand printRawFileCmd = command as PrintRawFileCommand;
            printRawFileCmd.IsNotNull($"Invalid parameter in the PrintRawFile Handle method. {nameof(printRawFileCmd)}");

            await HandlePrintRawFile(Connection, printRawFileCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            PrintRawFileCommand printRawFilecommand = command as PrintRawFileCommand;

            PrintRawFileCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => PrintRawFileCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => PrintRawFileCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => PrintRawFileCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            PrintRawFileCompletion response = new PrintRawFileCompletion(printRawFilecommand.Headers.RequestId, new PrintRawFileCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
