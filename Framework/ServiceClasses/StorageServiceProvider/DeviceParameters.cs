/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.

\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.Storage.Commands;
using XFS4IoT.Storage.Completions;
using XFS4IoTFramework.Common;

namespace XFS4IoTFramework.Storage
{
    public sealed class StartExchangeResult : DeviceResult
    {
        public StartExchangeResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                   string ErrorDescription = null,
                                   StartExchangeCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public StartExchangeCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }
    }

    public sealed class EndExchangeResult : DeviceResult
    {
        public EndExchangeResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 string ErrorDescription = null,
                                 EndExchangeCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public EndExchangeCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }
    }

    public sealed record SetCardConfiguration
    {
        public SetCardConfiguration(int? Threshold,
                                    string CardId = null)
        {
            this.CardId = CardId;
            this.Threshold = Threshold;
        }

        public string CardId { get; set; }

        public int? Threshold { get; set; }
    }

    public sealed class SetCardUnitStorage
    {
        public SetCardUnitStorage(SetCardConfiguration Configuration,
                                  int? InitialCount)
        {
            this.Configuration = Configuration;
            this.InitialCount = InitialCount;
        }

        /// <summary>
        /// If this property is null, no need to change card unit configuration
        /// </summary>
        public SetCardConfiguration Configuration { get; init; }

        /// <summary>
        /// Set to InitialCount and Count, reset to RetainCount to zero
        /// If this property is not set, no need to update in the device class
        /// </summary>
        public int? InitialCount { get; init; }
    }

    public sealed class SetCardStorageRequest
    {
        public SetCardStorageRequest(Dictionary<string, SetCardUnitStorage> CardStorageToSet)
        {
            this.CardStorageToSet = CardStorageToSet;
        }

        public Dictionary<string, SetCardUnitStorage> CardStorageToSet { get; init; }
    }

    public sealed class SetCardStorageResult : DeviceResult
    {
        public SetCardStorageResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    string ErrorDescription = null,
                                    SetStorageCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            NewCardStorage = null;
        }

        public SetCardStorageResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                    Dictionary<string, SetCardUnitStorage> NewCardStorage)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.NewCardStorage = NewCardStorage;
        }

        public SetStorageCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        public Dictionary<string, SetCardUnitStorage> NewCardStorage { get; init; }
    }
}