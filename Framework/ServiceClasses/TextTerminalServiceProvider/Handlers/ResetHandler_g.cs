/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * ResetHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(ResetCommand))]
    public partial class ResetHandler : ICommandHandler
    {
        public ResetHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ResetCommand resetCmd = command as ResetCommand;
            resetCmd.IsNotNull($"Invalid parameter in the Reset Handle method. {nameof(resetCmd)}");

            await HandleReset(Connection, resetCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ResetCommand resetcommand = command as ResetCommand;

            ResetCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ResetCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ResetCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ResetCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ResetCompletion response = new ResetCompletion(resetcommand.Headers.RequestId, new ResetCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
