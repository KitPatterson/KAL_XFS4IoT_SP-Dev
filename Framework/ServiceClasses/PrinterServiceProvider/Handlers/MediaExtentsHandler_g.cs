/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaExtentsHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Printer
{
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(MediaExtentsCommand))]
    public partial class MediaExtentsHandler : ICommandHandler
    {
        public MediaExtentsHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IPrinterDevice>();

            Printer = Provider.IsA<IPrinterService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(MediaExtentsHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(MediaExtentsHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var mediaExtentsCmd = command.IsA<MediaExtentsCommand>($"Invalid parameter in the MediaExtents Handle method. {nameof(MediaExtentsCommand)}");
            mediaExtentsCmd.Header.RequestId.HasValue.IsTrue();

            IMediaExtentsEvents events = new MediaExtentsEvents(Connection, mediaExtentsCmd.Header.RequestId.Value);

            var result = await HandleMediaExtents(events, mediaExtentsCmd, cancel);
            await Connection.SendMessageAsync(new MediaExtentsCompletion(mediaExtentsCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var mediaExtentscommand = command.IsA<MediaExtentsCommand>();
            mediaExtentscommand.Header.RequestId.HasValue.IsTrue();

            MediaExtentsCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new MediaExtentsCompletion(mediaExtentscommand.Header.RequestId.Value, new MediaExtentsCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private IPrinterDevice Device { get => Provider.Device.IsA<IPrinterDevice>(); }
        private IServiceProvider Provider { get; }
        private IPrinterService Printer { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
