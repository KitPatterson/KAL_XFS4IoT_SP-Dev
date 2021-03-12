using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace XFS4IoT
{
    /// <summary>
    /// Converter for base64 string <-> byte array
    /// Adding attribute [JsonConverter(typeof(Base64Converter))] in the message property for target field to be converted
    /// </summary>
    internal class Base64Converter : JsonConverter<List<byte>>
    {
        public override List<byte> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetBytesFromBase64().ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<byte> value, JsonSerializerOptions options)
        {
            writer.WriteBase64StringValue(value.ToArray());
        }
    }
}
