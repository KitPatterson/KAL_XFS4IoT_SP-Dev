/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteRawDataHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(WriteRawDataCommand))]
    public partial class WriteRawDataHandler : ICommandHandler
    {
        public WriteRawDataHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteRawDataHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(WriteRawDataHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(WriteRawDataHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            WriteRawDataCommand writeRawDataCmd = command as WriteRawDataCommand;
            writeRawDataCmd.IsNotNull($"Invalid parameter in the WriteRawData Handle method. {nameof(writeRawDataCmd)}");
            
            IWriteRawDataEvents events = new WriteRawDataEvents(Connection, writeRawDataCmd.Headers.RequestId);

            var result = await HandleWriteRawData(events, writeRawDataCmd, cancel);
            await Connection.SendMessageAsync(new WriteRawDataCompletion(writeRawDataCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            WriteRawDataCommand writeRawDatacommand = command as WriteRawDataCommand;

            WriteRawDataCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => WriteRawDataCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => WriteRawDataCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => WriteRawDataCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            WriteRawDataCompletion response = new WriteRawDataCompletion(writeRawDatacommand.Headers.RequestId, new WriteRawDataCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
