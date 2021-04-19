/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetQueryFormHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(GetQueryFormCommand))]
    public partial class GetQueryFormHandler : ICommandHandler
    {
        public GetQueryFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFormHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetQueryFormCommand getQueryFormCmd = command as GetQueryFormCommand;
            getQueryFormCmd.IsNotNull($"Invalid parameter in the GetQueryForm Handle method. {nameof(getQueryFormCmd)}");

            await HandleGetQueryForm(Connection, getQueryFormCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetQueryFormCommand getQueryFormcommand = command as GetQueryFormCommand;

            GetQueryFormCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetQueryFormCompletion response = new GetQueryFormCompletion(getQueryFormcommand.Headers.RequestId, new GetQueryFormCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
