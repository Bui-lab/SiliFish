using Microsoft.EntityFrameworkCore.Storage.Json;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.Repositories;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Extensions;
using System.Windows.Forms;
using static OfficeOpenXml.ExcelErrorValue;

namespace SiliFish.UI.Controls
{
    public partial class InterPoolControl : UserControl
    {
        private RunningModel model;
        private InterPool interPool;

        private void ReviewConductanceMatrix()
        {
            JunctionBase jb = interPool.Junctions.FirstOrDefault();
            if (jb == null) return;

            CellPool sourcePool = model.GetCellPools().FirstOrDefault(cp => cp.ID == jb.SourcePool) as CellPool;
            CellPool targetPool = model.GetCellPools().FirstOrDefault(cp => cp.ID == jb.TargetPool) as CellPool;

            string preRow = sourcePool.BodyLocation == BodyLocation.SupraSpinal ? "Cell" : "Somite";
            string preColumn = targetPool.BodyLocation == BodyLocation.SupraSpinal ? "Cell" : "Somite";
            dgConductanceMatrix.RowCount = sourcePool.BodyLocation == BodyLocation.SupraSpinal ?
                sourcePool.NumOfCells : model.ModelDimensions.NumberOfSomites;
            dgConductanceMatrix.ColumnCount = targetPool.BodyLocation == BodyLocation.SupraSpinal ?
                targetPool.NumOfCells : model.ModelDimensions.NumberOfSomites;
            foreach (DataGridViewRow row in dgConductanceMatrix.Rows)
                row.HeaderCell.Value = $"{preRow} {row.Index + 1}";
            foreach (DataGridViewColumn column in dgConductanceMatrix.Columns)
                column.HeaderCell.Value = $"{preColumn} {column.Index + 1}";
            foreach (JunctionBase junc in interPool.Junctions)
            {
                (int row, int col) = junc.GetCellIndices();
                dgConductanceMatrix[col - 1, row - 1].Value = $"{junc.Core.Conductance:0.#####} ({junc.Duration_ms:0.##} ms)";
            }
        }

        public InterPoolControl(RunningModel model)
        {
            InitializeComponent();
            this.model = model;
        }

        public override string ToString()
        {
            return "";
            /*string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return string.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddJunctionType.Text, activeStatus);
        */
        }

        public void WriteDataToControl(InterPool interPool)
        {
            if (interPool == null) return;
            this.interPool = interPool;
            eSourcePool.Text = interPool.SourcePool;
            eTargetPool.Text = interPool.TargetPool;
            JunctionBase jb = interPool.Junctions.FirstOrDefault();
            if (jb != null)
            {
                eCoreType.Text = jb.Core.CoreType;
            }
            ReviewConductanceMatrix();
        }
    }
}
