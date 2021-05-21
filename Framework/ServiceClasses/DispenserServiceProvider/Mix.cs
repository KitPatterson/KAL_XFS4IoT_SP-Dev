using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoTFramework.Dispenser;

namespace XFS4IoTServer.CashDispenser
{
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
            MinimumNumberOfBills,
            EqualEmptyingOfCashUnits,
            MaximumNumberOfCashUnits,
            Table
        }

        /// <summary>
        /// Mix Constructor
        /// </summary>
        /// <param name="MixNumber"></param>
        /// <param name="Type"></param>
        /// <param name="SubType"></param>
        /// <param name="Name"></param>
        public Mix(int MixNumber, TypeEnum Type, SubTypeEmum SubType, string Name)
        {
            this.MixNumber = MixNumber;
            this.Type = Type;
            this.SubType = SubType;
            this.Name = Name;
        }

        /// <summary>
        /// Calculate mix depending on the algorithm derived
        /// </summary>
        /// <param name="Currency"></param>
        /// <param name="Amount"></param>
        /// <param name="denomination"></param>
        /// <returns></returns>
        public abstract Denomination Calculate(string Currency, double Amount /*Need cash unit counts/status*/);

        /// <summary>
        /// Specifies whether the mix type is an algorithm or a house mix table
        /// </summary>
        public TypeEnum Type { get; private set; }

        /// <summary>
        /// Specifies predefined mix algorithm
        /// </summary>
        public SubTypeEmum SubType { get; private set; }

        /// <summary>
        /// Number identifying the mix algorithm or the house mix table
        /// Individual vendor-defined mix algorithms are defined above hexadecimal 7FFF. 
        /// Mix algorithms which are provided by the Service Provider are in the range hexadecimal 8000 - 8FFF. 
        /// Application defined mix algorithms start at hexadecimal 9000. 
        /// All numbers below 8000 hexadecimal are reserved
        /// </summary>
        public int MixNumber { get; private set; }

        /// <summary>
        /// Name of the mix table or algorithm
        /// </summary>
        public string Name { get; private set; }

    }

    /// <summary>
    /// MixTable
    ///  A mix defined by a fixed table. There is one denomination defined for each value.
    /// </summary>
    public sealed class MixTable : Mix
    {
        public sealed class MixRow
        {
            public MixRow(double Amount, List<int> Values)
            {
                this.Amount = Amount;
                this.Values = Values;
            }

            public double Amount { get; private set; }

            public List<int> Values { get; private set; }
        }

        public MixTable(int MixNumber,
                        string Name, 
                        List<double> Cols,
                        List<MixRow> Rows)
            : base(MixNumber, TypeEnum.Table, SubTypeEmum.Table, Name)
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

        public override Denomination Calculate(string Currency, double Amount /*Need cash unit counts/status*/)
        {
            Contracts.Assert(false, "Table mix is not supported yet.");
            throw new NotImplementedException();
        }

        public bool TableValid { get; private set; } = false;

        public List<double> Cols { get; private set; }
                        
        public List<MixRow> Rows { get; private set; }

        /// <summary>
        /// Key is a amount and value is a table of conbination of the mixes
        /// Key   - Amount
        /// Value - The quantity of each item denomination in the mix for the key
        /// </summary>
        public Dictionary<double, List<int>> Mixes { get; private set; } = new();
    }

    /// <summary>
    /// MinNumberMix
    /// Select a mix requiring the minimum possible number of items
    /// </summary>
    public sealed class MinNumberMix : Mix
    {
        public MinNumberMix(int MixNumber, 
                            TypeEnum Type, 
                            SubTypeEmum SubType, 
                            string Name)
            : base(MixNumber, Type, SubType, Name)
        { }
           
        public override Denomination Calculate(string Currency, double Amount /*Need cash unit counts/status*/)
        {
            // TODO
            return new Denomination(null, null);
        }
    }
}
