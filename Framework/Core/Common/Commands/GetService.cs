// (C) KAL ATM Software GmbH, 2021

using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Common.Commands
{
    [DataContract]
    [Command(Name = "Common.GetService")]
    public sealed class GetServiceCommand : Command<GetServiceCommand.PayloadData>
    {
        public GetServiceCommand(string RequestId, GetServiceCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout)
                : base(Timeout)
            {
            }


        }
    }
}
