/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetTransactionStateHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(GetTransactionStateCommand))]
    public partial class GetTransactionStateHandler : ICommandHandler
    {
        public GetTransactionStateHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICommonDevice>();

            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetTransactionStateHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getTransactionStateCmd = command.IsA<GetTransactionStateCommand>($"Invalid parameter in the GetTransactionState Handle method. {nameof(GetTransactionStateCommand)}");
            getTransactionStateCmd.Header.RequestId.HasValue.IsTrue();

            IGetTransactionStateEvents events = new GetTransactionStateEvents(Connection, getTransactionStateCmd.Header.RequestId.Value);

            var result = await HandleGetTransactionState(events, getTransactionStateCmd, cancel);
            await Connection.SendMessageAsync(new GetTransactionStateCompletion(getTransactionStateCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getTransactionStatecommand = command.IsA<GetTransactionStateCommand>();
            getTransactionStatecommand.Header.RequestId.HasValue.IsTrue();

            GetTransactionStateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetTransactionStateCompletion(getTransactionStatecommand.Header.RequestId.Value, new GetTransactionStateCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICommonDevice Device { get => Provider.Device.IsA<ICommonDevice>(); }
        private IServiceProvider Provider { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
