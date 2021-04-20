/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RawDataHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(RawDataCommand))]
    public partial class RawDataHandler : ICommandHandler
    {
        public RawDataHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RawDataHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(RawDataHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(RawDataHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            RawDataCommand rawDataCmd = command as RawDataCommand;
            rawDataCmd.IsNotNull($"Invalid parameter in the RawData Handle method. {nameof(rawDataCmd)}");
            
            IRawDataEvents events = new RawDataEvents(Connection, rawDataCmd.Headers.RequestId);

            var result = await HandleRawData(events, rawDataCmd, cancel);
            await Connection.SendMessageAsync(new RawDataCompletion(rawDataCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            RawDataCommand rawDatacommand = command as RawDataCommand;

            RawDataCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => RawDataCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => RawDataCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => RawDataCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            RawDataCompletion response = new RawDataCompletion(rawDatacommand.Headers.RequestId, new RawDataCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
