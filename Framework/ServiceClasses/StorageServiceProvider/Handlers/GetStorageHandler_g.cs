/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * GetStorageHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Storage, typeof(GetStorageCommand))]
    public partial class GetStorageHandler : ICommandHandler
    {
        public GetStorageHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetStorageHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetStorageHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IStorageDevice>();

            Storage = Provider.IsA<IStorageService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetStorageHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetStorageHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getStorageCmd = command.IsA<GetStorageCommand>($"Invalid parameter in the GetStorage Handle method. {nameof(GetStorageCommand)}");
            getStorageCmd.Header.RequestId.HasValue.IsTrue();

            IGetStorageEvents events = new GetStorageEvents(Connection, getStorageCmd.Header.RequestId.Value);

            var result = await HandleGetStorage(events, getStorageCmd, cancel);
            await Connection.SendMessageAsync(new GetStorageCompletion(getStorageCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getStoragecommand = command.IsA<GetStorageCommand>();
            getStoragecommand.Header.RequestId.HasValue.IsTrue();

            GetStorageCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetStorageCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetStorageCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetStorageCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetStorageCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetStorageCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetStorageCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetStorageCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetStorageCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetStorageCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetStorageCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetStorageCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetStorageCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetStorageCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetStorageCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetStorageCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetStorageCompletion(getStoragecommand.Header.RequestId.Value, new GetStorageCompletion.PayloadData(errorCode, commandException.Message));

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
