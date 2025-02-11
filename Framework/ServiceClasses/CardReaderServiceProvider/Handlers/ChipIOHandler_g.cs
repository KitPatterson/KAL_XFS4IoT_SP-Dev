/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipIOHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CardReader
{
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(ChipIOCommand))]
    public partial class ChipIOHandler : ICommandHandler
    {
        public ChipIOHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ChipIOHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ChipIOHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICardReaderDevice>();

            CardReader = Provider.IsA<ICardReaderService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ChipIOHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(ChipIOHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var chipIOCmd = command.IsA<ChipIOCommand>($"Invalid parameter in the ChipIO Handle method. {nameof(ChipIOCommand)}");
            chipIOCmd.Header.RequestId.HasValue.IsTrue();

            IChipIOEvents events = new ChipIOEvents(Connection, chipIOCmd.Header.RequestId.Value);

            var result = await HandleChipIO(events, chipIOCmd, cancel);
            await Connection.SendMessageAsync(new ChipIOCompletion(chipIOCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var chipIOcommand = command.IsA<ChipIOCommand>();
            chipIOcommand.Header.RequestId.HasValue.IsTrue();

            ChipIOCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ChipIOCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => ChipIOCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => ChipIOCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => ChipIOCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => ChipIOCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => ChipIOCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => ChipIOCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => ChipIOCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => ChipIOCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => ChipIOCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => ChipIOCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => ChipIOCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => ChipIOCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => ChipIOCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => ChipIOCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ChipIOCompletion(chipIOcommand.Header.RequestId.Value, new ChipIOCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICardReaderDevice Device { get => Provider.Device.IsA<ICardReaderDevice>(); }
        private IServiceProvider Provider { get; }
        private ICardReaderService CardReader { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
