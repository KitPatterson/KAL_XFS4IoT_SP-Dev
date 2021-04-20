/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DefineKeysHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(DefineKeysCommand))]
    public partial class DefineKeysHandler : ICommandHandler
    {
        public DefineKeysHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DefineKeysHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(DefineKeysHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(DefineKeysHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            DefineKeysCommand defineKeysCmd = command as DefineKeysCommand;
            defineKeysCmd.IsNotNull($"Invalid parameter in the DefineKeys Handle method. {nameof(defineKeysCmd)}");
            
            IDefineKeysEvents events = new DefineKeysEvents(Connection, defineKeysCmd.Headers.RequestId);

            var result = await HandleDefineKeys(events, defineKeysCmd, cancel);
            await Connection.SendMessageAsync(new DefineKeysCompletion(defineKeysCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            DefineKeysCommand defineKeyscommand = command as DefineKeysCommand;

            DefineKeysCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => DefineKeysCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => DefineKeysCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => DefineKeysCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            DefineKeysCompletion response = new DefineKeysCompletion(defineKeyscommand.Headers.RequestId, new DefineKeysCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
