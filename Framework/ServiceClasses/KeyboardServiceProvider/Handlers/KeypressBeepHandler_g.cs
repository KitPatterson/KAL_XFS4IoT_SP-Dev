/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Keyboard interface.
 * KeypressBeepHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Keyboard.Commands;
using XFS4IoT.Keyboard.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Keyboard
{
    [CommandHandler(XFSConstants.ServiceClass.Keyboard, typeof(KeypressBeepCommand))]
    public partial class KeypressBeepHandler : ICommandHandler
    {
        public KeypressBeepHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(KeypressBeepHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(KeypressBeepHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyboardDevice>();

            Keyboard = Provider.IsA<IKeyboardService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(KeypressBeepHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(KeypressBeepHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var keypressBeepCmd = command.IsA<KeypressBeepCommand>($"Invalid parameter in the KeypressBeep Handle method. {nameof(KeypressBeepCommand)}");
            keypressBeepCmd.Header.RequestId.HasValue.IsTrue();

            IKeypressBeepEvents events = new KeypressBeepEvents(Connection, keypressBeepCmd.Header.RequestId.Value);

            var result = await HandleKeypressBeep(events, keypressBeepCmd, cancel);
            await Connection.SendMessageAsync(new KeypressBeepCompletion(keypressBeepCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var keypressBeepcommand = command.IsA<KeypressBeepCommand>();
            keypressBeepcommand.Header.RequestId.HasValue.IsTrue();

            KeypressBeepCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => KeypressBeepCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new KeypressBeepCompletion(keypressBeepcommand.Header.RequestId.Value, new KeypressBeepCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private IKeyboardDevice Device { get => Provider.Device.IsA<IKeyboardDevice>(); }
        private IServiceProvider Provider { get; }
        private IKeyboardService Keyboard { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
