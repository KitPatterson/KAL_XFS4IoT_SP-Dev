/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class ReadRawDataHandler
    {

        private async Task<ReadRawDataCompletion.PayloadData> HandleReadRawData(IReadRawDataEvents events, ReadRawDataCommand readRawData, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.ReadRawData()");
            var result = await Device.ReadRawData(events, readRawData.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ReadRawData() -> {result.CompletionCode}");

            return result;
        }

    }
}
