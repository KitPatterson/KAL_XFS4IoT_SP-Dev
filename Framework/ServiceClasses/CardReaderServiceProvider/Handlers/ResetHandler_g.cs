/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ResetHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(ResetCommand))]
    public partial class ResetHandler : ICommandHandler
    {
        public ResetHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<CardReaderServiceClass>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var resetCmd = command.IsA<ResetCommand>($"Invalid parameter in the Reset Handle method. {nameof(ResetCommand)}");
            
            IResetEvents events = new ResetEvents(Connection, resetCmd.Headers.RequestId);

            var result = await HandleReset(events, resetCmd, cancel);
            await Connection.SendMessageAsync(new ResetCompletion(resetCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var resetcommand = command.IsA<ResetCommand>();

            ResetCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ResetCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ResetCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ResetCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ResetCompletion(resetcommand.Headers.RequestId, new ResetCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICardReaderDevice Device { get; }
        private CardReaderServiceClass Provider { get; }
        private ILogger Logger { get; }
    }

}
