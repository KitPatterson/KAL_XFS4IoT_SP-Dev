/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * LoadDefinitionHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Printer, typeof(LoadDefinitionCommand))]
    public partial class LoadDefinitionHandler : ICommandHandler
    {
        public LoadDefinitionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(LoadDefinitionHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(LoadDefinitionHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<IPrinterDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(LoadDefinitionHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            LoadDefinitionCommand loadDefinitionCmd = command as LoadDefinitionCommand;
            loadDefinitionCmd.IsNotNull($"Invalid parameter in the LoadDefinition Handle method. {nameof(loadDefinitionCmd)}");

            await HandleLoadDefinition(Connection, loadDefinitionCmd, cancel);
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            LoadDefinitionCommand loadDefinitioncommand = command as LoadDefinitionCommand;

            LoadDefinitionCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => LoadDefinitionCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => LoadDefinitionCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => LoadDefinitionCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            LoadDefinitionCompletion response = new LoadDefinitionCompletion(loadDefinitioncommand.Headers.RequestId, new LoadDefinitionCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public IPrinterDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}
