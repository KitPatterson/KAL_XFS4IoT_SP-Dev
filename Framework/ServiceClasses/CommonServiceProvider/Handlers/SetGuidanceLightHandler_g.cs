/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * SetGuidanceLightHandler_g.cs uses automatically generated parts. 
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
    [CommandHandler(XFSConstants.ServiceClass.Common, typeof(SetGuidanceLightCommand))]
    public partial class SetGuidanceLightHandler : ICommandHandler
    {
        public SetGuidanceLightHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<ServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(Provider.Device)}");
            Device = Provider.Device.IsA<ICommonDevice>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            SetGuidanceLightCommand setGuidanceLightCmd = command as SetGuidanceLightCommand;
            setGuidanceLightCmd.IsNotNull($"Invalid parameter in the SetGuidanceLight Handle method. {nameof(setGuidanceLightCmd)}");
            
            ISetGuidanceLightEvents events = new SetGuidanceLightEvents(Connection, setGuidanceLightCmd.Headers.RequestId);

            var result = await HandleSetGuidanceLight(events, setGuidanceLightCmd, cancel);
            await Connection.SendMessageAsync(new SetGuidanceLightCompletion(setGuidanceLightCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            SetGuidanceLightCommand setGuidanceLightcommand = command as SetGuidanceLightCommand;

            SetGuidanceLightCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => SetGuidanceLightCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => SetGuidanceLightCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => SetGuidanceLightCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            SetGuidanceLightCompletion response = new SetGuidanceLightCompletion(setGuidanceLightcommand.Headers.RequestId, new SetGuidanceLightCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        public ICommonDevice Device { get; }
        public ServiceProvider Provider { get; }
        private ILogger Logger { get; }
    }

}