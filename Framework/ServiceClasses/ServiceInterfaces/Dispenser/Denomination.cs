using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoTFramework.CashManagement;

namespace XFS4IoTFramework.Dispenser
{
    /// <summary>
    /// DenominationBase
    /// Representing output data of the Denominate and PresentStatus
    /// </summary>
    public class Denomination
    {
        public Denomination(Dictionary<string, double> CurrencyAmounts,
                            Dictionary<string, int> Values = null)
        {
            this.CurrencyAmounts = CurrencyAmounts;
            this.Values = Values;
        }

        /// <summary>
        /// Currencies to use for dispensing cash
        /// </summary>
        public Dictionary<string, double> CurrencyAmounts { get; set; }

        /// <summary>
        /// Key is cash unit name and the value is the number of cash to be used
        /// </summary>
        public Dictionary<string, int> Values { get; set; }

    }
}
