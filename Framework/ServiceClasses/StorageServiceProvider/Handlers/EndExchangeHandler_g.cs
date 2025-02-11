/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * EndExchangeHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Storage, typeof(EndExchangeCommand))]
    public partial class EndExchangeHandler : ICommandHandler
    {
        public EndExchangeHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EndExchangeHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EndExchangeHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IStorageDevice>();

            Storage = Provider.IsA<IStorageService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EndExchangeHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(EndExchangeHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var endExchangeCmd = command.IsA<EndExchangeCommand>($"Invalid parameter in the EndExchange Handle method. {nameof(EndExchangeCommand)}");
            endExchangeCmd.Header.RequestId.HasValue.IsTrue();

            IEndExchangeEvents events = new EndExchangeEvents(Connection, endExchangeCmd.Header.RequestId.Value);

            var result = await HandleEndExchange(events, endExchangeCmd, cancel);
            await Connection.SendMessageAsync(new EndExchangeCompletion(endExchangeCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var endExchangecommand = command.IsA<EndExchangeCommand>();
            endExchangecommand.Header.RequestId.HasValue.IsTrue();

            EndExchangeCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => EndExchangeCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => EndExchangeCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => EndExchangeCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new EndExchangeCompletion(endExchangecommand.Header.RequestId.Value, new EndExchangeCompletion.PayloadData(errorCode, commandException.Message));

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
