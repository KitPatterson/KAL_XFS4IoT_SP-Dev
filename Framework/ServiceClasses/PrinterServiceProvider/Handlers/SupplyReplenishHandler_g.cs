/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * SupplyReplenishHandler_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:07
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(SupplyReplenishCommand))]
    public partial class SupplyReplenishHandler : ICommandHandler
    {
        public SupplyReplenishHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SupplyReplenishHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<PrinterServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SupplyReplenishHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SupplyReplenishHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var supplyReplenishCmd = command.IsA<SupplyReplenishCommand>($"Invalid parameter in the SupplyReplenish Handle method. {nameof(SupplyReplenishCommand)}");
            
            ISupplyReplenishEvents events = new SupplyReplenishEvents(Connection, supplyReplenishCmd.Headers.RequestId);

            var result = await HandleSupplyReplenish(events, supplyReplenishCmd, cancel);
            await Connection.SendMessageAsync(new SupplyReplenishCompletion(supplyReplenishCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var supplyReplenishcommand = command.IsA<SupplyReplenishCommand>();

            SupplyReplenishCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SupplyReplenishCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SupplyReplenishCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SupplyReplenishCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new SupplyReplenishCompletion(supplyReplenishcommand.Headers.RequestId, new SupplyReplenishCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private IPrinterDevice Device { get; }
        private PrinterServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
