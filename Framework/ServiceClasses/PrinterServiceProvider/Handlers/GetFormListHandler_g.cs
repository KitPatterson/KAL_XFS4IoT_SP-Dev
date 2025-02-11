/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetFormListHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetFormListCommand))]
    public partial class GetFormListHandler : ICommandHandler
    {
        public GetFormListHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IPrinterDevice>();

            Printer = Provider.IsA<IPrinterService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetFormListHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetFormListHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getFormListCmd = command.IsA<GetFormListCommand>($"Invalid parameter in the GetFormList Handle method. {nameof(GetFormListCommand)}");
            getFormListCmd.Header.RequestId.HasValue.IsTrue();

            IGetFormListEvents events = new GetFormListEvents(Connection, getFormListCmd.Header.RequestId.Value);

            var result = await HandleGetFormList(events, getFormListCmd, cancel);
            await Connection.SendMessageAsync(new GetFormListCompletion(getFormListCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getFormListcommand = command.IsA<GetFormListCommand>();
            getFormListcommand.Header.RequestId.HasValue.IsTrue();

            GetFormListCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetFormListCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetFormListCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetFormListCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetFormListCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetFormListCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetFormListCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetFormListCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetFormListCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetFormListCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetFormListCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetFormListCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetFormListCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetFormListCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetFormListCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetFormListCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetFormListCompletion(getFormListcommand.Header.RequestId.Value, new GetFormListCompletion.PayloadData(errorCode, commandException.Message));

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
