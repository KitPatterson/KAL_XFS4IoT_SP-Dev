/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ReadHandler_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:05
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
            Provider = Dispatcher.IsA<TextTerminalServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ReadHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ReadHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var readCmd = command.IsA<ReadCommand>($"Invalid parameter in the Read Handle method. {nameof(ReadCommand)}");
            
            IReadEvents events = new ReadEvents(Connection, readCmd.Headers.RequestId);

            var result = await HandleRead(events, readCmd, cancel);
            await Connection.SendMessageAsync(new ReadCompletion(readCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var readcommand = command.IsA<ReadCommand>();

            ReadCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ReadCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ReadCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ReadCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ReadCompletion(readcommand.Headers.RequestId, new ReadCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ITextTerminalDevice Device { get; }
        private TextTerminalServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
