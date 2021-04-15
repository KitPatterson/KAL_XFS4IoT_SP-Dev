/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * SetKeyHandler_g.cs uses automatically generated parts. 
 * created at 15/04/2021 14:53:03
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
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(SetKeyCommand))]
    public partial class SetKeyHandler : ICommandHandler
    {
        public SetKeyHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetKeyHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher as ServiceProvider;
            Provider.IsNotNull($"Invalid parameter received in the {nameof(SetKeyHandler)} constructor. {nameof(Provider)}");
            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetKeyHandler)} constructor. {nameof(Provider.Device)}");

            Device = Provider.Device as ICardReaderDevice;
            Device.IsNotNull($"Invalid parameter received in the {nameof(SetKeyHandler)} constructor. {nameof(Device)}");

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetKeyHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SetKeyCommand setKeyCmd = command as SetKeyCommand;
            setKeyCmd.IsNotNull($"Invalid parameter in the SetKey Handle method. {nameof(setKeyCmd)}");

            await HandleSetKey(Connection, setKeyCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SetKeyCommand setKeycommand = command as SetKeyCommand;

            SetKeyCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetKeyCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetKeyCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetKeyCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SetKeyCompletion response = new SetKeyCompletion(setKeycommand.Headers.RequestId, new SetKeyCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICardReaderDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
