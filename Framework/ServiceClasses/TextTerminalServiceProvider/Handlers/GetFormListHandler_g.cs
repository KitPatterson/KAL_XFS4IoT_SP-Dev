/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * GetFormListHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(GetFormListCommand))]
    public partial class GetFormListHandler : ICommandHandler
    {
        public GetFormListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ITextTerminalDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetFormListHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetFormListCommand getFormListCmd = command as GetFormListCommand;
            getFormListCmd.IsNotNull($"Invalid parameter in the GetFormList Handle method. {nameof(getFormListCmd)}");
            
            IGetFormListEvents events = new GetFormListEvents(Connection, getFormListCmd.Headers.RequestId);

            var result = await HandleGetFormList(events, getFormListCmd, cancel);
            await Connection.SendMessageAsync(new GetFormListCompletion(getFormListCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetFormListCommand getFormListcommand = command as GetFormListCommand;

            GetFormListCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetFormListCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetFormListCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetFormListCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetFormListCompletion response = new GetFormListCompletion(getFormListcommand.Headers.RequestId, new GetFormListCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
