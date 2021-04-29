/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryIFMIdentifierHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(QueryIFMIdentifierCommand))]
    public partial class QueryIFMIdentifierHandler : ICommandHandler
    {
        public QueryIFMIdentifierHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(QueryIFMIdentifierHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<CardReaderServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(QueryIFMIdentifierHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(QueryIFMIdentifierHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var queryIFMIdentifierCmd = command.IsA<QueryIFMIdentifierCommand>($"Invalid parameter in the QueryIFMIdentifier Handle method. {nameof(QueryIFMIdentifierCommand)}");
            
            IQueryIFMIdentifierEvents events = new QueryIFMIdentifierEvents(Connection, queryIFMIdentifierCmd.Headers.RequestId);

            var result = await HandleQueryIFMIdentifier(events, queryIFMIdentifierCmd, cancel);
            await Connection.SendMessageAsync(new QueryIFMIdentifierCompletion(queryIFMIdentifierCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var queryIFMIdentifiercommand = command.IsA<QueryIFMIdentifierCommand>();

            QueryIFMIdentifierCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => QueryIFMIdentifierCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => QueryIFMIdentifierCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => QueryIFMIdentifierCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new QueryIFMIdentifierCompletion(queryIFMIdentifiercommand.Headers.RequestId, new QueryIFMIdentifierCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICardReaderDevice Device { get; }
        private CardReaderServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
