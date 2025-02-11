/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * DigestHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Crypto, typeof(DigestCommand))]
    public partial class DigestHandler : ICommandHandler
    {
        public DigestHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DigestHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(DigestHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICryptoDevice>();

            Crypto = Provider.IsA<ICryptoService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(DigestHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(DigestHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var digestCmd = command.IsA<DigestCommand>($"Invalid parameter in the Digest Handle method. {nameof(DigestCommand)}");
            digestCmd.Header.RequestId.HasValue.IsTrue();

            IDigestEvents events = new DigestEvents(Connection, digestCmd.Header.RequestId.Value);

            var result = await HandleDigest(events, digestCmd, cancel);
            await Connection.SendMessageAsync(new DigestCompletion(digestCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var digestcommand = command.IsA<DigestCommand>();
            digestcommand.Header.RequestId.HasValue.IsTrue();

            DigestCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => DigestCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => DigestCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => DigestCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => DigestCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => DigestCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => DigestCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => DigestCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => DigestCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => DigestCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => DigestCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => DigestCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => DigestCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => DigestCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => DigestCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => DigestCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new DigestCompletion(digestcommand.Header.RequestId.Value, new DigestCompletion.PayloadData(errorCode, commandException.Message));

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
