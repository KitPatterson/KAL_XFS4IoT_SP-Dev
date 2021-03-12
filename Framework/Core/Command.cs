// (C) KAL ATM Software GmbH, 2021

using System.Runtime.Serialization;

namespace XFS4IoT.Commands
{
    [DataContract]
    public abstract class Command<T> : Message<T> where T : MessagePayloadBase
    {
        /// <summary>
        /// Initialise any command objects
        /// </summary>
        /// <param name="Name">Name of the command to request operation to the device.</param>
        /// <param name="RequestId">request id</param>
        /// <param name="Payload">payload contents</param>
        public Command(string RequestId, T Payload) :
            base(RequestId, MessageHeader.TypeEnum.Command, Payload)
        { }
    }
}