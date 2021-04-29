/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetQueryFormHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(GetQueryFormCommand))]
    public partial class GetQueryFormHandler : ICommandHandler
    {
        public GetQueryFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<TextTerminalServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFormHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var getQueryFormCmd = command.IsA<GetQueryFormCommand>($"Invalid parameter in the GetQueryForm Handle method. {nameof(GetQueryFormCommand)}");
            
            IGetQueryFormEvents events = new GetQueryFormEvents(Connection, getQueryFormCmd.Headers.RequestId);

            var result = await HandleGetQueryForm(events, getQueryFormCmd, cancel);
            await Connection.SendMessageAsync(new GetQueryFormCompletion(getQueryFormCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var getQueryFormcommand = command.IsA<GetQueryFormCommand>();

            GetQueryFormCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetQueryFormCompletion(getQueryFormcommand.Headers.RequestId, new GetQueryFormCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ITextTerminalDevice Device { get; }
        private TextTerminalServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
