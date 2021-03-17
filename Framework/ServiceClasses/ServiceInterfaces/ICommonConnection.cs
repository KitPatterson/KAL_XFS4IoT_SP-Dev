// (C) KAL ATM Software GmbH, 2021

using XFS4IoTServer;
using XFS4IoT.Common.Events;

namespace XFS4IoTFramework.Common
{
    public interface ICommonConnection
    {
        void ServiceDetailEvent(ServiceDetailEvent.PayloadData Payload);
    }
}
