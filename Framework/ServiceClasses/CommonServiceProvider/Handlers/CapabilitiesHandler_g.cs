/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CapabilitiesHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Common
{
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(CapabilitiesCommand))]
    public partial class CapabilitiesHandler : ICommandHandler
    {
        public CapabilitiesHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICommonDevice>();

            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(CapabilitiesHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(CapabilitiesHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var capabilitiesCmd = command.IsA<CapabilitiesCommand>($"Invalid parameter in the Capabilities Handle method. {nameof(CapabilitiesCommand)}");
            capabilitiesCmd.Header.RequestId.HasValue.IsTrue();

            ICapabilitiesEvents events = new CapabilitiesEvents(Connection, capabilitiesCmd.Header.RequestId.Value);

            var result = await HandleCapabilities(events, capabilitiesCmd, cancel);
            await Connection.SendMessageAsync(new CapabilitiesCompletion(capabilitiesCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var capabilitiescommand = command.IsA<CapabilitiesCommand>();
            capabilitiescommand.Header.RequestId.HasValue.IsTrue();

            CapabilitiesCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new CapabilitiesCompletion(capabilitiescommand.Header.RequestId.Value, new CapabilitiesCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICommonDevice Device { get => Provider.Device.IsA<ICommonDevice>(); }
        private IServiceProvider Provider { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
