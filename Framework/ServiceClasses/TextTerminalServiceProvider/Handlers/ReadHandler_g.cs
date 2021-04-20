/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ReadHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(ReadCommand))]
    public partial class ReadHandler : ICommandHandler
    {
        public ReadHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ReadHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ReadHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ReadCommand readCmd = command as ReadCommand;
            readCmd.IsNotNull($"Invalid parameter in the Read Handle method. {nameof(readCmd)}");
            
            IReadEvents events = new ReadEvents(Connection, readCmd.Headers.RequestId);

            var result = await HandleRead(events, readCmd, cancel);
            await Connection.SendMessageAsync(new ReadCompletion(readCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ReadCommand readcommand = command as ReadCommand;

            ReadCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ReadCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ReadCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ReadCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ReadCompletion response = new ReadCompletion(readcommand.Headers.RequestId, new ReadCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
