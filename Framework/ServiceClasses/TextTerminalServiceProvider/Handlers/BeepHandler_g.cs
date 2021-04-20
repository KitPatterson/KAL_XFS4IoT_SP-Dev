/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * BeepHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(BeepCommand))]
    public partial class BeepHandler : ICommandHandler
    {
        public BeepHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(BeepHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(BeepHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(BeepHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            BeepCommand beepCmd = command as BeepCommand;
            beepCmd.IsNotNull($"Invalid parameter in the Beep Handle method. {nameof(beepCmd)}");
            
            IBeepEvents events = new BeepEvents(Connection, beepCmd.Headers.RequestId);

            var result = await HandleBeep(events, beepCmd, cancel);
            await Connection.SendMessageAsync(new BeepCompletion(beepCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            BeepCommand beepcommand = command as BeepCommand;

            BeepCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => BeepCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => BeepCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => BeepCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            BeepCompletion response = new BeepCompletion(beepcommand.Headers.RequestId, new BeepCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
