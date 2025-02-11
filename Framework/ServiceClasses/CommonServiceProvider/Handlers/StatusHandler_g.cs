/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * StatusHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Common
{
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(StatusCommand))]
    public partial class StatusHandler : ICommandHandler
    {
        public StatusHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICommonDevice>();

            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(StatusHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(StatusHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var statusCmd = command.IsA<StatusCommand>($"Invalid parameter in the Status Handle method. {nameof(StatusCommand)}");
            statusCmd.Header.RequestId.HasValue.IsTrue();

            IStatusEvents events = new StatusEvents(Connection, statusCmd.Header.RequestId.Value);

            var result = await HandleStatus(events, statusCmd, cancel);
            await Connection.SendMessageAsync(new StatusCompletion(statusCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var statuscommand = command.IsA<StatusCommand>();
            statuscommand.Header.RequestId.HasValue.IsTrue();

            StatusCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => StatusCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => StatusCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => StatusCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => StatusCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => StatusCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => StatusCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => StatusCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => StatusCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => StatusCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => StatusCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => StatusCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => StatusCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => StatusCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => StatusCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => StatusCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new StatusCompletion(statuscommand.Header.RequestId.Value, new StatusCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICommonDevice Device { get => Provider.Device.IsA<ICommonDevice>(); }
        private IServiceProvider Provider { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
