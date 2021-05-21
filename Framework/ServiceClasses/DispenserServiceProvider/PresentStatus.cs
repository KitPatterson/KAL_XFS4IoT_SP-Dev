using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoTFramework.Dispenser;

namespace XFS4IoTServer.CashDispenser
{
    /// <summary>
    /// PresentStatus
    /// Representing the last dispense / presented status
    /// </summary>
    public class PresentStatus
    {
        /// <summary>
        /// Last dispense status
        /// </summary>
        public enum PresentStatusEnum
        {
            Presented,
            NotPresented,
            Unknown
        }

        public PresentStatus()
        { }

        /// <summary>
        /// Supplies the status of the last dispense or present operation
        /// </summary>
        public PresentStatusEnum Status { get; set; } = PresentStatusEnum.Unknown;

        /// <summary>
        /// Key is cash unit name and the value is the number of cash to be used
        /// </summary>
        public Denomination LastDenomination { get; private set; } = null;

        /// <summary>
        /// E2E token used last
        /// </summary>
        public string Token { get; set; } = null;
    }
}
