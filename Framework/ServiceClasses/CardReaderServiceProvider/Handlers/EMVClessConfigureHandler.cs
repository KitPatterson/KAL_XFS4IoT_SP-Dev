/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessConfigureHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class EMVClessConfigureHandler
    {

        private Task<EMVClessConfigureCompletion.PayloadData> HandleEMVClessConfigure(IEMVClessConfigureEvents events, EMVClessConfigureCommand eMVClessConfigure, CancellationToken cancel)
        {
            //ToDo: Implement HandleEMVClessConfigure for CardReader.
            
            #if DEBUG
                throw new NotImplementedException("HandleEMVClessConfigure for CardReader is not implemented in EMVClessConfigureHandler.cs");
            #else
                #error HandleEMVClessConfigure for CardReader is not implemented in EMVClessConfigureHandler.cs
            #endif
        }

    }
}
