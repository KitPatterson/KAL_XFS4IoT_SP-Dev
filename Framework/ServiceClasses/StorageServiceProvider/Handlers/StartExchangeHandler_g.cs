/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * StartExchangeHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Storage.Commands;
using XFS4IoT.Storage.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Storage
{
    [CommandHandler(XFSConstants.ServiceClass.Storage, typeof(StartExchangeCommand))]
    public partial class StartExchangeHandler : ICommandHandler
    {
        public StartExchangeHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(StartExchangeHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(StartExchangeHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IStorageDevice>();

            Storage = Provider.IsA<IStorageService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(StartExchangeHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(StartExchangeHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var startExchangeCmd = command.IsA<StartExchangeCommand>($"Invalid parameter in the StartExchange Handle method. {nameof(StartExchangeCommand)}");
            startExchangeCmd.Header.RequestId.HasValue.IsTrue();

            IStartExchangeEvents events = new StartExchangeEvents(Connection, startExchangeCmd.Header.RequestId.Value);

            var result = await HandleStartExchange(events, startExchangeCmd, cancel);
            await Connection.SendMessageAsync(new StartExchangeCompletion(startExchangeCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var startExchangecommand = command.IsA<StartExchangeCommand>();
            startExchangecommand.Header.RequestId.HasValue.IsTrue();

            StartExchangeCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => StartExchangeCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => StartExchangeCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => StartExchangeCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new StartExchangeCompletion(startExchangecommand.Header.RequestId.Value, new StartExchangeCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private IStorageDevice Device { get => Provider.Device.IsA<IStorageDevice>(); }
        private IServiceProvider Provider { get; }
        private IStorageService Storage { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
