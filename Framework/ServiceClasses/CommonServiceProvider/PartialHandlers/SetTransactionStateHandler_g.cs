/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetTransactionStateHandler_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
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
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICommonDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetTransactionStateHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SetTransactionStateCommand setTransactionStateCmd = command as SetTransactionStateCommand;
            setTransactionStateCmd.IsNotNull($"Invalid parameter in the SetTransactionState Handle method. {nameof(setTransactionStateCmd)}");

            await HandleSetTransactionState(Connection, setTransactionStateCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SetTransactionStateCommand setTransactionStatecommand = command as SetTransactionStateCommand;

            SetTransactionStateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetTransactionStateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SetTransactionStateCompletion response = new SetTransactionStateCompletion(setTransactionStatecommand.Headers.RequestId, new SetTransactionStateCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
