/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ResetCountHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;

namespace XFS4IoTFramework.Printer
{
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(ResetCountCommand))]
    public partial class ResetCountHandler : ICommandHandler
    {
        public ResetCountHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetCountHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ResetCountHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ResetCountHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ResetCountCommand resetCountCmd = command as ResetCountCommand;
            resetCountCmd.IsNotNull($"Invalid parameter in the ResetCount Handle method. {nameof(resetCountCmd)}");
            
            IResetCountEvents events = new ResetCountEvents(Connection, resetCountCmd.Headers.RequestId);

            var result = await HandleResetCount(events, resetCountCmd, cancel);
            await Connection.SendMessageAsync(new ResetCountCompletion(resetCountCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ResetCountCommand resetCountcommand = command as ResetCountCommand;

            ResetCountCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ResetCountCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ResetCountCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ResetCountCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ResetCountCompletion response = new ResetCountCompletion(resetCountcommand.Headers.RequestId, new ResetCountCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
