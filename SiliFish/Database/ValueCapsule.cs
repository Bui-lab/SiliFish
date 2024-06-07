using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Repositories;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Database
{
    public class ValueCapsule
    {
        int StartIndex = 0;//keeps the index of the first item in the array
        int RollingWindow = 100;//the number of items that needs to be kept
        double[] Array = null;
        int UnitRecordID = 0;
        string Name;
        string dbName;
        double defValue;
        DatabaseDumper dumper;
        public double this[int i]
        {
            get => GetValue(i);
            set => SetValue(i, value);
        }
        public ValueCapsule(string name, int iMax, double defaultValue) 
        { 
            Name = name;
            defValue = defaultValue;
            Array = Enumerable.Repeat(defaultValue, iMax).ToArray();
        }
        public ValueCapsule(string name, int iMax, int unitRecordID, 
            int rollingWindow, int multiplier, 
            double defaultValue, string dbname, DatabaseDumper dumper)
        {
            this.dumper = dumper;
            dbName = dbname;
            Name = name;
            defValue = defaultValue;
            UnitRecordID = unitRecordID;
            RollingWindow = rollingWindow;
            int size = Math.Min(rollingWindow * multiplier, iMax);
            Array = Enumerable.Repeat(defaultValue, size).ToArray();
        }
        private double GetValue(int i)
        {
            try
            {
                if (UnitRecordID == 0)
                    return Array[i];
                if (i < StartIndex)
                    throw new Exception($"ValueCapsule GetValue with invalid index: " +
                        $"Name: {Name}; UnitRecordID: {UnitRecordID}; " +
                        $"i: {i}; StartIndex:{StartIndex}");
                else
                    return Array[i - StartIndex];
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return double.NaN;
            }
        }

        private void DumpData(double[] SubArray, int startIndex)
        {
            List<CellValueRecord> list = []; ;
            for (int i = 0; i < SubArray.Length; i++)
            {
                list.Add(new CellValueRecord(UnitRecordID, Name, i+startIndex, SubArray[i]));
            }
            dumper.Dump(list);
        }
        private void SetValue(int i, double value)
        {
            try
            {
                if (UnitRecordID == 0)
                    Array[i] = value;
                else
                {
                    if (i - StartIndex < 0)
                    { }//Exception
                    else if (i - StartIndex >= Array.Length)
                    {//dump data to database and move the rolling window size array to the start
                        int dumpEnd = Array.Length - RollingWindow;
                        double[] toDump = Array[0..dumpEnd];
                        DumpData(toDump, StartIndex);
                        for (int iter = 0; iter < RollingWindow; iter++)
                            Array[iter] = Array[iter + dumpEnd];
                        for (int iter = RollingWindow; iter < Array.Length; iter++)
                            Array[iter] = defValue;
                        StartIndex += dumpEnd;
                        Array[i - StartIndex] = value;
                    }
                    else
                        Array[i - StartIndex] = value;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public double MinValue(int iStart = 0, int iEnd = -1)
        {
            if (UnitRecordID > 0 && Array == null)
            {
                using SFDataContext dataContext = new(dbName);
                return dataContext.Values
                    .Where(v => v.CellID == UnitRecordID && v.ValueType == Name && v.TimeIndex >= iStart && v.TimeIndex <= iEnd)
                    .Min(v => v.Value);
            }
            return Array.MinValue(iStart, iEnd);
        }
        public double MaxValue(int iStart = 0, int iEnd = -1)
        {
            if (UnitRecordID > 0 && Array == null)
            {
                using SFDataContext dataContext = new(dbName);
                return dataContext.Values
                    .Where(v => v.CellID == UnitRecordID && v.ValueType == Name && v.TimeIndex >= iStart && v.TimeIndex <= iEnd)
                    .Max(v => v.Value);
            }
            return Array.MaxValue(iStart, iEnd);
        }

        /// <summary>
        /// To minimize Db access, once a full array is requested, it will be saved to the memory
        /// </summary>
        /// <returns></returns>
        public double[] AsArray()
        {
            if (UnitRecordID == 0 || Array != null)
                return Array;
            using SFDataContext dataContext = new(dbName);

            Array =
            [
                .. dataContext.Values
                                .Where(v => v.CellID == UnitRecordID && v.ValueType == Name)
                                .OrderBy(v => v.TimeIndex)
                                .Select(v => v.Value)
,
            ];
            StartIndex = 0;
            return Array;
        }

        /// <summary>
        /// To be called to conserve memory as plotting increases the memory requirement incrementally
        /// </summary>
        public void Flush()
        {
            if (UnitRecordID > 0)
            {
                Array = null;
            }
        }

        /// <summary>
        /// Save the rest of the data to database and clear value array
        /// </summary>
        /// <param name="runParam"></param>
        /// <param name="dbLink"></param>
        internal void FinalizeSimulation(RunParam runParam, SimulationDBLink dbLink)
        {
            if (dbLink == null) return;
            int dumpEnd = Array.Length;
            if (runParam.iMax < StartIndex + dumpEnd)
                dumpEnd = runParam.iMax - StartIndex;
            double[] toDump = Array[0..dumpEnd];
            dumper = dbLink.DatabaseDumper;
            DumpData(toDump, StartIndex);
            Array = null;
            StartIndex = -1;
        }
    }
}
