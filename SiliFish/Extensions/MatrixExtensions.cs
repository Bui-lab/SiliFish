using System;
using System.Collections.Generic;

namespace SiliFish.Extensions
{
    public static class MatrixExtensions
    {
        public static double RowSum(this double[,] matrix, int rowind)
        {
            double sum = 0;
            if (rowind < matrix.GetLength(0))
            {
                int numcol = matrix.GetLength(1);
                for (int colind = 0; colind < numcol; colind++)
                {
                    sum += matrix[rowind, colind];
                }
            }
            return sum;
        }
        public static double ColumnSum(this double[,] matrix, int colind)
        {
            double sum = 0;
            if (colind < matrix.GetLength(1))
            {
                int numrow = matrix.GetLength(0);
                for (int rowind = 0; rowind < numrow; rowind++)
                {
                    sum += matrix[rowind, colind];
                }
            }
            return sum;
        }

        public static List<string> ConvertRowToList(this double[,] matrix, int row)
        {
            int numofcols = matrix.GetLength(1);
            List<string> strlist = new ();
            for (int j = 0; j < numofcols; j++)
                strlist.Add(matrix[row, j].ToString());
            return strlist;
        }
        public static double[]? GetRow(this double[,] matrix, int rowIndex)
        {
            int numofcols = matrix.GetLength(1);
            double[] subset = new double[numofcols];
            for (int j = 0; j < numofcols; j++)
                subset[j] = matrix[rowIndex, j];
            return subset;
        }
        public static double[,]? GetRowsSubset(this double[,] matrix, int? rowstart=null, int? rowend=null)
        {
            if (rowstart == null)
                rowstart = 0;
            if (rowend==null || rowend >= matrix.GetLength(0))
                rowend = matrix.GetLength(0) - 1;
            if (rowstart < 0 || rowstart > rowend)
                return null;
            int istart = (int)rowstart;
            int numofrows = (int)rowend - istart + 1;
            int numofcols = matrix.GetLength(1);
            double[,] subset = new double[numofrows, numofcols];
            for (int i = 0; i < numofrows; i++)
                for (int j = 0; j < numofcols; j++)
                    subset[i, j] = matrix[istart + i, j];
            return subset;
        }

        public static void UpdateColumn(this double[,] matrix, int col, double[] values)
        {
            if (col < 0 || col >= matrix.GetLength(1))
                return;
            if (values.Length != matrix.GetLength(0))
                return;
            for (int i = 0; i < values.Length; i++)
                matrix[i, col] = values[i];

        }

        public static void UpdateRow(this double[,] matrix, int row, double[] values)
        {
            if (row < 0 || row >= matrix.GetLength(0))
                return;
            if (values?.Length != matrix.GetLength(1))
                return;
            for (int i = 0; i < values.Length; i++)
                matrix[row, i] = values[i];

        }
        public static double[]? GetColumn(this double[,] matrix, int colIndex)
        {
            int numofrows = matrix.GetLength(0);
            double[] subset = new double[numofrows];
            for (int i = 0; i < numofrows; i++)
                subset[i] = matrix[i, colIndex];
            return subset;
        }

        public static double[,]? GetColsSubset(this double[,] matrix, int? colstart = null, int? colend = null)
        {
            if (colstart == null)
                colstart = 0;
            if (colend == null || colend >= matrix.GetLength(1))
                colend = matrix.GetLength(1) - 1;
            if (colstart < 0 || colstart > colend)
                return null;
            int istart = (int)colstart;
            int numofrows = matrix.GetLength(0);
            int numofcols = (int)colend - istart + 1;
            double[,] subset = new double[numofrows, numofcols];
            for (int i = 0; i < numofrows; i++)
                for (int j = 0; j < numofcols; j++)
                    subset[i, j] = matrix[i, istart + j];
            return subset;
        }

        public static void PrintToConsole(this double[,] matrix)
        {
            int numofrows = matrix.GetLength(0);
            int numofcols = matrix.GetLength(1);
            for (int i = 0; i < numofrows; i++)
            {
                for (int j = 0; j < numofcols; j++)
                    Console.Write(matrix[i, j].ToString() + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
