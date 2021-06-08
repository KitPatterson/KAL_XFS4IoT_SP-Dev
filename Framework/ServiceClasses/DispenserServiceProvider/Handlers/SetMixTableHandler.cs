/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System;
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
    public partial class SetMixTableHandler
    {
        private Task<SetMixTableCompletion.PayloadData> HandleSetMixTable(ISetMixTableEvents events, SetMixTableCommand setMixTable, CancellationToken cancel)
        {
            if (setMixTable.Payload.MixNumber is null ||
                setMixTable.Payload.Name is null)
            {
                return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                             "Supplied MixNumber or Name is null."));
            }

            if (setMixTable.Payload.MixHeader is null ||
                setMixTable.Payload.MixHeader.Count == 0)
            {
                return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                             "Supplied MixHeader is empty."));
            }

            if (setMixTable.Payload.MixRows is null ||
                setMixTable.Payload.MixRows.Count == 0)
            {
                return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                             "Supplied MixRows is empty."));
            }

            DispenserServiceClass CashDispenserService = Dispenser.IsA<DispenserServiceClass>($"Unexpected object is specified. {nameof(Dispenser)}.");

            Dictionary<List<string>, Dictionary<double, Denomination>> mixes = new();

            foreach (double? d in setMixTable.Payload.MixHeader)
            {
                if (d is null)
                {
                    return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                 "Supplied value in the MixHeader is null."));
                }
            }

            List<MixTable.MixRow> mixRow = new();
            foreach (SetMixTableCommand.PayloadData.MixRowsClass row in setMixTable.Payload.MixRows)
            {
                if (row.Amount is null)
                {
                    return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                 "Supplied amount in the MixRows is null."));
                }

                if (setMixTable.Payload.MixHeader.Count != row.Mixture.Count)
                {
                    return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                 "Supplied mixHeader and the mixRow.Mixture size is different."));
                }

                foreach (int? v in row.Mixture)
                {
                    if (v is null)
                    {
                        return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                     "Supplied value in the Mixture is null."));
                    }
                }

                mixRow.Add(new MixTable.MixRow((double)row.Amount, row.Mixture.Select(r => (int)r).ToList()));
            }

            MixTable mixTable = new((int)setMixTable.Payload.MixNumber,
                                    setMixTable.Payload.Name,
                                    setMixTable.Payload.MixHeader.Select(c => (double)c).ToList(),
                                    mixRow);

            if (!mixTable.TableValid)
            {
                return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                       "Supplied amount and mixture are not identical."));
            }

            CashDispenserService.Mixes.Add(mixTable.MixNumber, mixTable);

            return Task.FromResult(new SetMixTableCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, null));
        }
    }
}
