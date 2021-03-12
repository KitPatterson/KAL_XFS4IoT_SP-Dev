// (C) KAL ATM Software GmbH, 2021

using System.Runtime.Serialization;

namespace XFS4IoT.Responses
{
    [DataContract]
    public abstract class Response<T> : Message<T> where T : MessagePayloadBase
    {
        /// <summary>
        /// Initialise any response object
        /// </summary>
        /// <param name="ID">Unique ID each message class for serialize/deserialize object</param>
        /// <param name="Name">Name of the response of the command required</param>
        /// <param name="RequestId">request id</param>
        /// <param name="Payload">payload contents</param>
        public Response(string RequestId, T Payload) :
            base(RequestId, MessageHeader.TypeEnum.Response, Payload)
        { }
    }
}
