/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * MediaExtentsHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(MediaExtentsCommand))]
    public partial class MediaExtentsHandler : ICommandHandler
    {
        public MediaExtentsHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as IPrinterDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(MediaExtentsHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            MediaExtentsCommand mediaExtentsCmd = command as MediaExtentsCommand;
            mediaExtentsCmd.IsNotNull($"Invalid parameter in the MediaExtents Handle method. {nameof(mediaExtentsCmd)}");

            await HandleMediaExtents(Connection, mediaExtentsCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            MediaExtentsCommand mediaExtentscommand = command as MediaExtentsCommand;

            MediaExtentsCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => MediaExtentsCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            MediaExtentsCompletion response = new MediaExtentsCompletion(mediaExtentscommand.Headers.RequestId, new MediaExtentsCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
