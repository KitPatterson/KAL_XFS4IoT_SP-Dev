/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Dispenser
{
    public partial class DenominateHandler
    {
        private Task<DenominateCompletion.PayloadData> HandleDenominate(IDenominateEvents events, DenominateCommand denominate, CancellationToken cancel)
        {
            /*
            ////////////////////////////////////////////////////////////////////////////
            // 1) Check that a given denomination can currently be paid out.
            if (denominate.Payload.MixNumber == 0 &&
                denominate.Payload.Denomination.Amount == 0)
            {
                
            }
            ////////////////////////////////////////////////////////////////////////////
            //  2) Test that a given amount matches a given denomination.
            else if (denominate.Payload.MixNumber == 0 &&
                     denominate.Payload.Denomination.Amount != 0)
            {
                
            }
            ////////////////////////////////////////////////////////////////////////////
            //  3) Calculate the denomination, given an amount and mix number.
            else if (denominate.Payload.MixNumber != 0 &&
                     denominate.Payload.Denomination.Values.Count == 0)
            {
                
            }
            ////////////////////////////////////////////////////////////////////////////
            //  4) Complete a partially specified denomination for a given amount.
            else if (denominate.Payload.MixNumber != 0 &&
                     denominate.Payload.Denomination.Values.Count != 0)
            {
                
            }
            else
            {
                Contracts.Assert(false, $"Unreachable code.");
            }
            */

            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InternalError, null));
        }
    }
}
