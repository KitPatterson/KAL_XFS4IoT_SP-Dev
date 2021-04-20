/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetLedHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.TextTerminal.Commands;
using XFS4IoT.TextTerminal.Completions;

namespace XFS4IoTFramework.TextTerminal
{
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(SetLedCommand))]
    public partial class SetLedHandler : ICommandHandler
    {
        public SetLedHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetLedHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetLedHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetLedHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SetLedCommand setLedCmd = command as SetLedCommand;
            setLedCmd.IsNotNull($"Invalid parameter in the SetLed Handle method. {nameof(setLedCmd)}");
            
            ISetLedEvents events = new SetLedEvents(Connection, setLedCmd.Headers.RequestId);

            var result = await HandleSetLed(events, setLedCmd, cancel);
            await Connection.SendMessageAsync(new SetLedCompletion(setLedCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SetLedCommand setLedcommand = command as SetLedCommand;

            SetLedCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetLedCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetLedCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetLedCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SetLedCompletion response = new SetLedCompletion(setLedcommand.Headers.RequestId, new SetLedCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
