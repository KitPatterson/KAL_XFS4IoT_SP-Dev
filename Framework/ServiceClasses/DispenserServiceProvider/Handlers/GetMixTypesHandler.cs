/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * GetMixTypesHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;

namespace XFS4IoTFramework.Dispenser
{
    public partial class GetMixTypesHandler
    {

        private Task<GetMixTypesCompletion.PayloadData> HandleGetMixTypes(IGetMixTypesEvents events, GetMixTypesCommand getMixTypes, CancellationToken cancel)
        {
            //ToDo: Implement HandleGetMixTypes for Dispenser.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetMixTypes for Dispenser is not implemented in GetMixTypesHandler.cs");
            #else
                #error HandleGetMixTypes for Dispenser is not implemented in GetMixTypesHandler.cs
            #endif
        }

    }
}
