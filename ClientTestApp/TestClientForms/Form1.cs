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

            XFS4IoT.CardReader.Commands.ReadRawData readRawDataCmd = new XFS4IoT.CardReader.Commands.ReadRawData(
                Guid.NewGuid().ToString(), 
                new XFS4IoT.CardReader.Commands.ReadRawDataPayload(
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
                    if (cmdResponse.GetType() == typeof(XFS4IoT.CardReader.Responses.ReadRawData))
                    {
                        XFS4IoT.CardReader.Responses.ReadRawData response = cmdResponse as XFS4IoT.CardReader.Responses.ReadRawData;
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

            XFS4IoT.CardReader.Commands.EjectCard ejectCmd = new XFS4IoT.CardReader.Commands.EjectCard(
                Guid.NewGuid().ToString(), new XFS4IoT.CardReader.Commands.EjectCardPayload(
                    CommandTimeout,
                    XFS4IoT.CardReader.Commands.EjectCardPayload.EjectPositionEnum.ExitPosition));

            textBoxCommand.Text = ejectCmd.Serialise();

            await cardReader.SendCommandAsync(ejectCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            object cmdResponse = await cardReader.ReceiveMessageAsync();
            if (cmdResponse is not null &&
                cmdResponse.GetType() == typeof(XFS4IoT.CardReader.Responses.EjectCard))
            {
                XFS4IoT.CardReader.Responses.EjectCard response = cmdResponse as XFS4IoT.CardReader.Responses.EjectCard;
                if (response is not null)
                {
                    textBoxResponse.Text = response.Serialise();
                }

                if (response.Payload.CompletionCode == XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success)
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

            XFS4IoT.Common.Commands.Status statusCmd = new XFS4IoT.Common.Commands.Status(Guid.NewGuid().ToString(), new XFS4IoT.Common.Commands.StatusPayload(CommandTimeout));
            textBoxCommand.Text = statusCmd.Serialise();

            await cardReader.SendCommandAsync(statusCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            object cmdResponse = await cardReader.ReceiveMessageAsync();
            if (cmdResponse is not null &&
                cmdResponse.GetType() == typeof(XFS4IoT.Common.Responses.Status))
            {
                XFS4IoT.Common.Responses.Status response = cmdResponse as XFS4IoT.Common.Responses.Status;
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

            XFS4IoT.Common.Commands.Capabilities capabilitiesCmd = new XFS4IoT.Common.Commands.Capabilities(Guid.NewGuid().ToString(), new XFS4IoT.Common.Commands.CapabilitiesPayload(CommandTimeout));
            textBoxCommand.Text = capabilitiesCmd.Serialise();

            await cardReader.SendCommandAsync(capabilitiesCmd);

            textBoxResponse.Text = string.Empty;
            textBoxEvent.Text = string.Empty;

            object cmdResponse = await cardReader.ReceiveMessageAsync();
            if (cmdResponse is not null &&
                cmdResponse.GetType() == typeof(XFS4IoT.Common.Responses.Capabilities))
            {
                XFS4IoT.Common.Responses.Capabilities response = cmdResponse as XFS4IoT.Common.Responses.Capabilities;
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

                            commandString = new XFS4IoT.Common.Commands.GetService(Guid.NewGuid().ToString(), new XFS4IoT.Commands.MessagePayload(CommandTimeout)).Serialise();
                            await Discovery.SendCommandAsync(new XFS4IoT.Common.Commands.GetService(Guid.NewGuid().ToString(), new XFS4IoT.Commands.MessagePayload(CommandTimeout)));
                            
                            object cmdResponse = await Discovery.ReceiveMessageAsync();
                            if (cmdResponse is not null)
                            {
                                XFS4IoT.Common.Responses.GetService response = cmdResponse as XFS4IoT.Common.Responses.GetService;
                                if (response is not null &&
                                    response.GetType() == typeof(XFS4IoT.Common.Responses.GetService))
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
