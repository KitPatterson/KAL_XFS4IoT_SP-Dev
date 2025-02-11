/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Lights interface.
 * SetLightHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Lights.Commands;
using XFS4IoT.Lights.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Lights
{
    [CommandHandler(XFSConstants.ServiceClass.Lights, typeof(SetLightCommand))]
    public partial class SetLightHandler : ICommandHandler
    {
        public SetLightHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetLightHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetLightHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ILightsDevice>();

            Lights = Provider.IsA<ILightsService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetLightHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(SetLightHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var setLightCmd = command.IsA<SetLightCommand>($"Invalid parameter in the SetLight Handle method. {nameof(SetLightCommand)}");
            setLightCmd.Header.RequestId.HasValue.IsTrue();

            ISetLightEvents events = new SetLightEvents(Connection, setLightCmd.Header.RequestId.Value);

            var result = await HandleSetLight(events, setLightCmd, cancel);
            await Connection.SendMessageAsync(new SetLightCompletion(setLightCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var setLightcommand = command.IsA<SetLightCommand>();
            setLightcommand.Header.RequestId.HasValue.IsTrue();

            SetLightCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetLightCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => SetLightCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => SetLightCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => SetLightCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => SetLightCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => SetLightCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => SetLightCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => SetLightCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => SetLightCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => SetLightCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => SetLightCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => SetLightCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => SetLightCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => SetLightCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => SetLightCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new SetLightCompletion(setLightcommand.Header.RequestId.Value, new SetLightCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ILightsDevice Device { get => Provider.Device.IsA<ILightsDevice>(); }
        private IServiceProvider Provider { get; }
        private ILightsService Lights { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
