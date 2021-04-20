/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCardHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(RetainCardCommand))]
    public partial class RetainCardHandler : ICommandHandler
    {
        public RetainCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(RetainCardHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            RetainCardCommand retainCardCmd = command as RetainCardCommand;
            retainCardCmd.IsNotNull($"Invalid parameter in the RetainCard Handle method. {nameof(retainCardCmd)}");
            
            IRetainCardEvents events = new RetainCardEvents(Connection, retainCardCmd.Headers.RequestId);

            var result = await HandleRetainCard(events, retainCardCmd, cancel);
            await Connection.SendMessageAsync(new RetainCardCompletion(retainCardCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            RetainCardCommand retainCardcommand = command as RetainCardCommand;

            RetainCardCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => RetainCardCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => RetainCardCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => RetainCardCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            RetainCardCompletion response = new RetainCardCompletion(retainCardcommand.Headers.RequestId, new RetainCardCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
