/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteRawDataHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(WriteRawDataHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(WriteRawDataHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(WriteRawDataHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(WriteRawDataHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            WriteRawDataCommand writeRawDataCmd = command as WriteRawDataCommand;
            writeRawDataCmd.IsNotNull($"Invalid parameter in the WriteRawData Handle method. {nameof(writeRawDataCmd)}");

            await HandleWriteRawData(Connection, writeRawDataCmd, cancel);
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
