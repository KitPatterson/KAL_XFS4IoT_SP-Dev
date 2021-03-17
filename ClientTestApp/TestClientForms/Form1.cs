using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.WebSockets;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace TestClientForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxServiceURI.Text = "ws://localhost";
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private async void AcceptCard_Click(object sender, EventArgs e)
        {
            var cardReader = new XFS4IoTClient.ClientConnection(new Uri($"{textBoxCardReader.Text}"));

            try
            {
                await cardReader.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            ReadRawDataCommand readRawDataCmd = new ReadRawDataCommand(
                Guid.NewGuid().ToString(), 
                new ReadRawDataCommand.PayloadData(
                    CommandTimeout,
                    true,
                    true,
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false));

            textBoxCommand.Text = readRawDataCmd.Serialise();

            await cardReader.SendCommandAsync(readRawDataCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            for (; ; )
            {
                object cmdResponse = await cardReader.ReceiveMessageAsync();
                if (cmdResponse is not null)
                {
                    if (cmdResponse.GetType() == typeof(ReadRawDataCompletion))
                    {
                        ReadRawDataCompletion response = cmdResponse as ReadRawDataCompletion;
                        textBoxResponse.Text = response.Serialise();
                        break;
                    }
                    else if (cmdResponse.GetType() == typeof(XFS4IoT.CardReader.Events.MediaInsertedEvent))
                    {
                        XFS4IoT.CardReader.Events.MediaInsertedEvent insertedEv = cmdResponse as XFS4IoT.CardReader.Events.MediaInsertedEvent;
                        textBoxEvent.Text = insertedEv.Serialise();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
   
        private async void EjectCard_Click(object sender, EventArgs e)
        {
            var cardReader = new XFS4IoTClient.ClientConnection(new Uri($"{textBoxCardReader.Text}"));

            try
            {
                await cardReader.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            EjectCardCommand ejectCmd = new EjectCardCommand(
                Guid.NewGuid().ToString(), new EjectCardCommand.PayloadData(
                    CommandTimeout,
                    EjectCardCommand.PayloadData.EjectPositionEnum.ExitPosition));

            textBoxCommand.Text = ejectCmd.Serialise();

            await cardReader.SendCommandAsync(ejectCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            object cmdResponse = await cardReader.ReceiveMessageAsync();
            if (cmdResponse is not null &&
                cmdResponse.GetType() == typeof(EjectCardCompletion))
            {
                EjectCardCompletion response = cmdResponse as EjectCardCompletion;
                if (response is not null)
                {
                    textBoxResponse.Text = response.Serialise();
                }

                if (response.Payload.CompletionCode == EjectCardCompletion.PayloadData.CompletionCodeEnum.Success)
                {
                    object unsolicEvent = await cardReader.ReceiveMessageAsync();
                    if (unsolicEvent.GetType() == typeof(XFS4IoT.CardReader.Events.MediaRemovedEvent))
                    {
                        XFS4IoT.CardReader.Events.MediaRemovedEvent removedEv = unsolicEvent as XFS4IoT.CardReader.Events.MediaRemovedEvent;
                        textBoxEvent.Text = removedEv.Serialise();
                    }
                }
            }
        }

        private async void buttonStatus_Click(object sender, EventArgs e)
        {
            var cardReader = new XFS4IoTClient.ClientConnection(new Uri($"{textBoxCardReader.Text}"));

            try
            {
                await cardReader.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            StatusCommand statusCmd = new StatusCommand(Guid.NewGuid().ToString(), new StatusCommand.PayloadData(CommandTimeout));
            textBoxCommand.Text = statusCmd.Serialise();

            await cardReader.SendCommandAsync(statusCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            object cmdResponse = await cardReader.ReceiveMessageAsync();
            if (cmdResponse is not null &&
                cmdResponse.GetType() == typeof(StatusCompletion))
            {
                StatusCompletion response = cmdResponse as StatusCompletion;
                if (response is not null)
                {
                    textBoxResponse.Text = response.Serialise();
                    textBoxStDevice.Text = response.Payload.Common.Device.ToString();
                    textBoxStMedia.Text = response.Payload.CardReader.Media.ToString();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var cardReader = new XFS4IoTClient.ClientConnection(new Uri($"{textBoxCardReader.Text}"));

            try
            {
                await cardReader.ConnectAsync();
            }
            catch (Exception)
            {
                return;
            }

            CapabilitiesCommand capabilitiesCmd = new CapabilitiesCommand(Guid.NewGuid().ToString(), new CapabilitiesCommand.PayloadData(CommandTimeout));
            textBoxCommand.Text = capabilitiesCmd.Serialise();

            await cardReader.SendCommandAsync(capabilitiesCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            object cmdResponse = await cardReader.ReceiveMessageAsync();
            if (cmdResponse is not null &&
                cmdResponse.GetType() == typeof(CapabilitiesCompletion))
            {
                CapabilitiesCompletion response = cmdResponse as CapabilitiesCompletion;
                if (response is not null)
                {
                    textBoxResponse.Text = response.Serialise();
                    textBoxDeviceType.Text = response.Payload.CardReader.Type.ToString();
                }
            }
        }

        private async void ServiceDiscovery_Click(object sender, EventArgs e)
        {
            int[] PortRanges = new int[]
            {
                80,  // Only for HTTP
                443, // Only for HTTPS
                5846,
                5847,
                5848,
                5849,
                5850,
                5851,
                5852,
                5853,
                5854,
                5855,
                5856
            };

            string commandString = string.Empty;
            string responseString = string.Empty;
            string cardServiceURI = string.Empty;

            textBoxCommand.Text = commandString;
            textBoxResponse.Text = responseString;
            textBoxCardReader.Text = cardServiceURI;
            textBoxEvent.Text = string.Empty;

            ServicePort = null;

            await Task.Run(async () =>
            {
                
                foreach (int port in PortRanges)
                {
                    try
                    {
                        ClientWebSocket socket = new ClientWebSocket();
                        var task = socket.ConnectAsync(new Uri($"{textBoxServiceURI.Text}:{port}/XFS4IoT/v1.0"), CancellationToken.None);
                        Task.WaitAny(new[] { task }, 400);
                        if (socket.State == WebSocketState.Open)
                        {
                            ServicePort = port;
                            try
                            {
                                socket.Dispose();
                            }
                            catch (Exception)
                            { }

                            var Discovery = new XFS4IoTClient.ClientConnection(new Uri($"{textBoxServiceURI.Text}:{ServicePort}/XFS4IoT/v1.0"));

                            try
                            {
                                await Discovery.ConnectAsync();
                            }
                            catch (Exception)
                            {
                                return;
                            }

                            commandString = new GetServiceCommand(Guid.NewGuid().ToString(), new GetServiceCommand.PayloadData(CommandTimeout)).Serialise();
                            await Discovery.SendCommandAsync(new GetServiceCommand(Guid.NewGuid().ToString(), new GetServiceCommand.PayloadData(CommandTimeout)));
                            
                            object cmdResponse = await Discovery.ReceiveMessageAsync();
                            if (cmdResponse is not null)
                            {
                                GetServiceCompletion response = cmdResponse as GetServiceCompletion;
                                if (response is not null &&
                                    response.GetType() == typeof(GetServiceCompletion))
                                {
                                    responseString = response.Serialise();
                                    var service =
                                        (from ep in response.Payload.Services
                                         where ep.ServiceUri.Contains("CardReader")
                                         select ep
                                         ).FirstOrDefault()
                                         ?.ServiceUri;

                                    if (!string.IsNullOrEmpty(service))
                                        cardServiceURI = (string)service;
                                }
                            }
                            return;
                        }
                    }
                    catch (System.Net.HttpListenerException)
                    { }
                }
            });

            if (ServicePort is null)
            {
                textBoxPort.Text = "";
                MessageBox.Show("Failed on finding services.");
            }
            else
                textBoxPort.Text = ServicePort.ToString();
            
            textBoxCommand.Text = commandString;
            textBoxResponse.Text = responseString;
            textBoxCardReader.Text = cardServiceURI;
        }

        int? ServicePort = null;
        readonly int CommandTimeout = 60000;
    }
}
