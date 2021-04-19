/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCardHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(RetainCardCommand))]
    public partial class RetainCardHandler : ICommandHandler
    {
        public RetainCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(RetainCardHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            RetainCardCommand retainCardCmd = command as RetainCardCommand;
            retainCardCmd.IsNotNull($"Invalid parameter in the RetainCard Handle method. {nameof(retainCardCmd)}");

            await HandleRetainCard(Connection, retainCardCmd, cancel);
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
