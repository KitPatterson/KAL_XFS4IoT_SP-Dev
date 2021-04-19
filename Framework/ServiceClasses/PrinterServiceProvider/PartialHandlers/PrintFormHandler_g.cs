/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrintFormHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(PrintFormCommand))]
    public partial class PrintFormHandler : ICommandHandler
    {
        public PrintFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PrintFormHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(PrintFormHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(PrintFormHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            PrintFormCommand printFormCmd = command as PrintFormCommand;
            printFormCmd.IsNotNull($"Invalid parameter in the PrintForm Handle method. {nameof(printFormCmd)}");

            await HandlePrintForm(Connection, printFormCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            PrintFormCommand printFormcommand = command as PrintFormCommand;

            PrintFormCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => PrintFormCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => PrintFormCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => PrintFormCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            PrintFormCompletion response = new PrintFormCompletion(printFormcommand.Headers.RequestId, new PrintFormCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
