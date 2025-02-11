/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GetKeyDetailHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.KeyManagement
{
    [CommandHandler(XFSConstants.ServiceClass.KeyManagement, typeof(GetKeyDetailCommand))]
    public partial class GetKeyDetailHandler : ICommandHandler
    {
        public GetKeyDetailHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetKeyDetailHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetKeyDetailHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyManagementDevice>();

            KeyManagement = Provider.IsA<IKeyManagementService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetKeyDetailHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetKeyDetailHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getKeyDetailCmd = command.IsA<GetKeyDetailCommand>($"Invalid parameter in the GetKeyDetail Handle method. {nameof(GetKeyDetailCommand)}");
            getKeyDetailCmd.Header.RequestId.HasValue.IsTrue();

            IGetKeyDetailEvents events = new GetKeyDetailEvents(Connection, getKeyDetailCmd.Header.RequestId.Value);

            var result = await HandleGetKeyDetail(events, getKeyDetailCmd, cancel);
            await Connection.SendMessageAsync(new GetKeyDetailCompletion(getKeyDetailCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getKeyDetailcommand = command.IsA<GetKeyDetailCommand>();
            getKeyDetailcommand.Header.RequestId.HasValue.IsTrue();

            GetKeyDetailCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetKeyDetailCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetKeyDetailCompletion(getKeyDetailcommand.Header.RequestId.Value, new GetKeyDetailCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private IKeyManagementDevice Device { get => Provider.Device.IsA<IKeyManagementDevice>(); }
        private IServiceProvider Provider { get; }
        private IKeyManagementService KeyManagement { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
