using SiliFish.DataTypes;

namespace SiliFish.UI.Extensions
{
    public static class DictionaryExtensions
    {

        /// <summary>
        /// Updates "value" cell's tag to handle distributions
        /// </summary>
        /// <param name="paramDict"></param>
        /// <param name="dgParamGrid"></param>
        /// <param name="colField"></param>
        /// <param name="colValue"></param>
        public static void WriteToGrid(this Dictionary<string, object> paramDict, DataGridView dgParamGrid, int colField, int colValue)
        {
            try
            {
                dgParamGrid.RowCount = paramDict?.Count ?? 0;
                if (paramDict == null)
                    return;
                int rowIndex = 0;
                foreach (string key in paramDict.Keys)
                {
                    dgParamGrid[colField, rowIndex].Value = key;
                    dgParamGrid[colValue, rowIndex].Tag = paramDict[key];
                    dgParamGrid[colValue, rowIndex++].Value = paramDict[key].ToString();
                }
            }
            catch {}
        }

        /// <summary>
        /// Accepts distribution from "value" cell's tag as values
        /// </summary>
        /// <param name="paramDict"></param>
        /// <param name="dgParamGrid"></param>
        /// <param name="colField"></param>
        /// <param name="colValue"></param>
        public static Dictionary<string, object> ReadFromGrid(this Dictionary<string, object> paramDict, DataGridView dgParamGrid, int colField, int colValue)
        {
            try
            {
                if (paramDict == null)
                    paramDict = new();
                else
                    paramDict.Clear();
                for (int rowIndex = 0; rowIndex < dgParamGrid.RowCount; rowIndex++)
                {
                    if (dgParamGrid[colValue, rowIndex].Tag is Distribution dist)
                        paramDict.Add(dgParamGrid[colField, rowIndex].Value.ToString(), dist);
                    else
                        paramDict.Add(dgParamGrid[colField, rowIndex].Value.ToString(), dgParamGrid[colValue, rowIndex].Value);
                }
                return paramDict;
            }
            catch { return null; }
        }
    }
}
