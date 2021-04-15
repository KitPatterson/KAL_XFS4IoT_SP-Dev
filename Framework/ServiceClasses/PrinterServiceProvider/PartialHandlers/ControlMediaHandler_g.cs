/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ControlMediaHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(ControlMediaCommand))]
    public partial class ControlMediaHandler : ICommandHandler
    {
        public ControlMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as IPrinterDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ControlMediaHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ControlMediaCommand controlMediaCmd = command as ControlMediaCommand;
            controlMediaCmd.IsNotNull($"Invalid parameter in the ControlMedia Handle method. {nameof(controlMediaCmd)}");

            await HandleControlMedia(Connection, controlMediaCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ControlMediaCommand controlMediacommand = command as ControlMediaCommand;

            ControlMediaCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ControlMediaCompletion response = new ControlMediaCompletion(controlMediacommand.Headers.RequestId, new ControlMediaCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
