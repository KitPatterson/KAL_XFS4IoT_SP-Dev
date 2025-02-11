/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ControlMediaHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(ControlMediaCommand))]
    public partial class ControlMediaHandler : ICommandHandler
    {
        public ControlMediaHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IPrinterDevice>();

            Printer = Provider.IsA<IPrinterService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ControlMediaHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(ControlMediaHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var controlMediaCmd = command.IsA<ControlMediaCommand>($"Invalid parameter in the ControlMedia Handle method. {nameof(ControlMediaCommand)}");
            controlMediaCmd.Header.RequestId.HasValue.IsTrue();

            IControlMediaEvents events = new ControlMediaEvents(Connection, controlMediaCmd.Header.RequestId.Value);

            var result = await HandleControlMedia(events, controlMediaCmd, cancel);
            await Connection.SendMessageAsync(new ControlMediaCompletion(controlMediaCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var controlMediacommand = command.IsA<ControlMediaCommand>();
            controlMediacommand.Header.RequestId.HasValue.IsTrue();

            ControlMediaCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => ControlMediaCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => ControlMediaCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => ControlMediaCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ControlMediaCompletion(controlMediacommand.Header.RequestId.Value, new ControlMediaCompletion.PayloadData(errorCode, commandException.Message));

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
