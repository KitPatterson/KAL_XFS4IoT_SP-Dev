/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetFormListHandler_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:07
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetFormListCommand))]
    public partial class GetFormListHandler : ICommandHandler
    {
        public GetFormListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<PrinterServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetFormListHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var getFormListCmd = command.IsA<GetFormListCommand>($"Invalid parameter in the GetFormList Handle method. {nameof(GetFormListCommand)}");
            
            IGetFormListEvents events = new GetFormListEvents(Connection, getFormListCmd.Headers.RequestId);

            var result = await HandleGetFormList(events, getFormListCmd, cancel);
            await Connection.SendMessageAsync(new GetFormListCompletion(getFormListCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var getFormListcommand = command.IsA<GetFormListCommand>();

            GetFormListCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetFormListCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetFormListCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetFormListCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetFormListCompletion(getFormListcommand.Headers.RequestId, new GetFormListCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private IPrinterDevice Device { get; }
        private PrinterServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
