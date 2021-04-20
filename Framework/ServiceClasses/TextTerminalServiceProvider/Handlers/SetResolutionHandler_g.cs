/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetResolutionHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(SetResolutionCommand))]
    public partial class SetResolutionHandler : ICommandHandler
    {
        public SetResolutionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetResolutionHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetResolutionHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetResolutionHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SetResolutionCommand setResolutionCmd = command as SetResolutionCommand;
            setResolutionCmd.IsNotNull($"Invalid parameter in the SetResolution Handle method. {nameof(setResolutionCmd)}");

            await HandleSetResolution(Connection, setResolutionCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SetResolutionCommand setResolutioncommand = command as SetResolutionCommand;

            SetResolutionCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetResolutionCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetResolutionCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetResolutionCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SetResolutionCompletion response = new SetResolutionCompletion(setResolutioncommand.Headers.RequestId, new SetResolutionCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
