// (C) KAL ATM Software GmbH, 2021

using System.Threading.Tasks;
using XFS4IoT.Responses;

namespace XFS4IoTServer
{
    public interface IConnection
    {
        Task SendMessageAsync(object message);
    }
}