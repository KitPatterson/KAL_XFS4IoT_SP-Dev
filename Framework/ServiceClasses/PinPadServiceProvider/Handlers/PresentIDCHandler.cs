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
using XFS4IoT.PinPad.Commands;
using XFS4IoT.PinPad.Completions;

namespace XFS4IoTFramework.PinPad
{
    public partial class PresentIDCHandler
    {

        private Task<PresentIDCCompletion.PayloadData> HandlePresentIDC(IPresentIDCEvents events, PresentIDCCommand presentIDC, CancellationToken cancel)
        {
            throw new NotImplementedException("HandlePresentIDC for PinPad is not implemented in PresentIDCHandler.cs");
        }
    }
}
