namespace SiliFish.UI.Controls
{
    partial class ModelControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelControl));
            openFileJson = new OpenFileDialog();
            saveFileJson = new SaveFileDialog();
            tJson = new TabPage();
            eModelJSON = new RichTextBox();
            pModelJSONTop = new Panel();
            linkOpenJsonViewer = new LinkLabel();
            pLineModelJSON = new Panel();
            linkSaveModelJSON = new LinkLabel();
            btnLoadModelJSON = new Button();
            btnDisplayModelJSON = new Button();
            tSettings = new TabPage();
            propSettings = new PropertyGrid();
            pSettingsTop = new Panel();
            ddDefaultMuscleCellCore = new ComboBox();
            lDefaultMuscleCore = new Label();
            ddDefaultNeuronCore = new ComboBox();
            lDefaultNeuronCore = new Label();
            tKinematics = new TabPage();
            propKinematics = new PropertyGrid();
            pKinematicsDescription = new Panel();
            eKinematicsDescription = new TextBox();
            pKinemSep = new Panel();
            tArchitecture = new TabPage();
            splitMain = new SplitContainer();
            splitCellsAndStimuli = new SplitContainer();
            splitCellPoolsAndCells = new SplitContainer();
            listCellPools = new ListBoxControl();
            pCellPoolsTop = new Panel();
            pCellPoolsSep = new Panel();
            lCellPoolsTitle = new Label();
            listCells = new ListBoxControl();
            pCellsTop = new Panel();
            pCellsSep = new Panel();
            lCellsTitle = new Label();
            listStimuli = new ListBoxControl();
            pStimuliTop = new Panel();
            pStimuliSep = new Panel();
            lStimuliTitle = new Label();
            splitConnections = new SplitContainer();
            splitChemicalJunctions = new SplitContainer();
            listIncoming = new ListBoxControl();
            pIncomingTop = new Panel();
            pConnectionsSep = new Panel();
            lIncomingTitle = new Label();
            listOutgoing = new ListBoxControl();
            pOutgoingTop = new Panel();
            panel2 = new Panel();
            lOutgoingTitle = new Label();
            listGap = new ListBoxControl();
            pGapTop = new Panel();
            panel4 = new Panel();
            lGapTitle = new Label();
            tGeneral = new TabPage();
            splitGeneral = new SplitContainer();
            lModelName = new Label();
            lDescription = new Label();
            eModelDescription = new TextBox();
            eModelName = new TextBox();
            splitDimensions = new SplitContainer();
            propModelDimensions = new PropertyGrid();
            pDrawing = new Panel();
            pbDrawing = new PictureBox();
            tabModel = new TabControl();
            saveFileCSV = new SaveFileDialog();
            openFileCSV = new OpenFileDialog();
            tJson.SuspendLayout();
            pModelJSONTop.SuspendLayout();
            tSettings.SuspendLayout();
            pSettingsTop.SuspendLayout();
            tKinematics.SuspendLayout();
            pKinematicsDescription.SuspendLayout();
            tArchitecture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitCellsAndStimuli).BeginInit();
            splitCellsAndStimuli.Panel1.SuspendLayout();
            splitCellsAndStimuli.Panel2.SuspendLayout();
            splitCellsAndStimuli.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitCellPoolsAndCells).BeginInit();
            splitCellPoolsAndCells.Panel1.SuspendLayout();
            splitCellPoolsAndCells.Panel2.SuspendLayout();
            splitCellPoolsAndCells.SuspendLayout();
            pCellPoolsTop.SuspendLayout();
            pCellsTop.SuspendLayout();
            pStimuliTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitConnections).BeginInit();
            splitConnections.Panel1.SuspendLayout();
            splitConnections.Panel2.SuspendLayout();
            splitConnections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitChemicalJunctions).BeginInit();
            splitChemicalJunctions.Panel1.SuspendLayout();
            splitChemicalJunctions.Panel2.SuspendLayout();
            splitChemicalJunctions.SuspendLayout();
            pIncomingTop.SuspendLayout();
            pOutgoingTop.SuspendLayout();
            pGapTop.SuspendLayout();
            tGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitGeneral).BeginInit();
            splitGeneral.Panel1.SuspendLayout();
            splitGeneral.Panel2.SuspendLayout();
            splitGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitDimensions).BeginInit();
            splitDimensions.Panel1.SuspendLayout();
            splitDimensions.Panel2.SuspendLayout();
            splitDimensions.SuspendLayout();
            pDrawing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbDrawing).BeginInit();
            tabModel.SuspendLayout();
            SuspendLayout();
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // saveFileJson
            // 
            saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // tJson
            // 
            tJson.Controls.Add(eModelJSON);
            tJson.Controls.Add(pModelJSONTop);
            tJson.Location = new Point(4, 24);
            tJson.Name = "tJson";
            tJson.Size = new Size(682, 555);
            tJson.TabIndex = 7;
            tJson.Text = "JSON File";
            tJson.UseVisualStyleBackColor = true;
            // 
            // eModelJSON
            // 
            eModelJSON.AcceptsTab = true;
            eModelJSON.Location = new Point(0, 40);
            eModelJSON.Name = "eModelJSON";
            eModelJSON.Size = new Size(682, 515);
            eModelJSON.TabIndex = 5;
            eModelJSON.Text = "";
            eModelJSON.TextChanged += eModelJSON_TextChanged;
            // 
            // pModelJSONTop
            // 
            pModelJSONTop.BackColor = Color.FromArgb(236, 239, 241);
            pModelJSONTop.Controls.Add(linkOpenJsonViewer);
            pModelJSONTop.Controls.Add(pLineModelJSON);
            pModelJSONTop.Controls.Add(linkSaveModelJSON);
            pModelJSONTop.Controls.Add(btnLoadModelJSON);
            pModelJSONTop.Controls.Add(btnDisplayModelJSON);
            pModelJSONTop.Dock = DockStyle.Top;
            pModelJSONTop.Location = new Point(0, 0);
            pModelJSONTop.Name = "pModelJSONTop";
            pModelJSONTop.Size = new Size(682, 40);
            pModelJSONTop.TabIndex = 4;
            // 
            // linkOpenJsonViewer
            // 
            linkOpenJsonViewer.AutoSize = true;
            linkOpenJsonViewer.Location = new Point(179, 13);
            linkOpenJsonViewer.Name = "linkOpenJsonViewer";
            linkOpenJsonViewer.Size = new Size(118, 15);
            linkOpenJsonViewer.TabIndex = 26;
            linkOpenJsonViewer.TabStop = true;
            linkOpenJsonViewer.Text = "Open in JSON Viewer";
            linkOpenJsonViewer.LinkClicked += linkOpenJsonViewer_LinkClicked;
            // 
            // pLineModelJSON
            // 
            pLineModelJSON.BackColor = Color.LightGray;
            pLineModelJSON.Dock = DockStyle.Bottom;
            pLineModelJSON.Location = new Point(0, 39);
            pLineModelJSON.Name = "pLineModelJSON";
            pLineModelJSON.Size = new Size(682, 1);
            pLineModelJSON.TabIndex = 25;
            // 
            // linkSaveModelJSON
            // 
            linkSaveModelJSON.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveModelJSON.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveModelJSON.Location = new Point(1188, 10);
            linkSaveModelJSON.Name = "linkSaveModelJSON";
            linkSaveModelJSON.Size = new Size(68, 16);
            linkSaveModelJSON.TabIndex = 5;
            linkSaveModelJSON.TabStop = true;
            linkSaveModelJSON.Text = "Save Model";
            linkSaveModelJSON.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnLoadModelJSON
            // 
            btnLoadModelJSON.BackColor = Color.FromArgb(96, 125, 139);
            btnLoadModelJSON.Enabled = false;
            btnLoadModelJSON.FlatAppearance.BorderColor = Color.LightGray;
            btnLoadModelJSON.FlatStyle = FlatStyle.Flat;
            btnLoadModelJSON.ForeColor = Color.White;
            btnLoadModelJSON.Location = new Point(88, 8);
            btnLoadModelJSON.Name = "btnLoadModelJSON";
            btnLoadModelJSON.Size = new Size(75, 24);
            btnLoadModelJSON.TabIndex = 1;
            btnLoadModelJSON.Text = "Load";
            btnLoadModelJSON.UseVisualStyleBackColor = false;
            btnLoadModelJSON.Click += btnLoadModelJSON_Click;
            // 
            // btnDisplayModelJSON
            // 
            btnDisplayModelJSON.BackColor = Color.FromArgb(96, 125, 139);
            btnDisplayModelJSON.FlatAppearance.BorderColor = Color.LightGray;
            btnDisplayModelJSON.FlatStyle = FlatStyle.Flat;
            btnDisplayModelJSON.ForeColor = Color.White;
            btnDisplayModelJSON.Location = new Point(8, 8);
            btnDisplayModelJSON.Name = "btnDisplayModelJSON";
            btnDisplayModelJSON.Size = new Size(75, 24);
            btnDisplayModelJSON.TabIndex = 0;
            btnDisplayModelJSON.Text = "Display";
            btnDisplayModelJSON.UseVisualStyleBackColor = false;
            btnDisplayModelJSON.Click += btnDisplayModelJSON_Click;
            // 
            // tSettings
            // 
            tSettings.Controls.Add(propSettings);
            tSettings.Controls.Add(pSettingsTop);
            tSettings.Location = new Point(4, 24);
            tSettings.Name = "tSettings";
            tSettings.Size = new Size(682, 555);
            tSettings.TabIndex = 5;
            tSettings.Text = "Advanced Settings";
            tSettings.UseVisualStyleBackColor = true;
            tSettings.Visible = false;
            // 
            // propSettings
            // 
            propSettings.BackColor = Color.FromArgb(207, 216, 220);
            propSettings.Dock = DockStyle.Fill;
            propSettings.HelpBackColor = Color.FromArgb(207, 216, 220);
            propSettings.HelpBorderColor = Color.FromArgb(0, 184, 212);
            propSettings.Location = new Point(0, 70);
            propSettings.Name = "propSettings";
            propSettings.Size = new Size(682, 485);
            propSettings.TabIndex = 0;
            // 
            // pSettingsTop
            // 
            pSettingsTop.Controls.Add(ddDefaultMuscleCellCore);
            pSettingsTop.Controls.Add(lDefaultMuscleCore);
            pSettingsTop.Controls.Add(ddDefaultNeuronCore);
            pSettingsTop.Controls.Add(lDefaultNeuronCore);
            pSettingsTop.Dock = DockStyle.Top;
            pSettingsTop.Location = new Point(0, 0);
            pSettingsTop.Name = "pSettingsTop";
            pSettingsTop.Size = new Size(682, 70);
            pSettingsTop.TabIndex = 1;
            // 
            // ddDefaultMuscleCellCore
            // 
            ddDefaultMuscleCellCore.DropDownStyle = ComboBoxStyle.DropDownList;
            ddDefaultMuscleCellCore.FormattingEnabled = true;
            ddDefaultMuscleCellCore.Location = new Point(153, 32);
            ddDefaultMuscleCellCore.Name = "ddDefaultMuscleCellCore";
            ddDefaultMuscleCellCore.Size = new Size(187, 23);
            ddDefaultMuscleCellCore.TabIndex = 3;
            ddDefaultMuscleCellCore.SelectedIndexChanged += ddDefaultMuscleCore_SelectedIndexChanged;
            // 
            // lDefaultMuscleCore
            // 
            lDefaultMuscleCore.AutoSize = true;
            lDefaultMuscleCore.Location = new Point(8, 35);
            lDefaultMuscleCore.Name = "lDefaultMuscleCore";
            lDefaultMuscleCore.Size = new Size(137, 15);
            lDefaultMuscleCore.TabIndex = 2;
            lDefaultMuscleCore.Text = "Default Muscle Cell Core";
            // 
            // ddDefaultNeuronCore
            // 
            ddDefaultNeuronCore.DropDownStyle = ComboBoxStyle.DropDownList;
            ddDefaultNeuronCore.FormattingEnabled = true;
            ddDefaultNeuronCore.Location = new Point(153, 5);
            ddDefaultNeuronCore.Name = "ddDefaultNeuronCore";
            ddDefaultNeuronCore.Size = new Size(187, 23);
            ddDefaultNeuronCore.TabIndex = 1;
            ddDefaultNeuronCore.SelectedIndexChanged += ddDefaultNeuronCore_SelectedIndexChanged;
            // 
            // lDefaultNeuronCore
            // 
            lDefaultNeuronCore.AutoSize = true;
            lDefaultNeuronCore.Location = new Point(8, 8);
            lDefaultNeuronCore.Name = "lDefaultNeuronCore";
            lDefaultNeuronCore.Size = new Size(116, 15);
            lDefaultNeuronCore.TabIndex = 0;
            lDefaultNeuronCore.Text = "Default Neuron Core";
            // 
            // tKinematics
            // 
            tKinematics.Controls.Add(propKinematics);
            tKinematics.Controls.Add(pKinematicsDescription);
            tKinematics.Location = new Point(4, 24);
            tKinematics.Name = "tKinematics";
            tKinematics.Size = new Size(682, 555);
            tKinematics.TabIndex = 6;
            tKinematics.Text = "Kinematics";
            tKinematics.UseVisualStyleBackColor = true;
            tKinematics.Visible = false;
            // 
            // propKinematics
            // 
            propKinematics.BackColor = Color.FromArgb(207, 216, 220);
            propKinematics.Dock = DockStyle.Fill;
            propKinematics.HelpBackColor = Color.FromArgb(207, 216, 220);
            propKinematics.HelpBorderColor = Color.FromArgb(0, 184, 212);
            propKinematics.Location = new Point(0, 0);
            propKinematics.Name = "propKinematics";
            propKinematics.Size = new Size(682, 515);
            propKinematics.TabIndex = 1;
            // 
            // pKinematicsDescription
            // 
            pKinematicsDescription.BackColor = Color.WhiteSmoke;
            pKinematicsDescription.Controls.Add(eKinematicsDescription);
            pKinematicsDescription.Controls.Add(pKinemSep);
            pKinematicsDescription.Dock = DockStyle.Bottom;
            pKinematicsDescription.Location = new Point(0, 515);
            pKinematicsDescription.Name = "pKinematicsDescription";
            pKinematicsDescription.Size = new Size(682, 40);
            pKinematicsDescription.TabIndex = 7;
            // 
            // eKinematicsDescription
            // 
            eKinematicsDescription.BackColor = Color.FromArgb(236, 239, 241);
            eKinematicsDescription.Dock = DockStyle.Top;
            eKinematicsDescription.Location = new Point(0, 0);
            eKinematicsDescription.Multiline = true;
            eKinematicsDescription.Name = "eKinematicsDescription";
            eKinematicsDescription.ReadOnly = true;
            eKinematicsDescription.Size = new Size(682, 39);
            eKinematicsDescription.TabIndex = 26;
            eKinematicsDescription.Text = "Kinematics parameters are used in generating the animations and motoneuron based statistics.\r\nThey do not effect the how the model behaves and can be modified after the simulation.";
            // 
            // pKinemSep
            // 
            pKinemSep.BackColor = Color.LightGray;
            pKinemSep.Dock = DockStyle.Bottom;
            pKinemSep.Location = new Point(0, 39);
            pKinemSep.Name = "pKinemSep";
            pKinemSep.Size = new Size(682, 1);
            pKinemSep.TabIndex = 25;
            // 
            // tArchitecture
            // 
            tArchitecture.BackColor = Color.White;
            tArchitecture.Controls.Add(splitMain);
            tArchitecture.Location = new Point(4, 24);
            tArchitecture.Name = "tArchitecture";
            tArchitecture.Size = new Size(682, 555);
            tArchitecture.TabIndex = 1;
            tArchitecture.Text = "Architecture";
            tArchitecture.UseVisualStyleBackColor = true;
            tArchitecture.Visible = false;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.FromArgb(55, 71, 79); 
            splitMain.BorderStyle = BorderStyle.FixedSingle;
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 0);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(splitCellsAndStimuli);
            splitMain.Panel1MinSize = 200;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(splitConnections);
            splitMain.Panel2MinSize = 200;
            splitMain.Size = new Size(682, 555);
            splitMain.SplitterDistance = 316;
            splitMain.SplitterWidth = 2;
            splitMain.TabIndex = 5;
            // 
            // splitCellsAndStimuli
            // 
            splitCellsAndStimuli.Dock = DockStyle.Fill;
            splitCellsAndStimuli.Location = new Point(0, 0);
            splitCellsAndStimuli.Name = "splitCellsAndStimuli";
            splitCellsAndStimuli.Orientation = Orientation.Horizontal;
            // 
            // splitCellsAndStimuli.Panel1
            // 
            splitCellsAndStimuli.Panel1.Controls.Add(splitCellPoolsAndCells);
            // 
            // splitCellsAndStimuli.Panel2
            // 
            splitCellsAndStimuli.Panel2.Controls.Add(listStimuli);
            splitCellsAndStimuli.Panel2.Controls.Add(pStimuliTop);
            splitCellsAndStimuli.Size = new Size(314, 553);
            splitCellsAndStimuli.SplitterDistance = 362;
            splitCellsAndStimuli.SplitterWidth = 2;
            splitCellsAndStimuli.TabIndex = 9;
            // 
            // splitCellPoolsAndCells
            // 
            splitCellPoolsAndCells.Dock = DockStyle.Fill;
            splitCellPoolsAndCells.Location = new Point(0, 0);
            splitCellPoolsAndCells.Name = "splitCellPoolsAndCells";
            splitCellPoolsAndCells.Orientation = Orientation.Horizontal;
            // 
            // splitCellPoolsAndCells.Panel1
            // 
            splitCellPoolsAndCells.Panel1.Controls.Add(listCellPools);
            splitCellPoolsAndCells.Panel1.Controls.Add(pCellPoolsTop);
            // 
            // splitCellPoolsAndCells.Panel2
            // 
            splitCellPoolsAndCells.Panel2.Controls.Add(listCells);
            splitCellPoolsAndCells.Panel2.Controls.Add(pCellsTop);
            splitCellPoolsAndCells.Size = new Size(314, 362);
            splitCellPoolsAndCells.SplitterDistance = 176;
            splitCellPoolsAndCells.SplitterWidth = 2;
            splitCellPoolsAndCells.TabIndex = 8;
            // 
            // listCellPools
            // 
            listCellPools.Dock = DockStyle.Fill;
            listCellPools.Location = new Point(0, 26);
            listCellPools.Name = "listCellPools";
            listCellPools.SelectedIndex = -1;
            listCellPools.SelectedItem = null;
            listCellPools.SelectionMode = SelectionMode.MultiExtended;
            listCellPools.Size = new Size(314, 150);
            listCellPools.TabIndex = 7;
            listCellPools.ItemAdd += listCellPools_ItemAdd;
            listCellPools.ItemDelete += listCellPools_ItemDelete;
            listCellPools.ItemCopy += listCellPools_ItemCopy;
            listCellPools.ItemView += listCellPools_ItemView;
            listCellPools.ItemToggleActive += listCellPools_ItemToggleActive;
            listCellPools.ItemsSort += listCellPools_ItemsSort;
            listCellPools.ItemSelect += listCellPools_ItemSelect;
            listCellPools.ItemPlot += listCellPools_ItemPlot;
            listCellPools.ItemHighlight += listCellPools_ItemHighlight;
            listCellPools.ItemsExport += listCellPools_ItemsExport;
            listCellPools.ItemsImport += listCellPools_ItemsImport;
            // 
            // pCellPoolsTop
            // 
            pCellPoolsTop.BackColor = Color.FromArgb(207, 216, 220);
            pCellPoolsTop.Controls.Add(pCellPoolsSep);
            pCellPoolsTop.Controls.Add(lCellPoolsTitle);
            pCellPoolsTop.Dock = DockStyle.Top;
            pCellPoolsTop.Location = new Point(0, 0);
            pCellPoolsTop.Name = "pCellPoolsTop";
            pCellPoolsTop.Size = new Size(314, 26);
            pCellPoolsTop.TabIndex = 6;
            // 
            // pCellPoolsSep
            // 
            pCellPoolsSep.BackColor = Color.LightGray;
            pCellPoolsSep.Dock = DockStyle.Bottom;
            pCellPoolsSep.Location = new Point(0, 25);
            pCellPoolsSep.Name = "pCellPoolsSep";
            pCellPoolsSep.Size = new Size(314, 1);
            pCellPoolsSep.TabIndex = 25;
            // 
            // lCellPoolsTitle
            // 
            lCellPoolsTitle.AutoSize = true;
            lCellPoolsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lCellPoolsTitle.Location = new Point(8, 6);
            lCellPoolsTitle.Name = "lCellPoolsTitle";
            lCellPoolsTitle.Size = new Size(59, 15);
            lCellPoolsTitle.TabIndex = 0;
            lCellPoolsTitle.Text = "Cell Pools";
            // 
            // listCells
            // 
            listCells.Dock = DockStyle.Fill;
            listCells.Location = new Point(0, 26);
            listCells.Name = "listCells";
            listCells.SelectedIndex = -1;
            listCells.SelectedItem = null;
            listCells.SelectionMode = SelectionMode.MultiExtended;
            listCells.Size = new Size(314, 159);
            listCells.TabIndex = 0;
            listCells.ItemAdd += listCells_ItemAdd;
            listCells.ItemDelete += listCells_ItemDelete;
            listCells.ItemCopy += listCells_ItemCopy;
            listCells.ItemView += listCells_ItemView;
            listCells.ItemToggleActive += listCells_ItemToggleActive;
            listCells.ItemsSort += listCells_ItemsSort;
            listCells.ItemSelect += listCells_ItemSelect;
            listCells.ItemPlot += listCells_ItemPlot;
            listCells.ItemHighlight += listCells_ItemHighlight;
            listCells.ItemsExport += listCells_ItemsExport;
            listCells.ItemsImport += listCells_ItemsImport;
            // 
            // pCellsTop
            // 
            pCellsTop.BackColor = Color.FromArgb(207, 216, 220);
            pCellsTop.Controls.Add(pCellsSep);
            pCellsTop.Controls.Add(lCellsTitle);
            pCellsTop.Dock = DockStyle.Top;
            pCellsTop.Location = new Point(0, 0);
            pCellsTop.Name = "pCellsTop";
            pCellsTop.Size = new Size(314, 26);
            pCellsTop.TabIndex = 7;
            // 
            // pCellsSep
            // 
            pCellsSep.BackColor = Color.LightGray;
            pCellsSep.Dock = DockStyle.Bottom;
            pCellsSep.Location = new Point(0, 25);
            pCellsSep.Name = "pCellsSep";
            pCellsSep.Size = new Size(314, 1);
            pCellsSep.TabIndex = 25;
            // 
            // lCellsTitle
            // 
            lCellsTitle.AutoSize = true;
            lCellsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lCellsTitle.Location = new Point(8, 6);
            lCellsTitle.Name = "lCellsTitle";
            lCellsTitle.Size = new Size(32, 15);
            lCellsTitle.TabIndex = 0;
            lCellsTitle.Text = "Cells";
            // 
            // listStimuli
            // 
            listStimuli.Dock = DockStyle.Fill;
            listStimuli.Location = new Point(0, 26);
            listStimuli.Name = "listStimuli";
            listStimuli.SelectedIndex = -1;
            listStimuli.SelectedItem = null;
            listStimuli.SelectionMode = SelectionMode.MultiExtended;
            listStimuli.Size = new Size(314, 164);
            listStimuli.TabIndex = 0;
            listStimuli.ItemAdd += listStimuli_ItemAdd;
            listStimuli.ItemDelete += listStimuli_ItemDelete;
            listStimuli.ItemCopy += listStimuli_ItemCopy;
            listStimuli.ItemView += listStimuli_ItemView;
            listStimuli.ItemToggleActive += listStimuli_ItemToggleActive;
            listStimuli.ItemsSort += listStimuli_ItemsSort;
            listStimuli.ItemSelect += listStimuli_ItemSelect;
            listStimuli.ItemPlot += listStimuli_ItemPlot;
            listStimuli.ItemsExport += listStimuli_ItemsExport;
            listStimuli.ItemsImport += listStimuli_ItemsImport;
            // 
            // pStimuliTop
            // 
            pStimuliTop.BackColor = Color.FromArgb(207, 216, 220);
            pStimuliTop.Controls.Add(pStimuliSep);
            pStimuliTop.Controls.Add(lStimuliTitle);
            pStimuliTop.Dock = DockStyle.Top;
            pStimuliTop.Location = new Point(0, 0);
            pStimuliTop.Name = "pStimuliTop";
            pStimuliTop.Size = new Size(314, 26);
            pStimuliTop.TabIndex = 8;
            // 
            // pStimuliSep
            // 
            pStimuliSep.BackColor = Color.LightGray;
            pStimuliSep.Dock = DockStyle.Bottom;
            pStimuliSep.Location = new Point(0, 25);
            pStimuliSep.Name = "pStimuliSep";
            pStimuliSep.Size = new Size(314, 1);
            pStimuliSep.TabIndex = 25;
            // 
            // lStimuliTitle
            // 
            lStimuliTitle.AutoSize = true;
            lStimuliTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lStimuliTitle.Location = new Point(8, 6);
            lStimuliTitle.Name = "lStimuliTitle";
            lStimuliTitle.Size = new Size(46, 15);
            lStimuliTitle.TabIndex = 0;
            lStimuliTitle.Text = "Stimuli";
            // 
            // splitConnections
            // 
            splitConnections.Dock = DockStyle.Fill;
            splitConnections.Location = new Point(0, 0);
            splitConnections.Name = "splitConnections";
            splitConnections.Orientation = Orientation.Horizontal;
            // 
            // splitConnections.Panel1
            // 
            splitConnections.Panel1.Controls.Add(splitChemicalJunctions);
            // 
            // splitConnections.Panel2
            // 
            splitConnections.Panel2.Controls.Add(listGap);
            splitConnections.Panel2.Controls.Add(pGapTop);
            splitConnections.Size = new Size(363, 553);
            splitConnections.SplitterDistance = 276;
            splitConnections.SplitterWidth = 2;
            splitConnections.TabIndex = 9;
            // 
            // splitChemicalJunctions
            // 
            splitChemicalJunctions.Dock = DockStyle.Fill;
            splitChemicalJunctions.Location = new Point(0, 0);
            splitChemicalJunctions.Name = "splitChemicalJunctions";
            splitChemicalJunctions.Orientation = Orientation.Horizontal;
            // 
            // splitChemicalJunctions.Panel1
            // 
            splitChemicalJunctions.Panel1.Controls.Add(listIncoming);
            splitChemicalJunctions.Panel1.Controls.Add(pIncomingTop);
            // 
            // splitChemicalJunctions.Panel2
            // 
            splitChemicalJunctions.Panel2.Controls.Add(listOutgoing);
            splitChemicalJunctions.Panel2.Controls.Add(pOutgoingTop);
            splitChemicalJunctions.Size = new Size(363, 276);
            splitChemicalJunctions.SplitterDistance = 132;
            splitChemicalJunctions.SplitterWidth = 2;
            splitChemicalJunctions.TabIndex = 8;
            // 
            // listIncoming
            // 
            listIncoming.Dock = DockStyle.Fill;
            listIncoming.Location = new Point(0, 26);
            listIncoming.Name = "listIncoming";
            listIncoming.SelectedIndex = -1;
            listIncoming.SelectedItem = null;
            listIncoming.SelectionMode = SelectionMode.MultiExtended;
            listIncoming.Size = new Size(363, 106);
            listIncoming.TabIndex = 8;
            listIncoming.ItemAdd += listConnections_ItemAdd;
            listIncoming.ItemDelete += listConnections_ItemDelete;
            listIncoming.ItemCopy += listConnections_ItemCopy;
            listIncoming.ItemView += listConnections_ItemView;
            listIncoming.ItemToggleActive += listConnections_ItemToggleActive;
            listIncoming.ItemsSort += listConnections_ItemsSort;
            listIncoming.ItemSelect += listConnections_ItemSelect;
            listIncoming.ItemPlot += listConnections_ItemPlot;
            listIncoming.ItemsExport += listConnections_ItemsExport;
            listIncoming.ItemsImport += listConnections_ItemsImport;
            // 
            // pIncomingTop
            // 
            pIncomingTop.BackColor = Color.FromArgb(207, 216, 220);
            pIncomingTop.Controls.Add(pConnectionsSep);
            pIncomingTop.Controls.Add(lIncomingTitle);
            pIncomingTop.Dock = DockStyle.Top;
            pIncomingTop.Location = new Point(0, 0);
            pIncomingTop.Name = "pIncomingTop";
            pIncomingTop.Size = new Size(363, 26);
            pIncomingTop.TabIndex = 7;
            // 
            // pConnectionsSep
            // 
            pConnectionsSep.BackColor = Color.LightGray;
            pConnectionsSep.Dock = DockStyle.Bottom;
            pConnectionsSep.Location = new Point(0, 25);
            pConnectionsSep.Name = "pConnectionsSep";
            pConnectionsSep.Size = new Size(363, 1);
            pConnectionsSep.TabIndex = 25;
            // 
            // lIncomingTitle
            // 
            lIncomingTitle.AutoSize = true;
            lIncomingTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lIncomingTitle.Location = new Point(8, 6);
            lIncomingTitle.Name = "lIncomingTitle";
            lIncomingTitle.Size = new Size(130, 15);
            lIncomingTitle.TabIndex = 0;
            lIncomingTitle.Text = "Incoming Connections";
            // 
            // listOutgoing
            // 
            listOutgoing.Dock = DockStyle.Fill;
            listOutgoing.Location = new Point(0, 26);
            listOutgoing.Name = "listOutgoing";
            listOutgoing.SelectedIndex = -1;
            listOutgoing.SelectedItem = null;
            listOutgoing.SelectionMode = SelectionMode.MultiExtended;
            listOutgoing.Size = new Size(363, 117);
            listOutgoing.TabIndex = 9;
            listOutgoing.ItemAdd += listConnections_ItemAdd;
            listOutgoing.ItemDelete += listConnections_ItemDelete;
            listOutgoing.ItemCopy += listConnections_ItemCopy;
            listOutgoing.ItemView += listConnections_ItemView;
            listOutgoing.ItemToggleActive += listConnections_ItemToggleActive;
            listOutgoing.ItemsSort += listConnections_ItemsSort;
            listOutgoing.ItemSelect += listConnections_ItemSelect;
            listOutgoing.ItemPlot += listConnections_ItemPlot;
            listOutgoing.ItemsExport += listConnections_ItemsExport;
            listOutgoing.ItemsImport += listConnections_ItemsImport;
            // 
            // pOutgoingTop
            // 
            pOutgoingTop.BackColor = Color.FromArgb(207, 216, 220);
            pOutgoingTop.Controls.Add(panel2);
            pOutgoingTop.Controls.Add(lOutgoingTitle);
            pOutgoingTop.Dock = DockStyle.Top;
            pOutgoingTop.Location = new Point(0, 0);
            pOutgoingTop.Name = "pOutgoingTop";
            pOutgoingTop.Size = new Size(363, 26);
            pOutgoingTop.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightGray;
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 25);
            panel2.Name = "panel2";
            panel2.Size = new Size(363, 1);
            panel2.TabIndex = 25;
            // 
            // lOutgoingTitle
            // 
            lOutgoingTitle.AutoSize = true;
            lOutgoingTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lOutgoingTitle.Location = new Point(8, 6);
            lOutgoingTitle.Name = "lOutgoingTitle";
            lOutgoingTitle.Size = new Size(130, 15);
            lOutgoingTitle.TabIndex = 0;
            lOutgoingTitle.Text = "Outgoing Connections";
            // 
            // listGap
            // 
            listGap.Dock = DockStyle.Fill;
            listGap.Location = new Point(0, 26);
            listGap.Name = "listGap";
            listGap.SelectedIndex = -1;
            listGap.SelectedItem = null;
            listGap.SelectionMode = SelectionMode.MultiExtended;
            listGap.Size = new Size(363, 250);
            listGap.TabIndex = 9;
            listGap.ItemAdd += listConnections_ItemAdd;
            listGap.ItemDelete += listConnections_ItemDelete;
            listGap.ItemCopy += listConnections_ItemCopy;
            listGap.ItemView += listConnections_ItemView;
            listGap.ItemToggleActive += listConnections_ItemToggleActive;
            listGap.ItemsSort += listConnections_ItemsSort;
            listGap.ItemSelect += listConnections_ItemSelect;
            listGap.ItemPlot += listConnections_ItemPlot;
            listGap.ItemsExport += listConnections_ItemsExport;
            listGap.ItemsImport += listConnections_ItemsImport;
            // 
            // pGapTop
            // 
            pGapTop.BackColor = Color.FromArgb(207, 216, 220);
            pGapTop.Controls.Add(panel4);
            pGapTop.Controls.Add(lGapTitle);
            pGapTop.Dock = DockStyle.Top;
            pGapTop.Location = new Point(0, 0);
            pGapTop.Name = "pGapTop";
            pGapTop.Size = new Size(363, 26);
            pGapTop.TabIndex = 8;
            // 
            // panel4
            // 
            panel4.BackColor = Color.LightGray;
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 25);
            panel4.Name = "panel4";
            panel4.Size = new Size(363, 1);
            panel4.TabIndex = 25;
            // 
            // lGapTitle
            // 
            lGapTitle.AutoSize = true;
            lGapTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lGapTitle.Location = new Point(8, 6);
            lGapTitle.Name = "lGapTitle";
            lGapTitle.Size = new Size(100, 15);
            lGapTitle.TabIndex = 0;
            lGapTitle.Text = "Gap Connections";
            // 
            // tGeneral
            // 
            tGeneral.BackColor = Color.White;
            tGeneral.Controls.Add(splitGeneral);
            tGeneral.Location = new Point(4, 24);
            tGeneral.Name = "tGeneral";
            tGeneral.Size = new Size(682, 555);
            tGeneral.TabIndex = 4;
            tGeneral.Text = "General";
            tGeneral.Visible = false;
            // 
            // splitGeneral
            // 
            splitGeneral.BackColor = Color.FromArgb(55, 71, 79);
            splitGeneral.Dock = DockStyle.Fill;
            splitGeneral.Location = new Point(0, 0);
            splitGeneral.Name = "splitGeneral";
            splitGeneral.Orientation = Orientation.Horizontal;
            // 
            // splitGeneral.Panel1
            // 
            splitGeneral.Panel1.BackColor = Color.White;
            splitGeneral.Panel1.Controls.Add(lModelName);
            splitGeneral.Panel1.Controls.Add(lDescription);
            splitGeneral.Panel1.Controls.Add(eModelDescription);
            splitGeneral.Panel1.Controls.Add(eModelName);
            // 
            // splitGeneral.Panel2
            // 
            splitGeneral.Panel2.Controls.Add(splitDimensions);
            splitGeneral.Size = new Size(682, 555);
            splitGeneral.SplitterDistance = 98;
            splitGeneral.SplitterWidth = 2;
            splitGeneral.TabIndex = 62;
            // 
            // lModelName
            // 
            lModelName.AutoSize = true;
            lModelName.Location = new Point(3, 9);
            lModelName.Name = "lModelName";
            lModelName.Size = new Size(76, 15);
            lModelName.TabIndex = 0;
            lModelName.Text = "Model Name";
            // 
            // lDescription
            // 
            lDescription.AutoSize = true;
            lDescription.Location = new Point(3, 36);
            lDescription.Name = "lDescription";
            lDescription.Size = new Size(67, 15);
            lDescription.TabIndex = 1;
            lDescription.Text = "Description";
            // 
            // eModelDescription
            // 
            eModelDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eModelDescription.Location = new Point(101, 33);
            eModelDescription.Multiline = true;
            eModelDescription.Name = "eModelDescription";
            eModelDescription.ScrollBars = ScrollBars.Vertical;
            eModelDescription.Size = new Size(578, 62);
            eModelDescription.TabIndex = 7;
            // 
            // eModelName
            // 
            eModelName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eModelName.Location = new Point(101, 6);
            eModelName.Name = "eModelName";
            eModelName.Size = new Size(578, 23);
            eModelName.TabIndex = 6;
            // 
            // splitDimensions
            // 
            splitDimensions.BackColor = Color.FromArgb(55, 71, 79);
            splitDimensions.BorderStyle = BorderStyle.FixedSingle;
            splitDimensions.Dock = DockStyle.Fill;
            splitDimensions.Location = new Point(0, 0);
            splitDimensions.Name = "splitDimensions";
            // 
            // splitDimensions.Panel1
            // 
            splitDimensions.Panel1.Controls.Add(propModelDimensions);
            // 
            // splitDimensions.Panel2
            // 
            splitDimensions.Panel2.Controls.Add(pDrawing);
            splitDimensions.Size = new Size(682, 456);
            splitDimensions.SplitterDistance = 258;
            splitDimensions.SplitterWidth = 2;
            splitDimensions.TabIndex = 61;
            // 
            // propModelDimensions
            // 
            propModelDimensions.BackColor = Color.FromArgb(207, 216, 220);
            propModelDimensions.Dock = DockStyle.Fill;
            propModelDimensions.HelpBackColor = Color.FromArgb(207, 216, 220);
            propModelDimensions.HelpBorderColor = Color.FromArgb(0, 184, 212);
            propModelDimensions.Location = new Point(0, 0);
            propModelDimensions.Name = "propModelDimensions";
            propModelDimensions.Size = new Size(256, 454);
            propModelDimensions.TabIndex = 60;
            // 
            // pDrawing
            // 
            pDrawing.AutoScroll = true;
            pDrawing.Controls.Add(pbDrawing);
            pDrawing.Dock = DockStyle.Fill;
            pDrawing.Location = new Point(0, 0);
            pDrawing.Name = "pDrawing";
            pDrawing.Size = new Size(421, 454);
            pDrawing.TabIndex = 15;
            // 
            // pbDrawing
            // 
            pbDrawing.BackColor = Color.White;
            pbDrawing.Dock = DockStyle.Fill;
            pbDrawing.Image = (Image)resources.GetObject("pbDrawing.Image");
            pbDrawing.Location = new Point(0, 0);
            pbDrawing.Name = "pbDrawing";
            pbDrawing.Size = new Size(421, 454);
            pbDrawing.SizeMode = PictureBoxSizeMode.Zoom;
            pbDrawing.TabIndex = 12;
            pbDrawing.TabStop = false;
            // 
            // tabModel
            // 
            tabModel.Controls.Add(tGeneral);
            tabModel.Controls.Add(tArchitecture);
            tabModel.Controls.Add(tKinematics);
            tabModel.Controls.Add(tSettings);
            tabModel.Controls.Add(tJson);
            tabModel.Dock = DockStyle.Fill;
            tabModel.Location = new Point(0, 0);
            tabModel.Name = "tabModel";
            tabModel.SelectedIndex = 0;
            tabModel.Size = new Size(690, 583);
            tabModel.TabIndex = 2;
            tabModel.Tag = "";
            // 
            // saveFileCSV
            // 
            saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // openFileCSV
            // 
            openFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // ModelControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabModel);
            Name = "ModelControl";
            Size = new Size(690, 583);
            tJson.ResumeLayout(false);
            pModelJSONTop.ResumeLayout(false);
            pModelJSONTop.PerformLayout();
            tSettings.ResumeLayout(false);
            pSettingsTop.ResumeLayout(false);
            pSettingsTop.PerformLayout();
            tKinematics.ResumeLayout(false);
            pKinematicsDescription.ResumeLayout(false);
            pKinematicsDescription.PerformLayout();
            tArchitecture.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            splitCellsAndStimuli.Panel1.ResumeLayout(false);
            splitCellsAndStimuli.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitCellsAndStimuli).EndInit();
            splitCellsAndStimuli.ResumeLayout(false);
            splitCellPoolsAndCells.Panel1.ResumeLayout(false);
            splitCellPoolsAndCells.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitCellPoolsAndCells).EndInit();
            splitCellPoolsAndCells.ResumeLayout(false);
            pCellPoolsTop.ResumeLayout(false);
            pCellPoolsTop.PerformLayout();
            pCellsTop.ResumeLayout(false);
            pCellsTop.PerformLayout();
            pStimuliTop.ResumeLayout(false);
            pStimuliTop.PerformLayout();
            splitConnections.Panel1.ResumeLayout(false);
            splitConnections.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitConnections).EndInit();
            splitConnections.ResumeLayout(false);
            splitChemicalJunctions.Panel1.ResumeLayout(false);
            splitChemicalJunctions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitChemicalJunctions).EndInit();
            splitChemicalJunctions.ResumeLayout(false);
            pIncomingTop.ResumeLayout(false);
            pIncomingTop.PerformLayout();
            pOutgoingTop.ResumeLayout(false);
            pOutgoingTop.PerformLayout();
            pGapTop.ResumeLayout(false);
            pGapTop.PerformLayout();
            tGeneral.ResumeLayout(false);
            splitGeneral.Panel1.ResumeLayout(false);
            splitGeneral.Panel1.PerformLayout();
            splitGeneral.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitGeneral).EndInit();
            splitGeneral.ResumeLayout(false);
            splitDimensions.Panel1.ResumeLayout(false);
            splitDimensions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitDimensions).EndInit();
            splitDimensions.ResumeLayout(false);
            pDrawing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbDrawing).EndInit();
            tabModel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private OpenFileDialog openFileJson;
        private SaveFileDialog saveFileJson;
        private TabPage tJson;
        private RichTextBox eModelJSON;
        private Panel pModelJSONTop;
        private Panel pLineModelJSON;
        private LinkLabel linkSaveModelJSON;
        private Button btnLoadModelJSON;
        private Button btnDisplayModelJSON;
        private TabPage tSettings;
        private PropertyGrid propSettings;
        private Panel pSettingsTop;
        private ComboBox ddDefaultMuscleCellCore;
        private Label lDefaultMuscleCore;
        private ComboBox ddDefaultNeuronCore;
        private Label lDefaultNeuronCore;
        private TabPage tKinematics;
        private PropertyGrid propKinematics;
        private Panel pKinematicsDescription;
        private TextBox eKinematicsDescription;
        private Panel pKinemSep;
        private TabPage tArchitecture;
        private SplitContainer splitMain;
        private SplitContainer splitCellPoolsAndCells;
        private ListBoxControl listCellPools;
        private ListBoxControl listCells;
        private Panel pCellsTop;
        private Panel pCellsSep;
        private Label lCellsTitle;
        private Panel pCellPoolsTop;
        private Panel pCellPoolsSep;
        private Label lCellPoolsTitle;
        private SplitContainer splitConnections;
        private Panel pIncomingTop;
        private Panel pConnectionsSep;
        private Label lIncomingTitle;
        private ListBoxControl listStimuli;
        private Panel pStimuliTop;
        private Panel pStimuliSep;
        private Label lStimuliTitle;
        private TabPage tGeneral;
        private SplitContainer splitDimensions;
        private PropertyGrid propModelDimensions;
        private Panel pDrawing;
        private PictureBox pbDrawing;
        private TextBox eModelDescription;
        private TextBox eModelName;
        private Label lDescription;
        private Label lModelName;
        private TabControl tabModel;
        private LinkLabel linkOpenJsonViewer;
        private SaveFileDialog saveFileCSV;
        private OpenFileDialog openFileCSV;
        private SplitContainer splitGeneral;
        private SplitContainer splitCellsAndStimuli;
        private SplitContainer splitChemicalJunctions;
        private Panel pOutgoingTop;
        private Panel panel2;
        private Label lOutgoingTitle;
        private Panel pGapTop;
        private Panel panel4;
        private Label lGapTitle;
        private ListBoxControl listIncoming;
        private ListBoxControl listOutgoing;
        private ListBoxControl listGap;
    }
}
