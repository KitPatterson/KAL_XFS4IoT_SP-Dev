/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteTrackHandler_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(WriteTrackCommand))]
    public partial class WriteTrackHandler : ICommandHandler
    {
        public WriteTrackHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteTrackHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(WriteTrackHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(WriteTrackHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(WriteTrackHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(WriteTrackHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            WriteTrackCommand writeTrackCmd = command as WriteTrackCommand;
            writeTrackCmd.IsNotNull($"Invalid parameter in the WriteTrack Handle method. {nameof(writeTrackCmd)}");

            await HandleWriteTrack(Connection, writeTrackCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            WriteTrackCommand writeTrackcommand = command as WriteTrackCommand;

            WriteTrackCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => WriteTrackCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => WriteTrackCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => WriteTrackCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            WriteTrackCompletion response = new WriteTrackCompletion(writeTrackcommand.Headers.RequestId, new WriteTrackCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
