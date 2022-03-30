﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoT;

namespace XFS4IoTServer
{
    public class ServiceProvider : CommandDispatcher, IServiceProvider
    {
        public ServiceProvider(XFS4IoTServer.EndpointDetails EndpointDetails, string ServiceName, IEnumerable<XFSConstants.ServiceClass> Services, IDevice Device, ILogger Logger)
            : base(Services, Logger)
        {
            EndpointDetails.IsNotNull($"The endpoint details are invalid. {nameof(EndpointDetails)}");
            Device.IsNotNull($"The device interface is an invalid. {nameof(Device)}");

            this.Device = Device;
            this.logger = Logger.IsNotNull();
            this.Name = ServiceName;

            (Uri, WSUri) = EndpointDetails.ServiceUri(ServiceName);

            Logger.Log(Constants.Framework, $"Listening on {Uri}");

            this.EndPoint = new EndPoint(Uri,
                                         CommandDecoder,
                                         this,
                                         Logger);
        }

        public async override Task RunAsync() => await Task.WhenAll(EndPoint.RunAsync(), Device.RunAsync(), base.RunAsync());

        public string Name { get; internal set; }
        private readonly EndPoint EndPoint;
        private readonly ILogger logger;

        private MessageDecoder CommandDecoder { get; } = new MessageDecoder(MessageDecoder.AutoPopulateType.Command);
        public Uri Uri { get; }
        public Uri WSUri { get; }

        public IDevice Device { get; internal set; }

        public async Task BroadcastEvent(object payload)
        {
            logger.Log(nameof(ServiceProvider), $"Broadcasting unsolicited event");

            // Create all the send tasks at once so that we can send in parallel. 
            var sendTasks = from connection in EndPoint.Connections
                            select connection.SendMessageAsync(payload);
            await Task.WhenAll(sendTasks);

            logger.Log(nameof(ServiceProvider), $"Finished broadcasting unsolicited event");
        }

        public async Task BroadcastEvent(IEnumerable<IConnection> connections, object payload)
        {
            logger.Log(nameof(ServiceProvider), $"Broadcasting unsolicited event to specified connections");

            var sendTasks = from connection in EndPoint.Connections
                            where connections.Contains(connection)
                            select connection.SendMessageAsync(payload);
            await Task.WhenAll(sendTasks);

            logger.Log(nameof(ServiceProvider), $"Finished broadcasting unsolicited event to specified connections");
        }
    }
}