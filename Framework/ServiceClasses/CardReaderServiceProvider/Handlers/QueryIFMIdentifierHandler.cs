/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * QueryIFMIdentifierHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class QueryIFMIdentifierHandler
    {
        /// <summary>
        /// IFMIdentifierInfo
        /// Provide IFM identifier information
        /// </summary>
        public sealed class IFMIdentifierInfo
        {
            public IFMIdentifierInfo(QueryIFMIdentifierCompletion.PayloadData.IfmAuthorityEnum IFMAuthority,
                                     List<byte> IFMIdentifier)
            {
                this.IFMAuthority = IFMAuthority;
                this.IFMIdentifier = IFMIdentifier;
            }

            public QueryIFMIdentifierCompletion.PayloadData.IfmAuthorityEnum IFMAuthority { get; private set; }
            /// <summary>
            /// The IFM Identifier of the chip card reader (or IFM) as assigned by the specified authority.
            /// </summary>
            public List<byte> IFMIdentifier { get; private set; }
        }

        /// <summary>
        /// QueryIFMIdentifierResult
        /// Return information for IFM identifiers
        /// </summary>
        public sealed class QueryIFMIdentifierResult : DeviceResult
        {
            public QueryIFMIdentifierResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                            string ErrorDescription = null,
                                            List<IFMIdentifierInfo> IFMIdentifiers = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.IFMIdentifiers = IFMIdentifiers;
            }

            public QueryIFMIdentifierResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                            List<IFMIdentifierInfo> IFMIdentifiers = null)
                : base(CompletionCode, null)
            {
                this.IFMIdentifiers = IFMIdentifiers;
            }

            public List<IFMIdentifierInfo> IFMIdentifiers;
        }

        private async Task<QueryIFMIdentifierCompletion.PayloadData> HandleQueryIFMIdentifier(IQueryIFMIdentifierEvents events, QueryIFMIdentifierCommand queryIFMIdentifier, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.QueryIFMIdentifier()");
            var result = await Device.QueryIFMIdentifier();
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.QueryIFMIdentifier() -> {result.CompletionCode}");

            /// XFS4IoT spec has a BUG, only one output structure allows for the IFM information and should be array. April 2021 preview
            if (result.IFMIdentifiers is not null && 
                result.IFMIdentifiers.Count > 0 &&
                result.IFMIdentifiers[0].IFMIdentifier.Count > 0)
            {
                return new QueryIFMIdentifierCompletion.PayloadData(result.CompletionCode,
                                                                    result.ErrorDescription,
                                                                    result.IFMIdentifiers[0].IFMAuthority,
                                                                    Convert.ToBase64String(result.IFMIdentifiers[0].IFMIdentifier.ToArray()));
            }

            return new QueryIFMIdentifierCompletion.PayloadData(result.CompletionCode,
                                                                result.ErrorDescription);
        }
    }
}
