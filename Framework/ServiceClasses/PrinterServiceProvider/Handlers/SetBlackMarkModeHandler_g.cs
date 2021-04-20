/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SetBlackMarkModeHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(SetBlackMarkModeCommand))]
    public partial class SetBlackMarkModeHandler : ICommandHandler
    {
        public SetBlackMarkModeHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetBlackMarkModeHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetBlackMarkModeHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetBlackMarkModeHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SetBlackMarkModeCommand setBlackMarkModeCmd = command as SetBlackMarkModeCommand;
            setBlackMarkModeCmd.IsNotNull($"Invalid parameter in the SetBlackMarkMode Handle method. {nameof(setBlackMarkModeCmd)}");
            
            ISetBlackMarkModeEvents events = new SetBlackMarkModeEvents(Connection, setBlackMarkModeCmd.Headers.RequestId);

            var result = await HandleSetBlackMarkMode(events, setBlackMarkModeCmd, cancel);
            await Connection.SendMessageAsync(new SetBlackMarkModeCompletion(setBlackMarkModeCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SetBlackMarkModeCommand setBlackMarkModecommand = command as SetBlackMarkModeCommand;

            SetBlackMarkModeCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetBlackMarkModeCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetBlackMarkModeCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetBlackMarkModeCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SetBlackMarkModeCompletion response = new SetBlackMarkModeCompletion(setBlackMarkModecommand.Headers.RequestId, new SetBlackMarkModeCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
