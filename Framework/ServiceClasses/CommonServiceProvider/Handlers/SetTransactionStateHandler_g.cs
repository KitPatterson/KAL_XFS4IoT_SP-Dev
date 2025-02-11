/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetTransactionStateHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(SetTransactionStateCommand))]
    public partial class SetTransactionStateHandler : ICommandHandler
    {
        public SetTransactionStateHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICommonDevice>();

            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetTransactionStateHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var setTransactionStateCmd = command.IsA<SetTransactionStateCommand>($"Invalid parameter in the SetTransactionState Handle method. {nameof(SetTransactionStateCommand)}");
            setTransactionStateCmd.Header.RequestId.HasValue.IsTrue();

            ISetTransactionStateEvents events = new SetTransactionStateEvents(Connection, setTransactionStateCmd.Header.RequestId.Value);

            var result = await HandleSetTransactionState(events, setTransactionStateCmd, cancel);
            await Connection.SendMessageAsync(new SetTransactionStateCompletion(setTransactionStateCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var setTransactionStatecommand = command.IsA<SetTransactionStateCommand>();
            setTransactionStatecommand.Header.RequestId.HasValue.IsTrue();

            SetTransactionStateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new SetTransactionStateCompletion(setTransactionStatecommand.Header.RequestId.Value, new SetTransactionStateCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICommonDevice Device { get => Provider.Device.IsA<ICommonDevice>(); }
        private IServiceProvider Provider { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
