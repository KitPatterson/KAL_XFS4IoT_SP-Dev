using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

using XFS4IoT;
using XFS4IoT.Completions;

namespace KAL.XFS4IoTSP.CardReader.Sample
{
    [DataContract]
    [Completion(Name ="CardReader.KALCustomCompletion")]
    public class KALCustomCompletion : Completion<KALCustomCompletion.PayloadData>
    {
        public KALCustomCompletion( string RequestId, PayloadData Payload ) : base(RequestId, Payload )
        {
        }

        public class PayloadData : MessagePayloadBase
        {
            public PayloadData() : base()
            {

            }
            [DataMember]
            public string ResultInfo { get; set; }
        }
    }
}