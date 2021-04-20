/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryMediaHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetQueryMediaCommand))]
    public partial class GetQueryMediaHandler : ICommandHandler
    {
        public GetQueryMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryMediaHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryMediaHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryMediaHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetQueryMediaCommand getQueryMediaCmd = command as GetQueryMediaCommand;
            getQueryMediaCmd.IsNotNull($"Invalid parameter in the GetQueryMedia Handle method. {nameof(getQueryMediaCmd)}");
            
            IGetQueryMediaEvents events = new GetQueryMediaEvents(Connection, getQueryMediaCmd.Headers.RequestId);

            var result = await HandleGetQueryMedia(events, getQueryMediaCmd, cancel);
            await Connection.SendMessageAsync(new GetQueryMediaCompletion(getQueryMediaCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetQueryMediaCommand getQueryMediacommand = command as GetQueryMediaCommand;

            GetQueryMediaCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryMediaCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetQueryMediaCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetQueryMediaCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetQueryMediaCompletion response = new GetQueryMediaCompletion(getQueryMediacommand.Headers.RequestId, new GetQueryMediaCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
