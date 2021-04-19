/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipPowerHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(ChipPowerCommand))]
    public partial class ChipPowerHandler : ICommandHandler
    {
        public ChipPowerHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ChipPowerHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(ChipPowerHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ChipPowerHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(ChipPowerHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ChipPowerHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            ChipPowerCommand chipPowerCmd = command as ChipPowerCommand;
            chipPowerCmd.IsNotNull($"Invalid parameter in the ChipPower Handle method. {nameof(chipPowerCmd)}");

            await HandleChipPower(Connection, chipPowerCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            ChipPowerCommand chipPowercommand = command as ChipPowerCommand;

            ChipPowerCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ChipPowerCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ChipPowerCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ChipPowerCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            ChipPowerCompletion response = new ChipPowerCompletion(chipPowercommand.Headers.RequestId, new ChipPowerCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
