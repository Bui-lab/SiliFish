using System.Collections.Generic;

namespace SiliFish.ModelUnits
{
    public interface IDataExporterImporter
    {
        static List<string> ColumnNames { get; }
        static string ColumnNamesCommaDelimited { get; }
        List<string> ExportValues();
        void ImportValues(List<string> values);
    }
}