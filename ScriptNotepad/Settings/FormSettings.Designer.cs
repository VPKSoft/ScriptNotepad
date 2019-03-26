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
            this.btSelectPluginFolder = new System.Windows.Forms.Button();
            this.tbPluginFolder = new System.Windows.Forms.TextBox();
            this.lbPluginFolder = new System.Windows.Forms.Label();
            this.lbSelectLanguageDescription = new System.Windows.Forms.Label();
            this.cmbSelectLanguageValue = new System.Windows.Forms.ComboBox();
            this.cbDocumentContentHistory = new System.Windows.Forms.CheckBox();
            this.nudDocumentContentHistory = new System.Windows.Forms.NumericUpDown();
            this.lbDocumentContentHistory = new System.Windows.Forms.Label();
            this.nudHistoryDocuments = new System.Windows.Forms.NumericUpDown();
            this.lbHistoryDocuments = new System.Windows.Forms.Label();
            this.gpDefaultEncoding = new System.Windows.Forms.GroupBox();
            this.btUTF8 = new System.Windows.Forms.PictureBox();
            this.btSystemDefaultEncoding = new System.Windows.Forms.PictureBox();
            this.cmbCharacterSet = new System.Windows.Forms.ComboBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.lbCharacterSet = new System.Windows.Forms.Label();
            this.lbEncoding = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.pbDefaultFolder = new System.Windows.Forms.PictureBox();
            this.tcMain.SuspendLayout();
            this.tpgGeneralSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentContentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).BeginInit();
            this.gpDefaultEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefaultFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpgGeneralSettings);
            this.tcMain.Location = new System.Drawing.Point(12, 12);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(565, 397);
            this.tcMain.TabIndex = 0;
            // 
            // tpgGeneralSettings
            // 
            this.tpgGeneralSettings.Controls.Add(this.pbDefaultFolder);
            this.tpgGeneralSettings.Controls.Add(this.btSelectPluginFolder);
            this.tpgGeneralSettings.Controls.Add(this.tbPluginFolder);
            this.tpgGeneralSettings.Controls.Add(this.lbPluginFolder);
            this.tpgGeneralSettings.Controls.Add(this.lbSelectLanguageDescription);
            this.tpgGeneralSettings.Controls.Add(this.cmbSelectLanguageValue);
            this.tpgGeneralSettings.Controls.Add(this.cbDocumentContentHistory);
            this.tpgGeneralSettings.Controls.Add(this.nudDocumentContentHistory);
            this.tpgGeneralSettings.Controls.Add(this.lbDocumentContentHistory);
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
            // btSelectPluginFolder
            // 
            this.btSelectPluginFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectPluginFolder.Location = new System.Drawing.Point(520, 195);
            this.btSelectPluginFolder.Name = "btSelectPluginFolder";
            this.btSelectPluginFolder.Size = new System.Drawing.Size(31, 20);
            this.btSelectPluginFolder.TabIndex = 30;
            this.btSelectPluginFolder.Text = "...";
            this.btSelectPluginFolder.UseVisualStyleBackColor = true;
            this.btSelectPluginFolder.Click += new System.EventHandler(this.btSelectPluginFolder_Click);
            // 
            // tbPluginFolder
            // 
            this.tbPluginFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginFolder.Location = new System.Drawing.Point(225, 195);
            this.tbPluginFolder.Name = "tbPluginFolder";
            this.tbPluginFolder.Size = new System.Drawing.Size(263, 20);
            this.tbPluginFolder.TabIndex = 29;
            this.tbPluginFolder.TextChanged += new System.EventHandler(this.tbPluginFolder_TextChanged);
            // 
            // lbPluginFolder
            // 
            this.lbPluginFolder.AutoSize = true;
            this.lbPluginFolder.Location = new System.Drawing.Point(6, 199);
            this.lbPluginFolder.Name = "lbPluginFolder";
            this.lbPluginFolder.Size = new System.Drawing.Size(164, 13);
            this.lbPluginFolder.TabIndex = 28;
            this.lbPluginFolder.Text = "Plug-in path (a restart is required):";
            // 
            // lbSelectLanguageDescription
            // 
            this.lbSelectLanguageDescription.AutoSize = true;
            this.lbSelectLanguageDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbSelectLanguageDescription.Location = new System.Drawing.Point(3, 162);
            this.lbSelectLanguageDescription.Margin = new System.Windows.Forms.Padding(17, 15, 17, 15);
            this.lbSelectLanguageDescription.Name = "lbSelectLanguageDescription";
            this.lbSelectLanguageDescription.Size = new System.Drawing.Size(156, 13);
            this.lbSelectLanguageDescription.TabIndex = 26;
            this.lbSelectLanguageDescription.Text = "Language (a restart is required):";
            this.lbSelectLanguageDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSelectLanguageValue
            // 
            this.cmbSelectLanguageValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSelectLanguageValue.DisplayMember = "DisplayName";
            this.cmbSelectLanguageValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectLanguageValue.FormattingEnabled = true;
            this.cmbSelectLanguageValue.Location = new System.Drawing.Point(225, 159);
            this.cmbSelectLanguageValue.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.cmbSelectLanguageValue.Name = "cmbSelectLanguageValue";
            this.cmbSelectLanguageValue.Size = new System.Drawing.Size(326, 21);
            this.cmbSelectLanguageValue.TabIndex = 27;
            // 
            // cbDocumentContentHistory
            // 
            this.cbDocumentContentHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDocumentContentHistory.AutoSize = true;
            this.cbDocumentContentHistory.Checked = true;
            this.cbDocumentContentHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDocumentContentHistory.Location = new System.Drawing.Point(431, 126);
            this.cbDocumentContentHistory.Name = "cbDocumentContentHistory";
            this.cbDocumentContentHistory.Size = new System.Drawing.Size(15, 14);
            this.cbDocumentContentHistory.TabIndex = 13;
            this.cbDocumentContentHistory.UseVisualStyleBackColor = true;
            this.cbDocumentContentHistory.CheckedChanged += new System.EventHandler(this.cbDocumentContentHistory_CheckedChanged);
            // 
            // nudDocumentContentHistory
            // 
            this.nudDocumentContentHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDocumentContentHistory.Location = new System.Drawing.Point(452, 124);
            this.nudDocumentContentHistory.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudDocumentContentHistory.Name = "nudDocumentContentHistory";
            this.nudDocumentContentHistory.Size = new System.Drawing.Size(90, 20);
            this.nudDocumentContentHistory.TabIndex = 12;
            this.nudDocumentContentHistory.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lbDocumentContentHistory
            // 
            this.lbDocumentContentHistory.AutoSize = true;
            this.lbDocumentContentHistory.Location = new System.Drawing.Point(3, 126);
            this.lbDocumentContentHistory.Name = "lbDocumentContentHistory";
            this.lbDocumentContentHistory.Size = new System.Drawing.Size(289, 13);
            this.lbDocumentContentHistory.TabIndex = 11;
            this.lbDocumentContentHistory.Text = "How many closed document contents to keep in the history:";
            // 
            // nudHistoryDocuments
            // 
            this.nudHistoryDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            20,
            0,
            0,
            0});
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
            // gpDefaultEncoding
            // 
            this.gpDefaultEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // pbDefaultFolder
            // 
            this.pbDefaultFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDefaultFolder.Image = global::ScriptNotepad.Properties.Resources._default;
            this.pbDefaultFolder.Location = new System.Drawing.Point(494, 195);
            this.pbDefaultFolder.Name = "pbDefaultFolder";
            this.pbDefaultFolder.Size = new System.Drawing.Size(21, 21);
            this.pbDefaultFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbDefaultFolder.TabIndex = 31;
            this.pbDefaultFolder.TabStop = false;
            this.ttMain.SetToolTip(this.pbDefaultFolder, "Set to default");
            this.pbDefaultFolder.Click += new System.EventHandler(this.pbDefaultFolder_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentContentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).EndInit();
            this.gpDefaultEncoding.ResumeLayout(false);
            this.gpDefaultEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefaultFolder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpgGeneralSettings;
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
        private System.Windows.Forms.CheckBox cbDocumentContentHistory;
        private System.Windows.Forms.NumericUpDown nudDocumentContentHistory;
        private System.Windows.Forms.Label lbDocumentContentHistory;
        private System.Windows.Forms.Label lbSelectLanguageDescription;
        private System.Windows.Forms.ComboBox cmbSelectLanguageValue;
        private System.Windows.Forms.Button btSelectPluginFolder;
        private System.Windows.Forms.TextBox tbPluginFolder;
        private System.Windows.Forms.Label lbPluginFolder;
        private System.Windows.Forms.PictureBox pbDefaultFolder;
    }
}