namespace SiliFish.UI.Dialogs
{
    partial class MultipleRunDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pBottom = new Panel();
            btnCancel = new Button();
            btnStart = new Button();
            lMessage = new Label();
            eNumber = new NumericUpDown();
            lNumber = new Label();
            simulationSettingsControl1 = new Controls.General.SimulationSettingsControl();
            lSimulationSettings = new Label();
            panel1 = new Panel();
            pBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eNumber).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pBottom
            // 
            pBottom.Controls.Add(btnCancel);
            pBottom.Controls.Add(btnStart);
            pBottom.Dock = DockStyle.Bottom;
            pBottom.Location = new Point(0, 239);
            pBottom.Name = "pBottom";
            pBottom.Size = new Size(288, 37);
            pBottom.TabIndex = 24;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(93, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnStart
            // 
            btnStart.DialogResult = DialogResult.OK;
            btnStart.Location = new Point(12, 6);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lMessage
            // 
            lMessage.Location = new Point(12, 20);
            lMessage.Name = "lMessage";
            lMessage.Size = new Size(264, 64);
            lMessage.TabIndex = 25;
            lMessage.Text = "The simulation will be run with the same settings multiple times and time related information will be listed. \r\nOnly the last simulation results will be available.";
            // 
            // eNumber
            // 
            eNumber.Location = new Point(145, 90);
            eNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            eNumber.Name = "eNumber";
            eNumber.Size = new Size(52, 23);
            eNumber.TabIndex = 26;
            eNumber.TextAlign = HorizontalAlignment.Right;
            eNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lNumber
            // 
            lNumber.AutoSize = true;
            lNumber.Location = new Point(12, 92);
            lNumber.Name = "lNumber";
            lNumber.Size = new Size(127, 15);
            lNumber.TabIndex = 27;
            lNumber.Text = "# of simulations to run";
            // 
            // simulationSettingsControl1
            // 
            simulationSettingsControl1.Location = new Point(3, 29);
            simulationSettingsControl1.Name = "simulationSettingsControl1";
            simulationSettingsControl1.Size = new Size(143, 84);
            simulationSettingsControl1.TabIndex = 28;
            // 
            // lSimulationSettings
            // 
            lSimulationSettings.AutoSize = true;
            lSimulationSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lSimulationSettings.Location = new Point(3, 11);
            lSimulationSettings.Name = "lSimulationSettings";
            lSimulationSettings.Size = new Size(115, 15);
            lSimulationSettings.TabIndex = 29;
            lSimulationSettings.Text = "Simulation Settings";
            // 
            // panel1
            // 
            panel1.Controls.Add(lSimulationSettings);
            panel1.Controls.Add(simulationSettingsControl1);
            panel1.Location = new Point(12, 119);
            panel1.Name = "panel1";
            panel1.Size = new Size(264, 116);
            panel1.TabIndex = 30;
            // 
            // MultipleRunDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 276);
            Controls.Add(panel1);
            Controls.Add(lNumber);
            Controls.Add(eNumber);
            Controls.Add(lMessage);
            Controls.Add(pBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MultipleRunDialog";
            Text = "Run Time Stats";
            pBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)eNumber).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pBottom;
        private Button btnCancel;
        private Button btnStart;
        private Label lMessage;
        private NumericUpDown eNumber;
        private Label lNumber;
        private Controls.General.SimulationSettingsControl simulationSettingsControl1;
        private Label lSimulationSettings;
        private Panel panel1;
    }
}