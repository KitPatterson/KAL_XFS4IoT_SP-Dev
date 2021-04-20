/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessIssuerUpdateHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(EMVClessIssuerUpdateCommand))]
    public partial class EMVClessIssuerUpdateHandler : ICommandHandler
    {
        public EMVClessIssuerUpdateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessIssuerUpdateHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessIssuerUpdateHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICardReaderDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessIssuerUpdateHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            EMVClessIssuerUpdateCommand eMVClessIssuerUpdateCmd = command as EMVClessIssuerUpdateCommand;
            eMVClessIssuerUpdateCmd.IsNotNull($"Invalid parameter in the EMVClessIssuerUpdate Handle method. {nameof(eMVClessIssuerUpdateCmd)}");

            await HandleEMVClessIssuerUpdate(Connection, eMVClessIssuerUpdateCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            EMVClessIssuerUpdateCommand eMVClessIssuerUpdatecommand = command as EMVClessIssuerUpdateCommand;

            EMVClessIssuerUpdateCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EMVClessIssuerUpdateCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => EMVClessIssuerUpdateCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => EMVClessIssuerUpdateCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            EMVClessIssuerUpdateCompletion response = new EMVClessIssuerUpdateCompletion(eMVClessIssuerUpdatecommand.Headers.RequestId, new EMVClessIssuerUpdateCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
