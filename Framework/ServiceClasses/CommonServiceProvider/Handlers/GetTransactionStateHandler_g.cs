/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetTransactionStateHandler_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:04
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(GetTransactionStateCommand))]
    public partial class GetTransactionStateHandler : ICommandHandler
    {
        public GetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<CommonServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetTransactionStateHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var getTransactionStateCmd = command.IsA<GetTransactionStateCommand>($"Invalid parameter in the GetTransactionState Handle method. {nameof(GetTransactionStateCommand)}");
            
            IGetTransactionStateEvents events = new GetTransactionStateEvents(Connection, getTransactionStateCmd.Headers.RequestId);

            var result = await HandleGetTransactionState(events, getTransactionStateCmd, cancel);
            await Connection.SendMessageAsync(new GetTransactionStateCompletion(getTransactionStateCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var getTransactionStatecommand = command.IsA<GetTransactionStateCommand>();

            GetTransactionStateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new GetTransactionStateCompletion(getTransactionStatecommand.Headers.RequestId, new GetTransactionStateCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICommonDevice Device { get; }
        private CommonServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
