/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * SetStorageHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Storage, typeof(SetStorageCommand))]
    public partial class SetStorageHandler : ICommandHandler
    {
        public SetStorageHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetStorageHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetStorageHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IStorageDevice>();

            Storage = Provider.IsA<IStorageService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetStorageHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(SetStorageHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var setStorageCmd = command.IsA<SetStorageCommand>($"Invalid parameter in the SetStorage Handle method. {nameof(SetStorageCommand)}");
            setStorageCmd.Header.RequestId.HasValue.IsTrue();

            ISetStorageEvents events = new SetStorageEvents(Connection, setStorageCmd.Header.RequestId.Value);

            var result = await HandleSetStorage(events, setStorageCmd, cancel);
            await Connection.SendMessageAsync(new SetStorageCompletion(setStorageCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var setStoragecommand = command.IsA<SetStorageCommand>();
            setStoragecommand.Header.RequestId.HasValue.IsTrue();

            SetStorageCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetStorageCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => SetStorageCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => SetStorageCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => SetStorageCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => SetStorageCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => SetStorageCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => SetStorageCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => SetStorageCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => SetStorageCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => SetStorageCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => SetStorageCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => SetStorageCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => SetStorageCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => SetStorageCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => SetStorageCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new SetStorageCompletion(setStoragecommand.Header.RequestId.Value, new SetStorageCompletion.PayloadData(errorCode, commandException.Message));

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
