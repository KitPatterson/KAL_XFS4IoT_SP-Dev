// (C) KAL ATM Software GmbH, 2021

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;
using System;
using XFS4IoT;
using XFS4IoT.CardReader.Responses;
using XFS4IoT.Responses;

namespace XFS4IoTCoreTest.Response
{
    using static Assert;

    [TestClass]
    public class UnserialiseTests
    {
        private readonly AssemblyName AssemblyName;

        public UnserialiseTests()
        {
            AssemblyName = Assembly.GetAssembly(typeof(UnserialiseTests))?.GetName();
        }

        [TestMethod]
        public void UnserialiseStringToObject()
        {
            var ReadCardJSON = @"{""headers"":{""name"":""CardReader.ReadRawData"",""requestId"":""ee6d592b-483c-4c22-98ef-1070e290bf4f"",""type"":1},""payload"":{""completionCode"":0,""errorDescription"":""OK"",""data"":[{""data"":""123456789"",""status"":0,""type"":0},{""data"":""123456789"",""status"":1,""type"":1},{""data"":""123456789"",""status"":2,""type"":2}],""errorCode"":0}}";

            var assemblyName = Assembly.GetAssembly(typeof(ReadRawData))?.GetName();
            IsNotNull(assemblyName);

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Response, assemblyName)
            {
                { typeof(ReadRawData) }
            };

            bool rc = decoder.TryUnserialise(ReadCardJSON, out object resultMessage);

            IsTrue(rc);
            IsNotNull(resultMessage);

            Response<ReadRawDataPayload> result = resultMessage as Response<ReadRawDataPayload> ?? throw new Exception();

            IsNotNull(result);

            IsInstanceOfType(result, typeof(ReadRawData));
            ReadRawData readCardCommand = result as ReadRawData;
            IsNotNull(readCardCommand);
            IsNotNull(readCardCommand.Payload);
            ReadRawDataPayload readCardPayload = readCardCommand.Payload as ReadRawDataPayload;
            IsNotNull(readCardPayload);
            AreEqual(3, readCardPayload.Data.Count);
        }

        [TestMethod]
        public void UnserialiseStringToObjectNoHeader()
        {
            var AcceptCardJSON = @"{
                ""payload"":{
                    ""timeout"":5000,
                    ""track1"":true,
                    ""track2"": true,
                    ""track3"":true,
                    ""chip"":true,
                    ""security"":true,
                    ""fluxInactive"":true,
                    ""watermark"":true,
                    ""memoryChip"":true,
                    ""track1Front"":true,
                    ""frontImage"":true,
                    ""backImage"":true,
                    ""track1JIS"":true,
                    ""track3JIS"":true,
                    ""ddi"":true
            }";

            var assemblyName = Assembly.GetAssembly(typeof(ReadRawData))?.GetName();
            IsNotNull(assemblyName);

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Response, assemblyName)
            {
                { typeof(ReadRawData) }
            };

            bool rc = decoder.TryUnserialise(AcceptCardJSON, out object result);
            IsFalse(rc);
            AreEqual(null, result);
        }

        [TestMethod]
        public void UnserialiseStringToObjectNotJSON()
        {
            var AcceptCardJSON = @"Not JSON";
            var assemblyName = Assembly.GetAssembly(typeof(ReadRawData))?.GetName();
            IsNotNull(assemblyName);

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Response, assemblyName)
            {
                { typeof(ReadRawData) }
            };

            bool rc = decoder.TryUnserialise(AcceptCardJSON, out object result);
            IsFalse(rc);
            AreEqual(null, result);
        }
    }
}
