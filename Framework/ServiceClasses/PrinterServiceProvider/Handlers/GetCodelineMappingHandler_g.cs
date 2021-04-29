/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetCodelineMappingHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;

namespace XFS4IoTFramework.Printer
{
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetCodelineMappingCommand))]
    public partial class GetCodelineMappingHandler : ICommandHandler
    {
        public GetCodelineMappingHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetCodelineMappingHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetCodelineMappingHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetCodelineMappingHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetCodelineMappingCommand getCodelineMappingCmd = command as GetCodelineMappingCommand;
            getCodelineMappingCmd.IsNotNull($"Invalid parameter in the GetCodelineMapping Handle method. {nameof(getCodelineMappingCmd)}");
            
            IGetCodelineMappingEvents events = new GetCodelineMappingEvents(Connection, getCodelineMappingCmd.Headers.RequestId);

            var result = await HandleGetCodelineMapping(events, getCodelineMappingCmd, cancel);
            await Connection.SendMessageAsync(new GetCodelineMappingCompletion(getCodelineMappingCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetCodelineMappingCommand getCodelineMappingcommand = command as GetCodelineMappingCommand;

            GetCodelineMappingCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetCodelineMappingCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetCodelineMappingCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetCodelineMappingCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetCodelineMappingCompletion response = new GetCodelineMappingCompletion(getCodelineMappingcommand.Headers.RequestId, new GetCodelineMappingCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}