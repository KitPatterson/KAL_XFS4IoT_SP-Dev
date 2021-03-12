// (C) KAL ATM Software GmbH, 2021

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTPrinter;
using XFS4IoTServer;

namespace Printer
{
    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.GetFormList))]
    public class GetFormListHandler : ICommandHandler
    {
        public GetFormListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetFormListHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetFormList(Connection, command as XFS4IoT.Printer.Commands.GetFormList, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.GetFormList getFormListcommand = command as XFS4IoT.Printer.Commands.GetFormList;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.GetFormList response = new XFS4IoT.Printer.Responses.GetFormList(getFormListcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.GetFormListPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetFormList(IConnection connection, XFS4IoT.Printer.Commands.GetFormList getFormList, CancellationToken cancel)
        {
            getFormList.IsNotNull($"Invalid parameter in the ExecuteGetFormList method. {nameof(getFormList)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getFormList.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetFormList()");
            Task<XFS4IoT.Printer.Responses.GetFormListPayload> task = ServiceProvider.Device.GetFormList(printerConnection, getFormList.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetFormList() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.GetFormList response = new XFS4IoT.Printer.Responses.GetFormList(getFormList.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.GetMediaList))]
    public class GetMediaListHandler : ICommandHandler
    {
        public GetMediaListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetMediaListHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetMediaListHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetMediaList(Connection, command as XFS4IoT.Printer.Commands.GetMediaList, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.GetMediaList getMediaListcommand = command as XFS4IoT.Printer.Commands.GetMediaList;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.GetMediaList response = new XFS4IoT.Printer.Responses.GetMediaList(getMediaListcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.GetMediaListPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetMediaList(IConnection connection, XFS4IoT.Printer.Commands.GetMediaList getMediaList, CancellationToken cancel)
        {
            getMediaList.IsNotNull($"Invalid parameter in the ExecuteGetMediaList method. {nameof(getMediaList)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getMediaList.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetMediaList()");
            Task<XFS4IoT.Printer.Responses.GetMediaListPayload> task = ServiceProvider.Device.GetMediaList(printerConnection, getMediaList.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetMediaList() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.GetMediaList response = new XFS4IoT.Printer.Responses.GetMediaList(getMediaList.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.GetQueryForm))]
    public class GetQueryFormHandler : ICommandHandler
    {
        public GetQueryFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetQueryForm(Connection, command as XFS4IoT.Printer.Commands.GetQueryForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.GetQueryForm getQueryFormcommand = command as XFS4IoT.Printer.Commands.GetQueryForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.GetQueryForm response = new XFS4IoT.Printer.Responses.GetQueryForm(getQueryFormcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.GetQueryFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetQueryForm(IConnection connection, XFS4IoT.Printer.Commands.GetQueryForm getQueryForm, CancellationToken cancel)
        {
            getQueryForm.IsNotNull($"Invalid parameter in the ExecuteGetQueryForm method. {nameof(getQueryForm)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getQueryForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetQueryForm()");
            Task<XFS4IoT.Printer.Responses.GetQueryFormPayload> task = ServiceProvider.Device.GetQueryForm(printerConnection, getQueryForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetQueryForm() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.GetQueryForm response = new XFS4IoT.Printer.Responses.GetQueryForm(getQueryForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.GetQueryMedia))]
    public class GetQueryMediaHandler : ICommandHandler
    {
        public GetQueryMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryMediaHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryMediaHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetQueryMedia(Connection, command as XFS4IoT.Printer.Commands.GetQueryMedia, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.GetQueryMedia getQueryMediacommand = command as XFS4IoT.Printer.Commands.GetQueryMedia;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.GetQueryMedia response = new XFS4IoT.Printer.Responses.GetQueryMedia(getQueryMediacommand.Headers.RequestId, new XFS4IoT.Printer.Responses.GetQueryMediaPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetQueryMedia(IConnection connection, XFS4IoT.Printer.Commands.GetQueryMedia getQueryMedia, CancellationToken cancel)
        {
            getQueryMedia.IsNotNull($"Invalid parameter in the ExecuteGetQueryMedia method. {nameof(getQueryMedia)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getQueryMedia.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetQueryMedia()");
            Task<XFS4IoT.Printer.Responses.GetQueryMediaPayload> task = ServiceProvider.Device.GetQueryMedia(printerConnection, getQueryMedia.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetQueryMedia() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.GetQueryMedia response = new XFS4IoT.Printer.Responses.GetQueryMedia(getQueryMedia.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.GetQueryField))]
    public class GetQueryFieldHandler : ICommandHandler
    {
        public GetQueryFieldHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFieldHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetQueryField(Connection, command as XFS4IoT.Printer.Commands.GetQueryField, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.GetQueryField getQueryFieldcommand = command as XFS4IoT.Printer.Commands.GetQueryField;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.GetQueryField response = new XFS4IoT.Printer.Responses.GetQueryField(getQueryFieldcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.GetQueryFieldPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetQueryField(IConnection connection, XFS4IoT.Printer.Commands.GetQueryField getQueryField, CancellationToken cancel)
        {
            getQueryField.IsNotNull($"Invalid parameter in the ExecuteGetQueryField method. {nameof(getQueryField)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getQueryField.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetQueryField()");
            Task<XFS4IoT.Printer.Responses.GetQueryFieldPayload> task = ServiceProvider.Device.GetQueryField(printerConnection, getQueryField.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetQueryField() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.GetQueryField response = new XFS4IoT.Printer.Responses.GetQueryField(getQueryField.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.GetCodelineMapping))]
    public class GetCodelineMappingHandler : ICommandHandler
    {
        public GetCodelineMappingHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetCodelineMappingHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetCodelineMappingHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetCodelineMapping(Connection, command as XFS4IoT.Printer.Commands.GetCodelineMapping, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.GetCodelineMapping getCodelineMappingcommand = command as XFS4IoT.Printer.Commands.GetCodelineMapping;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.GetCodelineMapping response = new XFS4IoT.Printer.Responses.GetCodelineMapping(getCodelineMappingcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.GetCodelineMappingPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetCodelineMapping(IConnection connection, XFS4IoT.Printer.Commands.GetCodelineMapping getCodelineMapping, CancellationToken cancel)
        {
            getCodelineMapping.IsNotNull($"Invalid parameter in the ExecuteGetCodelineMapping method. {nameof(getCodelineMapping)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getCodelineMapping.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetCodelineMapping()");
            Task<XFS4IoT.Printer.Responses.GetCodelineMappingPayload> task = ServiceProvider.Device.GetCodelineMapping(printerConnection, getCodelineMapping.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetCodelineMapping() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.GetCodelineMapping response = new XFS4IoT.Printer.Responses.GetCodelineMapping(getCodelineMapping.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.ControlMedia))]
    public class ControlMediaHandler : ICommandHandler
    {
        public ControlMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ControlMediaHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ControlMediaHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteControlMedia(Connection, command as XFS4IoT.Printer.Commands.ControlMedia, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.ControlMedia controlMediacommand = command as XFS4IoT.Printer.Commands.ControlMedia;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.ControlMedia response = new XFS4IoT.Printer.Responses.ControlMedia(controlMediacommand.Headers.RequestId, new XFS4IoT.Printer.Responses.ControlMediaPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteControlMedia(IConnection connection, XFS4IoT.Printer.Commands.ControlMedia controlMedia, CancellationToken cancel)
        {
            controlMedia.IsNotNull($"Invalid parameter in the ExecuteControlMedia method. {nameof(controlMedia)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, controlMedia.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.ControlMedia()");
            Task<XFS4IoT.Printer.Responses.ControlMediaPayload> task = ServiceProvider.Device.ControlMedia(printerConnection, controlMedia.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.ControlMedia() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.ControlMedia response = new XFS4IoT.Printer.Responses.ControlMedia(controlMedia.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.PrintForm))]
    public class PrintFormHandler : ICommandHandler
    {
        public PrintFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PrintFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(PrintFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecutePrintForm(Connection, command as XFS4IoT.Printer.Commands.PrintForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.PrintForm printFormcommand = command as XFS4IoT.Printer.Commands.PrintForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.PrintForm response = new XFS4IoT.Printer.Responses.PrintForm(printFormcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.PrintFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecutePrintForm(IConnection connection, XFS4IoT.Printer.Commands.PrintForm printForm, CancellationToken cancel)
        {
            printForm.IsNotNull($"Invalid parameter in the ExecutePrintForm method. {nameof(printForm)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, printForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.PrintForm()");
            Task<XFS4IoT.Printer.Responses.PrintFormPayload> task = ServiceProvider.Device.PrintForm(printerConnection, printForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.PrintForm() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.PrintForm response = new XFS4IoT.Printer.Responses.PrintForm(printForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.ReadForm))]
    public class ReadFormHandler : ICommandHandler
    {
        public ReadFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ReadFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReadForm(Connection, command as XFS4IoT.Printer.Commands.ReadForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.ReadForm readFormcommand = command as XFS4IoT.Printer.Commands.ReadForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.ReadForm response = new XFS4IoT.Printer.Responses.ReadForm(readFormcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.ReadFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReadForm(IConnection connection, XFS4IoT.Printer.Commands.ReadForm readForm, CancellationToken cancel)
        {
            readForm.IsNotNull($"Invalid parameter in the ExecuteReadForm method. {nameof(readForm)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, readForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.ReadForm()");
            Task<XFS4IoT.Printer.Responses.ReadFormPayload> task = ServiceProvider.Device.ReadForm(printerConnection, readForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.ReadForm() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.ReadForm response = new XFS4IoT.Printer.Responses.ReadForm(readForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.RawData))]
    public class RawDataHandler : ICommandHandler
    {
        public RawDataHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RawDataHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(RawDataHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteRawData(Connection, command as XFS4IoT.Printer.Commands.RawData, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.RawData rawDatacommand = command as XFS4IoT.Printer.Commands.RawData;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.RawData response = new XFS4IoT.Printer.Responses.RawData(rawDatacommand.Headers.RequestId, new XFS4IoT.Printer.Responses.RawDataPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteRawData(IConnection connection, XFS4IoT.Printer.Commands.RawData rawData, CancellationToken cancel)
        {
            rawData.IsNotNull($"Invalid parameter in the ExecuteRawData method. {nameof(rawData)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, rawData.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.RawData()");
            Task<XFS4IoT.Printer.Responses.RawDataPayload> task = ServiceProvider.Device.RawData(printerConnection, rawData.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.RawData() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.RawData response = new XFS4IoT.Printer.Responses.RawData(rawData.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.MediaExtents))]
    public class MediaExtentsHandler : ICommandHandler
    {
        public MediaExtentsHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(MediaExtentsHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(MediaExtentsHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteMediaExtents(Connection, command as XFS4IoT.Printer.Commands.MediaExtents, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.MediaExtents mediaExtentscommand = command as XFS4IoT.Printer.Commands.MediaExtents;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.MediaExtents response = new XFS4IoT.Printer.Responses.MediaExtents(mediaExtentscommand.Headers.RequestId, new XFS4IoT.Printer.Responses.MediaExtentsPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteMediaExtents(IConnection connection, XFS4IoT.Printer.Commands.MediaExtents mediaExtents, CancellationToken cancel)
        {
            mediaExtents.IsNotNull($"Invalid parameter in the ExecuteMediaExtents method. {nameof(mediaExtents)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, mediaExtents.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.MediaExtents()");
            Task<XFS4IoT.Printer.Responses.MediaExtentsPayload> task = ServiceProvider.Device.MediaExtents(printerConnection, mediaExtents.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.MediaExtents() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.MediaExtents response = new XFS4IoT.Printer.Responses.MediaExtents(mediaExtents.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.ResetCount))]
    public class ResetCountHandler : ICommandHandler
    {
        public ResetCountHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetCountHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ResetCountHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteResetCount(Connection, command as XFS4IoT.Printer.Commands.ResetCount, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.ResetCount resetCountcommand = command as XFS4IoT.Printer.Commands.ResetCount;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.ResetCount response = new XFS4IoT.Printer.Responses.ResetCount(resetCountcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.ResetCountPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteResetCount(IConnection connection, XFS4IoT.Printer.Commands.ResetCount resetCount, CancellationToken cancel)
        {
            resetCount.IsNotNull($"Invalid parameter in the ExecuteResetCount method. {nameof(resetCount)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, resetCount.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.ResetCount()");
            Task<XFS4IoT.Printer.Responses.ResetCountPayload> task = ServiceProvider.Device.ResetCount(printerConnection, resetCount.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.ResetCount() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.ResetCount response = new XFS4IoT.Printer.Responses.ResetCount(resetCount.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.ReadImage))]
    public class ReadImageHandler : ICommandHandler
    {
        public ReadImageHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadImageHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ReadImageHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReadImage(Connection, command as XFS4IoT.Printer.Commands.ReadImage, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.ReadImage readImagecommand = command as XFS4IoT.Printer.Commands.ReadImage;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.ReadImage response = new XFS4IoT.Printer.Responses.ReadImage(readImagecommand.Headers.RequestId, new XFS4IoT.Printer.Responses.ReadImagePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReadImage(IConnection connection, XFS4IoT.Printer.Commands.ReadImage readImage, CancellationToken cancel)
        {
            readImage.IsNotNull($"Invalid parameter in the ExecuteReadImage method. {nameof(readImage)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, readImage.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.ReadImage()");
            Task<XFS4IoT.Printer.Responses.ReadImagePayload> task = ServiceProvider.Device.ReadImage(printerConnection, readImage.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.ReadImage() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.ReadImage response = new XFS4IoT.Printer.Responses.ReadImage(readImage.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.Reset))]
    public class ResetHandler : ICommandHandler
    {
        public ResetHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReset(Connection, command as XFS4IoT.Printer.Commands.Reset, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.Reset resetcommand = command as XFS4IoT.Printer.Commands.Reset;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.Reset response = new XFS4IoT.Printer.Responses.Reset(resetcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.ResetPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReset(IConnection connection, XFS4IoT.Printer.Commands.Reset reset, CancellationToken cancel)
        {
            reset.IsNotNull($"Invalid parameter in the ExecuteReset method. {nameof(reset)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, reset.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.Reset()");
            Task<XFS4IoT.Printer.Responses.ResetPayload> task = ServiceProvider.Device.Reset(printerConnection, reset.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.Reset() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.Reset response = new XFS4IoT.Printer.Responses.Reset(reset.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.RetractMedia))]
    public class RetractMediaHandler : ICommandHandler
    {
        public RetractMediaHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RetractMediaHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(RetractMediaHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteRetractMedia(Connection, command as XFS4IoT.Printer.Commands.RetractMedia, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.RetractMedia retractMediacommand = command as XFS4IoT.Printer.Commands.RetractMedia;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.RetractMedia response = new XFS4IoT.Printer.Responses.RetractMedia(retractMediacommand.Headers.RequestId, new XFS4IoT.Printer.Responses.RetractMediaPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteRetractMedia(IConnection connection, XFS4IoT.Printer.Commands.RetractMedia retractMedia, CancellationToken cancel)
        {
            retractMedia.IsNotNull($"Invalid parameter in the ExecuteRetractMedia method. {nameof(retractMedia)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, retractMedia.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.RetractMedia()");
            Task<XFS4IoT.Printer.Responses.RetractMediaPayload> task = ServiceProvider.Device.RetractMedia(printerConnection, retractMedia.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.RetractMedia() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.RetractMedia response = new XFS4IoT.Printer.Responses.RetractMedia(retractMedia.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.DispensePaper))]
    public class DispensePaperHandler : ICommandHandler
    {
        public DispensePaperHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DispensePaperHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(DispensePaperHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteDispensePaper(Connection, command as XFS4IoT.Printer.Commands.DispensePaper, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.DispensePaper dispensePapercommand = command as XFS4IoT.Printer.Commands.DispensePaper;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.DispensePaper response = new XFS4IoT.Printer.Responses.DispensePaper(dispensePapercommand.Headers.RequestId, new XFS4IoT.Printer.Responses.DispensePaperPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteDispensePaper(IConnection connection, XFS4IoT.Printer.Commands.DispensePaper dispensePaper, CancellationToken cancel)
        {
            dispensePaper.IsNotNull($"Invalid parameter in the ExecuteDispensePaper method. {nameof(dispensePaper)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, dispensePaper.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.DispensePaper()");
            Task<XFS4IoT.Printer.Responses.DispensePaperPayload> task = ServiceProvider.Device.DispensePaper(printerConnection, dispensePaper.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.DispensePaper() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.DispensePaper response = new XFS4IoT.Printer.Responses.DispensePaper(dispensePaper.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.PrintRawFile))]
    public class PrintRawFileHandler : ICommandHandler
    {
        public PrintRawFileHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PrintRawFileHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(PrintRawFileHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecutePrintRawFile(Connection, command as XFS4IoT.Printer.Commands.PrintRawFile, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.PrintRawFile printRawFilecommand = command as XFS4IoT.Printer.Commands.PrintRawFile;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.PrintRawFile response = new XFS4IoT.Printer.Responses.PrintRawFile(printRawFilecommand.Headers.RequestId, new XFS4IoT.Printer.Responses.PrintRawFilePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecutePrintRawFile(IConnection connection, XFS4IoT.Printer.Commands.PrintRawFile printRawFile, CancellationToken cancel)
        {
            printRawFile.IsNotNull($"Invalid parameter in the ExecutePrintRawFile method. {nameof(printRawFile)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, printRawFile.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.PrintRawFile()");
            Task<XFS4IoT.Printer.Responses.PrintRawFilePayload> task = ServiceProvider.Device.PrintRawFile(printerConnection, printRawFile.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.PrintRawFile() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.PrintRawFile response = new XFS4IoT.Printer.Responses.PrintRawFile(printRawFile.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.LoadDefinition))]
    public class LoadDefinitionHandler : ICommandHandler
    {
        public LoadDefinitionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(LoadDefinitionHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(LoadDefinitionHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteLoadDefinition(Connection, command as XFS4IoT.Printer.Commands.LoadDefinition, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.LoadDefinition loadDefinitioncommand = command as XFS4IoT.Printer.Commands.LoadDefinition;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.LoadDefinition response = new XFS4IoT.Printer.Responses.LoadDefinition(loadDefinitioncommand.Headers.RequestId, new XFS4IoT.Printer.Responses.LoadDefinitionPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteLoadDefinition(IConnection connection, XFS4IoT.Printer.Commands.LoadDefinition loadDefinition, CancellationToken cancel)
        {
            loadDefinition.IsNotNull($"Invalid parameter in the ExecuteLoadDefinition method. {nameof(loadDefinition)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, loadDefinition.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.LoadDefinition()");
            Task<XFS4IoT.Printer.Responses.LoadDefinitionPayload> task = ServiceProvider.Device.LoadDefinition(printerConnection, loadDefinition.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.LoadDefinition() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.LoadDefinition response = new XFS4IoT.Printer.Responses.LoadDefinition(loadDefinition.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.SupplyReplenish))]
    public class SupplyReplenishHandler : ICommandHandler
    {
        public SupplyReplenishHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SupplyReplenishHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SupplyReplenishHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSupplyReplenish(Connection, command as XFS4IoT.Printer.Commands.SupplyReplenish, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.SupplyReplenish supplyReplenishcommand = command as XFS4IoT.Printer.Commands.SupplyReplenish;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.SupplyReplenish response = new XFS4IoT.Printer.Responses.SupplyReplenish(supplyReplenishcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.SupplyReplenishPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSupplyReplenish(IConnection connection, XFS4IoT.Printer.Commands.SupplyReplenish supplyReplenish, CancellationToken cancel)
        {
            supplyReplenish.IsNotNull($"Invalid parameter in the ExecuteSupplyReplenish method. {nameof(supplyReplenish)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, supplyReplenish.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.SupplyReplenish()");
            Task<XFS4IoT.Printer.Responses.SupplyReplenishPayload> task = ServiceProvider.Device.SupplyReplenish(printerConnection, supplyReplenish.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.SupplyReplenish() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.SupplyReplenish response = new XFS4IoT.Printer.Responses.SupplyReplenish(supplyReplenish.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.ControlPassbook))]
    public class ControlPassbookHandler : ICommandHandler
    {
        public ControlPassbookHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ControlPassbookHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ControlPassbookHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteControlPassbook(Connection, command as XFS4IoT.Printer.Commands.ControlPassbook, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.ControlPassbook controlPassbookcommand = command as XFS4IoT.Printer.Commands.ControlPassbook;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.ControlPassbook response = new XFS4IoT.Printer.Responses.ControlPassbook(controlPassbookcommand.Headers.RequestId, new XFS4IoT.Printer.Responses.ControlPassbookPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteControlPassbook(IConnection connection, XFS4IoT.Printer.Commands.ControlPassbook controlPassbook, CancellationToken cancel)
        {
            controlPassbook.IsNotNull($"Invalid parameter in the ExecuteControlPassbook method. {nameof(controlPassbook)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, controlPassbook.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.ControlPassbook()");
            Task<XFS4IoT.Printer.Responses.ControlPassbookPayload> task = ServiceProvider.Device.ControlPassbook(printerConnection, controlPassbook.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.ControlPassbook() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.ControlPassbook response = new XFS4IoT.Printer.Responses.ControlPassbook(controlPassbook.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Printer.Commands.SetBlackMarkMode))]
    public class SetBlackMarkModeHandler : ICommandHandler
    {
        public SetBlackMarkModeHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetBlackMarkModeHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SetBlackMarkModeHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSetBlackMarkMode(Connection, command as XFS4IoT.Printer.Commands.SetBlackMarkMode, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.Printer.Commands.SetBlackMarkMode setBlackMarkModecommand = command as XFS4IoT.Printer.Commands.SetBlackMarkMode;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.Printer.Responses.SetBlackMarkMode response = new XFS4IoT.Printer.Responses.SetBlackMarkMode(setBlackMarkModecommand.Headers.RequestId, new XFS4IoT.Printer.Responses.SetBlackMarkModePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetBlackMarkMode(IConnection connection, XFS4IoT.Printer.Commands.SetBlackMarkMode setBlackMarkMode, CancellationToken cancel)
        {
            setBlackMarkMode.IsNotNull($"Invalid parameter in the ExecuteSetBlackMarkMode method. {nameof(setBlackMarkMode)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, setBlackMarkMode.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.SetBlackMarkMode()");
            Task<XFS4IoT.Printer.Responses.SetBlackMarkModePayload> task = ServiceProvider.Device.SetBlackMarkMode(printerConnection, setBlackMarkMode.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.SetBlackMarkMode() -> {task.Result.CompletionCode}");

            XFS4IoT.Printer.Responses.SetBlackMarkMode response = new XFS4IoT.Printer.Responses.SetBlackMarkMode(setBlackMarkMode.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.Status))]
    public class StatusHandler : ICommandHandler
    {
        public StatusHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.Status response = new XFS4IoT.Common.Responses.Status(statuscommand.Headers.RequestId, new XFS4IoT.Common.Responses.StatusPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteStatus(IConnection connection, XFS4IoT.Common.Commands.Status status, CancellationToken cancel)
        {
            status.IsNotNull($"Invalid parameter in the ExecuteStatus method. {nameof(status)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, status.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.Status()");
            Task<XFS4IoT.Common.Responses.StatusPayload> task = ServiceProvider.Device.Status(printerConnection, status.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.Status() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.Status response = new XFS4IoT.Common.Responses.Status(status.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.Capabilities))]
    public class CapabilitiesHandler : ICommandHandler
    {
        public CapabilitiesHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.Capabilities response = new XFS4IoT.Common.Responses.Capabilities(capabilitiescommand.Headers.RequestId, new XFS4IoT.Common.Responses.CapabilitiesPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteCapabilities(IConnection connection, XFS4IoT.Common.Commands.Capabilities capabilities, CancellationToken cancel)
        {
            capabilities.IsNotNull($"Invalid parameter in the ExecuteCapabilities method. {nameof(capabilities)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, capabilities.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.Capabilities()");
            Task<XFS4IoT.Common.Responses.CapabilitiesPayload> task = ServiceProvider.Device.Capabilities(printerConnection, capabilities.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.Capabilities() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.Capabilities response = new XFS4IoT.Common.Responses.Capabilities(capabilities.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.SetGuidanceLight))]
    public class SetGuidanceLightHandler : ICommandHandler
    {
        public SetGuidanceLightHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.SetGuidanceLight response = new XFS4IoT.Common.Responses.SetGuidanceLight(setGuidanceLightcommand.Headers.RequestId, new XFS4IoT.Common.Responses.SetGuidanceLightPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetGuidanceLight(IConnection connection, XFS4IoT.Common.Commands.SetGuidanceLight setGuidanceLight, CancellationToken cancel)
        {
            setGuidanceLight.IsNotNull($"Invalid parameter in the ExecuteSetGuidanceLight method. {nameof(setGuidanceLight)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, setGuidanceLight.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.SetGuidanceLight()");
            Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> task = ServiceProvider.Device.SetGuidanceLight(printerConnection, setGuidanceLight.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.SetGuidanceLight() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SetGuidanceLight response = new XFS4IoT.Common.Responses.SetGuidanceLight(setGuidanceLight.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.PowerSaveControl))]
    public class PowerSaveControlHandler : ICommandHandler
    {
        public PowerSaveControlHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.PowerSaveControl response = new XFS4IoT.Common.Responses.PowerSaveControl(powerSaveControlcommand.Headers.RequestId, new XFS4IoT.Common.Responses.PowerSaveControlPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecutePowerSaveControl(IConnection connection, XFS4IoT.Common.Commands.PowerSaveControl powerSaveControl, CancellationToken cancel)
        {
            powerSaveControl.IsNotNull($"Invalid parameter in the ExecutePowerSaveControl method. {nameof(powerSaveControl)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, powerSaveControl.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.PowerSaveControl()");
            Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> task = ServiceProvider.Device.PowerSaveControl(printerConnection, powerSaveControl.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.PowerSaveControl() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.PowerSaveControl response = new XFS4IoT.Common.Responses.PowerSaveControl(powerSaveControl.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.SynchronizeCommand))]
    public class SynchronizeCommandHandler : ICommandHandler
    {
        public SynchronizeCommandHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.SynchronizeCommand response = new XFS4IoT.Common.Responses.SynchronizeCommand(synchronizeCommandcommand.Headers.RequestId, new XFS4IoT.Common.Responses.SynchronizeCommandPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSynchronizeCommand(IConnection connection, XFS4IoT.Common.Commands.SynchronizeCommand synchronizeCommand, CancellationToken cancel)
        {
            synchronizeCommand.IsNotNull($"Invalid parameter in the ExecuteSynchronizeCommand method. {nameof(synchronizeCommand)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, synchronizeCommand.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.SynchronizeCommand()");
            Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> task = ServiceProvider.Device.SynchronizeCommand(printerConnection, synchronizeCommand.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.SynchronizeCommand() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SynchronizeCommand response = new XFS4IoT.Common.Responses.SynchronizeCommand(synchronizeCommand.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.SetTransactionState))]
    public class SetTransactionStateHandler : ICommandHandler
    {
        public SetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.SetTransactionState response = new XFS4IoT.Common.Responses.SetTransactionState(setTransactionStatecommand.Headers.RequestId, new XFS4IoT.Common.Responses.SetTransactionStatePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetTransactionState(IConnection connection, XFS4IoT.Common.Commands.SetTransactionState setTransactionState, CancellationToken cancel)
        {
            setTransactionState.IsNotNull($"Invalid parameter in the ExecuteSetTransactionState method. {nameof(setTransactionState)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, setTransactionState.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.SetTransactionState()");
            Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> task = ServiceProvider.Device.SetTransactionState(printerConnection, setTransactionState.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.SetTransactionState() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SetTransactionState response = new XFS4IoT.Common.Responses.SetTransactionState(setTransactionState.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(PrinterServiceProvider), typeof(XFS4IoT.Common.Commands.GetTransactionState))]
    public class GetTransactionStateHandler : ICommandHandler
    {
        public GetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as PrinterServiceProvider;

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

            XFS4IoT.Common.Responses.GetTransactionState response = new XFS4IoT.Common.Responses.GetTransactionState(getTransactionStatecommand.Headers.RequestId, new XFS4IoT.Common.Responses.GetTransactionStatePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetTransactionState(IConnection connection, XFS4IoT.Common.Commands.GetTransactionState getTransactionState, CancellationToken cancel)
        {
            getTransactionState.IsNotNull($"Invalid parameter in the ExecuteGetTransactionState method. {nameof(getTransactionState)}");

            IPrinterConnection printerConnection = new PrinterConnection(connection, getTransactionState.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "PrinterDev.GetTransactionState()");
            Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> task = ServiceProvider.Device.GetTransactionState(printerConnection, getTransactionState.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"PrinterDev.GetTransactionState() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.GetTransactionState response = new XFS4IoT.Common.Responses.GetTransactionState(getTransactionState.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public PrinterServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

}
