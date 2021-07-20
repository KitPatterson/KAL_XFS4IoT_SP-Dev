/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Crypto interface.
 * CryptoDataHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Crypto.Commands;
using XFS4IoT.Crypto.Completions;

namespace XFS4IoTFramework.Crypto
{
    public partial class CryptoDataHandler
    {

        private Task<CryptoDataCompletion.PayloadData> HandleCryptoData(ICryptoDataEvents events, CryptoDataCommand cryptoData, CancellationToken cancel)
        {
            //ToDo: Implement HandleCryptoData for Crypto.
            
            #if DEBUG
                throw new NotImplementedException("HandleCryptoData for Crypto is not implemented in CryptoDataHandler.cs");
            #else
                #error HandleCryptoData for Crypto is not implemented in CryptoDataHandler.cs
            #endif
        }

    }
}
