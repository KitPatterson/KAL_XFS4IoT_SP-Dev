﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTServer.CashManagement
{
    /// <summary>
    /// Class structure used by the device class providing details of the items movement after the operation
    /// The framework updates an internal cash unit counts
    /// </summary>
    public sealed class ItemMovement
    {
        public ItemMovement(int? DispensedCount = null,
                            int? PresentedCount = null,
                            int? RetractedCount = null,
                            int? RejectCount = null,
                            List<BankNoteNumber> StoredBankNoteList = null)

        {
            this.DispensedCount = DispensedCount;
            this.PresentedCount = PresentedCount;
            this.RetractedCount = RetractedCount;
            this.RejectCount = RejectCount;
            this.StoredBankNoteList = StoredBankNoteList;
        }

        /// <summary>
        /// The number of items dispensed from this cash unit. 
        /// </summary>
        public int? DispensedCount { get; set; }

        /// <summary>
        /// The number of items from this cash unit that have been presented to the customer. 
        /// </summary>
        public int? PresentedCount { get; set; }

        /// <summary>
        /// The number of items that have been accessible to a customer and retracted into the 
        /// cash unit.
        /// </summary>
        public int? RetractedCount { get; set; }

        /// <summary>
        /// The number of items dispensed from this cash unit which have been rejected.
        /// </summary>
        public int? RejectCount { get; set; }

        /// <summary>
        /// List of banknote numbers have been moved to the cash unit
        /// </summary>
        public List<BankNoteNumber> StoredBankNoteList { get; set; }
    }
}
