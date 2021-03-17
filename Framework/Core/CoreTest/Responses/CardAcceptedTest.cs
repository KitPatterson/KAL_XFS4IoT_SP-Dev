// (C) KAL ATM Software GmbH, 2021

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTCoreTest.Response
{
    using static Assert; 

    [TestClass]
    public class ResponseSerialisationTest
    {
        [TestMethod]
        public void Constructor()
        {
            List<ReadRawDataCompletion.PayloadData.DataClass> Data = new List<ReadRawDataCompletion.PayloadData.DataClass>();
            Data.Add(new ReadRawDataCompletion.PayloadData.DataClass(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track1, ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.Ok,      "123456789"));
            Data.Add(new ReadRawDataCompletion.PayloadData.DataClass(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track2, ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.ErrorDataMissing, "123456789"));
            Data.Add(new ReadRawDataCompletion.PayloadData.DataClass(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track3, ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.ErrorDataInvalid, "123456789"));
            var response = new ReadRawDataCompletion(Guid.NewGuid().ToString(), new ReadRawDataCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, "OK", ReadRawDataCompletion.PayloadData.StatusEnum.Ok, Data));
            AreEqual(3, response.Payload.Data.Count);
            AreEqual(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track1, response.Payload.Data[0].DataSource);
            AreEqual("123456789", response.Payload.Data[1].Data);
            AreEqual(ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.ErrorDataInvalid, response.Payload.Data[2].Status);
        }

        [TestMethod]
        public void SerialiseString()
        {
            List<ReadRawDataCompletion.PayloadData.DataClass> Data = new List<ReadRawDataCompletion.PayloadData.DataClass>();
            Data.Add(new ReadRawDataCompletion.PayloadData.DataClass(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track1, ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.Ok, "123456789"));
            Data.Add(new ReadRawDataCompletion.PayloadData.DataClass(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track2, ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.ErrorDataMissing, "123456789"));
            Data.Add(new ReadRawDataCompletion.PayloadData.DataClass(ReadRawDataCompletion.PayloadData.DataClass.DataSourceEnum.Track3, ReadRawDataCompletion.PayloadData.DataClass.StatusEnum.ErrorDataInvalid, "123456789"));
            var response = new ReadRawDataCompletion("ee6d592b-483c-4c22-98ef-1070e290bf4f", new ReadRawDataCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, "OK", ReadRawDataCompletion.PayloadData.StatusEnum.Ok, Data));

            string res = response.Serialise();

            AreEqual(@"{""payload"":{""errorCode"":""ok"",""data"":[{""type"":""track1"",""status"":""ok"",""data"":""123456789""},{""type"":""track2"",""status"":""missing"",""data"":""123456789""},{""type"":""track3"",""status"":""invalid"",""data"":""123456789""}],""completionCode"":""success"",""errorDescription"":""OK""},""headers"":{""name"":""CardReader.ReadRawData"",""requestId"":""ee6d592b-483c-4c22-98ef-1070e290bf4f"",""type"":""response""}}", res);
        }
    }
}
