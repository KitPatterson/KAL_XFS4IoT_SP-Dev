/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System.Linq;
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
    public partial class GetMixTableHandler
    {
        private Task<GetMixTableCompletion.PayloadData> HandleGetMixTable(IGetMixTableEvents events, GetMixTableCommand getMixTable, CancellationToken cancel)
        {
            Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");
            DispenserServiceClass CashDispenserService = Dispenser as DispenserServiceClass;

            if (getMixTable.Payload.MixNumber is null ||
                !CashDispenserService.Mixes.ContainsKey((int)getMixTable.Payload.MixNumber) ||
                CashDispenserService.Mixes[(int)getMixTable.Payload.MixNumber].Type != Mix.TypeEnum.Table)
            {
                return Task.FromResult(new GetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData, 
                                                                             $"Invalid mix number supplied. {getMixTable.Payload.MixNumber}",
                                                                             GetMixTableCompletion.PayloadData.ErrorCodeEnum.InvalidMixNumber));
            }

            CashDispenserService.Mixes[(int)getMixTable.Payload.MixNumber].IsA<MixTable>($"Unexpected internal mix type received. {CashDispenserService.Mixes[(int)getMixTable.Payload.MixNumber].GetType()}");
            MixTable mixTable = CashDispenserService.Mixes[(int)getMixTable.Payload.MixNumber] as MixTable;

            List<GetMixTableCompletion.PayloadData.MixRowsClass> mixRows = new();
            foreach (var table in mixTable.Mixes)
            {
                mixRows.Add(new GetMixTableCompletion.PayloadData.MixRowsClass(table.Key,
                                                                               table.Value.Select(t => t).ToList()));
            }

            /// XFS YAML isn't right on preview 4
            return Task.FromResult(new GetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                         null,
                                                                         null,
                                                                         CashDispenserService.Mixes[(int)getMixTable.Payload.MixNumber].MixNumber,
                                                                         CashDispenserService.Mixes[(int)getMixTable.Payload.MixNumber].Name,
                                                                         mixTable.Cols.Select(c => c).ToList(),
                                                                         mixRows));
        }
    }
}
