/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT PinPad interface.
 * GetQueryPCIPTSDeviceIdHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.PinPad.Commands;
using XFS4IoT.PinPad.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.PinPad
{
    [CommandHandler(XFSConstants.ServiceClass.PinPad, typeof(GetQueryPCIPTSDeviceIdCommand))]
    public partial class GetQueryPCIPTSDeviceIdHandler : ICommandHandler
    {
        public GetQueryPCIPTSDeviceIdHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryPCIPTSDeviceIdHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetQueryPCIPTSDeviceIdHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IPinPadDevice>();

            PinPad = Provider.IsA<IPinPadService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryPCIPTSDeviceIdHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetQueryPCIPTSDeviceIdHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getQueryPCIPTSDeviceIdCmd = command.IsA<GetQueryPCIPTSDeviceIdCommand>($"Invalid parameter in the GetQueryPCIPTSDeviceId Handle method. {nameof(GetQueryPCIPTSDeviceIdCommand)}");
            getQueryPCIPTSDeviceIdCmd.Header.RequestId.HasValue.IsTrue();

            IGetQueryPCIPTSDeviceIdEvents events = new GetQueryPCIPTSDeviceIdEvents(Connection, getQueryPCIPTSDeviceIdCmd.Header.RequestId.Value);

            var result = await HandleGetQueryPCIPTSDeviceId(events, getQueryPCIPTSDeviceIdCmd, cancel);
            await Connection.SendMessageAsync(new GetQueryPCIPTSDeviceIdCompletion(getQueryPCIPTSDeviceIdCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getQueryPCIPTSDeviceIdcommand = command.IsA<GetQueryPCIPTSDeviceIdCommand>();
            getQueryPCIPTSDeviceIdcommand.Header.RequestId.HasValue.IsTrue();

            GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetQueryPCIPTSDeviceIdCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetQueryPCIPTSDeviceIdCompletion(getQueryPCIPTSDeviceIdcommand.Header.RequestId.Value, new GetQueryPCIPTSDeviceIdCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private IPinPadDevice Device { get => Provider.Device.IsA<IPinPadDevice>(); }
        private IServiceProvider Provider { get; }
        private IPinPadService PinPad { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
