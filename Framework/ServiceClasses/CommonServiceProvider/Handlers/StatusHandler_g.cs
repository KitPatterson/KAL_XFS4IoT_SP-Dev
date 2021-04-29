/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * StatusHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(StatusCommand))]
    public partial class StatusHandler : ICommandHandler
    {
        public StatusHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(StatusHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            StatusCommand statusCmd = command as StatusCommand;
            statusCmd.IsNotNull($"Invalid parameter in the Status Handle method. {nameof(statusCmd)}");
            
            IStatusEvents events = new StatusEvents(Connection, statusCmd.Headers.RequestId);

            var result = await HandleStatus(events, statusCmd, cancel);
            await Connection.SendMessageAsync(new StatusCompletion(statusCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            StatusCommand statuscommand = command as StatusCommand;

            StatusCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => StatusCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => StatusCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => StatusCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            StatusCompletion response = new StatusCompletion(statuscommand.Headers.RequestId, new StatusCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}