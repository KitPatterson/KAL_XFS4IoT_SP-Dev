/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ControlMediaHandler_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:07
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(ControlMediaCommand))]
    public partial class ControlMediaHandler : ICommandHandler
    {
        public ControlMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<PrinterServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ControlMediaHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var controlMediaCmd = command.IsA<ControlMediaCommand>($"Invalid parameter in the ControlMedia Handle method. {nameof(ControlMediaCommand)}");
            
            IControlMediaEvents events = new ControlMediaEvents(Connection, controlMediaCmd.Headers.RequestId);

            var result = await HandleControlMedia(events, controlMediaCmd, cancel);
            await Connection.SendMessageAsync(new ControlMediaCompletion(controlMediaCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var controlMediacommand = command.IsA<ControlMediaCommand>();

            ControlMediaCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ControlMediaCompletion(controlMediacommand.Headers.RequestId, new ControlMediaCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private IPrinterDevice Device { get; }
        private PrinterServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
