/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessPerformTransactionHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(EMVClessPerformTransactionCommand))]
    public partial class EMVClessPerformTransactionHandler : ICommandHandler
    {
        public EMVClessPerformTransactionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            EMVClessPerformTransactionCommand eMVClessPerformTransactionCmd = command as EMVClessPerformTransactionCommand;
            eMVClessPerformTransactionCmd.IsNotNull($"Invalid parameter in the EMVClessPerformTransaction Handle method. {nameof(eMVClessPerformTransactionCmd)}");

            await HandleEMVClessPerformTransaction(Connection, eMVClessPerformTransactionCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            EMVClessPerformTransactionCommand eMVClessPerformTransactioncommand = command as EMVClessPerformTransactionCommand;

            EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            EMVClessPerformTransactionCompletion response = new EMVClessPerformTransactionCompletion(eMVClessPerformTransactioncommand.Headers.RequestId, new EMVClessPerformTransactionCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
