/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessQueryApplicationsHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(EMVClessQueryApplicationsCommand))]
    public partial class EMVClessQueryApplicationsHandler : ICommandHandler
    {
        public EMVClessQueryApplicationsHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessQueryApplicationsHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessQueryApplicationsHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessQueryApplicationsHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            EMVClessQueryApplicationsCommand eMVClessQueryApplicationsCmd = command as EMVClessQueryApplicationsCommand;
            eMVClessQueryApplicationsCmd.IsNotNull($"Invalid parameter in the EMVClessQueryApplications Handle method. {nameof(eMVClessQueryApplicationsCmd)}");
            
            IEMVClessQueryApplicationsEvents events = new EMVClessQueryApplicationsEvents(Connection, eMVClessQueryApplicationsCmd.Headers.RequestId);

            var result = await HandleEMVClessQueryApplications(events, eMVClessQueryApplicationsCmd, cancel);
            await Connection.SendMessageAsync(new EMVClessQueryApplicationsCompletion(eMVClessQueryApplicationsCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            EMVClessQueryApplicationsCommand eMVClessQueryApplicationscommand = command as EMVClessQueryApplicationsCommand;

            EMVClessQueryApplicationsCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EMVClessQueryApplicationsCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => EMVClessQueryApplicationsCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => EMVClessQueryApplicationsCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            EMVClessQueryApplicationsCompletion response = new EMVClessQueryApplicationsCompletion(eMVClessQueryApplicationscommand.Headers.RequestId, new EMVClessQueryApplicationsCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
