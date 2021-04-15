/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * PowerSaveControlHandler_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(PowerSaveControlCommand))]
    public partial class PowerSaveControlHandler : ICommandHandler
    {
        public PowerSaveControlHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICommonDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(PowerSaveControlHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            PowerSaveControlCommand powerSaveControlCmd = command as PowerSaveControlCommand;
            powerSaveControlCmd.IsNotNull($"Invalid parameter in the PowerSaveControl Handle method. {nameof(powerSaveControlCmd)}");

            await HandlePowerSaveControl(Connection, powerSaveControlCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            PowerSaveControlCommand powerSaveControlcommand = command as PowerSaveControlCommand;

            PowerSaveControlCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => PowerSaveControlCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => PowerSaveControlCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => PowerSaveControlCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            PowerSaveControlCompletion response = new PowerSaveControlCompletion(powerSaveControlcommand.Headers.RequestId, new PowerSaveControlCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
