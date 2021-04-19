/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SynchronizeCommandHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICommonDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SynchronizeCommandCommand synchronizeCommandCmd = command as SynchronizeCommandCommand;
            synchronizeCommandCmd.IsNotNull($"Invalid parameter in the SynchronizeCommand Handle method. {nameof(synchronizeCommandCmd)}");

            await HandleSynchronizeCommand(Connection, synchronizeCommandCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SynchronizeCommandCommand synchronizeCommandcommand = command as SynchronizeCommandCommand;

            SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SynchronizeCommandCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SynchronizeCommandCompletion response = new SynchronizeCommandCompletion(synchronizeCommandcommand.Headers.RequestId, new SynchronizeCommandCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
