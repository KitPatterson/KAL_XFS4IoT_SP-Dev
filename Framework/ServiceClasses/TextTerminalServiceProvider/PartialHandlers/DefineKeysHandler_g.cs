/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DefineKeysHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(DefineKeysHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(DefineKeysHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ITextTerminalDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(DefineKeysHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(DefineKeysHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            DefineKeysCommand defineKeysCmd = command as DefineKeysCommand;
            defineKeysCmd.IsNotNull($"Invalid parameter in the DefineKeys Handle method. {nameof(defineKeysCmd)}");

            await HandleDefineKeys(Connection, defineKeysCmd, cancel);
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
