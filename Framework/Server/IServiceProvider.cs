// (C) KAL ATM Software GmbH, 2021

using System;
using System.Threading.Tasks;
using XFS4IoT;

namespace XFS4IoTServer
{
    public interface IServiceProvider : ICommandDispatcher
    {
        Task RunAsync();
        string Name { get; }

        Uri Uri { get; }

        Uri WSUri { get; }
    }
}