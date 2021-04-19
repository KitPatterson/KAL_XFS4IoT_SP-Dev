/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CapabilitiesHandler_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(CapabilitiesCommand))]
    public partial class CapabilitiesHandler : ICommandHandler
    {
        public CapabilitiesHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICommonDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(CapabilitiesHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            CapabilitiesCommand capabilitiesCmd = command as CapabilitiesCommand;
            capabilitiesCmd.IsNotNull($"Invalid parameter in the Capabilities Handle method. {nameof(capabilitiesCmd)}");

            await HandleCapabilities(Connection, capabilitiesCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            CapabilitiesCommand capabilitiescommand = command as CapabilitiesCommand;

            CapabilitiesCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => CapabilitiesCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            CapabilitiesCompletion response = new CapabilitiesCompletion(capabilitiescommand.Headers.RequestId, new CapabilitiesCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
