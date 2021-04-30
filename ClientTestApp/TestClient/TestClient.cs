/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using XFS4IoT;
using System.Linq;
using System.Threading;
using XFS4IoT.CardReader;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;
using XFS4IoT.CardReader.Events;
using System.Runtime.InteropServices;

namespace TestClient
{
    class TestClient
    {
        async static Task Main(/*string[] args*/) => await new TestClient().Run();

        private async Task Run()
        {
            try
            {
                Logger.LogLine("Running test XFS4IoT application.");

                Logger.LogLine("Doing service discovery.");
                await DoServiceDiscovery();

                Logger.LogLine("Connecting to the card reader");
                await OpenCardReader();

                Logger.LogLine("Get card reader status");
                await GetCardReaderStatus();

                Logger.LogLine("Doing accept card");
                await DoAcceptCard();

                Logger.LogLine($"Done");

                // Start listening for unsolicited messages from the server. 
                while (true)
                {
                    switch (await cardReader.ReceiveMessageAsync())
                    {
                        case MediaRemovedEvent removed:
                            Logger.LogLine($"{nameof(MediaRemovedEvent)}: {removed.Serialise()}");
                            break;

                        case MediaInsertedEvent inserted:
                            Logger.LogLine($"{nameof(MediaInsertedEvent)} : {inserted.Serialise()}");
                            break;

                        case object message:
                            Logger.LogLine($"*** Unknown message received. {message.GetType()}");
                            break;
                    }
                }
            }
            catch (WebSocketException e)
            {
                Logger.LogError($"Connection error {e.Message}");
                System.Diagnostics.Debugger.Break();
            }
            catch (Exception e)
            {
                Logger.LogError($"Unhandled exception: {e.Message}");
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Messages that we expect to receive so that we can decode them. 
        /// </summary>
        private readonly MessageDecoder ResponseDecoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Response);

        private readonly ConsoleLogger Logger = new ConsoleLogger();

        public Uri CardReaderUri { get; private set; }
        //public Uri PinPadUri { get; private set; }
        //public Uri PrinterUri { get; private set; }

        private async Task DoServiceDiscovery()
        {
            const int port = 5846;
            var Discovery = new XFS4IoTClient.ClientConnection(
                    EndPoint: new Uri($"ws://localhost:{port}/XFS4IoT/v1.0"));

            try
            {
                await Discovery.ConnectAsync();
            }
            catch (Exception e)
            {
                Logger.LogLine($"Caught exception ... {e}");
                Thread.Sleep(30000);
            }

            Logger.LogLine($"{nameof(GetServiceCommand)}", ConsoleColor.Blue);

            await Discovery.SendCommandAsync(new GetServiceCommand(Guid.NewGuid().ToString(), new GetServiceCommand.PayloadData(60000)));
            Logger.LogLine($"Waiting for response...");

            switch (await Discovery.ReceiveMessageAsync())
            {

                case GetServiceCompletion response:
                    Logger.Log($"{nameof(GetServiceCompletion)}", ConsoleColor.Green);
                    Logger.WriteLine($" : {response.Serialise()}");
                    EndPointDetails(response.Payload);
                    return;

                case null:
                    Logger.LogError($"Invalid response to {nameof(GetServiceCompletion)}");
                    break;

                case object message:
                    Logger.LogError($"Invalid type of response {message.GetType()}");
                    break;

            }
        }

        private void EndPointDetails(GetServiceCompletion.PayloadData endpointDetails)
        {
            Logger.LogLine($"Got endpoint details {endpointDetails}");
            Logger.LogLine($"Services:\n{string.Join("\n", from ep in endpointDetails.Services select ep.ServiceUri)}");

            CardReaderUri = FindServiceUri(endpointDetails, XFSConstants.ServiceClass.CardReader);
        }

        private static Uri FindServiceUri(GetServiceCompletion.PayloadData endpointDetails, XFSConstants.ServiceClass ServiceClass)
        {
            var service =
                (from ep in endpointDetails.Services
                 where ep.ServiceUri.Contains(ServiceClass.ToString())
                 select ep
                 ).FirstOrDefault()
                 ?.ServiceUri;

            if (string.IsNullOrEmpty(service)) throw new Exception($"Failed to find a device {ServiceClass} endpoint");

            return new Uri(service);
        }

        private XFS4IoTClient.ClientConnection cardReader;
        private async Task OpenCardReader()
        {
            // Create the connection object. This doesn't start anything...  
            cardReader = new XFS4IoTClient.ClientConnection(
                    EndPoint: CardReaderUri ?? throw new NullReferenceException()
                    );

            // Open the actual network connection, with a timeout. 
            var cancel = new CancellationTokenSource();
            cancel.CancelAfter(10_000);
            await cardReader.ConnectAsync(cancel.Token);
        }

        private async Task GetCardReaderStatus()
        {
            Logger.LogLine($"{nameof(StatusCommand)}", ConsoleColor.Blue);

            // Create a new command and send it to the device
            var command = new StatusCommand(Guid.NewGuid().ToString(), new StatusCommand.PayloadData(Timeout: 1_000));
            await cardReader.SendCommandAsync(command);

            // Wait for a response from the device. 
            Logger.LogLine("Waiting for response... ");

            while (true)
            {
                switch (await cardReader.ReceiveMessageAsync())
                {
                    case StatusCompletion commandCompletion:
                        Logger.Log($"{nameof(ReadRawDataCompletion)}", ConsoleColor.Green);
                        Logger.WriteLine($" : {commandCompletion.Serialise()}");
                        return;

                    case null:
                        Logger.LogError($"Invalid null response.");
                        break;

                    case object unknown:
                        Logger.LogWarning($"Unexpected type of response. {unknown.GetType()}");
                        break;
                }
            }
        }


        private async Task DoAcceptCard()
        {
            Logger.LogLine($"{nameof(ReadRawDataCommand)}", ConsoleColor.Blue);

            //MessageBox((IntPtr)0, "Send CardReader ReadRawData command to read chip card", "XFS4IoT Test Client", 0);
            // Create a new command and send it to the device
            var command = new ReadRawDataCommand(Guid.NewGuid().ToString(),
                                                 new ReadRawDataCommand.PayloadData(
                                                        60_000,
                                                        Track1: true,
                                                        Track2: true,
                                                        Track3: true,
                                                        Chip: true,
                                                        Security: false,
                                                        FluxInactive: false,
                                                        Watermark: false,
                                                        MemoryChip: false,
                                                        Track1Front: false,
                                                        FrontImage: false,
                                                        BackImage: false,
                                                        Track1JIS: false,
                                                        Track3JIS: false,
                                                        Ddi: false));
            await cardReader.SendCommandAsync(command);

            // Wait for a response from the device. 
            Logger.LogLine("Waiting for response... ");

            while (true)
            {
                switch (await cardReader.ReceiveMessageAsync())
                {
                    case InsertCardEvent insertCardEvent:
                        Logger.Log($"{nameof(MediaInsertedEvent)}", ConsoleColor.Yellow);
                        Logger.WriteLine($" : {insertCardEvent.Serialise()}");
                        break;

                    case MediaInsertedEvent mediaInsertedEvent:
                        Logger.Log($"{nameof(MediaInsertedEvent)}", ConsoleColor.Yellow);
                        Logger.WriteLine($" : {mediaInsertedEvent.Serialise()}");
                        break;

                    case ReadRawDataCompletion commandCompletion:
                        Logger.Log($"{nameof(ReadRawDataCompletion)}", ConsoleColor.Green);
                        Logger.WriteLine($" : {commandCompletion.Serialise()}");
                        return;

                    case null:
                        Logger.LogError($"Invalid null response.");
                        break;

                    case object unknown:
                        Logger.LogWarning($"Unexpected type of response. {unknown.GetType()}");
                        break;
                }
            }
        }

        /// <summary>
        /// Log to the console, including timing details. 
        /// </summary>
        private class ConsoleLogger
        {
            public ConsoleLogger()
            {
                defaultColour = Console.ForegroundColor;
            }
            public void Log(string v, ConsoleColor? colour = null)
            {
                Console.ForegroundColor = colour ?? defaultColour; 
                Console.Write($"{DateTime.Now:hh:mm:ss.ffff} ({DateTime.Now - Start}): {v}");
                Console.ForegroundColor = defaultColour;
            }

            public void Write(string v, ConsoleColor? colour = null)
            {
                Console.ForegroundColor = colour ?? defaultColour;
                Console.Write(v);
                Console.ForegroundColor = defaultColour;
            }

            public void LogLine(string v, ConsoleColor? colour = null)
            {
                Console.ForegroundColor = colour ?? defaultColour;
                Console.WriteLine($"{DateTime.Now:hh:mm:ss.ffff} ({DateTime.Now - Start}): {v}");
                Console.ForegroundColor = defaultColour;
            }

            public void WriteLine(string v, ConsoleColor? colour = null)
            {
                Console.ForegroundColor = colour ?? defaultColour;
                Console.WriteLine(v);
                Console.ForegroundColor = defaultColour;
            }

            public void LogError(string v)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{DateTime.Now:hh:mm:ss.ffff} ({DateTime.Now - Start}): {v}");
                Console.ForegroundColor = defaultColour;
            }
            internal void LogWarning(string v)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{DateTime.Now:hh:mm:ss.ffff} ({DateTime.Now - Start}): {v}");
                Console.ForegroundColor = defaultColour;
            }

            public void Restart() => Start = DateTime.Now;

            private DateTime Start = DateTime.Now;
            private ConsoleColor defaultColour;
        }
    }
}
