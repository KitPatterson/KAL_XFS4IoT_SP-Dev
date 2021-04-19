/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * DispensePaperHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(DispensePaperCommand))]
    public partial class DispensePaperHandler : ICommandHandler
    {
        public DispensePaperHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DispensePaperHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(DispensePaperHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(DispensePaperHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            DispensePaperCommand dispensePaperCmd = command as DispensePaperCommand;
            dispensePaperCmd.IsNotNull($"Invalid parameter in the DispensePaper Handle method. {nameof(dispensePaperCmd)}");

            await HandleDispensePaper(Connection, dispensePaperCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            DispensePaperCommand dispensePapercommand = command as DispensePaperCommand;

            DispensePaperCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => DispensePaperCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => DispensePaperCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => DispensePaperCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            DispensePaperCompletion response = new DispensePaperCompletion(dispensePapercommand.Headers.RequestId, new DispensePaperCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
