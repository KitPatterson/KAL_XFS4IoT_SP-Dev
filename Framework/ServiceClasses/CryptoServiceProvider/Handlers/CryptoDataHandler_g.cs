/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * CryptoDataHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Crypto.Commands;
using XFS4IoT.Crypto.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Crypto
{
    [CommandHandler(XFSConstants.ServiceClass.Crypto, typeof(CryptoDataCommand))]
    public partial class CryptoDataHandler : ICommandHandler
    {
        public CryptoDataHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CryptoDataHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(CryptoDataHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICryptoDevice>();

            Crypto = Provider.IsA<ICryptoService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(CryptoDataHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(CryptoDataHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var cryptoDataCmd = command.IsA<CryptoDataCommand>($"Invalid parameter in the CryptoData Handle method. {nameof(CryptoDataCommand)}");
            cryptoDataCmd.Header.RequestId.HasValue.IsTrue();

            ICryptoDataEvents events = new CryptoDataEvents(Connection, cryptoDataCmd.Header.RequestId.Value);

            var result = await HandleCryptoData(events, cryptoDataCmd, cancel);
            await Connection.SendMessageAsync(new CryptoDataCompletion(cryptoDataCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var cryptoDatacommand = command.IsA<CryptoDataCommand>();
            cryptoDatacommand.Header.RequestId.HasValue.IsTrue();

            CryptoDataCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => CryptoDataCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => CryptoDataCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => CryptoDataCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new CryptoDataCompletion(cryptoDatacommand.Header.RequestId.Value, new CryptoDataCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICryptoDevice Device { get => Provider.Device.IsA<ICryptoDevice>(); }
        private IServiceProvider Provider { get; }
        private ICryptoService Crypto { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
