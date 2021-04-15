/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DispLightHandler_g.cs uses automatically generated parts. 
 * created at 15/04/2021 15:46:45
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.TextTerminal.Commands;
using XFS4IoT.TextTerminal.Completions;

namespace XFS4IoTFramework.TextTerminal
{
    [CommandHandler(XFSConstants.ServiceClass.TextTerminal, typeof(DispLightCommand))]
    public partial class DispLightHandler : ICommandHandler
    {
        public DispLightHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DispLightHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(DispLightHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(DispLightHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ITextTerminalDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(DispLightHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(DispLightHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            DispLightCommand dispLightCmd = command as DispLightCommand;
            dispLightCmd.IsNotNull($"Invalid parameter in the DispLight Handle method. {nameof(dispLightCmd)}");

            await HandleDispLight(Connection, dispLightCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            DispLightCommand dispLightcommand = command as DispLightCommand;

            DispLightCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => DispLightCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => DispLightCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => DispLightCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            DispLightCompletion response = new DispLightCompletion(dispLightcommand.Headers.RequestId, new DispLightCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ITextTerminalDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
