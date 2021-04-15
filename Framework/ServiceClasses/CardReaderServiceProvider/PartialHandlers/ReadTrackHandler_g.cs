/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ReadTrackHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(ReadTrackCommand))]
    public partial class ReadTrackHandler : ICommandHandler
    {
        public ReadTrackHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadTrackHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(ReadTrackHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ReadTrackHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(ReadTrackHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ReadTrackHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ReadTrackCommand readTrackCmd = command as ReadTrackCommand;
            readTrackCmd.IsNotNull($"Invalid parameter in the ReadTrack Handle method. {nameof(readTrackCmd)}");

            await HandleReadTrack(Connection, readTrackCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ReadTrackCommand readTrackcommand = command as ReadTrackCommand;

            ReadTrackCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ReadTrackCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ReadTrackCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ReadTrackCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ReadTrackCompletion response = new ReadTrackCompletion(readTrackcommand.Headers.RequestId, new ReadTrackCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
