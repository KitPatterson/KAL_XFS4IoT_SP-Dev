/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * GetItemInfoHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.CashManagement.Commands;
using XFS4IoT.CashManagement.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CashManagement
{
    [CommandHandler(XFSConstants.ServiceClass.CashManagement, typeof(GetItemInfoCommand))]
    public partial class GetItemInfoHandler : ICommandHandler
    {
        public GetItemInfoHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetItemInfoHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetItemInfoHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICashManagementDevice>();

            CashManagement = Provider.IsA<ICashManagementService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetItemInfoHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(GetItemInfoHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var getItemInfoCmd = command.IsA<GetItemInfoCommand>($"Invalid parameter in the GetItemInfo Handle method. {nameof(GetItemInfoCommand)}");
            getItemInfoCmd.Header.RequestId.HasValue.IsTrue();

            IGetItemInfoEvents events = new GetItemInfoEvents(Connection, getItemInfoCmd.Header.RequestId.Value);

            var result = await HandleGetItemInfo(events, getItemInfoCmd, cancel);
            await Connection.SendMessageAsync(new GetItemInfoCompletion(getItemInfoCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var getItemInfocommand = command.IsA<GetItemInfoCommand>();
            getItemInfocommand.Header.RequestId.HasValue.IsTrue();

            GetItemInfoCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => GetItemInfoCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetItemInfoCompletion(getItemInfocommand.Header.RequestId.Value, new GetItemInfoCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICashManagementDevice Device { get => Provider.Device.IsA<ICashManagementDevice>(); }
        private IServiceProvider Provider { get; }
        private ICashManagementService CashManagement { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
