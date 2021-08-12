/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CancelHandler_g.cs uses automatically generated parts.
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(CancelCommand))]
    public partial class CancelHandler : ICommandHandler
    {
        public CancelHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CancelHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(CancelHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICommonDevice>();

            Common = Provider.IsA<ICommonServiceClass>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(CancelHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var cancelCmd = command.IsA<CancelCommand>($"Invalid parameter in the Cancel Handle method. {nameof(CancelCommand)}");
            cancelCmd.Header.RequestId.HasValue.IsTrue();

            ICancelEvents events = new CancelEvents(Connection, cancelCmd.Header.RequestId.Value);

            var result = await HandleCancel(events, cancelCmd, cancel);
            await Connection.SendMessageAsync(new CancelCompletion(cancelCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var cancelcommand = command.IsA<CancelCommand>();
            cancelcommand.Header.RequestId.HasValue.IsTrue();

            CancelCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => CancelCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => CancelCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => CancelCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new CancelCompletion(cancelcommand.Header.RequestId.Value, new CancelCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICommonDevice Device { get => Provider.Device.IsA<ICommonDevice>(); }
        private IServiceProvider Provider { get; }
        private ICommonServiceClass Common { get; }
        private ILogger Logger { get; }
    }

}
