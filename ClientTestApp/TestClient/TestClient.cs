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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Don't let exceptions escape top level")]
        async static Task Main(/*string[] args*/)
        {
            try
            {
                Logger.WriteLine("Running test XFS4IoT application.");

                await DoServiceDiscovery();

                Logger.WriteLine("XFS4IoT SPs Connected. Doing accept card");

                await DoAcceptCard();
                Logger.WriteLine($"Done");

                // Start listening for messages from the server in the background to handle messages in an async
                // way. 

                while( true )
                {
                    switch (await cardReader.ReceiveMessageAsync())
                    {
                        case MediaRemovedEvent removed :
                            Logger.WriteLine($"{nameof(MediaRemovedEvent)}: {removed.Serialise()}");
                            break;
                        //CardReader.MediaRemovedEvent:
                        default:
                            Logger.WriteLine("*** Unknown message received.");
                            break;
                    }
                }
            }
            catch (WebSocketException e)
            {
                Logger.WriteError($"Connection error {e.Message}");
                System.Diagnostics.Debugger.Break();
            }
            catch (Exception e)
            {
                Logger.WriteError($"Unhandled exception: {e.Message}");
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Messages that we expect to receive so that we can decode them. 
        /// </summary>
        private static readonly MessageDecoder ResponseDecoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Response);

        private static readonly ConsoleLogger Logger = new ConsoleLogger();

        public static Uri CardReaderUri { get; private set; }
        public static Uri PinPadUri { get; private set; }
        public static Uri PrinterUri { get; private set; }

        private static async Task DoServiceDiscovery()
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
                Logger.WriteLine($"Caught exception ... {e}");
                Thread.Sleep(30000);
            }

            Logger.WriteLine($"Sending {nameof(GetServiceCommand)} command");

            await Discovery.SendCommandAsync(new GetServiceCommand(Guid.NewGuid().ToString(), new GetServiceCommand.PayloadData(60000)));
            Logger.WriteLine($"Waiting for response...");

            object cmdResponse = await Discovery.ReceiveMessageAsync();
            if (cmdResponse is null)
                Logger.WriteLine($"Invalid response to {nameof(GetServiceCompletion)}");
            GetServiceCompletion response = cmdResponse as GetServiceCompletion;
            if (response is null)
                Logger.WriteLine($"Invalid type of response to {nameof(GetServiceCompletion)}");

            if (response is not null)
                EndPointDetails(response.Payload);
        }

        private static void EndPointDetails(GetServiceCompletion.PayloadData endpointDetails)
        {
            Logger.WriteLine($"Got endpoint details {endpointDetails}");
            Logger.WriteLine($"Services:\n{string.Join("\n", from ep in endpointDetails.Services select ep.ToString())}");

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

        private static XFS4IoTClient.ClientConnection cardReader;

        private static async Task DoAcceptCard()
        {
            // Create the connection object. This doesn't start anything...  
            cardReader = new XFS4IoTClient.ClientConnection(
                    EndPoint: CardReaderUri ?? throw new NullReferenceException()
                    );

            // Open the actual network connection
            cardReader.ConnectAsync().Wait(10_000);

            Logger.WriteLine($"Sending {nameof(ReadRawDataCommand)} command");

            //MessageBox((IntPtr)0, "Send CardReader ReadRawData command to read chip card", "XFS4IoT Test Client", 0);
            // Create a new command and send it to the device
            var command = new ReadRawDataCommand(Guid.NewGuid().ToString(), new ReadRawDataCommand.PayloadData(60_000,
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
            Logger.WriteLine("Waiting for response... ");

            for (; ;)
            {
                object cmdResponse = await cardReader.ReceiveMessageAsync();

                if (cmdResponse is null)
                {
                    Logger.WriteLine($"Invalid response. {nameof(ReadRawDataCompletion)}");
                    break;
                }

                if (cmdResponse.GetType() != typeof(ReadRawDataCompletion))
                {
                    if (cmdResponse.GetType() == typeof(XFS4IoT.CardReader.Events.MediaInsertedEvent))
                    {
                        Logger.WriteLine($"Execute event received. {nameof(XFS4IoT.CardReader.Events.MediaInsertedEvent)}");
                        continue;
                    }
                    else
                    {
                        Logger.WriteLine($"Unexpected type of response. {nameof(ReadRawDataCompletion)}");
                    }
                }

                ReadRawDataCompletion response = cmdResponse as ReadRawDataCompletion;
                if (response is null)
                    Logger.WriteLine($"Invalid type of response. {nameof(ReadRawDataCompletion)}");
                else
                    Logger.WriteLine($"Response received. {nameof(ReadRawDataCompletion)}");

                break;
            }
        }


        /// <summary>
        /// Log to the console, including timing details. 
        /// </summary>
        private class ConsoleLogger
        {
            public void WriteLine(string v) => Console.WriteLine($"{DateTime.Now:hh:mm:ss.ffff} ({DateTime.Now - Start}): {v}");
            public void WriteError(string v)
            {
                var oldColour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{DateTime.Now:hh:mm:ss.ffff} ({DateTime.Now - Start}): {v}");
                Console.ForegroundColor = oldColour;
            }

            public void Restart() => Start = DateTime.Now;
            private DateTime Start = DateTime.Now;
        }
    }
}
