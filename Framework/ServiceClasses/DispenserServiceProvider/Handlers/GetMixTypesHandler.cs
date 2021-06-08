/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;
using XFS4IoT.Completions;
using XFS4IoTServer.CashDispenser;

namespace XFS4IoTFramework.Dispenser
{
    public partial class GetMixTypesHandler
    {
        private Task<GetMixTypesCompletion.PayloadData> HandleGetMixTypes(IGetMixTypesEvents events, GetMixTypesCommand getMixTypes, CancellationToken cancel)
        {
            DispenserServiceClass CashDispenserService = Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");

            List<GetMixTypesCompletion.PayloadData.MixTypesClass> mixes = new();

            foreach (var mix in CashDispenserService.Mixes)
            {
                GetMixTypesCompletion.PayloadData.MixTypesClass.MixTypeEnum type = mix.Value.Type switch
                {
                    Mix.TypeEnum.Algorithm => GetMixTypesCompletion.PayloadData.MixTypesClass.MixTypeEnum.MixAlgorithm,
                    _ => GetMixTypesCompletion.PayloadData.MixTypesClass.MixTypeEnum.MixTable
                };

                mixes.Add(new GetMixTypesCompletion.PayloadData.MixTypesClass(mix.Value.MixNumber, 
                                                                              type, 
                                                                              (int)mix.Value.SubType, 
                                                                              mix.Value.Name));
            }

            return Task.FromResult(new GetMixTypesCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                         null,
                                                                         mixes));
        }
    }
}
