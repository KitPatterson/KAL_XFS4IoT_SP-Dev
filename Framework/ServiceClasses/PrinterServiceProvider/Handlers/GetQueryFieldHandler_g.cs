/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryFieldHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Printer
{
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetQueryFieldCommand))]
    public partial class GetQueryFieldHandler : ICommandHandler
    {
        public GetQueryFieldHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IPrinterDevice>();

            Printer = Provider.IsA<IPrinterService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFieldHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getQueryFieldCmd = command.IsA<GetQueryFieldCommand>($"Invalid parameter in the GetQueryField Handle method. {nameof(GetQueryFieldCommand)}");
            getQueryFieldCmd.Header.RequestId.HasValue.IsTrue();

            IGetQueryFieldEvents events = new GetQueryFieldEvents(Connection, getQueryFieldCmd.Header.RequestId.Value);

            var result = await HandleGetQueryField(events, getQueryFieldCmd, cancel);
            await Connection.SendMessageAsync(new GetQueryFieldCompletion(getQueryFieldCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getQueryFieldcommand = command.IsA<GetQueryFieldCommand>();
            getQueryFieldcommand.Header.RequestId.HasValue.IsTrue();

            GetQueryFieldCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetQueryFieldCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetQueryFieldCompletion(getQueryFieldcommand.Header.RequestId.Value, new GetQueryFieldCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private IPrinterDevice Device { get => Provider.Device.IsA<IPrinterDevice>(); }
        private IServiceProvider Provider { get; }
        private IPrinterService Printer { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
