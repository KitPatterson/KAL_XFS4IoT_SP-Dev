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
    public partial class EjectCardHandler
    {

        private async Task HandleEjectCard(IConnection connection, EjectCardCommand ejectCard, CancellationToken cancel)
        {
            IEjectCardEvents events = new EjectCardEvents(connection, ejectCard.Headers.RequestId);

            Logger.Log(Constants.DeviceClass, "CardReaderDev.EjectCard()");
            var result = await Device.EjectCard(events, ejectCard.Payload, cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EjectCard() -> {result.CompletionCode}");

            await connection.SendMessageAsync(new EjectCardCompletion(ejectCard.Headers.RequestId, result));

            await Device.WaitForCardTaken(events, cancel);
        }

    }
}
