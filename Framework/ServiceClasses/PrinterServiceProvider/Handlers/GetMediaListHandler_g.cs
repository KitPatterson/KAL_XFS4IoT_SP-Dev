/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetMediaListHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 7:48:19 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Printer.Commands;
using XFS4IoT.Printer.Completions;

namespace XFS4IoTFramework.Printer
{
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(GetMediaListCommand))]
    public partial class GetMediaListHandler : ICommandHandler
    {
        public GetMediaListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetMediaListHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetMediaListHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetMediaListHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetMediaListCommand getMediaListCmd = command as GetMediaListCommand;
            getMediaListCmd.IsNotNull($"Invalid parameter in the GetMediaList Handle method. {nameof(getMediaListCmd)}");

            await HandleGetMediaList(Connection, getMediaListCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetMediaListCommand getMediaListcommand = command as GetMediaListCommand;

            GetMediaListCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetMediaListCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetMediaListCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetMediaListCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetMediaListCompletion response = new GetMediaListCompletion(getMediaListcommand.Headers.RequestId, new GetMediaListCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
