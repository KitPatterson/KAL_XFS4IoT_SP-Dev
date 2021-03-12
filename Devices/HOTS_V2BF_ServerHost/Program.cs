// (C) KAL ATM Software GmbH, 2021

using System;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoTServer;

namespace HOTS_V2BF_ServerHost
{
    class Server
    {
        static async Task Main(/*string[] args*/)
        {
            ConsoleLogger Logger = new ConsoleLogger();
            try
            {
                Logger.Log($"Running HOTS V2BF ServiceProvider Server");

                var Publisher = new ServicePublisher(Logger);
                var EndpointDetails = Publisher.EndpointDetails;

                var cardReaderDevice = new KAL.XFS4IoTSP.CardReader.HOTS.V2BF(Logger);
                Publisher.Add(new XFS4IoTCardReader.CardReaderServiceProvider(EndpointDetails, cardReaderDevice, Logger));

                await Publisher.RunAsync();
            }
            catch (Exception e) when (e.InnerException != null)
            {
                Logger.Warning($"Unhandled exception {e.InnerException.Message}");
            }
            catch (Exception e)
            {
                Logger.Warning($"Unhandled exception {e.Message}");
            }

        }

        private class ConsoleLogger : ILogger
        {
            public void Warning(string Message) => Warning("SvrHost", Message);
            public void Log(string Message) => Log("SvrHost", Message);

            public void Trace(string SubSystem, string Operation, string Message) => Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} ({(DateTime.Now - Start).TotalSeconds:000.000}): {Message}");

            public void Warning(string SubSystem, string Message) => Trace(SubSystem, "WARNING", Message);

            public void Log(string SubSystem, string Message) => Trace(SubSystem, "INFO", Message);

            public void TraceSensitive(string SubSystem, string Operation, string Message) => Trace(SubSystem, Operation, Message);

            public void WarningSensitive(string SubSystem, string Message) => Trace(SubSystem, "WARNING", Message);

            public void LogSensitive(string SubSystem, string Message) => Trace(SubSystem, "INFO", Message);

            private readonly DateTime Start = DateTime.Now;
        }
    }
}
