/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessPerformTransactionHandler_g.cs uses automatically generated parts. 
 * created at 29/04/2021 00:49:04
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
            Provider = Dispatcher.IsA<CardReaderServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var eMVClessPerformTransactionCmd = command.IsA<EMVClessPerformTransactionCommand>($"Invalid parameter in the EMVClessPerformTransaction Handle method. {nameof(EMVClessPerformTransactionCommand)}");
            
            IEMVClessPerformTransactionEvents events = new EMVClessPerformTransactionEvents(Connection, eMVClessPerformTransactionCmd.Headers.RequestId);

            var result = await HandleEMVClessPerformTransaction(events, eMVClessPerformTransactionCmd, cancel);
            await Connection.SendMessageAsync(new EMVClessPerformTransactionCompletion(eMVClessPerformTransactionCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var eMVClessPerformTransactioncommand = command.IsA<EMVClessPerformTransactionCommand>();

            EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => EMVClessPerformTransactionCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new EMVClessPerformTransactionCompletion(eMVClessPerformTransactioncommand.Headers.RequestId, new EMVClessPerformTransactionCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICardReaderDevice Device { get; }
        private CardReaderServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
