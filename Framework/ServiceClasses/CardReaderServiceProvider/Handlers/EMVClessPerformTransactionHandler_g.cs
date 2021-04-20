/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessPerformTransactionHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(EMVClessPerformTransactionCommand))]
    public partial class EMVClessPerformTransactionHandler : ICommandHandler
    {
        public EMVClessPerformTransactionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            EMVClessPerformTransactionCommand eMVClessPerformTransactionCmd = command as EMVClessPerformTransactionCommand;
            eMVClessPerformTransactionCmd.IsNotNull($"Invalid parameter in the EMVClessPerformTransaction Handle method. {nameof(eMVClessPerformTransactionCmd)}");
            
            IEMVClessPerformTransactionEvents events = new EMVClessPerformTransactionEvents(Connection, eMVClessPerformTransactionCmd.Headers.RequestId);

            var result = await HandleEMVClessPerformTransaction(events, eMVClessPerformTransactionCmd, cancel);
            await Connection.SendMessageAsync(new EMVClessPerformTransactionCompletion(eMVClessPerformTransactionCmd.Headers.RequestId, result));
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
