/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ClearScreenHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(ClearScreenCommand))]
    public partial class ClearScreenHandler : ICommandHandler
    {
        public ClearScreenHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ClearScreenHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ClearScreenHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ClearScreenHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ClearScreenCommand clearScreenCmd = command as ClearScreenCommand;
            clearScreenCmd.IsNotNull($"Invalid parameter in the ClearScreen Handle method. {nameof(clearScreenCmd)}");
            
            IClearScreenEvents events = new ClearScreenEvents(Connection, clearScreenCmd.Headers.RequestId);

            var result = await HandleClearScreen(events, clearScreenCmd, cancel);
            await Connection.SendMessageAsync(new ClearScreenCompletion(clearScreenCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ClearScreenCommand clearScreencommand = command as ClearScreenCommand;

            ClearScreenCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ClearScreenCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ClearScreenCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ClearScreenCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ClearScreenCompletion response = new ClearScreenCompletion(clearScreencommand.Headers.RequestId, new ClearScreenCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}