/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * GetCommandRandomNumberHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/

using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    public partial class GetCommandRandomNumberHandler
    {
        private async Task<GetCommandRandomNumberCompletion.PayloadData> HandleGetCommandRandomNumber(IGetCommandRandomNumberEvents events, GetCommandRandomNumberCommand getCommandRandomNumber, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CommonDev.GetCommandRandomNumber()");
            var result = await Device.GetCommandRandomNumber();
            Logger.Log(Constants.DeviceClass, $"CommonDev.GetCommandRandomNumber() -> {result.CompletionCode}");

            /// TODO: validate returned token

            return new GetCommandRandomNumberCompletion.PayloadData(result.CompletionCode,
                                                                    result.ErrorDescription,
                                                                    result.CommandRandomNumber);
        }

    }
}
