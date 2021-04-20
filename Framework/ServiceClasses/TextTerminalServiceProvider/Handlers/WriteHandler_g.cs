/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * WriteHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(WriteCommand))]
    public partial class WriteHandler : ICommandHandler
    {
        public WriteHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(WriteHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(WriteHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            WriteCommand writeCmd = command as WriteCommand;
            writeCmd.IsNotNull($"Invalid parameter in the Write Handle method. {nameof(writeCmd)}");
            
            IWriteEvents events = new WriteEvents(Connection, writeCmd.Headers.RequestId);

            var result = await HandleWrite(events, writeCmd, cancel);
            await Connection.SendMessageAsync(new WriteCompletion(writeCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            WriteCommand writecommand = command as WriteCommand;

            WriteCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => WriteCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => WriteCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => WriteCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            WriteCompletion response = new WriteCompletion(writecommand.Headers.RequestId, new WriteCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
