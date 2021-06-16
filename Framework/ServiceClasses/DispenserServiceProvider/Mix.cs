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
using XFS4IoT;
using XFS4IoTServer;
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
        /// <param name="CurrencyAmounts">Currency and amounts to denominate</param>
        /// <param name="CashUnits">Cash units to go through</param>
        /// <param name="MaxDispensableItems">Maximum number of items can be dispensed to the stacker.</param>
        /// <returns></returns>
        public abstract Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, int MaxDispensableItems, ILogger Logger);

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


        /// <summary>
        /// Find the cash unit with the highest value notes which are lower that the given value. If CurrentGreatest is zero, find the greatest of all notes.
        /// </summary>
        /// <param name="CurrentGreatest">Find the next value smaller than this. If this is zero, the greatest value available.</param>
        /// <param name="CurrencyID">Currency to use. Ignore other currencies.</param>
        /// <param name="CashUnits">The cash units to search through.</param>
        /// <returns></returns>
        protected static string FindNextGreatest(double CurrentGreatest, string CurrencyID, Dictionary<string, CashUnit> CashUnits, ref List<string> UnitsUsed)
        {
            double currentBiggest = 0;
            string biggestCashUnit = string.Empty;

            // Find the greatest value note of this currency. This will be the first 
            // note used.
            if (CurrentGreatest == 0)
            {
                UnitsUsed.Clear();

                foreach (var unit in CashUnits)
                {
                    if ((unit.Value.Type ==  CashUnit.TypeEnum.BillCassette ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                         unit.Value.Type == CashUnit.TypeEnum.Recycling)
                        && unit.Value.CurrencyID == CurrencyID
                        //This check is done elsewhere
                        && unit.Value.Value > currentBiggest
                        && (unit.Value.Status == CashUnit.StatusEnum.Ok ||
                            unit.Value.Status == CashUnit.StatusEnum.Low ||
                            unit.Value.Status == CashUnit.StatusEnum.High ||
                            unit.Value.Status == CashUnit.StatusEnum.Full)
                        && !unit.Value.AppLock)
                    {
                        currentBiggest = unit.Value.Value;
                        biggestCashUnit = unit.Key;
                    }
                }
            }
            else
            {
                foreach (var unit in CashUnits)
                {
                    if ((unit.Value.Type == CashUnit.TypeEnum.BillCassette ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                         unit.Value.Type == CashUnit.TypeEnum.Recycling)
                        && unit.Value.CurrencyID == CurrencyID
                        //This check is done elsewhere
                        && unit.Value.Value > 0
                        && unit.Value.Value >= currentBiggest
                        && unit.Value.Value <= CurrentGreatest
                        && !UnitsUsed.Contains(unit.Key)
                        && (unit.Value.Status == CashUnit.StatusEnum.Ok ||
                            unit.Value.Status == CashUnit.StatusEnum.Low ||
                            unit.Value.Status == CashUnit.StatusEnum.High ||
                            unit.Value.Status == CashUnit.StatusEnum.Full)
                        && !unit.Value.AppLock)
                    {
                        currentBiggest = unit.Value.Value;
                        biggestCashUnit = unit.Key;
                    }

                }
            }

            if (!string.IsNullOrEmpty(biggestCashUnit) &&
                !UnitsUsed.Contains(biggestCashUnit))
            {
                UnitsUsed.Add(biggestCashUnit);
            }

            return biggestCashUnit;
        }

        /// <summary>
        /// Find the cash unit with the smallest value notes which are lower that the given value. If CurrentGreatest is zero, find the greatest of all notes.
        /// </summary>
        /// <param name="CurrentLeast">Find the next value smaller than this. If this is zero, find the greatest value available.</param>
        /// <param name="CurrencyID">Currency to use. Ignore other currencies.</param>
        /// <param name="CashUnits">The cash units to search through.</param>
        /// <returns></returns>
        protected static string FindNextLeast(double CurrentLeast, string CurrencyID, Dictionary<string, CashUnit> CashUnits, ref List<string> UnitsUsed)
        {
            double currentSmallest = -1;
            string smallestCashUnit = string.Empty;

            // Find the greatest value note of this currency. This will be the first 
            // note used.
            if (CurrentLeast == 0)
            {
                UnitsUsed.Clear();

                foreach (var unit in CashUnits)
                {
                    if ((unit.Value.Type == CashUnit.TypeEnum.BillCassette ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                         unit.Value.Type == CashUnit.TypeEnum.Recycling)
                        && unit.Value.CurrencyID == CurrencyID
                        //This check is done elsewhere
                        && unit.Value.Value < currentSmallest
                        && (unit.Value.Status == CashUnit.StatusEnum.Ok ||
                            unit.Value.Status == CashUnit.StatusEnum.Low ||
                            unit.Value.Status == CashUnit.StatusEnum.High ||
                            unit.Value.Status == CashUnit.StatusEnum.Full)
                        && !unit.Value.AppLock)
                    {
                        currentSmallest = unit.Value.Value;
                        smallestCashUnit = unit.Key;
                    }
                }
            }
            else
            {
                foreach (var unit in CashUnits)
                {
                    if ((unit.Value.Type == CashUnit.TypeEnum.BillCassette ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                         unit.Value.Type == CashUnit.TypeEnum.Recycling)
                        && unit.Value.CurrencyID == CurrencyID
                        //This check is done elsewhere
                        && unit.Value.Value > 0
                        && unit.Value.Value <= currentSmallest
                        && unit.Value.Value >= CurrentLeast
                        && !UnitsUsed.Contains(unit.Key)
                        && (unit.Value.Status == CashUnit.StatusEnum.Ok ||
                            unit.Value.Status == CashUnit.StatusEnum.Low ||
                            unit.Value.Status == CashUnit.StatusEnum.High ||
                            unit.Value.Status == CashUnit.StatusEnum.Full)
                        && !unit.Value.AppLock)
                    {
                        currentSmallest = unit.Value.Value;
                        smallestCashUnit = unit.Key;
                    }
                }
            }

            if (!string.IsNullOrEmpty(smallestCashUnit) &&
                !UnitsUsed.Contains(smallestCashUnit))
            {
                UnitsUsed.Add(smallestCashUnit);
            }

            return smallestCashUnit;
        }

        /// <summary>
        /// Find the cash unit with the most full notes are lower that the given value. If CurrentGreatest is zero, find the greatest of all notes.
        /// </summary>
        /// <param name="LastMostFull">Find the next value smaller than this. If this is zero, find the greatest value available.</param>
        /// <param name="CurrencyID">Currency to use. Ignore other currencies.</param>
        /// <param name="CashUnits">The cash units to search through.</param>
        /// <returns></returns>
        protected static string FindNextMostFull(double LastMostFull, string CurrencyID, Dictionary<string, CashUnit> CashUnits, List<string> UnitsUsed)
        {
            double currentMostFull = 0;
            string mostFullCashUnit = string.Empty;

            // Find the greatest value note of this currency. This will be the first 
            // note used.
            if (LastMostFull == 0)
            {
                UnitsUsed.Clear();

                foreach (var unit in CashUnits)
                {
                    if ((unit.Value.Type == CashUnit.TypeEnum.BillCassette ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                         unit.Value.Type == CashUnit.TypeEnum.Recycling)
                        && unit.Value.CurrencyID == CurrencyID
                        //This check is done elsewhere
                        && unit.Value.Count > currentMostFull
                        && (unit.Value.Status == CashUnit.StatusEnum.Ok ||
                            unit.Value.Status == CashUnit.StatusEnum.Low ||
                            unit.Value.Status == CashUnit.StatusEnum.High ||
                            unit.Value.Status == CashUnit.StatusEnum.Full)
                        && !unit.Value.AppLock)
                    {
                        currentMostFull = unit.Value.Count;
                        mostFullCashUnit = unit.Key;
                    }
                }
            }
            else
            {
                foreach (var unit in CashUnits)
                {
                    if ((unit.Value.Type == CashUnit.TypeEnum.BillCassette ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinCylinder ||
                         unit.Value.Type == CashUnit.TypeEnum.CoinDispenser ||
                         unit.Value.Type == CashUnit.TypeEnum.Recycling)
                        && unit.Value.CurrencyID == CurrencyID
                        //This check is done elsewhere
                        && unit.Value.Count > 0
                        && unit.Value.Count >= currentMostFull
                        && unit.Value.Count <= LastMostFull
                        && !UnitsUsed.Contains(unit.Key)
                        && (unit.Value.Status == CashUnit.StatusEnum.Ok ||
                            unit.Value.Status == CashUnit.StatusEnum.Low ||
                            unit.Value.Status == CashUnit.StatusEnum.High ||
                            unit.Value.Status == CashUnit.StatusEnum.Full)
                        && !unit.Value.AppLock)
                    {
                        currentMostFull = unit.Value.Count;
                        mostFullCashUnit = unit.Key;
                    }
                }
            }

            if (!string.IsNullOrEmpty(mostFullCashUnit) &&
                !UnitsUsed.Contains(mostFullCashUnit))
            {
                UnitsUsed.Add(mostFullCashUnit);
            }

            return mostFullCashUnit;
        }
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

        public override Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, int MaxDispensableItems, ILogger Logger)
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

        /// <summary>
        /// Calculate a dispensable denomination with a given value, using the  number of bills.
        /// 
        ///     8 5 1
        /// 10  1   2   3
        /// 10  0 2 0   2
        ///
        ///     8 5
        /// 10    2
        ///
        ///     10 8 5 1
        /// 20   1 1   2   3
        /// 20   1 0 2 0   2
        /// </summary>
        /// <param name="CurrencyAmounts">Value to denominate and Currency to denominate in.</param>
        /// <param name="CashUnits">cash units to denominate from.</param>
        /// <param name="Logger"></param>
        /// <returns>denominate to dispense</returns>
        public override Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, int MaxDispensableItems, ILogger Logger)
        {
            CurrencyAmounts.IsNotNull($"Currency and amounts should be provided to calculate mix.");
            CashUnits.IsNotNull($"Cash unit information should be provided to calculate mix.");

            //Array to make sure use cassettes only once
            List<string> CassetteUsed = new();

            Dictionary<string, int> totalSmallest = new();

            foreach (var ca in CurrencyAmounts)
            {
                // Find the greatest value note of this currency. This will be the first 
                // cash unit used.
                string currentCashUnit = FindNextGreatest(0, ca.Key, CashUnits, ref CassetteUsed);
                if (string.IsNullOrEmpty(currentCashUnit))
                {
                    return new Denomination(CurrencyAmounts);
                }

                // Find the greatest number from this cash unit which could be needed, and  can be 
                // dispensed. If we can't get enough notes we have to make up the rest with smaller
                // denominations.
                int greatestNumNotes;
                if (CashUnits[currentCashUnit].Value != 0)
                {
                    greatestNumNotes = (int)(ca.Value / CashUnits[currentCashUnit].Value);
                }
                else
                {
                    greatestNumNotes = 0;
                }
                if (greatestNumNotes > CashUnits[currentCashUnit].Count)
                    greatestNumNotes = CashUnits[currentCashUnit].Count;

                // We will have to dispense at least this many notes. If this is too many notes, 
                // there is no point in continuing the calculation.
                if (greatestNumNotes > MaxDispensableItems)
                {
                    return new Denomination(CurrencyAmounts);
                }

                double remainingAmount;
                int currentSmallestDenomNumNotes = 0;
                int currentSmallestNumNotes = greatestNumNotes;
                bool foundOne = false;
                int newDenomNumNotes;
                Dictionary<string, int> currentSmallest = new ();
                Dictionary<string, int> newDenom = new ();

                // Loop through all the posible dispenses from this cash unit to find the smallest.
                for (int numNotes = greatestNumNotes; numNotes >= 0; numNotes--)
                {
                    remainingAmount = ca.Value - (numNotes * CashUnits[currentCashUnit].Value);
                    // If the remaining amount is zero, we know that the denomination will be null
                    if (remainingAmount == 0)
                    {
                        newDenomNumNotes = 0;
                        newDenom.Clear();
                    }
                    else
                    {
                        if (!CalculateR(remainingAmount, ca.Key, currentCashUnit, CashUnits, ref CassetteUsed, ref newDenom)) 
                            continue; // If we can't do this bit of the mix, ignore it and go on to the next possibility. 

                        newDenomNumNotes = currentSmallest.Select(d => d.Value).Sum();
                    }
                    // Always take the first one, else take it only if it is smaller than the previouse smallest
                    // and this number of notes can be dispensed from this cash unit
                    if (foundOne == false ||
                        (numNotes < CashUnits[currentCashUnit].Count &&
                         newDenomNumNotes + numNotes < currentSmallestDenomNumNotes + currentSmallestNumNotes)
                        )
                    {
                        foundOne = true;
                        currentSmallestNumNotes = numNotes;
                        currentSmallestDenomNumNotes = newDenomNumNotes;
                        currentSmallest = newDenom;
                    }
                }

                if (currentSmallest.ContainsKey(currentCashUnit))
                    currentSmallest[currentCashUnit] = currentSmallestNumNotes;
                else
                    currentSmallest.Add(currentCashUnit, currentSmallestNumNotes);

                foreach (var smallest in currentSmallest)
                {
                    if (totalSmallest.ContainsKey(smallest.Key))
                        totalSmallest[smallest.Key] = smallest.Value;
                    else
                        totalSmallest.Add(smallest.Key, smallest.Value);
                }
            }

            // At this point. CurrentSmallestNumNotes is the number of notes from cash unit we should use
            // and CurrentSmallest is the smallest denomination required from the rest of the cash units
            // Add the notes from this cash unit to the denomination and return it.
            Denomination denom = new(CurrencyAmounts, totalSmallest);

            // Check that this denomination can actually be dispensed.
            if (denom.IsDispensable(CashUnits, Logger) != Denomination.DispensableResultEnum.Good)
            {
                return new Denomination(CurrencyAmounts);
            }

            return denom;
        }

        /// <summary>
        /// Recursive version of Calculate.
        /// Calculates the smallest denomination for the given amount, (and currency and cash units) from the cash units with values lower than the value of the given cash unit.
        /// Smallest denomination is 
        /// Min(PosibleNumberOfCurrentCashUnit + CalcualteR(CurrentCashUnit, Remainder))
        /// </summary>
        /// <param name="Amount">Amount to find the minimum denomination for.</param>
        /// <param name="Currency">Currency of notes to use</param>
        /// <param name="LastCashUnit">Last cash unit that was used. </param>
        /// <param name="CashUnits">cash units to use</param>
        /// <param name="UnitsUsed">List of used cash units on mixing</param>
        /// <param name="Denom">Denomination to dispense</param>
        /// <returns></returns>
        public static bool CalculateR(double Amount, string Currency, string LastCashUnit, Dictionary<string, CashUnit> CashUnits, ref List<string> UnitsUsed, ref Dictionary<string, int> Denom)
        {
            Contracts.Assert(Amount != 0, "Invalid parameter used for amount in CalculateR.");

            // Find the next cash unit to check
            string currentCashUnit = FindNextGreatest(CashUnits[LastCashUnit].Value, Currency, CashUnits, ref UnitsUsed);
            // We've run out of cash units. The remaining amount shouldn't be zero so we can't dispense.
            if (string.IsNullOrEmpty(currentCashUnit))
                return false;

            // Check now if this is the last cash unit.
            bool finalCashUnit = false;
            List<string> dummy = new();
            string nextCashUnit = FindNextGreatest(CashUnits[currentCashUnit].Value, Currency, CashUnits, ref dummy);
            if (string.IsNullOrEmpty(nextCashUnit))
                finalCashUnit = true;

            // Find the greatest number from this cash unit which could be needed, and  can be 
            // dispensed. If we can't get enough notes we have to make up the rest with smaller
            // denominations.
            int greatestNumNotes;
            if (CashUnits[currentCashUnit].Value != 0)
            {
                greatestNumNotes = (int)(Amount / CashUnits[currentCashUnit].Value);
            }
            else
            {
                greatestNumNotes = 0;
            }

            if (greatestNumNotes > CashUnits[currentCashUnit].Count)
                greatestNumNotes = CashUnits[currentCashUnit].Count;

            double remainingAmount;
            int currentSmallestDenomNumNotes = 0;
            int currentSmallestNumNotes = greatestNumNotes;
            bool foundOne = false;
            int newDenomNumNotes;
            Dictionary<string, int> newDenom = new();
            //  Loop through all the posible dispenses from this cash unit to find the 
            // smallest.
            for (int numNotes = greatestNumNotes; numNotes >= 0; numNotes--)
            {
                remainingAmount = Amount - (numNotes * CashUnits[currentCashUnit].Value);
                // If the remaining amount isn't zero, and this is the last cash unit, then 
                // we can't mix the remaining amount, so this number of notes is invalid.
                if (remainingAmount != 0 && 
                    finalCashUnit == true)
                    continue;

                // If the remaining amount is zero, we know that the denomination will be null
                if (remainingAmount == 0)
                {
                    newDenom.Clear();
                    newDenomNumNotes = 0;
                }
                else
                {
                    if (!CalculateR(remainingAmount, Currency, currentCashUnit, CashUnits, ref UnitsUsed, ref newDenom)) 
                        continue; // If we can't do this bit of the mix, ignore it and go on to the next possibility. 
                    newDenomNumNotes = newDenom.Select(d => d.Value).Sum();
                }
                // Always take the first one, else take if only if it is smaller than the previouse smallest
                if (foundOne == false ||
                    newDenomNumNotes + numNotes < currentSmallestDenomNumNotes + currentSmallestNumNotes)
                {
                    foundOne = true;
                    currentSmallestNumNotes = numNotes;
                    currentSmallestDenomNumNotes = newDenomNumNotes;
                    Denom = newDenom;
                }
            }

            // At this point. CurrentSmallestNumNotes is the number of notes from cash unit we should use
            // and CurrentSmallest is the smallest denomination required from the rest of the cash units
            // Add the notes from this cash unit to the denomination and return it.
            if (Denom.ContainsKey(currentCashUnit))
                Denom[currentCashUnit] = currentSmallestNumNotes;
            else
                Denom.Add(currentCashUnit, currentSmallestNumNotes);

            if (new Denomination(new Dictionary<string, double>() { { Currency, Amount } }, Denom).CheckTotalAmount(CashUnits))
            {
                if (UnitsUsed.Contains(currentCashUnit))
                    UnitsUsed.Remove(currentCashUnit);
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// EqualEmptyingMix
    /// Mix notes to give equal use of each logical cash unit.
    /// </summary>
    [Serializable()]
    public sealed class EqualEmptyingMix : Mix
    {
        public EqualEmptyingMix(int MixNumber)
            : base(MixNumber,
                   TypeEnum.Algorithm,
                   (int)SubTypeEmum.EqualEmptyingOfCashUnits,
                   "Equal emptying of cash units")
        { }

        /// <summary>
        /// Calculate a denomination with the given value, such that each cash unit will have equal usage.
        ///
        /// Algorithm:	Calculate sum of cash unit values.
        ///             If the amount is smaller than this sum, use a special case.
        ///             Otherwise, divide the amount by the sum, take that many notes from each cash unit.
        ///             Use the minnotes algorithm to make up the remainder.
        ///             Currently the low case also uses the minnotes algorithm.
        ///             
        /// </summary>
        /// <param name="CurrencyAmounts">Value to denominate and Currency to denominate in.</param>
        /// <param name="CashUnits">cash units to denominate from.</param>
        /// <param name="Logger"></param>
        /// <returns>denominate to dispense</returns>
        public override Denomination Calculate(Dictionary<string, double> CurrencyAmounts, Dictionary<string, CashUnit> CashUnits, int MaxDispensableItems, ILogger Logger)
        {
            CurrencyAmounts.IsNotNull($"Currency and amounts should be provided to calculate mix.");
            CashUnits.IsNotNull($"Cash unit information should be provided to calculate mix.");

            //Array to make sure use cassettes only once
            List<string> CassetteUsed = new();
            Dictionary<string, int> totalEqualEmptying = new();
            foreach (var ca in CurrencyAmounts)
            {
                Dictionary<string, int> newDenom = new();

                string biggestCashUnit = FindNextGreatest(0, ca.Key, CashUnits, ref CassetteUsed);
                string smallestCashUnit = FindNextLeast(0, ca.Key, CashUnits, ref CassetteUsed);


                if (string.IsNullOrEmpty(biggestCashUnit))
                {
                    Logger.Warning(Constants.Framework, "Unable to find upper bounds cash unit.");
                    return new Denomination(CurrencyAmounts);
                }
                if (string.IsNullOrEmpty(smallestCashUnit))
                {
                    Logger.Warning(Constants.Framework, "Unable to find lower bounds cash unit.");
                    return new Denomination(CurrencyAmounts);
                }

                // Find the sum of the cash unit values.
                double sum = 0;
                foreach (var unit in CashUnits)
                {
                    // If this cash unit isn't valid, skip it.
                    if ((unit.Value.Type != CashUnit.TypeEnum.BillCassette &&
                         unit.Value.Type != CashUnit.TypeEnum.CoinCylinder &&
                         unit.Value.Type != CashUnit.TypeEnum.CoinDispenser &&
                         unit.Value.Type != CashUnit.TypeEnum.Recycling)
                       || unit.Value.CurrencyID != ca.Key
                       || (unit.Value.Status != CashUnit.StatusEnum.Ok &&
                           unit.Value.Status != CashUnit.StatusEnum.Low &&
                           unit.Value.Status != CashUnit.StatusEnum.High &&
                           unit.Value.Status != CashUnit.StatusEnum.Full)
                       || unit.Value.AppLock)
                    {
                        continue;
                    }
                    else
                    {
                        sum += unit.Value.Value;
                    }
                }

                if (ca.Value < sum)
                {
                    // If the amount is equal to the biggest cash unit, try to deliver this with smaller
                    // notes first.
                    if (ca.Value == CashUnits[biggestCashUnit].Value)
                    {
                        if (!CalculateLow(ca.Value, ca.Key, biggestCashUnit, CashUnits, ref CassetteUsed, ref newDenom, Logger))
                        {
                            CalculateLow(ca.Value, ca.Key, string.Empty, CashUnits, ref CassetteUsed, ref newDenom, Logger);
                        }
                    }
                    else
                    {
                        CalculateLow(ca.Value, ca.Key, string.Empty, CashUnits, ref CassetteUsed, ref newDenom, Logger);
                    }

                }
                else
                {
                    int equalNumNotes = (int)(ca.Value / sum);
                    double remainder = 0;
                    bool mixFailure = false;

                    do
                    {
                        mixFailure = false;
                        newDenom.Clear();
                        // Attempt to remove this number of notes from each cash unit.  If a cash unit
                        // contains fewer notes, take all of them.
                        foreach (var unit in CashUnits)
                        {
                            // If this cash unit isn't valid, skip it.
                            if ((unit.Value.Type != CashUnit.TypeEnum.BillCassette &&
                             unit.Value.Type != CashUnit.TypeEnum.CoinCylinder &&
                             unit.Value.Type != CashUnit.TypeEnum.CoinDispenser &&
                             unit.Value.Type != CashUnit.TypeEnum.Recycling)
                           || unit.Value.CurrencyID != ca.Key
                           || (unit.Value.Status != CashUnit.StatusEnum.Ok &&
                               unit.Value.Status != CashUnit.StatusEnum.Low &&
                               unit.Value.Status != CashUnit.StatusEnum.High &&
                               unit.Value.Status != CashUnit.StatusEnum.Full)
                           || unit.Value.AppLock)
                            {
                                continue;
                            }
                            else if (equalNumNotes > unit.Value.Count)
                            {
                                if (newDenom.ContainsKey(unit.Key))
                                    newDenom[unit.Key] = unit.Value.Count;
                                else
                                    newDenom.Add(unit.Key, unit.Value.Count);
                            }
                            else
                            {
                                if (newDenom.ContainsKey(unit.Key))
                                    newDenom[unit.Key] = equalNumNotes;
                                else
                                    newDenom.Add(unit.Key, equalNumNotes);
                            }
                        }

                        if (Denomination.GetTotalAmount(ca.Key, newDenom, CashUnits) > ca.Value)
                        {
                            mixFailure = true;
                            // Check to ensure the Remainder is not negative
                            if (equalNumNotes > 0)
                            {
                                equalNumNotes--;
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }

                        // Find the amount remaining to be allocated.  Don't use EqualNumNotes * sum, because
                        // some of the cash units might have fewer notes than EqualNumNotes
                        remainder = ca.Value - Denomination.GetTotalAmount(ca.Key, newDenom, CashUnits);


                        // Use the Low algorithm for the remainder
                        if (!CalculateLow(remainder, ca.Key, string.Empty, CashUnits, ref CassetteUsed, ref newDenom, Logger))
                        {
                            if (equalNumNotes > 0)
                            {
                                equalNumNotes--;
                            }
                            mixFailure = true;
                        }

                        // Check that this denomination can actually be dispensed.
                        if (mixFailure == false &&
                            new Denomination(new Dictionary<string, double>() { { ca.Key, ca.Value } }, newDenom).IsDispensable(CashUnits, Logger) != Denomination.DispensableResultEnum.Good)
                        {
                            if (equalNumNotes > 0)
                            {
                                equalNumNotes--;
                            }
                            mixFailure = true;
                        }

                        // Consistency check
                        if (Denomination.GetTotalAmount(ca.Key, newDenom, CashUnits) != ca.Value)
                        {
                            if (equalNumNotes > 0)
                            {
                                equalNumNotes--;
                            }
                            mixFailure = true;
                        }

                    } while (equalNumNotes > 0 && mixFailure == true);

                    if (mixFailure)
                    {
                        // We can't allocate notes based on the Equal mix.  Bail out and try the MinNotes
                        // algorithm for the entire amount.
                        Denomination tempDenom = new MinNumberMix(1).Calculate(new Dictionary<string, double>() { { ca.Key, ca.Value } }, CashUnits, MaxDispensableItems, Logger);
                        newDenom = tempDenom?.Values;
                    }
                }

                // Check that this denomination can actually be dispensed.
                if (new Denomination(new Dictionary<string, double>(){ { ca.Key, ca.Value } }, newDenom).IsDispensable(CashUnits, Logger) != Denomination.DispensableResultEnum.Good)
                {
                    return new Denomination(CurrencyAmounts);
                }

                foreach (var d in newDenom)
                {
                    if (totalEqualEmptying.ContainsKey(d.Key))
                        totalEqualEmptying[d.Key] += d.Value;
                    else
                        totalEqualEmptying.Add(d.Key, d.Value);
                }
            }

            // At this point. CurrentSmallestNumNotes is the number of notes from cash unit we should use
            // and CurrentSmallest is the smallest denomination required from the rest of the cash units
            // Add the notes from this cash unit to the denomination and return it.
            Denomination denom = new(CurrencyAmounts, totalEqualEmptying);

            // Check that this denomination can actually be dispensed.
            if (denom.IsDispensable(CashUnits, Logger) != Denomination.DispensableResultEnum.Good)
            {
                return new Denomination(CurrencyAmounts);
            }

            return denom;
        }

        private bool CalculateLow(double Amount, string Currency, string LastCashUnit, Dictionary<string, CashUnit> CashUnits, ref List<string> UnitsUsed, ref Dictionary<string, int> Denom, ILogger Logger)
        {
            CashUnits.IsNotNull($"Cash unit information should be provided to calculate mix.");
            Denom.IsNotNull($"An empty denomination passed in. " + nameof(CalculateLow));

            Dictionary<string, int> originalTotalDenomination = new(Denom);

            // Find the greatest value note of this currency. This will be the first 
            // note used.
            string currentCashUnit;
            if (string.IsNullOrEmpty(LastCashUnit))
                currentCashUnit = FindNextGreatest(0, Currency, CashUnits, ref UnitsUsed);
            else
                currentCashUnit = FindNextGreatest(CashUnits[LastCashUnit].Value, Currency, CashUnits, ref UnitsUsed);
            if (string.IsNullOrEmpty(currentCashUnit))
            {
                Logger.Warning(Constants.Framework, "Failed to find cash unit to denominate in " + nameof(CalculateLow));
                return false;
            }

            double remainingAmount = Amount;

            //Find the cash unit with the next biggest value of bill of the correct 
            // currency in it.
            do
            {
                CashUnits.ContainsKey(currentCashUnit).IsTrue($"");

                // Find the number of notes of this value which are needed.
                int numNotes = (int)(remainingAmount / CashUnits[currentCashUnit].Value);
                // If we can't dispense this many notes, dispence what we can and make up the rest from 
                // the other cash units.
                if (Denom.ContainsKey(currentCashUnit) &&
                    (CashUnits[currentCashUnit].Count + Denom[currentCashUnit] < numNotes))
                {
                    numNotes = CashUnits[currentCashUnit].Count;
                }

                if (Denom.ContainsKey(currentCashUnit))
                    Denom[currentCashUnit] += numNotes;
                else
                    Denom.Add(currentCashUnit, numNotes);

                // Find how much will be left after these notes are included.
                remainingAmount -= numNotes * CashUnits[currentCashUnit].Value;
                currentCashUnit = FindNextGreatest(CashUnits[currentCashUnit].Value, Currency, CashUnits, ref UnitsUsed);
            }
            while (!string.IsNullOrEmpty(currentCashUnit));

            // If the amount remaining isn't zero, then we can't dispense this value.
            if (remainingAmount != 0)
            {
                MinNumberMix.CalculateR(Amount, Currency, LastCashUnit, CashUnits, ref UnitsUsed, ref Denom);
                foreach (var oriDenom in originalTotalDenomination)
                {
                    if (Denom.ContainsKey(oriDenom.Key))
                        Denom[oriDenom.Key] += oriDenom.Value;
                    else
                        Denom.Add(oriDenom.Key, oriDenom.Value);
                }
            }

            // Don't check the total value of the Denomination, because we do that in the caller.
            return Denom.Select(c => c.Value).Sum() != 0;
        }
    }
}
