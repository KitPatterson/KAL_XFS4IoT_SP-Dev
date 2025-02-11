/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * TestCashUnitsHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.CashDispenser.Commands;
using XFS4IoT.CashDispenser.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CashDispenser
{
    [CommandHandler(XFSConstants.ServiceClass.CashDispenser, typeof(TestCashUnitsCommand))]
    public partial class TestCashUnitsHandler : ICommandHandler
    {
        public TestCashUnitsHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(TestCashUnitsHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(TestCashUnitsHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICashDispenserDevice>();

            CashDispenser = Provider.IsA<ICashDispenserService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(TestCashUnitsHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(TestCashUnitsHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var testCashUnitsCmd = command.IsA<TestCashUnitsCommand>($"Invalid parameter in the TestCashUnits Handle method. {nameof(TestCashUnitsCommand)}");
            testCashUnitsCmd.Header.RequestId.HasValue.IsTrue();

            ITestCashUnitsEvents events = new TestCashUnitsEvents(Connection, testCashUnitsCmd.Header.RequestId.Value);

            var result = await HandleTestCashUnits(events, testCashUnitsCmd, cancel);
            await Connection.SendMessageAsync(new TestCashUnitsCompletion(testCashUnitsCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var testCashUnitscommand = command.IsA<TestCashUnitsCommand>();
            testCashUnitscommand.Header.RequestId.HasValue.IsTrue();

            TestCashUnitsCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => TestCashUnitsCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new TestCashUnitsCompletion(testCashUnitscommand.Header.RequestId.Value, new TestCashUnitsCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICashDispenserDevice Device { get => Provider.Device.IsA<ICashDispenserDevice>(); }
        private IServiceProvider Provider { get; }
        private ICashDispenserService CashDispenser { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
