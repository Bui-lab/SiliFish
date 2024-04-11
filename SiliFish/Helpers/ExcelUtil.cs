﻿using OfficeOpenXml;
using SiliFish.ModelUnits;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiliFish.Helpers
{
    public static class ExcelUtil
    {
        public static List<string> ReadXLCellsFromLine(ExcelWorksheet worksheet, int line, int maxCol = -1)
        {
            List<string> cells = [];
            int colInd = 1;
            while (true)
            {
                string s = worksheet.Cells[line, colInd].Value?.ToString();
                if (string.IsNullOrEmpty(s) && maxCol < 0) break;
                cells.Add(s);
                if (colInd == maxCol) break;
                colInd++;
            }
            return cells;
        }
        public static ExcelPackage CreateWorkBook(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            ExcelPackage package = new(fileName);
            return package;    
        }
        //TODO the boolean return value is not used - keep a log and give a warning
        public static bool CreateWorkSheet(ExcelWorkbook workbook, string sheetName, List<string> columnNames, List<IDataExporterImporter> objList)
        {
            try
            {//check whether the objlist already contains the columnnames and columnnames argument is obsolete
                if (objList.Count > 1048575) //max number of rows excel allows
                    return false;
                ExcelWorksheet workSheet = workbook.Worksheets.Add(sheetName);
                int rowindex = 1;
                int colindex = 1;
                foreach (string s in columnNames)
                    workSheet.Cells[rowindex, colindex++].Value = s;
                foreach (IDataExporterImporter obj in objList)
                {
                    colindex = 1;
                    rowindex++;
                    foreach (string s in obj.ExportValues())
                        workSheet.Cells[rowindex, colindex++].Value = s;
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool AddWorksheet(ExcelPackage package, string sheetName, List<string> columnNames, List<List<string>> values)
        {
            try
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(sheetName);
                int rowIndex = 1;
                int colIndex = 1;
                foreach (string s in columnNames)
                    workSheet.Cells[rowIndex, colIndex++].Value = s;
                foreach (List<string> strings in values)
                {
                    rowIndex++;
                    colIndex = 1;
                    foreach (string s in strings)
                        workSheet.Cells[rowIndex, colIndex++].Value = s;
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

    }
}
