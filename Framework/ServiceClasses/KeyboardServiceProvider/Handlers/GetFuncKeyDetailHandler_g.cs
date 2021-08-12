/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Keyboard interface.
 * GetFuncKeyDetailHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Keyboard.Commands;
using XFS4IoT.Keyboard.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Keyboard
{
    [CommandHandler(XFSConstants.ServiceClass.Keyboard, typeof(GetFuncKeyDetailCommand))]
    public partial class GetFuncKeyDetailHandler : ICommandHandler
    {
        public GetFuncKeyDetailHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetFuncKeyDetailHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetFuncKeyDetailHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyboardDevice>();

            Keyboard = Provider.IsA<IKeyboardServiceClass>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetFuncKeyDetailHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var getFuncKeyDetailCmd = command.IsA<GetFuncKeyDetailCommand>($"Invalid parameter in the GetFuncKeyDetail Handle method. {nameof(GetFuncKeyDetailCommand)}");
            getFuncKeyDetailCmd.Header.RequestId.HasValue.IsTrue();

            IGetFuncKeyDetailEvents events = new GetFuncKeyDetailEvents(Connection, getFuncKeyDetailCmd.Header.RequestId.Value);

            var result = await HandleGetFuncKeyDetail(events, getFuncKeyDetailCmd, cancel);
            await Connection.SendMessageAsync(new GetFuncKeyDetailCompletion(getFuncKeyDetailCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var getFuncKeyDetailcommand = command.IsA<GetFuncKeyDetailCommand>();
            getFuncKeyDetailcommand.Header.RequestId.HasValue.IsTrue();

            GetFuncKeyDetailCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetFuncKeyDetailCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetFuncKeyDetailCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetFuncKeyDetailCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetFuncKeyDetailCompletion(getFuncKeyDetailcommand.Header.RequestId.Value, new GetFuncKeyDetailCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private IKeyboardDevice Device { get => Provider.Device.IsA<IKeyboardDevice>(); }
        private IServiceProvider Provider { get; }
        private IKeyboardServiceClass Keyboard { get; }
        private ILogger Logger { get; }
    }

}
