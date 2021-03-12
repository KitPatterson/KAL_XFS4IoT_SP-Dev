// (C) KAL ATM Software GmbH, 2021

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTCardReader;
using XFS4IoTServer;

namespace CardReader
{
    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.FormList))]
    public class FormListHandler : ICommandHandler
    {
        public FormListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(FormListHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(FormListHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteFormList(Connection, command as XFS4IoT.CardReader.Commands.FormList, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.FormList formListcommand = command as XFS4IoT.CardReader.Commands.FormList;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.FormList response = new XFS4IoT.CardReader.Responses.FormList(formListcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.FormListPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteFormList(IConnection connection, XFS4IoT.CardReader.Commands.FormList formList, CancellationToken cancel)
        {
            formList.IsNotNull($"Invalid parameter in the ExecuteFormList method. {nameof(formList)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, formList.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.FormList()");
            Task<XFS4IoT.CardReader.Responses.FormListPayload> task = ServiceProvider.Device.FormList(cardReaderConnection, formList.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.FormList() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.FormList response = new XFS4IoT.CardReader.Responses.FormList(formList.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.QueryForm))]
    public class QueryFormHandler : ICommandHandler
    {
        public QueryFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(QueryFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(QueryFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteQueryForm(Connection, command as XFS4IoT.CardReader.Commands.QueryForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.QueryForm queryFormcommand = command as XFS4IoT.CardReader.Commands.QueryForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.QueryForm response = new XFS4IoT.CardReader.Responses.QueryForm(queryFormcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.QueryFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteQueryForm(IConnection connection, XFS4IoT.CardReader.Commands.QueryForm queryForm, CancellationToken cancel)
        {
            queryForm.IsNotNull($"Invalid parameter in the ExecuteQueryForm method. {nameof(queryForm)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, queryForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.QueryForm()");
            Task<XFS4IoT.CardReader.Responses.QueryFormPayload> task = ServiceProvider.Device.QueryForm(cardReaderConnection, queryForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.QueryForm() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.QueryForm response = new XFS4IoT.CardReader.Responses.QueryForm(queryForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.QueryIFMIdentifier))]
    public class QueryIFMIdentifierHandler : ICommandHandler
    {
        public QueryIFMIdentifierHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(QueryIFMIdentifierHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(QueryIFMIdentifierHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteQueryIFMIdentifier(Connection, command as XFS4IoT.CardReader.Commands.QueryIFMIdentifier, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.QueryIFMIdentifier queryIFMIdentifiercommand = command as XFS4IoT.CardReader.Commands.QueryIFMIdentifier;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.QueryIFMIdentifier response = new XFS4IoT.CardReader.Responses.QueryIFMIdentifier(queryIFMIdentifiercommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.QueryIFMIdentifierPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteQueryIFMIdentifier(IConnection connection, XFS4IoT.CardReader.Commands.QueryIFMIdentifier queryIFMIdentifier, CancellationToken cancel)
        {
            queryIFMIdentifier.IsNotNull($"Invalid parameter in the ExecuteQueryIFMIdentifier method. {nameof(queryIFMIdentifier)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, queryIFMIdentifier.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.QueryIFMIdentifier()");
            Task<XFS4IoT.CardReader.Responses.QueryIFMIdentifierPayload> task = ServiceProvider.Device.QueryIFMIdentifier(cardReaderConnection, queryIFMIdentifier.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.QueryIFMIdentifier() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.QueryIFMIdentifier response = new XFS4IoT.CardReader.Responses.QueryIFMIdentifier(queryIFMIdentifier.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.EMVClessQueryApplications))]
    public class EMVClessQueryApplicationsHandler : ICommandHandler
    {
        public EMVClessQueryApplicationsHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessQueryApplicationsHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessQueryApplicationsHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteEMVClessQueryApplications(Connection, command as XFS4IoT.CardReader.Commands.EMVClessQueryApplications, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.EMVClessQueryApplications eMVClessQueryApplicationscommand = command as XFS4IoT.CardReader.Commands.EMVClessQueryApplications;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.EMVClessQueryApplications response = new XFS4IoT.CardReader.Responses.EMVClessQueryApplications(eMVClessQueryApplicationscommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.EMVClessQueryApplicationsPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteEMVClessQueryApplications(IConnection connection, XFS4IoT.CardReader.Commands.EMVClessQueryApplications eMVClessQueryApplications, CancellationToken cancel)
        {
            eMVClessQueryApplications.IsNotNull($"Invalid parameter in the ExecuteEMVClessQueryApplications method. {nameof(eMVClessQueryApplications)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, eMVClessQueryApplications.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessQueryApplications()");
            Task<XFS4IoT.CardReader.Responses.EMVClessQueryApplicationsPayload> task = ServiceProvider.Device.EMVClessQueryApplications(cardReaderConnection, eMVClessQueryApplications.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessQueryApplications() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.EMVClessQueryApplications response = new XFS4IoT.CardReader.Responses.EMVClessQueryApplications(eMVClessQueryApplications.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ReadTrack))]
    public class ReadTrackHandler : ICommandHandler
    {
        public ReadTrackHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadTrackHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ReadTrackHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReadTrack(Connection, command as XFS4IoT.CardReader.Commands.ReadTrack, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ReadTrack readTrackcommand = command as XFS4IoT.CardReader.Commands.ReadTrack;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ReadTrack response = new XFS4IoT.CardReader.Responses.ReadTrack(readTrackcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ReadTrackPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReadTrack(IConnection connection, XFS4IoT.CardReader.Commands.ReadTrack readTrack, CancellationToken cancel)
        {
            readTrack.IsNotNull($"Invalid parameter in the ExecuteReadTrack method. {nameof(readTrack)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, readTrack.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ReadTrack()");
            Task<XFS4IoT.CardReader.Responses.ReadTrackPayload> task = ServiceProvider.Device.ReadTrack(cardReaderConnection, readTrack.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ReadTrack() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ReadTrack response = new XFS4IoT.CardReader.Responses.ReadTrack(readTrack.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.WriteTrack))]
    public class WriteTrackHandler : ICommandHandler
    {
        public WriteTrackHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteTrackHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(WriteTrackHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteWriteTrack(Connection, command as XFS4IoT.CardReader.Commands.WriteTrack, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.WriteTrack writeTrackcommand = command as XFS4IoT.CardReader.Commands.WriteTrack;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.WriteTrack response = new XFS4IoT.CardReader.Responses.WriteTrack(writeTrackcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.WriteTrackPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteWriteTrack(IConnection connection, XFS4IoT.CardReader.Commands.WriteTrack writeTrack, CancellationToken cancel)
        {
            writeTrack.IsNotNull($"Invalid parameter in the ExecuteWriteTrack method. {nameof(writeTrack)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, writeTrack.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.WriteTrack()");
            Task<XFS4IoT.CardReader.Responses.WriteTrackPayload> task = ServiceProvider.Device.WriteTrack(cardReaderConnection, writeTrack.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.WriteTrack() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.WriteTrack response = new XFS4IoT.CardReader.Responses.WriteTrack(writeTrack.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.EjectCard))]
    public class EjectCardHandler : ICommandHandler
    {
        public EjectCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EjectCardHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(EjectCardHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteEjectCard(Connection, command as XFS4IoT.CardReader.Commands.EjectCard, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.EjectCard ejectCardcommand = command as XFS4IoT.CardReader.Commands.EjectCard;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.EjectCard response = new XFS4IoT.CardReader.Responses.EjectCard(ejectCardcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.EjectCardPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteEjectCard(IConnection connection, XFS4IoT.CardReader.Commands.EjectCard ejectCard, CancellationToken cancel)
        {
            ejectCard.IsNotNull($"Invalid parameter in the ExecuteEjectCard method. {nameof(ejectCard)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, ejectCard.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EjectCard()");
            Task<XFS4IoT.CardReader.Responses.EjectCardPayload> task = ServiceProvider.Device.EjectCard(cardReaderConnection, ejectCard.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EjectCard() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.EjectCard response = new XFS4IoT.CardReader.Responses.EjectCard(ejectCard.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);

            if (response.Payload.CompletionCode == XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success)
            {
                await ServiceProvider.Device.WaitForCardTaken(cardReaderConnection, cancel);
            }
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.RetainCard))]
    public class RetainCardHandler : ICommandHandler
    {
        public RetainCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(RetainCardHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteRetainCard(Connection, command as XFS4IoT.CardReader.Commands.RetainCard, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.RetainCard retainCardcommand = command as XFS4IoT.CardReader.Commands.RetainCard;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.RetainCard response = new XFS4IoT.CardReader.Responses.RetainCard(retainCardcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.RetainCardPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteRetainCard(IConnection connection, XFS4IoT.CardReader.Commands.RetainCard retainCard, CancellationToken cancel)
        {
            retainCard.IsNotNull($"Invalid parameter in the ExecuteRetainCard method. {nameof(retainCard)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, retainCard.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.RetainCard()");
            Task<XFS4IoT.CardReader.Responses.RetainCardPayload> task = ServiceProvider.Device.RetainCard(cardReaderConnection, retainCard.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.RetainCard() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.RetainCard response = new XFS4IoT.CardReader.Responses.RetainCard(retainCard.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ResetCount))]
    public class ResetCountHandler : ICommandHandler
    {
        public ResetCountHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetCountHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ResetCountHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteResetCount(Connection, command as XFS4IoT.CardReader.Commands.ResetCount, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ResetCount resetCountcommand = command as XFS4IoT.CardReader.Commands.ResetCount;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ResetCount response = new XFS4IoT.CardReader.Responses.ResetCount(resetCountcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ResetCountPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteResetCount(IConnection connection, XFS4IoT.CardReader.Commands.ResetCount resetCount, CancellationToken cancel)
        {
            resetCount.IsNotNull($"Invalid parameter in the ExecuteResetCount method. {nameof(resetCount)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, resetCount.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ResetCount()");
            Task<XFS4IoT.CardReader.Responses.ResetCountPayload> task = ServiceProvider.Device.ResetCount(cardReaderConnection, resetCount.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ResetCount() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ResetCount response = new XFS4IoT.CardReader.Responses.ResetCount(resetCount.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.SetKey))]
    public class SetKeyHandler : ICommandHandler
    {
        public SetKeyHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetKeyHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SetKeyHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSetKey(Connection, command as XFS4IoT.CardReader.Commands.SetKey, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.SetKey setKeycommand = command as XFS4IoT.CardReader.Commands.SetKey;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.SetKey response = new XFS4IoT.CardReader.Responses.SetKey(setKeycommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.SetKeyPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetKey(IConnection connection, XFS4IoT.CardReader.Commands.SetKey setKey, CancellationToken cancel)
        {
            setKey.IsNotNull($"Invalid parameter in the ExecuteSetKey method. {nameof(setKey)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, setKey.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.SetKey()");
            Task<XFS4IoT.CardReader.Responses.SetKeyPayload> task = ServiceProvider.Device.SetKey(cardReaderConnection, setKey.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.SetKey() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.SetKey response = new XFS4IoT.CardReader.Responses.SetKey(setKey.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ReadRawData))]
    public class ReadRawDataHandler : ICommandHandler
    {
        public ReadRawDataHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadRawDataHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ReadRawDataHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReadRawData(Connection, command as XFS4IoT.CardReader.Commands.ReadRawData, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ReadRawData readRawDatacommand = command as XFS4IoT.CardReader.Commands.ReadRawData;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ReadRawData response = new XFS4IoT.CardReader.Responses.ReadRawData(readRawDatacommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ReadRawDataPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReadRawData(IConnection connection, XFS4IoT.CardReader.Commands.ReadRawData readRawData, CancellationToken cancel)
        {
            readRawData.IsNotNull($"Invalid parameter in the ExecuteReadRawData method. {nameof(readRawData)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, readRawData.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ReadRawData()");
            Task<XFS4IoT.CardReader.Responses.ReadRawDataPayload> task = ServiceProvider.Device.ReadRawData(cardReaderConnection, readRawData.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ReadRawData() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ReadRawData response = new XFS4IoT.CardReader.Responses.ReadRawData(readRawData.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.WriteRawData))]
    public class WriteRawDataHandler : ICommandHandler
    {
        public WriteRawDataHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteRawDataHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(WriteRawDataHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteWriteRawData(Connection, command as XFS4IoT.CardReader.Commands.WriteRawData, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.WriteRawData writeRawDatacommand = command as XFS4IoT.CardReader.Commands.WriteRawData;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.WriteRawData response = new XFS4IoT.CardReader.Responses.WriteRawData(writeRawDatacommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.WriteRawDataPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteWriteRawData(IConnection connection, XFS4IoT.CardReader.Commands.WriteRawData writeRawData, CancellationToken cancel)
        {
            writeRawData.IsNotNull($"Invalid parameter in the ExecuteWriteRawData method. {nameof(writeRawData)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, writeRawData.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.WriteRawData()");
            Task<XFS4IoT.CardReader.Responses.WriteRawDataPayload> task = ServiceProvider.Device.WriteRawData(cardReaderConnection, writeRawData.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.WriteRawData() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.WriteRawData response = new XFS4IoT.CardReader.Responses.WriteRawData(writeRawData.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ChipIO))]
    public class ChipIOHandler : ICommandHandler
    {
        public ChipIOHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ChipIOHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ChipIOHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteChipIO(Connection, command as XFS4IoT.CardReader.Commands.ChipIO, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ChipIO chipIOcommand = command as XFS4IoT.CardReader.Commands.ChipIO;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ChipIO response = new XFS4IoT.CardReader.Responses.ChipIO(chipIOcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ChipIOPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteChipIO(IConnection connection, XFS4IoT.CardReader.Commands.ChipIO chipIO, CancellationToken cancel)
        {
            chipIO.IsNotNull($"Invalid parameter in the ExecuteChipIO method. {nameof(chipIO)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, chipIO.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ChipIO()");
            Task<XFS4IoT.CardReader.Responses.ChipIOPayload> task = ServiceProvider.Device.ChipIO(cardReaderConnection, chipIO.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ChipIO() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ChipIO response = new XFS4IoT.CardReader.Responses.ChipIO(chipIO.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.Reset))]
    public class ResetHandler : ICommandHandler
    {
        public ResetHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReset(Connection, command as XFS4IoT.CardReader.Commands.Reset, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.Reset resetcommand = command as XFS4IoT.CardReader.Commands.Reset;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.Reset response = new XFS4IoT.CardReader.Responses.Reset(resetcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ResetPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReset(IConnection connection, XFS4IoT.CardReader.Commands.Reset reset, CancellationToken cancel)
        {
            reset.IsNotNull($"Invalid parameter in the ExecuteReset method. {nameof(reset)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, reset.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.Reset()");
            Task<XFS4IoT.CardReader.Responses.ResetPayload> task = ServiceProvider.Device.Reset(cardReaderConnection, reset.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.Reset() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.Reset response = new XFS4IoT.CardReader.Responses.Reset(reset.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ChipPower))]
    public class ChipPowerHandler : ICommandHandler
    {
        public ChipPowerHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ChipPowerHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ChipPowerHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteChipPower(Connection, command as XFS4IoT.CardReader.Commands.ChipPower, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ChipPower chipPowercommand = command as XFS4IoT.CardReader.Commands.ChipPower;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ChipPower response = new XFS4IoT.CardReader.Responses.ChipPower(chipPowercommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ChipPowerPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteChipPower(IConnection connection, XFS4IoT.CardReader.Commands.ChipPower chipPower, CancellationToken cancel)
        {
            chipPower.IsNotNull($"Invalid parameter in the ExecuteChipPower method. {nameof(chipPower)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, chipPower.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ChipPower()");
            Task<XFS4IoT.CardReader.Responses.ChipPowerPayload> task = ServiceProvider.Device.ChipPower(cardReaderConnection, chipPower.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ChipPower() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ChipPower response = new XFS4IoT.CardReader.Responses.ChipPower(chipPower.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ParseData))]
    public class ParseDataHandler : ICommandHandler
    {
        public ParseDataHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ParseDataHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ParseDataHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteParseData(Connection, command as XFS4IoT.CardReader.Commands.ParseData, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ParseData parseDatacommand = command as XFS4IoT.CardReader.Commands.ParseData;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ParseData response = new XFS4IoT.CardReader.Responses.ParseData(parseDatacommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ParseDataPayload(errorCode, commandException.Message, 0));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteParseData(IConnection connection, XFS4IoT.CardReader.Commands.ParseData parseData, CancellationToken cancel)
        {
            parseData.IsNotNull($"Invalid parameter in the ExecuteParseData method. {nameof(parseData)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, parseData.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ParseData()");
            Task<XFS4IoT.CardReader.Responses.ParseDataPayload> task = ServiceProvider.Device.ParseData(cardReaderConnection, parseData.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ParseData() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ParseData response = new XFS4IoT.CardReader.Responses.ParseData(parseData.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.ParkCard))]
    public class ParkCardHandler : ICommandHandler
    {
        public ParkCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ParkCardHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ParkCardHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteParkCard(Connection, command as XFS4IoT.CardReader.Commands.ParkCard, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.ParkCard parkCardcommand = command as XFS4IoT.CardReader.Commands.ParkCard;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.ParkCard response = new XFS4IoT.CardReader.Responses.ParkCard(parkCardcommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.ParkCardPayload(errorCode, commandException.Message, 0));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteParkCard(IConnection connection, XFS4IoT.CardReader.Commands.ParkCard parkCard, CancellationToken cancel)
        {
            parkCard.IsNotNull($"Invalid parameter in the ExecuteParkCard method. {nameof(parkCard)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, parkCard.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.ParkCard()");
            Task<XFS4IoT.CardReader.Responses.ParkCardPayload> task = ServiceProvider.Device.ParkCard(cardReaderConnection, parkCard.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ParkCard() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.ParkCard response = new XFS4IoT.CardReader.Responses.ParkCard(parkCard.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.EMVClessConfigure))]
    public class EMVClessConfigureHandler : ICommandHandler
    {
        public EMVClessConfigureHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessConfigureHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteEMVClessConfigure(Connection, command as XFS4IoT.CardReader.Commands.EMVClessConfigure, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.EMVClessConfigure eMVClessConfigurecommand = command as XFS4IoT.CardReader.Commands.EMVClessConfigure;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.EMVClessConfigure response = new XFS4IoT.CardReader.Responses.EMVClessConfigure(eMVClessConfigurecommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.EMVClessConfigurePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteEMVClessConfigure(IConnection connection, XFS4IoT.CardReader.Commands.EMVClessConfigure eMVClessConfigure, CancellationToken cancel)
        {
            eMVClessConfigure.IsNotNull($"Invalid parameter in the ExecuteEMVClessConfigure method. {nameof(eMVClessConfigure)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, eMVClessConfigure.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessConfigure()");
            Task<XFS4IoT.CardReader.Responses.EMVClessConfigurePayload> task = ServiceProvider.Device.EMVClessConfigure(cardReaderConnection, eMVClessConfigure.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessConfigure() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.EMVClessConfigure response = new XFS4IoT.CardReader.Responses.EMVClessConfigure(eMVClessConfigure.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.EMVClessPerformTransaction))]
    public class EMVClessPerformTransactionHandler : ICommandHandler
    {
        public EMVClessPerformTransactionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessPerformTransactionHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteEMVClessPerformTransaction(Connection, command as XFS4IoT.CardReader.Commands.EMVClessPerformTransaction, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.EMVClessPerformTransaction eMVClessPerformTransactioncommand = command as XFS4IoT.CardReader.Commands.EMVClessPerformTransaction;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.EMVClessPerformTransaction response = new XFS4IoT.CardReader.Responses.EMVClessPerformTransaction(eMVClessPerformTransactioncommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.EMVClessPerformTransactionPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteEMVClessPerformTransaction(IConnection connection, XFS4IoT.CardReader.Commands.EMVClessPerformTransaction eMVClessPerformTransaction, CancellationToken cancel)
        {
            eMVClessPerformTransaction.IsNotNull($"Invalid parameter in the ExecuteEMVClessPerformTransaction method. {nameof(eMVClessPerformTransaction)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, eMVClessPerformTransaction.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessPerformTransaction()");
            Task<XFS4IoT.CardReader.Responses.EMVClessPerformTransactionPayload> task = ServiceProvider.Device.EMVClessPerformTransaction(cardReaderConnection, eMVClessPerformTransaction.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessPerformTransaction() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.EMVClessPerformTransaction response = new XFS4IoT.CardReader.Responses.EMVClessPerformTransaction(eMVClessPerformTransaction.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.CardReader.Commands.EMVClessIssuerUpdate))]
    public class EMVClessIssuerUpdateHandler : ICommandHandler
    {
        public EMVClessIssuerUpdateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EMVClessIssuerUpdateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(EMVClessIssuerUpdateHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteEMVClessIssuerUpdate(Connection, command as XFS4IoT.CardReader.Commands.EMVClessIssuerUpdate, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.CardReader.Commands.EMVClessIssuerUpdate eMVClessIssuerUpdatecommand = command as XFS4IoT.CardReader.Commands.EMVClessIssuerUpdate;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.CardReader.Responses.EMVClessIssuerUpdate response = new XFS4IoT.CardReader.Responses.EMVClessIssuerUpdate(eMVClessIssuerUpdatecommand.Headers.RequestId, new XFS4IoT.CardReader.Responses.EMVClessIssuerUpdatePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteEMVClessIssuerUpdate(IConnection connection, XFS4IoT.CardReader.Commands.EMVClessIssuerUpdate eMVClessIssuerUpdate, CancellationToken cancel)
        {
            eMVClessIssuerUpdate.IsNotNull($"Invalid parameter in the ExecuteEMVClessIssuerUpdate method. {nameof(eMVClessIssuerUpdate)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, eMVClessIssuerUpdate.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessIssuerUpdate()");
            Task<XFS4IoT.CardReader.Responses.EMVClessIssuerUpdatePayload> task = ServiceProvider.Device.EMVClessIssuerUpdate(cardReaderConnection, eMVClessIssuerUpdate.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessIssuerUpdate() -> {task.Result.CompletionCode}");

            XFS4IoT.CardReader.Responses.EMVClessIssuerUpdate response = new XFS4IoT.CardReader.Responses.EMVClessIssuerUpdate(eMVClessIssuerUpdate.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.Status))]
    public class StatusHandler : ICommandHandler
    {
        public StatusHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(StatusHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteStatus(Connection, command as XFS4IoT.Common.Commands.Status, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.Status statuscommand = command as XFS4IoT.Common.Commands.Status;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.Status response = new XFS4IoT.Common.Responses.Status(statuscommand.Headers.RequestId, new XFS4IoT.Common.Responses.StatusPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteStatus(IConnection connection, XFS4IoT.Common.Commands.Status status, CancellationToken cancel)
        {
            status.IsNotNull($"Invalid parameter in the ExecuteStatus method. {nameof(status)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, status.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.Status()");
            Task<XFS4IoT.Common.Responses.StatusPayload> task = ServiceProvider.Device.Status(cardReaderConnection, status.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.Status() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.Status response = new XFS4IoT.Common.Responses.Status(status.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.Capabilities))]
    public class CapabilitiesHandler : ICommandHandler
    {
        public CapabilitiesHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(CapabilitiesHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteCapabilities(Connection, command as XFS4IoT.Common.Commands.Capabilities, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.Capabilities capabilitiescommand = command as XFS4IoT.Common.Commands.Capabilities;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.Capabilities response = new XFS4IoT.Common.Responses.Capabilities(capabilitiescommand.Headers.RequestId, new XFS4IoT.Common.Responses.CapabilitiesPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteCapabilities(IConnection connection, XFS4IoT.Common.Commands.Capabilities capabilities, CancellationToken cancel)
        {
            capabilities.IsNotNull($"Invalid parameter in the ExecuteCapabilities method. {nameof(capabilities)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, capabilities.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.Capabilities()");
            Task<XFS4IoT.Common.Responses.CapabilitiesPayload> task = ServiceProvider.Device.Capabilities(cardReaderConnection, capabilities.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.Capabilities() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.Capabilities response = new XFS4IoT.Common.Responses.Capabilities(capabilities.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.SetGuidanceLight))]
    public class SetGuidanceLightHandler : ICommandHandler
    {
        public SetGuidanceLightHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSetGuidanceLight(Connection, command as XFS4IoT.Common.Commands.SetGuidanceLight, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.SetGuidanceLight setGuidanceLightcommand = command as XFS4IoT.Common.Commands.SetGuidanceLight;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.SetGuidanceLight response = new XFS4IoT.Common.Responses.SetGuidanceLight(setGuidanceLightcommand.Headers.RequestId, new XFS4IoT.Common.Responses.SetGuidanceLightPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetGuidanceLight(IConnection connection, XFS4IoT.Common.Commands.SetGuidanceLight setGuidanceLight, CancellationToken cancel)
        {
            setGuidanceLight.IsNotNull($"Invalid parameter in the ExecuteSetGuidanceLight method. {nameof(setGuidanceLight)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, setGuidanceLight.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.SetGuidanceLight()");
            Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> task = ServiceProvider.Device.SetGuidanceLight(cardReaderConnection, setGuidanceLight.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.SetGuidanceLight() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SetGuidanceLight response = new XFS4IoT.Common.Responses.SetGuidanceLight(setGuidanceLight.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.PowerSaveControl))]
    public class PowerSaveControlHandler : ICommandHandler
    {
        public PowerSaveControlHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(PowerSaveControlHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecutePowerSaveControl(Connection, command as XFS4IoT.Common.Commands.PowerSaveControl, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.PowerSaveControl powerSaveControlcommand = command as XFS4IoT.Common.Commands.PowerSaveControl;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.PowerSaveControl response = new XFS4IoT.Common.Responses.PowerSaveControl(powerSaveControlcommand.Headers.RequestId, new XFS4IoT.Common.Responses.PowerSaveControlPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecutePowerSaveControl(IConnection connection, XFS4IoT.Common.Commands.PowerSaveControl powerSaveControl, CancellationToken cancel)
        {
            powerSaveControl.IsNotNull($"Invalid parameter in the ExecutePowerSaveControl method. {nameof(powerSaveControl)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, powerSaveControl.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.PowerSaveControl()");
            Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> task = ServiceProvider.Device.PowerSaveControl(cardReaderConnection, powerSaveControl.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.PowerSaveControl() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.PowerSaveControl response = new XFS4IoT.Common.Responses.PowerSaveControl(powerSaveControl.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.SynchronizeCommand))]
    public class SynchronizeCommandHandler : ICommandHandler
    {
        public SynchronizeCommandHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSynchronizeCommand(Connection, command as XFS4IoT.Common.Commands.SynchronizeCommand, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.SynchronizeCommand synchronizeCommandcommand = command as XFS4IoT.Common.Commands.SynchronizeCommand;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.SynchronizeCommand response = new XFS4IoT.Common.Responses.SynchronizeCommand(synchronizeCommandcommand.Headers.RequestId, new XFS4IoT.Common.Responses.SynchronizeCommandPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSynchronizeCommand(IConnection connection, XFS4IoT.Common.Commands.SynchronizeCommand synchronizeCommand, CancellationToken cancel)
        {
            synchronizeCommand.IsNotNull($"Invalid parameter in the ExecuteSynchronizeCommand method. {nameof(synchronizeCommand)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, synchronizeCommand.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.SynchronizeCommand()");
            Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> task = ServiceProvider.Device.SynchronizeCommand(cardReaderConnection, synchronizeCommand.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.SynchronizeCommand() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SynchronizeCommand response = new XFS4IoT.Common.Responses.SynchronizeCommand(synchronizeCommand.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.SetTransactionState))]
    public class SetTransactionStateHandler : ICommandHandler
    {
        public SetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SetTransactionStateHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSetTransactionState(Connection, command as XFS4IoT.Common.Commands.SetTransactionState, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.SetTransactionState setTransactionStatecommand = command as XFS4IoT.Common.Commands.SetTransactionState;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.SetTransactionState response = new XFS4IoT.Common.Responses.SetTransactionState(setTransactionStatecommand.Headers.RequestId, new XFS4IoT.Common.Responses.SetTransactionStatePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetTransactionState(IConnection connection, XFS4IoT.Common.Commands.SetTransactionState setTransactionState, CancellationToken cancel)
        {
            setTransactionState.IsNotNull($"Invalid parameter in the ExecuteSetTransactionState method. {nameof(setTransactionState)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, setTransactionState.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.SetTransactionState()");
            Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> task = ServiceProvider.Device.SetTransactionState(cardReaderConnection, setTransactionState.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.SetTransactionState() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SetTransactionState response = new XFS4IoT.Common.Responses.SetTransactionState(setTransactionState.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(CardReaderServiceProvider), typeof(XFS4IoT.Common.Commands.GetTransactionState))]
    public class GetTransactionStateHandler : ICommandHandler
    {
        public GetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as CardReaderServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetTransactionStateHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetTransactionState(Connection, command as XFS4IoT.Common.Commands.GetTransactionState, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Common.Commands.GetTransactionState getTransactionStatecommand = command as XFS4IoT.Common.Commands.GetTransactionState;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;
            else if (commandException.GetType() == typeof(NotImplementedException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.UnsupportedCommand;

            XFS4IoT.Common.Responses.GetTransactionState response = new XFS4IoT.Common.Responses.GetTransactionState(getTransactionStatecommand.Headers.RequestId, new XFS4IoT.Common.Responses.GetTransactionStatePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetTransactionState(IConnection connection, XFS4IoT.Common.Commands.GetTransactionState getTransactionState, CancellationToken cancel)
        {
            getTransactionState.IsNotNull($"Invalid parameter in the ExecuteGetTransactionState method. {nameof(getTransactionState)}");

            ICardReaderConnection cardReaderConnection = new CardReaderConnection(connection, getTransactionState.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.GetTransactionState()");
            Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> task = ServiceProvider.Device.GetTransactionState(cardReaderConnection, getTransactionState.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.GetTransactionState() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.GetTransactionState response = new XFS4IoT.Common.Responses.GetTransactionState(getTransactionState.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public CardReaderServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

}
