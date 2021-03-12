﻿// (C) KAL ATM Software GmbH, 2021

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using XFS4IoT.Commands;
using System;
using XFS4IoT;
using XFS4IoT.CardReader.Commands;
using System.Reflection;
using XFS4IoT.Events;

namespace XFS4IoTCoreTest.Command
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
            var ReadCardJSON = @"{
                ""headers"":{
                    ""name"":""CardReader.ReadRawData"",
                    ""requestId"":""b34800d0-9dd2-4d50-89ea-92d1b13df54b"",
                    ""type"":""command""
                },
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
                 }
            }";

            var assemblyName = Assembly.GetAssembly(typeof(ReadRawData))?.GetName();
            IsNotNull(assemblyName);

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Command, assemblyName)
            {
                { typeof(ReadRawData) }
            };

            bool rc = decoder.TryUnserialise(ReadCardJSON, out object resultMessage);

            IsTrue(rc);
            IsNotNull(resultMessage);

            Command<ReadRawDataPayload> result = resultMessage as Command<ReadRawDataPayload> ?? throw new Exception();

            IsNotNull(result);

            IsInstanceOfType(result, typeof(ReadRawData));
            ReadRawData readCardCommand = result as ReadRawData;
            IsNotNull(readCardCommand);
            IsNotNull(readCardCommand.Payload);
            ReadRawDataPayload readCardPayload = readCardCommand.Payload as ReadRawDataPayload;
            IsNotNull(readCardPayload);
            AreEqual(true, readCardPayload.Track1);
        }

        [TestMethod]
        public void UnserialiseStringToEvent()
        {
            var MediaInsertedJSON = @"{
                ""headers"":{
                    ""name"":""CardReader.MediaInsertedEvent"",
                    ""requestId"":""b34800d0-9dd2-4d50-89ea-92d1b13df54b"",
                    ""type"":""event""
                },
                ""payload"":{}
            }";

            var assemblyName = Assembly.GetAssembly(typeof(XFS4IoT.CardReader.Events.MediaInsertedEvent))?.GetName();
            IsNotNull(assemblyName);

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Command, assemblyName)
            {
                { typeof(XFS4IoT.CardReader.Events.MediaInsertedEvent) }
            };

            bool rc = decoder.TryUnserialise(MediaInsertedJSON, out object resultMessage);

            IsTrue(rc);
            IsNotNull(resultMessage);

            Event<MessagePayloadBase> result = resultMessage as Event<MessagePayloadBase> ?? throw new Exception();

            IsNotNull(result);

            IsInstanceOfType(result, typeof(XFS4IoT.CardReader.Events.MediaInsertedEvent));
            XFS4IoT.CardReader.Events.MediaInsertedEvent mediaInsertedEvent = result as XFS4IoT.CardReader.Events.MediaInsertedEvent;
            IsNotNull(mediaInsertedEvent);
            IsNotNull(mediaInsertedEvent.Payload);
            MessagePayloadBase mediaInsertedPayload = mediaInsertedEvent.Payload as MessagePayloadBase;
            IsNotNull(mediaInsertedPayload);
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

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Command, assemblyName)
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
            var assemblyName = Assembly.GetAssembly(typeof(ReadRawData))?.GetName();
            IsNotNull(assemblyName);

            var AcceptCardJSON = @"Not JSON";

            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Command, assemblyName)
            {
                { typeof(ReadRawData) }
            };

            bool rc = decoder.TryUnserialise(AcceptCardJSON, out object result);
            IsFalse(rc);
            AreEqual(null, result);
        }

        [Command(Name = "Common.TestCommand1")]
        public class TestCommand1 : Command<MessagePayload>
        {
            public TestCommand1() : base(Guid.NewGuid().ToString(), new MessagePayload(10000)) { }
        }
        [Command(Name = "Common.TestCommand2")]
        public class TestCommand2 : Command<MessagePayload>
        {
            public TestCommand2() : base(Guid.NewGuid().ToString(), new MessagePayload(20000)) { }
        }
        [Response(Name = "Common.TestResponse1")]
        public class TestResponse1 : XFS4IoT.Responses.Response<XFS4IoT.Responses.MessagePayload>
        {
            public TestResponse1() : base(Guid.NewGuid().ToString(), new XFS4IoT.Responses.MessagePayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success, "good")) { }
        }
        [Response(Name = "Common.TestResponse2")]
        public class TestResponse2 : XFS4IoT.Responses.Response<XFS4IoT.Responses.MessagePayload>
        {
            public TestResponse2() : base(Guid.NewGuid().ToString(), new XFS4IoT.Responses.MessagePayload(XFS4IoT.Responses.MessagePayload.CompletionCodeEnum.Success, "error")) { }
        }

        [TestMethod]
        public void MessageDecoderCommandAtributeInit()
        {
            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Command, AssemblyName);

            var results = (from string x in decoder select x).ToArray();

            AreEqual(2, results.Length);
            AreEqual("Common.TestCommand1", results[0]);
            AreEqual("Common.TestCommand2", results[1]);
        }

        [TestMethod]
        public void MessageDecoderResponseAtributeInit()
        {
            var decoder = new MessageDecoder(MessageDecoder.AutoPopulateType.Response, AssemblyName);

            var results = (from string x in decoder select x).ToArray();

            AreEqual(2, results.Length);
            AreEqual("Common.TestResponse1", results[0]);
            AreEqual("Common.TestResponse2", results[1]);
        }
    }
}
