// (C) KAL ATM Software GmbH, 2021

using System.Collections;

namespace XFS4IoT
{
    public interface IMessageDecoder: IEnumerable
    {
        bool TryUnserialise(string JSON, out object result);
    }
}