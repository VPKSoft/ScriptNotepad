namespace ScriptNotepad.DialogForms
{
    partial class FormDialogQueryNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogQueryNumber));
            this.lbFunctionDescription = new System.Windows.Forms.Label();
            this.nudValueStart = new System.Windows.Forms.NumericUpDown();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbDelimiter = new System.Windows.Forms.Label();
            this.nudValueEnd = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudValueStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValueEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFunctionDescription
            // 
            this.lbFunctionDescription.AutoSize = true;
            this.lbFunctionDescription.Location = new System.Drawing.Point(12, 14);
            this.lbFunctionDescription.Name = "lbFunctionDescription";
            this.lbFunctionDescription.Size = new System.Drawing.Size(280, 13);
            this.lbFunctionDescription.TabIndex = 0;
            this.lbFunctionDescription.Text = "The description to where the entered number will be used:";
            // 
            // nudValueStart
            // 
            this.nudValueStart.Location = new System.Drawing.Point(12, 38);
            this.nudValueStart.Name = "nudValueStart";
            this.nudValueStart.Size = new System.Drawing.Size(140, 20);
            this.nudValueStart.TabIndex = 2;
            this.nudValueStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 64);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 3;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(242, 64);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // lbDelimiter
            // 
            this.lbDelimiter.AutoSize = true;
            this.lbDelimiter.Location = new System.Drawing.Point(158, 38);
            this.lbDelimiter.Name = "lbDelimiter";
            this.lbDelimiter.Size = new System.Drawing.Size(13, 13);
            this.lbDelimiter.TabIndex = 5;
            this.lbDelimiter.Text = "_";
            // 
            // nudValueEnd
            // 
            this.nudValueEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudValueEnd.Location = new System.Drawing.Point(177, 38);
            this.nudValueEnd.Name = "nudValueEnd";
            this.nudValueEnd.Size = new System.Drawing.Size(140, 20);
            this.nudValueEnd.TabIndex = 6;
            // 
            // FormDialogQueryNumber
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(329, 99);
            this.Controls.Add(this.nudValueEnd);
            this.Controls.Add(this.lbDelimiter);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.nudValueStart);
            this.Controls.Add(this.lbFunctionDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogQueryNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormDialogQueryNumber";
            ((System.ComponentModel.ISupportInitialize)(this.nudValueStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValueEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFunctionDescription;
        private System.Windows.Forms.NumericUpDown nudValueStart;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lbDelimiter;
        private System.Windows.Forms.NumericUpDown nudValueEnd;
    }
}