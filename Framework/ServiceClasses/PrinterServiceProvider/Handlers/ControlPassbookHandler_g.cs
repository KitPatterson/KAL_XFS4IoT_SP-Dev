/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ControlPassbookHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(ControlPassbookCommand))]
    public partial class ControlPassbookHandler : ICommandHandler
    {
        public ControlPassbookHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ControlPassbookHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ControlPassbookHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ControlPassbookHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ControlPassbookCommand controlPassbookCmd = command as ControlPassbookCommand;
            controlPassbookCmd.IsNotNull($"Invalid parameter in the ControlPassbook Handle method. {nameof(controlPassbookCmd)}");
            
            IControlPassbookEvents events = new ControlPassbookEvents(Connection, controlPassbookCmd.Headers.RequestId);

            var result = await HandleControlPassbook(events, controlPassbookCmd, cancel);
            await Connection.SendMessageAsync(new ControlPassbookCompletion(controlPassbookCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ControlPassbookCommand controlPassbookcommand = command as ControlPassbookCommand;

            ControlPassbookCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ControlPassbookCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ControlPassbookCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ControlPassbookCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ControlPassbookCompletion response = new ControlPassbookCompletion(controlPassbookcommand.Headers.RequestId, new ControlPassbookCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}