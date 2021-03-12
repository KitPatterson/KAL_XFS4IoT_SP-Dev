// (C) KAL ATM Software GmbH, 2021

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoTServer;

namespace Server
{
    [CommandHandler(typeof(ServicePublisher), typeof(XFS4IoT.Common.Commands.GetService))]
    public class GetServiceHandler : ICommandHandler
    {

        public GetServiceHandler(ICommandDispatcher Dispatcher, ILogger Logger)   
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetServiceHandler)} constructor. {nameof(Dispatcher)}");
            Contracts.IsTrue(Dispatcher is ServicePublisher, $"Expected a {nameof(XFS4IoTServer.ServicePublisher)} got {Dispatcher.GetType().FullName}");
            Logger.IsNotNull($"Invalid parameter received in the {nameof(GetServiceHandler)} constructor. {nameof(Logger)}");

            this.ServicePublisher = (ServicePublisher)Dispatcher;
            this.Logger = Logger;
        }

        public ServicePublisher ServicePublisher { get; }
        public ILogger Logger { get; }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            Connection.IsNotNull($"Invalid parameter received in the {nameof(Handle)} method. {nameof(Connection)}");
            command.IsNotNull($"Invalid parameter received in the {nameof(Handle)} method. {nameof(command)}");
            Contracts.IsNotNull(cancel, $"Invalid parameter received in the {nameof(Handle)} method. {nameof(cancel)}");

            XFS4IoT.Common.Commands.GetService getServiceCommand = command as XFS4IoT.Common.Commands.GetService;
            getServiceCommand.IsNotNull($"Unexpected command received in the {nameof(Handle)} method. {nameof(command)}");

            // For now just return good result and fixed services available
            XFS4IoT.Common.Responses.GetServicePayload payLoad = new XFS4IoT.Common.Responses.GetServicePayload(XFS4IoT.Common.Responses.GetServicePayload.CompletionCodeEnum.Success,
                                                                                                                "ok",
                                                                                                                "KAL",
                                                                                                                from service in ServicePublisher.Services
                                                                                                                select new XFS4IoT.Common.Responses.GetServicePayload.ServiceUriDetails(service.WSUri.AbsoluteUri));

            await Connection.SendMessageAsync(new XFS4IoT.Common.Responses.GetService(getServiceCommand.Headers.RequestId, payLoad));
        }

        public async Task HandleError(IConnection Connection, object command, Exception commandErrorException)
        {
            Connection.IsNotNull($"Invalid parameter received in the {nameof(Handle)} method. {nameof(Connection)}");
            command.IsNotNull($"Invalid parameter received in the {nameof(Handle)} method. {nameof(command)}");
            commandErrorException.IsNotNull($"Invalid parameter received in the {nameof(Handle)} method. {nameof(commandErrorException)}");

            XFS4IoT.Common.Commands.GetService getServiceCommand = command as XFS4IoT.Common.Commands.GetService;
            getServiceCommand.IsNotNull($"Unexpected command received in the {nameof(Handle)} method. {nameof(command)}");

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandErrorException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Common.Responses.GetServicePayload payLoad = new XFS4IoT.Common.Responses.GetServicePayload(errorCode, commandErrorException.Message);
  
            await Connection.SendMessageAsync(new XFS4IoT.Common.Responses.GetService(getServiceCommand.Headers.RequestId, payLoad));
        }
    }
}
