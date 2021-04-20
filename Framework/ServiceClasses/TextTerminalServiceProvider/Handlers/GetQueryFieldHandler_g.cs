/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetQueryFieldHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(GetQueryFieldCommand))]
    public partial class GetQueryFieldHandler : ICommandHandler
    {
        public GetQueryFieldHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFieldHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetQueryFieldCommand getQueryFieldCmd = command as GetQueryFieldCommand;
            getQueryFieldCmd.IsNotNull($"Invalid parameter in the GetQueryField Handle method. {nameof(getQueryFieldCmd)}");
            
            IGetQueryFieldEvents events = new GetQueryFieldEvents(Connection, getQueryFieldCmd.Headers.RequestId);

            var result = await HandleGetQueryField(events, getQueryFieldCmd, cancel);
            await Connection.SendMessageAsync(new GetQueryFieldCompletion(getQueryFieldCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetQueryFieldCommand getQueryFieldcommand = command as GetQueryFieldCommand;

            GetQueryFieldCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetQueryFieldCompletion response = new GetQueryFieldCompletion(getQueryFieldcommand.Headers.RequestId, new GetQueryFieldCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
