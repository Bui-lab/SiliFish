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
            pBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eNumber).BeginInit();
            SuspendLayout();
            // 
            // pBottom
            // 
            pBottom.Controls.Add(btnCancel);
            pBottom.Controls.Add(btnStart);
            pBottom.Dock = DockStyle.Bottom;
            pBottom.Location = new Point(0, 124);
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
            // RunTimeStatsDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 161);
            Controls.Add(lNumber);
            Controls.Add(eNumber);
            Controls.Add(lMessage);
            Controls.Add(pBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RunTimeStatsDialog";
            Text = "Run Time Stats";
            pBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)eNumber).EndInit();
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
    }
}