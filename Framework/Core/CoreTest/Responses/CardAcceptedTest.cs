// (C) KAL ATM Software GmbH, 2021

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XFS4IoT.Responses;
using XFS4IoT.CardReader.Responses;

namespace XFS4IoTCoreTest.Response
{
    using static Assert; 

    [TestClass]
    public class ResponseSerialisationTest
    {
        [TestMethod]
        public void Constructor()
        {
            List<ReadRawDataPayload.DataClass> Data = new List<ReadRawDataPayload.DataClass>();
            Data.Add(new ReadRawDataPayload.DataClass(ReadRawDataPayload.DataClass.DataSourceEnum.Track1, ReadRawDataPayload.DataClass.StatusEnum.Ok,      "123456789"));
            Data.Add(new ReadRawDataPayload.DataClass(ReadRawDataPayload.DataClass.DataSourceEnum.Track2, ReadRawDataPayload.DataClass.StatusEnum.ErrorDataMissing, "123456789"));
            Data.Add(new ReadRawDataPayload.DataClass(ReadRawDataPayload.DataClass.DataSourceEnum.Track3, ReadRawDataPayload.DataClass.StatusEnum.ErrorDataInvalid, "123456789"));
            var response = new ReadRawData(Guid.NewGuid().ToString(), new ReadRawDataPayload(MessagePayload.CompletionCodeEnum.Success, "OK", ReadRawDataPayload.StatusEnum.Ok, Data));
            AreEqual(3, response.Payload.Data.Count);
            AreEqual(ReadRawDataPayload.DataClass.DataSourceEnum.Track1, response.Payload.Data[0].DataSource);
            AreEqual("123456789", response.Payload.Data[1].Data);
            AreEqual(ReadRawDataPayload.DataClass.StatusEnum.ErrorDataInvalid, response.Payload.Data[2].Status);
        }

        [TestMethod]
        public void SerialiseString()
        {
            List<ReadRawDataPayload.DataClass> Data = new List<ReadRawDataPayload.DataClass>();
            Data.Add(new ReadRawDataPayload.DataClass(ReadRawDataPayload.DataClass.DataSourceEnum.Track1, ReadRawDataPayload.DataClass.StatusEnum.Ok, "123456789"));
            Data.Add(new ReadRawDataPayload.DataClass(ReadRawDataPayload.DataClass.DataSourceEnum.Track2, ReadRawDataPayload.DataClass.StatusEnum.ErrorDataMissing, "123456789"));
            Data.Add(new ReadRawDataPayload.DataClass(ReadRawDataPayload.DataClass.DataSourceEnum.Track3, ReadRawDataPayload.DataClass.StatusEnum.ErrorDataInvalid, "123456789"));
            var response = new ReadRawData("ee6d592b-483c-4c22-98ef-1070e290bf4f", new ReadRawDataPayload(MessagePayload.CompletionCodeEnum.Success, "OK", ReadRawDataPayload.StatusEnum.Ok, Data));

            string res = response.Serialise();

            AreEqual(@"{""payload"":{""errorCode"":""ok"",""data"":[{""type"":""track1"",""status"":""ok"",""data"":""123456789""},{""type"":""track2"",""status"":""missing"",""data"":""123456789""},{""type"":""track3"",""status"":""invalid"",""data"":""123456789""}],""completionCode"":""success"",""errorDescription"":""OK""},""headers"":{""name"":""CardReader.ReadRawData"",""requestId"":""ee6d592b-483c-4c22-98ef-1070e290bf4f"",""type"":""response""}}", res);
        }
    }
}
