/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetQueryFormHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetQueryFormCommand))]
    public partial class GetQueryFormHandler : ICommandHandler
    {
        public GetQueryFormHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IPrinterDevice>();

            Printer = Provider.IsA<IPrinterService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFormHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetQueryFormHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getQueryFormCmd = command.IsA<GetQueryFormCommand>($"Invalid parameter in the GetQueryForm Handle method. {nameof(GetQueryFormCommand)}");
            getQueryFormCmd.Header.RequestId.HasValue.IsTrue();

            IGetQueryFormEvents events = new GetQueryFormEvents(Connection, getQueryFormCmd.Header.RequestId.Value);

            var result = await HandleGetQueryForm(events, getQueryFormCmd, cancel);
            await Connection.SendMessageAsync(new GetQueryFormCompletion(getQueryFormCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getQueryFormcommand = command.IsA<GetQueryFormCommand>();
            getQueryFormcommand.Header.RequestId.HasValue.IsTrue();

            GetQueryFormCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetQueryFormCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetQueryFormCompletion(getQueryFormcommand.Header.RequestId.Value, new GetQueryFormCompletion.PayloadData(errorCode, commandException.Message));

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
