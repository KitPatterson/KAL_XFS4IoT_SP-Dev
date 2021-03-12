// (C) KAL ATM Software GmbH, 2021

using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Common.Commands
{
    [DataContract]
    [Command(Name = "Common.GetService")]
    public sealed class GetService : Command<MessagePayload>
    {
        public GetService(string RequestId, MessagePayload Payload)
            : base(RequestId, Payload)
        { }
    }
}
