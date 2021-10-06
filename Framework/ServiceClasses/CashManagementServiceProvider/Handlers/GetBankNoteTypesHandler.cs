/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * GetBankNoteTypesHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CashManagement.Commands;
using XFS4IoT.CashManagement.Completions;

namespace XFS4IoTFramework.CashManagement
{
    public partial class GetBankNoteTypesHandler
    {

        private Task<GetBankNoteTypesCompletion.PayloadData> HandleGetBankNoteTypes(IGetBankNoteTypesEvents events, GetBankNoteTypesCommand getBankNoteTypes, CancellationToken cancel)
        {
            //ToDo: Implement HandleGetBankNoteTypes for CashManagement.
            
            #if DEBUG
                throw new NotImplementedException("HandleGetBankNoteTypes for CashManagement is not implemented in GetBankNoteTypesHandler.cs");
            #else
                #error HandleGetBankNoteTypes for CashManagement is not implemented in GetBankNoteTypesHandler.cs
            #endif
        }

    }
}
