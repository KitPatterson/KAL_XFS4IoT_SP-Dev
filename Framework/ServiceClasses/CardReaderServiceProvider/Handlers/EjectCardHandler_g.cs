/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EjectCardHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(EjectCardCommand))]
    public partial class EjectCardHandler : ICommandHandler
    {
        public EjectCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EjectCardHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EjectCardHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EjectCardHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            EjectCardCommand ejectCardCmd = command as EjectCardCommand;
            ejectCardCmd.IsNotNull($"Invalid parameter in the EjectCard Handle method. {nameof(ejectCardCmd)}");
            
            IEjectCardEvents events = new EjectCardEvents(Connection, ejectCardCmd.Headers.RequestId);

            var result = await HandleEjectCard(events, ejectCardCmd, cancel);
            await Connection.SendMessageAsync(new EjectCardCompletion(ejectCardCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            EjectCardCommand ejectCardcommand = command as EjectCardCommand;

            EjectCardCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EjectCardCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => EjectCardCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => EjectCardCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            EjectCardCompletion response = new EjectCardCompletion(ejectCardcommand.Headers.RequestId, new EjectCardCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
