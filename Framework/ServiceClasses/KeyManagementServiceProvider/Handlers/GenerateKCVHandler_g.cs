/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * GenerateKCVHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.KeyManagement, typeof(GenerateKCVCommand))]
    public partial class GenerateKCVHandler : ICommandHandler
    {
        public GenerateKCVHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GenerateKCVHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GenerateKCVHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyManagementDevice>();

            KeyManagement = Provider.IsA<IKeyManagementService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GenerateKCVHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GenerateKCVHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var generateKCVCmd = command.IsA<GenerateKCVCommand>($"Invalid parameter in the GenerateKCV Handle method. {nameof(GenerateKCVCommand)}");
            generateKCVCmd.Header.RequestId.HasValue.IsTrue();

            IGenerateKCVEvents events = new GenerateKCVEvents(Connection, generateKCVCmd.Header.RequestId.Value);

            var result = await HandleGenerateKCV(events, generateKCVCmd, cancel);
            await Connection.SendMessageAsync(new GenerateKCVCompletion(generateKCVCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var generateKCVcommand = command.IsA<GenerateKCVCommand>();
            generateKCVcommand.Header.RequestId.HasValue.IsTrue();

            GenerateKCVCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GenerateKCVCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GenerateKCVCompletion(generateKCVcommand.Header.RequestId.Value, new GenerateKCVCompletion.PayloadData(errorCode, commandException.Message));

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
