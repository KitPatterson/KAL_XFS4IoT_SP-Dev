/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GetCertificateHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.KeyManagement, typeof(GetCertificateCommand))]
    public partial class GetCertificateHandler : ICommandHandler
    {
        public GetCertificateHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetCertificateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetCertificateHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyManagementDevice>();

            KeyManagement = Provider.IsA<IKeyManagementService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetCertificateHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetCertificateHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getCertificateCmd = command.IsA<GetCertificateCommand>($"Invalid parameter in the GetCertificate Handle method. {nameof(GetCertificateCommand)}");
            getCertificateCmd.Header.RequestId.HasValue.IsTrue();

            IGetCertificateEvents events = new GetCertificateEvents(Connection, getCertificateCmd.Header.RequestId.Value);

            var result = await HandleGetCertificate(events, getCertificateCmd, cancel);
            await Connection.SendMessageAsync(new GetCertificateCompletion(getCertificateCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getCertificatecommand = command.IsA<GetCertificateCommand>();
            getCertificatecommand.Header.RequestId.HasValue.IsTrue();

            GetCertificateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetCertificateCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetCertificateCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetCertificateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetCertificateCompletion(getCertificatecommand.Header.RequestId.Value, new GetCertificateCompletion.PayloadData(errorCode, commandException.Message));

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
