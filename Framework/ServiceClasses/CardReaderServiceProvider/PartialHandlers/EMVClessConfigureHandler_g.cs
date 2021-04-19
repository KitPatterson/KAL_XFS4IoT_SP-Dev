/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessConfigureHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(EMVClessConfigureCommand))]
    public partial class EMVClessConfigureHandler : ICommandHandler
    {
        public EMVClessConfigureHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            EMVClessConfigureCommand eMVClessConfigureCmd = command as EMVClessConfigureCommand;
            eMVClessConfigureCmd.IsNotNull($"Invalid parameter in the EMVClessConfigure Handle method. {nameof(eMVClessConfigureCmd)}");

            await HandleEMVClessConfigure(Connection, eMVClessConfigureCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            EMVClessConfigureCommand eMVClessConfigurecommand = command as EMVClessConfigureCommand;

            EMVClessConfigureCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EMVClessConfigureCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => EMVClessConfigureCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => EMVClessConfigureCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            EMVClessConfigureCompletion response = new EMVClessConfigureCompletion(eMVClessConfigurecommand.Headers.RequestId, new EMVClessConfigureCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
