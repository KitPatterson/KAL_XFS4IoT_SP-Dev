/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CapabilitiesHandler_g.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
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
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(CapabilitiesHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            CapabilitiesCommand capabilitiesCmd = command as CapabilitiesCommand;
            capabilitiesCmd.IsNotNull($"Invalid parameter in the Capabilities Handle method. {nameof(capabilitiesCmd)}");
            
            ICapabilitiesEvents events = new CapabilitiesEvents(Connection, capabilitiesCmd.Headers.RequestId);

            var result = await HandleCapabilities(events, capabilitiesCmd, cancel);
            await Connection.SendMessageAsync(new CapabilitiesCompletion(capabilitiesCmd.Headers.RequestId, result));
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
