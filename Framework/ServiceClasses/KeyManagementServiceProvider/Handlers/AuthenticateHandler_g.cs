/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * AuthenticateHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.KeyManagement
{
    [CommandHandler(XFSConstants.ServiceClass.KeyManagement, typeof(AuthenticateCommand))]
    public partial class AuthenticateHandler : ICommandHandler
    {
        public AuthenticateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(AuthenticateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(AuthenticateHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<IKeyManagementDevice>();

            KeyManagement = Provider.IsA<IKeyManagementServiceClass>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(AuthenticateHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var authenticateCmd = command.IsA<AuthenticateCommand>($"Invalid parameter in the Authenticate Handle method. {nameof(AuthenticateCommand)}");
            authenticateCmd.Header.RequestId.HasValue.IsTrue();

            IAuthenticateEvents events = new AuthenticateEvents(Connection, authenticateCmd.Header.RequestId.Value);

            var result = await HandleAuthenticate(events, authenticateCmd, cancel);
            await Connection.SendMessageAsync(new AuthenticateCompletion(authenticateCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var authenticatecommand = command.IsA<AuthenticateCommand>();
            authenticatecommand.Header.RequestId.HasValue.IsTrue();

            AuthenticateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => AuthenticateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => AuthenticateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => AuthenticateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new AuthenticateCompletion(authenticatecommand.Header.RequestId.Value, new AuthenticateCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private IKeyManagementDevice Device { get => Provider.Device.IsA<IKeyManagementDevice>(); }
        private IServiceProvider Provider { get; }
        private IKeyManagementServiceClass KeyManagement { get; }
        private ILogger Logger { get; }
    }

}
