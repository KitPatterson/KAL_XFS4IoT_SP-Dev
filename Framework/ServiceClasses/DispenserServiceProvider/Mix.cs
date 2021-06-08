/***********************************************************************************************\
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
using XFS4IoT;
using XFS4IoTFramework.Dispenser;
using XFS4IoTServer.CashManagement;

namespace XFS4IoTServer.CashDispenser
{
    [Serializable()]
    public abstract class Mix
    {
        /// <summary>
        /// The mix type is an algorithm or a house mix table
        /// </summary>
        public enum TypeEnum
        {
            Table, 
            Algorithm
        }

        /// <summary>
        /// XFS Predefined mix algorithm
        /// </summary>
        public enum SubTypeEmum
        {
            Table = 0,
            MinimumNumberOfBills = 1,
            EqualEmptyingOfCashUnits = 2,
            MaximumNumberOfCashUnits = 3,
        }

        /// <summary>
        /// Mix Constructor
        /// </summary>
        public Mix(int MixNumber, 
                   TypeEnum Type, 
                   int SubType, 
                   string Name)
        {
            this.MixNumber = MixNumber;
            this.Type = Type;
            this.SubType = SubType;
            this.Name = Name;
        }

        /// <summary>
        /// Calculate mix depending on the algorithm derived
        /// </summary>
        /// <param name="CurrencyAmount"></param>
        /// <param name="CashUnits"></param>
        /// <returns></returns>
        public abstract Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, ILogger Logger);

        /// <summary>
        /// Specifies whether the mix type is an algorithm or a house mix table
        /// </summary>
        public TypeEnum Type { get; init; }

        /// <summary>
        /// Specifies predefined mix algorithm
        /// Individual vendor-defined mix algorithms are defined above hexadecimal 7FFF. 
        /// Mix algorithms which are provided by the Service Provider are in the range hexadecimal 8000 - 8FFF. 
        /// Application defined mix algorithms start at hexadecimal 9000. 
        /// All numbers below 8000 hexadecimal are reserved
        /// Predefined 1-3
        /// 1 = MINIMUM_NUMBER_OF_BILLS
        /// 2 = EQUAL_EMPTYING_OF_CASH_UNITS
        /// 3 = MAXIMUM_NUMBER_OF_CASH_UNITS
        /// </summary>
        public int SubType { get; init; }

        /// <summary>
        /// Number identifying the mix algorithm or the house mix table
        /// Individual vendor-defined mix algorithms are defined above hexadecimal 7FFF. 
        /// Mix algorithms which are provided by the Service Provider are in the range hexadecimal 8000 - 8FFF. 
        /// Application defined mix algorithms start at hexadecimal 9000. 
        /// All numbers below 8000 hexadecimal are reserved
        /// </summary>
        public int MixNumber { get; init; }

        /// <summary>
        /// Name of the mix table or algorithm
        /// </summary>
        public string Name { get; init; }
    }

    /// <summary>
    /// MixTable
    ///  A mix defined by a fixed table. There is one denomination defined for each value.
    /// </summary>
    [Serializable()]
    public sealed class MixTable : Mix
    {
        public sealed class MixRow
        {
            public MixRow(double Amount, List<int> Values)
            {
                this.Amount = Amount;
                this.Values = Values;
            }

            public double Amount { get; init; }

            public List<int> Values { get; init; }
        }

        public MixTable(int MixNumber,
                        string Name, 
                        List<double> Cols,
                        List<MixRow> Rows)
            : base(MixNumber, 
                   TypeEnum.Table,
                   (int)SubTypeEmum.Table, 
                   Name)
        {
            this.Cols = Cols;
            this.Rows = Rows;

            foreach (MixRow row in Rows)
            {
                double mixAmount = 0;
                for (int i=0; i<Cols.Count; i++)
                {
                    mixAmount += Cols[i] * row.Values[i];
                }

                if ((double)row.Amount != mixAmount)
                {
                    TableValid = false;
                    break;
                }
                else
                {
                    Mixes.Add(mixAmount, row.Values);
                }
                
                if (!TableValid)
                    break;
            }

            if (!TableValid)
                Mixes.Clear();
        }

        public override Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, ILogger Logger)
        {
            Contracts.Assert(false, "Table mix is not supported yet.");
            throw new NotImplementedException();
        }

        public bool TableValid { get; init; } = false;

        public List<double> Cols { get; init; }
                        
        public List<MixRow> Rows { get; init; }

        /// <summary>
        /// Key is a amount and value is a table of conbination of the mixes
        /// Key   - Amount
        /// Value - The quantity of each item denomination in the mix for the key
        /// </summary>
        public Dictionary<double, List<int>> Mixes { get; init; } = new();
    }

    /// <summary>
    /// MinNumberMix
    /// Select a mix requiring the minimum possible number of items
    /// </summary>
    [Serializable()]
    public sealed class MinNumberMix : Mix
    {
        public MinNumberMix(int MixNumber)
            : base(MixNumber, 
                   TypeEnum.Algorithm, 
                   (int)SubTypeEmum.MinimumNumberOfBills, 
                   "Minimum Number Of Bills")
        { }
           
        public override Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, ILogger Logger)
        {
            return new Denomination(null, null, Logger);
        }
    }
}
