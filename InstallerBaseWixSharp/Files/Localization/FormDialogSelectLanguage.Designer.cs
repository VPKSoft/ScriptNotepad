namespace InstallerBaseWixSharp.Files.Localization
{
    partial class FormDialogSelectLanguage
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
            this.lbInstallationLanguage = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.btNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbInstallationLanguage
            // 
            this.lbInstallationLanguage.AutoSize = true;
            this.lbInstallationLanguage.Location = new System.Drawing.Point(12, 9);
            this.lbInstallationLanguage.Name = "lbInstallationLanguage";
            this.lbInstallationLanguage.Size = new System.Drawing.Size(134, 13);
            this.lbInstallationLanguage.TabIndex = 0;
            this.lbInstallationLanguage.Text = "Select the setup language:";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLanguage.DisplayMember = "Value";
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(15, 25);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(306, 21);
            this.cmbLanguage.TabIndex = 1;
            this.cmbLanguage.ValueMember = "Key";
            // 
            // btNext
            // 
            this.btNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btNext.Location = new System.Drawing.Point(246, 52);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 2;
            this.btNext.Text = "Next >>";
            this.btNext.UseVisualStyleBackColor = true;
            // 
            // FormDialogSelectLanguage
            // 
            this.AcceptButton = this.btNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btNext;
            this.ClientSize = new System.Drawing.Size(333, 87);
            this.ControlBox = false;
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.cmbLanguage);
            this.Controls.Add(this.lbInstallationLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogSelectLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language Selection";
            this.Shown += new System.EventHandler(this.FormDialogSelectLanguage_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInstallationLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Button btNext;
    }
}



