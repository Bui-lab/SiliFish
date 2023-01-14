using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public static class ModelFile
    {
        public static void Save(string fileName, ModelBase model)
        {
            JsonUtil.SaveToJsonFile(fileName, model);
        }

        public static ModelBase Load(string fileName, out List<string> issues)
        {
            string json = FileUtil.ReadFromFile(fileName);
            CheckJSONVersion(ref json, out issues);
            if (json.Contains("\"ClassType\": \"RunningModel\","))
                return (RunningModel)JsonUtil.ToObject(typeof(RunningModel), json);
            else
                return (ModelTemplate)JsonUtil.ToObject(typeof(ModelTemplate), json);
        }

        private static void CheckJSONVersion(ref string json, out List<string> issues)
        {
            issues = JsonUtil.CheckJsonVersion(ref json);
        }
    }
}
