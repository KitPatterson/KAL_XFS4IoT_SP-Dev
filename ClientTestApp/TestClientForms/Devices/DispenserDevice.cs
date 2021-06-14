using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XFS4IoT.CashManagement.Commands;
using XFS4IoT.CashManagement.Completions;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;

namespace TestClientForms.Devices
{
    public class DispenserDevice : CommonDevice
    {
        public DispenserDevice(string serviceName, TextBox cmdBox, TextBox rspBox, TextBox evtBox, TextBox uriBox, TextBox portBox, TextBox serviceUriBox) 
            : base(serviceName, cmdBox, rspBox, evtBox, uriBox, portBox, serviceUriBox)
        {
        }

        public async Task GetCashUnitInfo()
        {
            var dispenser = new XFS4IoTClient.ClientConnection(new Uri($"{ServiceUriBox.Text}"));

            try
            {
                await dispenser.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            var getCashUnitInfoCmd = new GetCashUnitInfoCommand(RequestId++, new(CommandTimeout));

            CmdBox.Text = getCashUnitInfoCmd.Serialise();

            await dispenser.SendCommandAsync(getCashUnitInfoCmd);

            RspBox.Text = string.Empty;
            EvtBox.Text = string.Empty;

            object cmdResponse = await dispenser.ReceiveMessageAsync();
            if (cmdResponse is GetCashUnitInfoCompletion response)
            {
                RspBox.Text = response.Serialise();
            }
        }

        public async Task GetMixTypes()
        {
            var dispenser = new XFS4IoTClient.ClientConnection(new Uri($"{ServiceUriBox.Text}"));

            try
            {
                await dispenser.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            var getMixTypesCmd = new GetMixTypesCommand(RequestId++, new(CommandTimeout));

            CmdBox.Text = getMixTypesCmd.Serialise();

            await dispenser.SendCommandAsync(getMixTypesCmd);

            RspBox.Text = string.Empty;
            EvtBox.Text = string.Empty;

            object cmdResponse = await dispenser.ReceiveMessageAsync();
            if (cmdResponse is GetMixTypesCompletion response)
            {
                RspBox.Text = response.Serialise();
            }
        }

        public async Task GetPresentStatus()
        {
            var dispenser = new XFS4IoTClient.ClientConnection(new Uri($"{ServiceUriBox.Text}"));

            try
            {
                await dispenser.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            var getPresentStatusCmd = new GetPresentStatusCommand(RequestId++, new(CommandTimeout, GetPresentStatusCommand.PayloadData.PositionEnum.Default));

            CmdBox.Text = getPresentStatusCmd.Serialise();

            await dispenser.SendCommandAsync(getPresentStatusCmd);

            RspBox.Text = string.Empty;
            EvtBox.Text = string.Empty;

            object cmdResponse = await dispenser.ReceiveMessageAsync();
            if (cmdResponse is GetPresentStatusCompletion response)
            {
                RspBox.Text = response.Serialise();
            }
        }

        public async Task Denominate()
        {
            var dispenser = new XFS4IoTClient.ClientConnection(new Uri($"{ServiceUriBox.Text}"));

            try
            {
                await dispenser.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            var getPresentStatusCmd = new DenominateCommand(RequestId++, new(CommandTimeout));

            CmdBox.Text = getPresentStatusCmd.Serialise();

            await dispenser.SendCommandAsync(getPresentStatusCmd);

            RspBox.Text = string.Empty;
            EvtBox.Text = string.Empty;

            object cmdResponse = await dispenser.ReceiveMessageAsync();
            if (cmdResponse is GetPresentStatusCompletion response)
            {
                RspBox.Text = response.Serialise();
            }
        }

    }
}
