/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetTransactionStateHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(SetTransactionStateCommand))]
    public partial class SetTransactionStateHandler : ICommandHandler
    {
        public SetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<CommonServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetTransactionStateHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var setTransactionStateCmd = command.IsA<SetTransactionStateCommand>($"Invalid parameter in the SetTransactionState Handle method. {nameof(SetTransactionStateCommand)}");
            
            ISetTransactionStateEvents events = new SetTransactionStateEvents(Connection, setTransactionStateCmd.Headers.RequestId);

            var result = await HandleSetTransactionState(events, setTransactionStateCmd, cancel);
            await Connection.SendMessageAsync(new SetTransactionStateCompletion(setTransactionStateCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var setTransactionStatecommand = command.IsA<SetTransactionStateCommand>();

            SetTransactionStateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new SetTransactionStateCompletion(setTransactionStatecommand.Headers.RequestId, new SetTransactionStateCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICommonDevice Device { get; }
        private CommonServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
