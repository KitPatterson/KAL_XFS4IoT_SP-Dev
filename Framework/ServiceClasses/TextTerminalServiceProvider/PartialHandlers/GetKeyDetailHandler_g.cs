/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetKeyDetailHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(GetKeyDetailCommand))]
    public partial class GetKeyDetailHandler : ICommandHandler
    {
        public GetKeyDetailHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetKeyDetailHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetKeyDetailHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetKeyDetailHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetKeyDetailCommand getKeyDetailCmd = command as GetKeyDetailCommand;
            getKeyDetailCmd.IsNotNull($"Invalid parameter in the GetKeyDetail Handle method. {nameof(getKeyDetailCmd)}");

            await HandleGetKeyDetail(Connection, getKeyDetailCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetKeyDetailCommand getKeyDetailcommand = command as GetKeyDetailCommand;

            GetKeyDetailCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetKeyDetailCompletion response = new GetKeyDetailCompletion(getKeyDetailcommand.Headers.RequestId, new GetKeyDetailCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
