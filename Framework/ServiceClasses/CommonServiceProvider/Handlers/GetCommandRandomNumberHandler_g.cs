/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetCommandRandomNumberHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(GetCommandRandomNumberCommand))]
    public partial class GetCommandRandomNumberHandler : ICommandHandler
    {
        public GetCommandRandomNumberHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetCommandRandomNumberHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(GetCommandRandomNumberHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(GetCommandRandomNumberHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            GetCommandRandomNumberCommand getCommandRandomNumberCmd = command as GetCommandRandomNumberCommand;
            getCommandRandomNumberCmd.IsNotNull($"Invalid parameter in the GetCommandRandomNumber Handle method. {nameof(getCommandRandomNumberCmd)}");
            
            IGetCommandRandomNumberEvents events = new GetCommandRandomNumberEvents(Connection, getCommandRandomNumberCmd.Headers.RequestId);

            var result = await HandleGetCommandRandomNumber(events, getCommandRandomNumberCmd, cancel);
            await Connection.SendMessageAsync(new GetCommandRandomNumberCompletion(getCommandRandomNumberCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            GetCommandRandomNumberCommand getCommandRandomNumbercommand = command as GetCommandRandomNumberCommand;

            GetCommandRandomNumberCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => GetCommandRandomNumberCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => GetCommandRandomNumberCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => GetCommandRandomNumberCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            GetCommandRandomNumberCompletion response = new GetCommandRandomNumberCompletion(getCommandRandomNumbercommand.Headers.RequestId, new GetCommandRandomNumberCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
