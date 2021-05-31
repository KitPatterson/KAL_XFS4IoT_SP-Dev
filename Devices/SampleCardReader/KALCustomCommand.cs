using System.Runtime.Serialization;

using XFS4IoT;
using XFS4IoT.Commands;
using XFS4IoT.Completions;

namespace KAL.XFS4IoTSP.CardReader.Sample
{
    [DataContract]
    [Command(Name ="CardReader.KALCustomerCommand")]
    public class KALCustomCommand : Command<KALCustomCommand.PayloadData>
    {
        [DataContract]
        public class PayloadData : XFS4IoT.Commands.MessagePayload
        {
            public PayloadData( int Timeout ) : base(Timeout)
            { }
            public string InputValue { get; set; }

        }

        public KALCustomCommand( string RequestId, PayloadData payload) : base( RequestId, payload)
        { }
    }
}