/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SynchronizeCommandHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(SynchronizeCommandCommand))]
    public partial class SynchronizeCommandHandler : ICommandHandler
    {
        public SynchronizeCommandHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<CommonServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var synchronizeCommandCmd = command.IsA<SynchronizeCommandCommand>($"Invalid parameter in the SynchronizeCommand Handle method. {nameof(SynchronizeCommandCommand)}");
            
            ISynchronizeCommandEvents events = new SynchronizeCommandEvents(Connection, synchronizeCommandCmd.Headers.RequestId);

            var result = await HandleSynchronizeCommand(events, synchronizeCommandCmd, cancel);
            await Connection.SendMessageAsync(new SynchronizeCommandCompletion(synchronizeCommandCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var synchronizeCommandcommand = command.IsA<SynchronizeCommandCommand>();

            SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new SynchronizeCommandCompletion(synchronizeCommandcommand.Headers.RequestId, new SynchronizeCommandCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICommonDevice Device { get; }
        private CommonServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
