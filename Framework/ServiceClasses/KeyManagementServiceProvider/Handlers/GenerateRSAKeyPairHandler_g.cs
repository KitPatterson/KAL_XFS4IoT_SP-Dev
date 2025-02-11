/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GenerateRSAKeyPairHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.KeyManagement, typeof(GenerateRSAKeyPairCommand))]
    public partial class GenerateRSAKeyPairHandler : ICommandHandler
    {
        public GenerateRSAKeyPairHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GenerateRSAKeyPairHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GenerateRSAKeyPairHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyManagementDevice>();

            KeyManagement = Provider.IsA<IKeyManagementService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GenerateRSAKeyPairHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GenerateRSAKeyPairHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var generateRSAKeyPairCmd = command.IsA<GenerateRSAKeyPairCommand>($"Invalid parameter in the GenerateRSAKeyPair Handle method. {nameof(GenerateRSAKeyPairCommand)}");
            generateRSAKeyPairCmd.Header.RequestId.HasValue.IsTrue();

            IGenerateRSAKeyPairEvents events = new GenerateRSAKeyPairEvents(Connection, generateRSAKeyPairCmd.Header.RequestId.Value);

            var result = await HandleGenerateRSAKeyPair(events, generateRSAKeyPairCmd, cancel);
            await Connection.SendMessageAsync(new GenerateRSAKeyPairCompletion(generateRSAKeyPairCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var generateRSAKeyPaircommand = command.IsA<GenerateRSAKeyPairCommand>();
            generateRSAKeyPaircommand.Header.RequestId.HasValue.IsTrue();

            GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GenerateRSAKeyPairCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GenerateRSAKeyPairCompletion(generateRSAKeyPaircommand.Header.RequestId.Value, new GenerateRSAKeyPairCompletion.PayloadData(errorCode, commandException.Message));

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
