/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ParkCardHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(ParkCardCommand))]
    public partial class ParkCardHandler : ICommandHandler
    {
        public ParkCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ParkCardHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ParkCardHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ParkCardHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ParkCardCommand parkCardCmd = command as ParkCardCommand;
            parkCardCmd.IsNotNull($"Invalid parameter in the ParkCard Handle method. {nameof(parkCardCmd)}");
            
            IParkCardEvents events = new ParkCardEvents(Connection, parkCardCmd.Headers.RequestId);

            var result = await HandleParkCard(events, parkCardCmd, cancel);
            await Connection.SendMessageAsync(new ParkCardCompletion(parkCardCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ParkCardCommand parkCardcommand = command as ParkCardCommand;

            ParkCardCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ParkCardCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ParkCardCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ParkCardCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ParkCardCompletion response = new ParkCardCompletion(parkCardcommand.Headers.RequestId, new ParkCardCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
