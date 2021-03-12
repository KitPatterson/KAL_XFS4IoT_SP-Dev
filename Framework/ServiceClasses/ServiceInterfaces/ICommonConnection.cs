// (C) KAL ATM Software GmbH, 2021

using XFS4IoTServer;

namespace XFS4IoTFramework.Common
{
    public interface ICommonConnection
    {
        void ServiceDetailEvent(XFS4IoT.Common.Events.ServiceDetailEventPayload Payload);
    }
}
