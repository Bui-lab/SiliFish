namespace SiliFish.UI.Controls.Model_Edit
{
    partial class ConnectionSelectionControl
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
            cbChemOut = new CheckBox();
            cbChemIn = new CheckBox();
            cbGapJunctions = new CheckBox();
            lLabel = new Label();
            SuspendLayout();
            // 
            // cbChemOut
            // 
            cbChemOut.AutoSize = true;
            cbChemOut.Location = new Point(25, 96);
            cbChemOut.Name = "cbChemOut";
            cbChemOut.Size = new Size(183, 19);
            cbChemOut.TabIndex = 7;
            cbChemOut.Text = "Outgoing Chemical Junctions";
            cbChemOut.UseVisualStyleBackColor = true;
            // 
            // cbChemIn
            // 
            cbChemIn.AutoSize = true;
            cbChemIn.Location = new Point(25, 71);
            cbChemIn.Name = "cbChemIn";
            cbChemIn.Size = new Size(183, 19);
            cbChemIn.TabIndex = 6;
            cbChemIn.Text = "Incoming Chemical Junctions";
            cbChemIn.UseVisualStyleBackColor = true;
            // 
            // cbGapJunctions
            // 
            cbGapJunctions.AutoSize = true;
            cbGapJunctions.Location = new Point(25, 46);
            cbGapJunctions.Name = "cbGapJunctions";
            cbGapJunctions.Size = new Size(100, 19);
            cbGapJunctions.TabIndex = 5;
            cbGapJunctions.Text = "Gap Junctions";
            cbGapJunctions.UseVisualStyleBackColor = true;
            // 
            // lLabel
            // 
            lLabel.AutoSize = true;
            lLabel.Location = new Point(25, 18);
            lLabel.Name = "lLabel";
            lLabel.Size = new Size(251, 15);
            lLabel.TabIndex = 4;
            lLabel.Text = "Please select the junctions you want to export.";
            // 
            // ConnectionSelectionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cbChemOut);
            Controls.Add(cbChemIn);
            Controls.Add(cbGapJunctions);
            Controls.Add(lLabel);
            Name = "ConnectionSelectionControl";
            Size = new Size(313, 126);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cbChemOut;
        private CheckBox cbChemIn;
        private CheckBox cbGapJunctions;
        private Label lLabel;
    }
}
