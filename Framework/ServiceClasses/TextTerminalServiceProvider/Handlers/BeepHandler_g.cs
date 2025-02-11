/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * BeepHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.TextTerminal.Commands;
using XFS4IoT.TextTerminal.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.TextTerminal
{
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(BeepCommand))]
    public partial class BeepHandler : ICommandHandler
    {
        public BeepHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(BeepHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(BeepHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ITextTerminalDevice>();

            TextTerminal = Provider.IsA<ITextTerminalService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(BeepHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(BeepHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var beepCmd = command.IsA<BeepCommand>($"Invalid parameter in the Beep Handle method. {nameof(BeepCommand)}");
            beepCmd.Header.RequestId.HasValue.IsTrue();

            IBeepEvents events = new BeepEvents(Connection, beepCmd.Header.RequestId.Value);

            var result = await HandleBeep(events, beepCmd, cancel);
            await Connection.SendMessageAsync(new BeepCompletion(beepCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var beepcommand = command.IsA<BeepCommand>();
            beepcommand.Header.RequestId.HasValue.IsTrue();

            BeepCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => BeepCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => BeepCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => BeepCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => BeepCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => BeepCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => BeepCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => BeepCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => BeepCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => BeepCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => BeepCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => BeepCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => BeepCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => BeepCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => BeepCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => BeepCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new BeepCompletion(beepcommand.Header.RequestId.Value, new BeepCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ITextTerminalDevice Device { get => Provider.Device.IsA<ITextTerminalDevice>(); }
        private IServiceProvider Provider { get; }
        private ITextTerminalService TextTerminal { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
