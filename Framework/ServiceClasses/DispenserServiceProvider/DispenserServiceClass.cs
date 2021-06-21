/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoTServer.CashDispenser;
using XFS4IoTFramework.Dispenser;
using XFS4IoTServer.Common;

namespace XFS4IoTServer
{
    public partial class DispenserServiceClass
    {
        public DispenserServiceClass(IServiceProvider ServiceProvider, ILogger logger, IPersistentData PersistentData)
            : this(ServiceProvider, logger)
        {
            this.PersistentData = PersistentData.IsNotNull($"No persistent data interface is set. " + typeof(Mix).FullName);

            // Load persistent data
            Dictionary<int, Mix> tableMixes = PersistentData.Load<Dictionary<int, Mix>>(typeof(Mix).FullName);
            if (tableMixes is not null)
            {
                // merge table mix set by the application
                foreach (var t in tableMixes)
                    AddMix(t.Key, t.Value);
            }
        }

        /// <summary>
        /// Add vendor specific mix algorithm
        /// </summary>
        /// <param name="mix">new mix algorithm to support for a customization</param>
        public void AddMix(int mixNumber, Mix mix)
        {
            if (Mixes.ContainsKey(mixNumber))
                Mixes.Remove(mixNumber);// replace algorithm
            Mixes.Add(mixNumber, mix);

            if (mix.Type == Mix.TypeEnum.Table)
            {
                // Save table mix set by the application
                Dictionary<int, Mix> tableMixes = PersistentData.Load<Dictionary<int, Mix>>(typeof(Mix).FullName);
                if (tableMixes is null)
                    tableMixes = new();

                if (tableMixes.ContainsKey(mixNumber))
                    tableMixes.Remove(mixNumber);// Replace exiting one
                tableMixes.Add(mixNumber, mix);

                if (!PersistentData.Store(typeof(Mix).FullName, tableMixes))
                {
                    Logger.Warning(Constants.Framework, "Failed to save persistent data." + typeof(Mix).FullName);
                }
            }
        }

        /// <summary>
        /// Return mix algorithm available
        /// </summary>
        /// <returns></returns>
        public Mix GetMix(int mixNumber)
        {
            if (Mixes.ContainsKey(mixNumber))
                return Mixes[mixNumber];
            return null;
        }

        public IEnumerator GetMixAlgorithms() => Mixes.Values.GetEnumerator();

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

        /// <summary>
        /// Persistent data storage access
        /// </summary>
        private readonly IPersistentData PersistentData;

        /// <summary>
        /// Supported Mix algorithm
        /// </summary>
        private readonly Dictionary<int, Mix> Mixes = new()
        {
            { 1, new MinNumberMix(1) },
            { 2, new EqualEmptyingMix(2) }
        };
    }
}
