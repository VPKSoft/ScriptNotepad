namespace ScriptNotepad.DialogForms
{
    partial class FormDialogQueryEncoding
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogQueryEncoding));
            this.lbCharacterSet = new System.Windows.Forms.Label();
            this.cmbCharacterSet = new System.Windows.Forms.ComboBox();
            this.lbEncoding = new System.Windows.Forms.Label();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btUTF8 = new System.Windows.Forms.PictureBox();
            this.btSystemDefaultEncoding = new System.Windows.Forms.PictureBox();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.pbClearFilterText = new System.Windows.Forms.PictureBox();
            this.lbFilterEncodings = new System.Windows.Forms.Label();
            this.tbFilterEncodings = new System.Windows.Forms.TextBox();
            this.cbUseUnicodeBOM = new System.Windows.Forms.CheckBox();
            this.cbUnicodeFailInvalidCharacters = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClearFilterText)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCharacterSet
            // 
            this.lbCharacterSet.AutoSize = true;
            this.lbCharacterSet.Location = new System.Drawing.Point(12, 9);
            this.lbCharacterSet.Name = "lbCharacterSet";
            this.lbCharacterSet.Size = new System.Drawing.Size(73, 13);
            this.lbCharacterSet.TabIndex = 0;
            this.lbCharacterSet.Text = "Character set:";
            // 
            // cmbCharacterSet
            // 
            this.cmbCharacterSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCharacterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCharacterSet.FormattingEnabled = true;
            this.cmbCharacterSet.Location = new System.Drawing.Point(153, 6);
            this.cmbCharacterSet.Name = "cmbCharacterSet";
            this.cmbCharacterSet.Size = new System.Drawing.Size(269, 21);
            this.cmbCharacterSet.TabIndex = 1;
            this.cmbCharacterSet.SelectedIndexChanged += new System.EventHandler(this.cmbCharacterSet_SelectedIndexChanged);
            // 
            // lbEncoding
            // 
            this.lbEncoding.AutoSize = true;
            this.lbEncoding.Location = new System.Drawing.Point(12, 36);
            this.lbEncoding.Name = "lbEncoding";
            this.lbEncoding.Size = new System.Drawing.Size(55, 13);
            this.lbEncoding.TabIndex = 2;
            this.lbEncoding.Text = "Encoding:";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Location = new System.Drawing.Point(153, 33);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(269, 21);
            this.cmbEncoding.TabIndex = 3;
            this.cmbEncoding.SelectedIndexChanged += new System.EventHandler(this.cmbCharacterSet_SelectedIndexChanged);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 114);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(374, 114);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btUTF8
            // 
            this.btUTF8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUTF8.Image = global::ScriptNotepad.Properties.Resources.unicode;
            this.btUTF8.Location = new System.Drawing.Point(428, 33);
            this.btUTF8.Name = "btUTF8";
            this.btUTF8.Size = new System.Drawing.Size(21, 21);
            this.btUTF8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btUTF8.TabIndex = 12;
            this.btUTF8.TabStop = false;
            this.ttMain.SetToolTip(this.btUTF8, "Set to unicode (UTF8)");
            this.btUTF8.Click += new System.EventHandler(this.btDefaultEncodings_Click);
            // 
            // btSystemDefaultEncoding
            // 
            this.btSystemDefaultEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSystemDefaultEncoding.Image = global::ScriptNotepad.Properties.Resources.default_image;
            this.btSystemDefaultEncoding.Location = new System.Drawing.Point(428, 6);
            this.btSystemDefaultEncoding.Name = "btSystemDefaultEncoding";
            this.btSystemDefaultEncoding.Size = new System.Drawing.Size(21, 21);
            this.btSystemDefaultEncoding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btSystemDefaultEncoding.TabIndex = 11;
            this.btSystemDefaultEncoding.TabStop = false;
            this.ttMain.SetToolTip(this.btSystemDefaultEncoding, "Set to system default");
            this.btSystemDefaultEncoding.Click += new System.EventHandler(this.btDefaultEncodings_Click);
            // 
            // pbClearFilterText
            // 
            this.pbClearFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbClearFilterText.Image = global::ScriptNotepad.Properties.Resources.Erase;
            this.pbClearFilterText.Location = new System.Drawing.Point(428, 83);
            this.pbClearFilterText.Name = "pbClearFilterText";
            this.pbClearFilterText.Size = new System.Drawing.Size(21, 21);
            this.pbClearFilterText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbClearFilterText.TabIndex = 15;
            this.pbClearFilterText.TabStop = false;
            this.ttMain.SetToolTip(this.pbClearFilterText, "Clear filter text");
            this.pbClearFilterText.Click += new System.EventHandler(this.pbClearFilterText_Click);
            // 
            // lbFilterEncodings
            // 
            this.lbFilterEncodings.AutoSize = true;
            this.lbFilterEncodings.Location = new System.Drawing.Point(12, 88);
            this.lbFilterEncodings.Name = "lbFilterEncodings";
            this.lbFilterEncodings.Size = new System.Drawing.Size(84, 13);
            this.lbFilterEncodings.TabIndex = 13;
            this.lbFilterEncodings.Text = "Filter encodings:";
            // 
            // tbFilterEncodings
            // 
            this.tbFilterEncodings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterEncodings.Location = new System.Drawing.Point(153, 83);
            this.tbFilterEncodings.Name = "tbFilterEncodings";
            this.tbFilterEncodings.Size = new System.Drawing.Size(269, 20);
            this.tbFilterEncodings.TabIndex = 14;
            // 
            // cbUseUnicodeBOM
            // 
            this.cbUseUnicodeBOM.AutoSize = true;
            this.cbUseUnicodeBOM.Location = new System.Drawing.Point(15, 60);
            this.cbUseUnicodeBOM.Name = "cbUseUnicodeBOM";
            this.cbUseUnicodeBOM.Size = new System.Drawing.Size(115, 17);
            this.cbUseUnicodeBOM.TabIndex = 16;
            this.cbUseUnicodeBOM.Text = "Use Unicode BOM";
            this.cbUseUnicodeBOM.UseVisualStyleBackColor = true;
            // 
            // cbUnicodeFailInvalidCharacters
            // 
            this.cbUnicodeFailInvalidCharacters.AutoSize = true;
            this.cbUnicodeFailInvalidCharacters.Location = new System.Drawing.Point(153, 60);
            this.cbUnicodeFailInvalidCharacters.Name = "cbUnicodeFailInvalidCharacters";
            this.cbUnicodeFailInvalidCharacters.Size = new System.Drawing.Size(181, 17);
            this.cbUnicodeFailInvalidCharacters.TabIndex = 17;
            this.cbUnicodeFailInvalidCharacters.Text = "Fail on invalid Unicode character";
            this.cbUnicodeFailInvalidCharacters.UseVisualStyleBackColor = true;
            // 
            // FormDialogQueryEncoding
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(461, 149);
            this.Controls.Add(this.cbUnicodeFailInvalidCharacters);
            this.Controls.Add(this.cbUseUnicodeBOM);
            this.Controls.Add(this.pbClearFilterText);
            this.Controls.Add(this.tbFilterEncodings);
            this.Controls.Add(this.lbFilterEncodings);
            this.Controls.Add(this.btUTF8);
            this.Controls.Add(this.btSystemDefaultEncoding);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cmbEncoding);
            this.Controls.Add(this.lbEncoding);
            this.Controls.Add(this.cmbCharacterSet);
            this.Controls.Add(this.lbCharacterSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogQueryEncoding";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select encoding";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDialogQueryEncoding_FormClosing);
            this.Shown += new System.EventHandler(this.FormDialogQueryEncoding_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClearFilterText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCharacterSet;
        private System.Windows.Forms.ComboBox cmbCharacterSet;
        private System.Windows.Forms.Label lbEncoding;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.PictureBox btUTF8;
        private System.Windows.Forms.PictureBox btSystemDefaultEncoding;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.Label lbFilterEncodings;
        private System.Windows.Forms.TextBox tbFilterEncodings;
        private System.Windows.Forms.PictureBox pbClearFilterText;
        private System.Windows.Forms.CheckBox cbUseUnicodeBOM;
        private System.Windows.Forms.CheckBox cbUnicodeFailInvalidCharacters;
    }
}