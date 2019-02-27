namespace ScriptNotepad.Settings
{
    partial class FormSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpgGeneralSettings = new System.Windows.Forms.TabPage();
            this.gpDefaultEncoding = new System.Windows.Forms.GroupBox();
            this.cmbCharacterSet = new System.Windows.Forms.ComboBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.lbCharacterSet = new System.Windows.Forms.Label();
            this.lbEncoding = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.btSystemDefaultEncoding = new System.Windows.Forms.PictureBox();
            this.btUTF8 = new System.Windows.Forms.PictureBox();
            this.lbHistoryDocuments = new System.Windows.Forms.Label();
            this.nudHistoryDocuments = new System.Windows.Forms.NumericUpDown();
            this.tcMain.SuspendLayout();
            this.tpgGeneralSettings.SuspendLayout();
            this.gpDefaultEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpgGeneralSettings);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Location = new System.Drawing.Point(12, 12);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(565, 397);
            this.tcMain.TabIndex = 0;
            // 
            // tpgGeneralSettings
            // 
            this.tpgGeneralSettings.Controls.Add(this.nudHistoryDocuments);
            this.tpgGeneralSettings.Controls.Add(this.lbHistoryDocuments);
            this.tpgGeneralSettings.Controls.Add(this.gpDefaultEncoding);
            this.tpgGeneralSettings.Location = new System.Drawing.Point(4, 22);
            this.tpgGeneralSettings.Name = "tpgGeneralSettings";
            this.tpgGeneralSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpgGeneralSettings.Size = new System.Drawing.Size(557, 371);
            this.tpgGeneralSettings.TabIndex = 0;
            this.tpgGeneralSettings.Text = "General";
            this.tpgGeneralSettings.UseVisualStyleBackColor = true;
            // 
            // gpDefaultEncoding
            // 
            this.gpDefaultEncoding.Controls.Add(this.btUTF8);
            this.gpDefaultEncoding.Controls.Add(this.btSystemDefaultEncoding);
            this.gpDefaultEncoding.Controls.Add(this.cmbCharacterSet);
            this.gpDefaultEncoding.Controls.Add(this.cmbEncoding);
            this.gpDefaultEncoding.Controls.Add(this.lbCharacterSet);
            this.gpDefaultEncoding.Controls.Add(this.lbEncoding);
            this.gpDefaultEncoding.Location = new System.Drawing.Point(6, 6);
            this.gpDefaultEncoding.Name = "gpDefaultEncoding";
            this.gpDefaultEncoding.Size = new System.Drawing.Size(536, 86);
            this.gpDefaultEncoding.TabIndex = 8;
            this.gpDefaultEncoding.TabStop = false;
            this.gpDefaultEncoding.Text = "Default encoding";
            // 
            // cmbCharacterSet
            // 
            this.cmbCharacterSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCharacterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCharacterSet.FormattingEnabled = true;
            this.cmbCharacterSet.Location = new System.Drawing.Point(147, 22);
            this.cmbCharacterSet.Name = "cmbCharacterSet";
            this.cmbCharacterSet.Size = new System.Drawing.Size(351, 21);
            this.cmbCharacterSet.TabIndex = 5;
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Location = new System.Drawing.Point(147, 49);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(351, 21);
            this.cmbEncoding.TabIndex = 7;
            // 
            // lbCharacterSet
            // 
            this.lbCharacterSet.AutoSize = true;
            this.lbCharacterSet.Location = new System.Drawing.Point(6, 25);
            this.lbCharacterSet.Name = "lbCharacterSet";
            this.lbCharacterSet.Size = new System.Drawing.Size(73, 13);
            this.lbCharacterSet.TabIndex = 4;
            this.lbCharacterSet.Text = "Character set:";
            // 
            // lbEncoding
            // 
            this.lbEncoding.AutoSize = true;
            this.lbEncoding.Location = new System.Drawing.Point(6, 52);
            this.lbEncoding.Name = "lbEncoding";
            this.lbEncoding.Size = new System.Drawing.Size(55, 13);
            this.lbEncoding.TabIndex = 6;
            this.lbEncoding.Text = "Encoding:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(502, 415);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 415);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btSystemDefaultEncoding
            // 
            this.btSystemDefaultEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSystemDefaultEncoding.Image = global::ScriptNotepad.Properties.Resources._default;
            this.btSystemDefaultEncoding.Location = new System.Drawing.Point(504, 22);
            this.btSystemDefaultEncoding.Name = "btSystemDefaultEncoding";
            this.btSystemDefaultEncoding.Size = new System.Drawing.Size(21, 21);
            this.btSystemDefaultEncoding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btSystemDefaultEncoding.TabIndex = 9;
            this.btSystemDefaultEncoding.TabStop = false;
            this.ttMain.SetToolTip(this.btSystemDefaultEncoding, "Set to system default");
            this.btSystemDefaultEncoding.Click += new System.EventHandler(this.btDefaultEncodings_Click);
            // 
            // btUTF8
            // 
            this.btUTF8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUTF8.Image = global::ScriptNotepad.Properties.Resources.unicode;
            this.btUTF8.Location = new System.Drawing.Point(504, 49);
            this.btUTF8.Name = "btUTF8";
            this.btUTF8.Size = new System.Drawing.Size(21, 21);
            this.btUTF8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btUTF8.TabIndex = 10;
            this.btUTF8.TabStop = false;
            this.ttMain.SetToolTip(this.btUTF8, "Set to unicode (UTF8)");
            this.btUTF8.Click += new System.EventHandler(this.btDefaultEncodings_Click);
            // 
            // lbHistoryDocuments
            // 
            this.lbHistoryDocuments.AutoSize = true;
            this.lbHistoryDocuments.Location = new System.Drawing.Point(3, 100);
            this.lbHistoryDocuments.Name = "lbHistoryDocuments";
            this.lbHistoryDocuments.Size = new System.Drawing.Size(232, 13);
            this.lbHistoryDocuments.TabIndex = 9;
            this.lbHistoryDocuments.Text = "How many documents to keep in the file history:";
            // 
            // nudHistoryDocuments
            // 
            this.nudHistoryDocuments.Location = new System.Drawing.Point(452, 98);
            this.nudHistoryDocuments.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudHistoryDocuments.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudHistoryDocuments.Name = "nudHistoryDocuments";
            this.nudHistoryDocuments.Size = new System.Drawing.Size(90, 20);
            this.nudHistoryDocuments.TabIndex = 10;
            this.nudHistoryDocuments.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // FormSettings
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Shown += new System.EventHandler(this.FormSettings_Shown);
            this.tcMain.ResumeLayout(false);
            this.tpgGeneralSettings.ResumeLayout(false);
            this.tpgGeneralSettings.PerformLayout();
            this.gpDefaultEncoding.ResumeLayout(false);
            this.gpDefaultEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpgGeneralSettings;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gpDefaultEncoding;
        private System.Windows.Forms.ComboBox cmbCharacterSet;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label lbCharacterSet;
        private System.Windows.Forms.Label lbEncoding;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.PictureBox btUTF8;
        private System.Windows.Forms.PictureBox btSystemDefaultEncoding;
        private System.Windows.Forms.NumericUpDown nudHistoryDocuments;
        private System.Windows.Forms.Label lbHistoryDocuments;
    }
}