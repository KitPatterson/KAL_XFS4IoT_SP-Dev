// (C) KAL ATM Software GmbH, 2021

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTTextTerminal;
using XFS4IoTServer;

namespace TextTerminal
{
    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.GetFormList))]
    public class GetFormListHandler : ICommandHandler
    {
        public GetFormListHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetFormListHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetFormListHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetFormList(Connection, command as XFS4IoT.TextTerminal.Commands.GetFormList, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.GetFormList getFormListcommand = command as XFS4IoT.TextTerminal.Commands.GetFormList;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.GetFormList response = new XFS4IoT.TextTerminal.Responses.GetFormList(getFormListcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.GetFormListPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetFormList(IConnection connection, XFS4IoT.TextTerminal.Commands.GetFormList getFormList, CancellationToken cancel)
        {
            getFormList.IsNotNull($"Invalid parameter in the ExecuteGetFormList method. {nameof(getFormList)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, getFormList.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.GetFormList()");
            Task<XFS4IoT.TextTerminal.Responses.GetFormListPayload> task = ServiceProvider.Device.GetFormList(textTerminalConnection, getFormList.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.GetFormList() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.GetFormList response = new XFS4IoT.TextTerminal.Responses.GetFormList(getFormList.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.GetQueryForm))]
    public class GetQueryFormHandler : ICommandHandler
    {
        public GetQueryFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetQueryForm(Connection, command as XFS4IoT.TextTerminal.Commands.GetQueryForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.GetQueryForm getQueryFormcommand = command as XFS4IoT.TextTerminal.Commands.GetQueryForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.GetQueryForm response = new XFS4IoT.TextTerminal.Responses.GetQueryForm(getQueryFormcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.GetQueryFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetQueryForm(IConnection connection, XFS4IoT.TextTerminal.Commands.GetQueryForm getQueryForm, CancellationToken cancel)
        {
            getQueryForm.IsNotNull($"Invalid parameter in the ExecuteGetQueryForm method. {nameof(getQueryForm)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, getQueryForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.GetQueryForm()");
            Task<XFS4IoT.TextTerminal.Responses.GetQueryFormPayload> task = ServiceProvider.Device.GetQueryForm(textTerminalConnection, getQueryForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.GetQueryForm() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.GetQueryForm response = new XFS4IoT.TextTerminal.Responses.GetQueryForm(getQueryForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.GetQueryField))]
    public class GetQueryFieldHandler : ICommandHandler
    {
        public GetQueryFieldHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetQueryFieldHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetQueryFieldHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetQueryField(Connection, command as XFS4IoT.TextTerminal.Commands.GetQueryField, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.GetQueryField getQueryFieldcommand = command as XFS4IoT.TextTerminal.Commands.GetQueryField;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.GetQueryField response = new XFS4IoT.TextTerminal.Responses.GetQueryField(getQueryFieldcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.GetQueryFieldPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetQueryField(IConnection connection, XFS4IoT.TextTerminal.Commands.GetQueryField getQueryField, CancellationToken cancel)
        {
            getQueryField.IsNotNull($"Invalid parameter in the ExecuteGetQueryField method. {nameof(getQueryField)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, getQueryField.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.GetQueryField()");
            Task<XFS4IoT.TextTerminal.Responses.GetQueryFieldPayload> task = ServiceProvider.Device.GetQueryField(textTerminalConnection, getQueryField.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.GetQueryField() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.GetQueryField response = new XFS4IoT.TextTerminal.Responses.GetQueryField(getQueryField.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.GetKeyDetail))]
    public class GetKeyDetailHandler : ICommandHandler
    {
        public GetKeyDetailHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetKeyDetailHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(GetKeyDetailHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteGetKeyDetail(Connection, command as XFS4IoT.TextTerminal.Commands.GetKeyDetail, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.GetKeyDetail getKeyDetailcommand = command as XFS4IoT.TextTerminal.Commands.GetKeyDetail;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.GetKeyDetail response = new XFS4IoT.TextTerminal.Responses.GetKeyDetail(getKeyDetailcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.GetKeyDetailPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteGetKeyDetail(IConnection connection, XFS4IoT.TextTerminal.Commands.GetKeyDetail getKeyDetail, CancellationToken cancel)
        {
            getKeyDetail.IsNotNull($"Invalid parameter in the ExecuteGetKeyDetail method. {nameof(getKeyDetail)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, getKeyDetail.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.GetKeyDetail()");
            Task<XFS4IoT.TextTerminal.Responses.GetKeyDetailPayload> task = ServiceProvider.Device.GetKeyDetail(textTerminalConnection, getKeyDetail.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.GetKeyDetail() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.GetKeyDetail response = new XFS4IoT.TextTerminal.Responses.GetKeyDetail(getKeyDetail.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.Beep))]
    public class BeepHandler : ICommandHandler
    {
        public BeepHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(BeepHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(BeepHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteBeep(Connection, command as XFS4IoT.TextTerminal.Commands.Beep, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.Beep beepcommand = command as XFS4IoT.TextTerminal.Commands.Beep;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.Beep response = new XFS4IoT.TextTerminal.Responses.Beep(beepcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.BeepPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteBeep(IConnection connection, XFS4IoT.TextTerminal.Commands.Beep beep, CancellationToken cancel)
        {
            beep.IsNotNull($"Invalid parameter in the ExecuteBeep method. {nameof(beep)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, beep.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.Beep()");
            Task<XFS4IoT.TextTerminal.Responses.BeepPayload> task = ServiceProvider.Device.Beep(textTerminalConnection, beep.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.Beep() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.Beep response = new XFS4IoT.TextTerminal.Responses.Beep(beep.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.ClearScreen))]
    public class ClearScreenHandler : ICommandHandler
    {
        public ClearScreenHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ClearScreenHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ClearScreenHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteClearScreen(Connection, command as XFS4IoT.TextTerminal.Commands.ClearScreen, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.ClearScreen clearScreencommand = command as XFS4IoT.TextTerminal.Commands.ClearScreen;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.ClearScreen response = new XFS4IoT.TextTerminal.Responses.ClearScreen(clearScreencommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.ClearScreenPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteClearScreen(IConnection connection, XFS4IoT.TextTerminal.Commands.ClearScreen clearScreen, CancellationToken cancel)
        {
            clearScreen.IsNotNull($"Invalid parameter in the ExecuteClearScreen method. {nameof(clearScreen)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, clearScreen.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.ClearScreen()");
            Task<XFS4IoT.TextTerminal.Responses.ClearScreenPayload> task = ServiceProvider.Device.ClearScreen(textTerminalConnection, clearScreen.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.ClearScreen() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.ClearScreen response = new XFS4IoT.TextTerminal.Responses.ClearScreen(clearScreen.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.DispLight))]
    public class DispLightHandler : ICommandHandler
    {
        public DispLightHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DispLightHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(DispLightHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteDispLight(Connection, command as XFS4IoT.TextTerminal.Commands.DispLight, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.DispLight dispLightcommand = command as XFS4IoT.TextTerminal.Commands.DispLight;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.DispLight response = new XFS4IoT.TextTerminal.Responses.DispLight(dispLightcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.DispLightPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteDispLight(IConnection connection, XFS4IoT.TextTerminal.Commands.DispLight dispLight, CancellationToken cancel)
        {
            dispLight.IsNotNull($"Invalid parameter in the ExecuteDispLight method. {nameof(dispLight)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, dispLight.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.DispLight()");
            Task<XFS4IoT.TextTerminal.Responses.DispLightPayload> task = ServiceProvider.Device.DispLight(textTerminalConnection, dispLight.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.DispLight() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.DispLight response = new XFS4IoT.TextTerminal.Responses.DispLight(dispLight.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.SetLed))]
    public class SetLedHandler : ICommandHandler
    {
        public SetLedHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetLedHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SetLedHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSetLed(Connection, command as XFS4IoT.TextTerminal.Commands.SetLed, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.SetLed setLedcommand = command as XFS4IoT.TextTerminal.Commands.SetLed;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.SetLed response = new XFS4IoT.TextTerminal.Responses.SetLed(setLedcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.SetLedPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetLed(IConnection connection, XFS4IoT.TextTerminal.Commands.SetLed setLed, CancellationToken cancel)
        {
            setLed.IsNotNull($"Invalid parameter in the ExecuteSetLed method. {nameof(setLed)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, setLed.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.SetLed()");
            Task<XFS4IoT.TextTerminal.Responses.SetLedPayload> task = ServiceProvider.Device.SetLed(textTerminalConnection, setLed.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.SetLed() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.SetLed response = new XFS4IoT.TextTerminal.Responses.SetLed(setLed.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.SetResolution))]
    public class SetResolutionHandler : ICommandHandler
    {
        public SetResolutionHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetResolutionHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(SetResolutionHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteSetResolution(Connection, command as XFS4IoT.TextTerminal.Commands.SetResolution, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.SetResolution setResolutioncommand = command as XFS4IoT.TextTerminal.Commands.SetResolution;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.SetResolution response = new XFS4IoT.TextTerminal.Responses.SetResolution(setResolutioncommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.SetResolutionPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteSetResolution(IConnection connection, XFS4IoT.TextTerminal.Commands.SetResolution setResolution, CancellationToken cancel)
        {
            setResolution.IsNotNull($"Invalid parameter in the ExecuteSetResolution method. {nameof(setResolution)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, setResolution.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.SetResolution()");
            Task<XFS4IoT.TextTerminal.Responses.SetResolutionPayload> task = ServiceProvider.Device.SetResolution(textTerminalConnection, setResolution.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.SetResolution() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.SetResolution response = new XFS4IoT.TextTerminal.Responses.SetResolution(setResolution.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.WriteForm))]
    public class WriteFormHandler : ICommandHandler
    {
        public WriteFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(WriteFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteWriteForm(Connection, command as XFS4IoT.TextTerminal.Commands.WriteForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.WriteForm writeFormcommand = command as XFS4IoT.TextTerminal.Commands.WriteForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.WriteForm response = new XFS4IoT.TextTerminal.Responses.WriteForm(writeFormcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.WriteFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteWriteForm(IConnection connection, XFS4IoT.TextTerminal.Commands.WriteForm writeForm, CancellationToken cancel)
        {
            writeForm.IsNotNull($"Invalid parameter in the ExecuteWriteForm method. {nameof(writeForm)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, writeForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.WriteForm()");
            Task<XFS4IoT.TextTerminal.Responses.WriteFormPayload> task = ServiceProvider.Device.WriteForm(textTerminalConnection, writeForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.WriteForm() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.WriteForm response = new XFS4IoT.TextTerminal.Responses.WriteForm(writeForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.ReadForm))]
    public class ReadFormHandler : ICommandHandler
    {
        public ReadFormHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadFormHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ReadFormHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReadForm(Connection, command as XFS4IoT.TextTerminal.Commands.ReadForm, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.ReadForm readFormcommand = command as XFS4IoT.TextTerminal.Commands.ReadForm;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.ReadForm response = new XFS4IoT.TextTerminal.Responses.ReadForm(readFormcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.ReadFormPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReadForm(IConnection connection, XFS4IoT.TextTerminal.Commands.ReadForm readForm, CancellationToken cancel)
        {
            readForm.IsNotNull($"Invalid parameter in the ExecuteReadForm method. {nameof(readForm)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, readForm.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.ReadForm()");
            Task<XFS4IoT.TextTerminal.Responses.ReadFormPayload> task = ServiceProvider.Device.ReadForm(textTerminalConnection, readForm.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.ReadForm() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.ReadForm response = new XFS4IoT.TextTerminal.Responses.ReadForm(readForm.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.Write))]
    public class WriteHandler : ICommandHandler
    {
        public WriteHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(WriteHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(WriteHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteWrite(Connection, command as XFS4IoT.TextTerminal.Commands.Write, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.Write writecommand = command as XFS4IoT.TextTerminal.Commands.Write;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.Write response = new XFS4IoT.TextTerminal.Responses.Write(writecommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.WritePayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteWrite(IConnection connection, XFS4IoT.TextTerminal.Commands.Write write, CancellationToken cancel)
        {
            write.IsNotNull($"Invalid parameter in the ExecuteWrite method. {nameof(write)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, write.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.Write()");
            Task<XFS4IoT.TextTerminal.Responses.WritePayload> task = ServiceProvider.Device.Write(textTerminalConnection, write.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.Write() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.Write response = new XFS4IoT.TextTerminal.Responses.Write(write.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.Read))]
    public class ReadHandler : ICommandHandler
    {
        public ReadHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ReadHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteRead(Connection, command as XFS4IoT.TextTerminal.Commands.Read, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.Read readcommand = command as XFS4IoT.TextTerminal.Commands.Read;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.Read response = new XFS4IoT.TextTerminal.Responses.Read(readcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.ReadPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteRead(IConnection connection, XFS4IoT.TextTerminal.Commands.Read read, CancellationToken cancel)
        {
            read.IsNotNull($"Invalid parameter in the ExecuteRead method. {nameof(read)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, read.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.Read()");
            Task<XFS4IoT.TextTerminal.Responses.ReadPayload> task = ServiceProvider.Device.Read(textTerminalConnection, read.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.Read() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.Read response = new XFS4IoT.TextTerminal.Responses.Read(read.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.Reset))]
    public class ResetHandler : ICommandHandler
    {
        public ResetHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteReset(Connection, command as XFS4IoT.TextTerminal.Commands.Reset, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.Reset resetcommand = command as XFS4IoT.TextTerminal.Commands.Reset;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.Reset response = new XFS4IoT.TextTerminal.Responses.Reset(resetcommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.ResetPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteReset(IConnection connection, XFS4IoT.TextTerminal.Commands.Reset reset, CancellationToken cancel)
        {
            reset.IsNotNull($"Invalid parameter in the ExecuteReset method. {nameof(reset)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, reset.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.Reset()");
            Task<XFS4IoT.TextTerminal.Responses.ResetPayload> task = ServiceProvider.Device.Reset(textTerminalConnection, reset.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.Reset() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.Reset response = new XFS4IoT.TextTerminal.Responses.Reset(reset.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.TextTerminal.Commands.DefineKeys))]
    public class DefineKeysHandler : ICommandHandler
    {
        public DefineKeysHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(DefineKeysHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

            logger.IsNotNull($"Invalid parameter in the {nameof(DefineKeysHandler)} constructor. {nameof(logger)}");
            this.Logger = logger;
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel) => await ExecuteDefineKeys(Connection, command as XFS4IoT.TextTerminal.Commands.DefineKeys, cancel);

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            XFS4IoT.TextTerminal.Commands.DefineKeys defineKeyscommand = command as XFS4IoT.TextTerminal.Commands.DefineKeys;

            XFS4IoT.Responses.MessagePayload.CompletionCodeEnum errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InternalError;
            if (commandException.GetType() == typeof(InvalidDataException))
                errorCode = XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.InvalidData;

            XFS4IoT.TextTerminal.Responses.DefineKeys response = new XFS4IoT.TextTerminal.Responses.DefineKeys(defineKeyscommand.Headers.RequestId, new XFS4IoT.TextTerminal.Responses.DefineKeysPayload(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private async Task ExecuteDefineKeys(IConnection connection, XFS4IoT.TextTerminal.Commands.DefineKeys defineKeys, CancellationToken cancel)
        {
            defineKeys.IsNotNull($"Invalid parameter in the ExecuteDefineKeys method. {nameof(defineKeys)}");

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, defineKeys.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.DefineKeys()");
            Task<XFS4IoT.TextTerminal.Responses.DefineKeysPayload> task = ServiceProvider.Device.DefineKeys(textTerminalConnection, defineKeys.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.DefineKeys() -> {task.Result.CompletionCode}");

            XFS4IoT.TextTerminal.Responses.DefineKeys response = new XFS4IoT.TextTerminal.Responses.DefineKeys(defineKeys.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.Status))]
    public class StatusHandler : ICommandHandler
    {
        public StatusHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(StatusHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, status.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.Status()");
            Task<XFS4IoT.Common.Responses.StatusPayload> task = ServiceProvider.Device.Status(textTerminalConnection, status.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.Status() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.Status response = new XFS4IoT.Common.Responses.Status(status.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.Capabilities))]
    public class CapabilitiesHandler : ICommandHandler
    {
        public CapabilitiesHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CapabilitiesHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, capabilities.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.Capabilities()");
            Task<XFS4IoT.Common.Responses.CapabilitiesPayload> task = ServiceProvider.Device.Capabilities(textTerminalConnection, capabilities.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.Capabilities() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.Capabilities response = new XFS4IoT.Common.Responses.Capabilities(capabilities.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.SetGuidanceLight))]
    public class SetGuidanceLightHandler : ICommandHandler
    {
        public SetGuidanceLightHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetGuidanceLightHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, setGuidanceLight.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.SetGuidanceLight()");
            Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> task = ServiceProvider.Device.SetGuidanceLight(textTerminalConnection, setGuidanceLight.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.SetGuidanceLight() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SetGuidanceLight response = new XFS4IoT.Common.Responses.SetGuidanceLight(setGuidanceLight.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.PowerSaveControl))]
    public class PowerSaveControlHandler : ICommandHandler
    {
        public PowerSaveControlHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(PowerSaveControlHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, powerSaveControl.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.PowerSaveControl()");
            Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> task = ServiceProvider.Device.PowerSaveControl(textTerminalConnection, powerSaveControl.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.PowerSaveControl() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.PowerSaveControl response = new XFS4IoT.Common.Responses.PowerSaveControl(powerSaveControl.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.SynchronizeCommand))]
    public class SynchronizeCommandHandler : ICommandHandler
    {
        public SynchronizeCommandHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SynchronizeCommandHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, synchronizeCommand.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.SynchronizeCommand()");
            Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> task = ServiceProvider.Device.SynchronizeCommand(textTerminalConnection, synchronizeCommand.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.SynchronizeCommand() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SynchronizeCommand response = new XFS4IoT.Common.Responses.SynchronizeCommand(synchronizeCommand.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.SetTransactionState))]
    public class SetTransactionStateHandler : ICommandHandler
    {
        public SetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(SetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, setTransactionState.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.SetTransactionState()");
            Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> task = ServiceProvider.Device.SetTransactionState(textTerminalConnection, setTransactionState.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.SetTransactionState() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.SetTransactionState response = new XFS4IoT.Common.Responses.SetTransactionState(setTransactionState.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

    [CommandHandler(typeof(TextTerminalServiceProvider), typeof(XFS4IoT.Common.Commands.GetTransactionState))]
    public class GetTransactionStateHandler : ICommandHandler
    {
        public GetTransactionStateHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(GetTransactionStateHandler)} constructor. {nameof(Dispatcher)}");
            this.ServiceProvider = Dispatcher as TextTerminalServiceProvider;

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

            ITextTerminalConnection textTerminalConnection = new TextTerminalConnection(connection, getTransactionState.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "TextTerminalDev.GetTransactionState()");
            Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> task = ServiceProvider.Device.GetTransactionState(textTerminalConnection, getTransactionState.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"TextTerminalDev.GetTransactionState() -> {task.Result.CompletionCode}");

            XFS4IoT.Common.Responses.GetTransactionState response = new XFS4IoT.Common.Responses.GetTransactionState(getTransactionState.Headers.RequestId, task.Result);

            await connection.SendMessageAsync(response);
        }

        public TextTerminalServiceProvider ServiceProvider { get; }
        private ILogger Logger { get; }
    }

}
