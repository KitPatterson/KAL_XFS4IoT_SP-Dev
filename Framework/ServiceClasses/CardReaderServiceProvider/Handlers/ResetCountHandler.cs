/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ResetCountHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/

using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    /// <summary>
    /// ResetCountResult
    /// Return result of resetting retain bin counters
    /// </summary>
    public sealed class ResetCountResult : DeviceResult
    {
        public ResetCountResult(MessagePayload.CompletionCodeEnum CompletionCode,
                               string ErrorDescription = null)
            : base(CompletionCode, ErrorDescription)
        { }
    }

    public partial class ResetCountHandler
    {
        private async Task<ResetCountCompletion.PayloadData> HandleResetCount(IResetCountEvents events, ResetCountCommand resetCount, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.ResetCount()");
            var result = await Device.ResetBinCount();
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.ResetCount() -> {result.CompletionCode}");

            return new ResetCountCompletion.PayloadData(result.CompletionCode, result.ErrorDescription);
        }
    }
}
