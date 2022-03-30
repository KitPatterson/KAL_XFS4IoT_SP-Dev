/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CashAcceptor.Commands;
using XFS4IoT.CashAcceptor.Completions;
using XFS4IoTFramework.CashManagement;

namespace XFS4IoTFramework.CashAcceptor
{
    public partial class GetCashInStatusHandler
    {
        private Task<GetCashInStatusCompletion.PayloadData> HandleGetCashInStatus(IGetCashInStatusEvents events, GetCashInStatusCommand getCashInStatus, CancellationToken cancel)
        {
            CashAcceptor.CashInStatus.IsNotNull($"{nameof(CashAcceptor.CashInStatus)} is not set as expected.");

            return Task.FromResult(new GetCashInStatusCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                             string.Empty,
                                                                             CashAcceptor.CashInStatus.Status switch 
                                                                             {
                                                                                 CashInStatusClass.StatusEnum.Active => GetCashInStatusCompletion.PayloadData.StatusEnum.Active,
                                                                                 CashInStatusClass.StatusEnum.Ok => GetCashInStatusCompletion.PayloadData.StatusEnum.Ok,
                                                                                 CashInStatusClass.StatusEnum.Reset => GetCashInStatusCompletion.PayloadData.StatusEnum.Reset,
                                                                                 CashInStatusClass.StatusEnum.Retract => GetCashInStatusCompletion.PayloadData.StatusEnum.Retract,
                                                                                 CashInStatusClass.StatusEnum.Rollback => GetCashInStatusCompletion.PayloadData.StatusEnum.Rollback,
                                                                                 _ => GetCashInStatusCompletion.PayloadData.StatusEnum.Unknown,
                                                                             },
                                                                             CashAcceptor.CashInStatus.NumOfRefusedItems,
                                                                             CashAcceptor.CashInStatus.CashCounts?.CopyTo()));
        }
    }
}
