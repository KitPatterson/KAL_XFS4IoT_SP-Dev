/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RetractMediaHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(RetractMediaCommand))]
    public partial class RetractMediaHandler : ICommandHandler
    {
        public RetractMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RetractMediaHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(RetractMediaHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(RetractMediaHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            RetractMediaCommand retractMediaCmd = command as RetractMediaCommand;
            retractMediaCmd.IsNotNull($"Invalid parameter in the RetractMedia Handle method. {nameof(retractMediaCmd)}");

            await HandleRetractMedia(Connection, retractMediaCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            RetractMediaCommand retractMediacommand = command as RetractMediaCommand;

            RetractMediaCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => RetractMediaCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => RetractMediaCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => RetractMediaCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            RetractMediaCompletion response = new RetractMediaCompletion(retractMediacommand.Headers.RequestId, new RetractMediaCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
