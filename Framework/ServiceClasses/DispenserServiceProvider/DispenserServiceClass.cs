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
using XFS4IoTServer.CashDispenser;
using XFS4IoTFramework.Dispenser;
using XFS4IoTServer.Common;

namespace XFS4IoTServer
{
    public partial class DispenserServiceClass
    {

        /// <summary>
        /// Supported mix in the framework
        /// </summary>
        internal Dictionary<int, Mix> Mixes = new()
        {
            { 1, new MinNumberMix(1, Mix.TypeEnum.Algorithm, Mix.SubTypeEmum.MinimumNumberOfBills, "Minimum Number Of Bills") }
        };

        /// <summary>
        /// Keep last present status per position
        /// </summary>
        internal Dictionary<CashDispenserCapabilitiesClass.OutputPositionEnum, PresentStatus> LastPresentStatus = new()
        {
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom,  new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Center,  new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Default, new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Front,   new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Left,    new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Rear,    new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Right,   new PresentStatus() },
            { CashDispenserCapabilitiesClass.OutputPositionEnum.Top,     new PresentStatus() }
        };

        internal CommonServiceClass CommonService { get; set; }
        internal CashManagementServiceClass CashManagementService { get; set; }
    }
}
