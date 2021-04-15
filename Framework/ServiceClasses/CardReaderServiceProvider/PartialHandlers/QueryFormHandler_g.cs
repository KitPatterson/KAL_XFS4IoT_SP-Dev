/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryFormHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(QueryFormCommand))]
    public partial class QueryFormHandler : ICommandHandler
    {
        public QueryFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(QueryFormHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(QueryFormHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(QueryFormHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(QueryFormHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(QueryFormHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            QueryFormCommand queryFormCmd = command as QueryFormCommand;
            queryFormCmd.IsNotNull($"Invalid parameter in the QueryForm Handle method. {nameof(queryFormCmd)}");

            await HandleQueryForm(Connection, queryFormCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            QueryFormCommand queryFormcommand = command as QueryFormCommand;

            QueryFormCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => QueryFormCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => QueryFormCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => QueryFormCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            QueryFormCompletion response = new QueryFormCompletion(queryFormcommand.Headers.RequestId, new QueryFormCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
