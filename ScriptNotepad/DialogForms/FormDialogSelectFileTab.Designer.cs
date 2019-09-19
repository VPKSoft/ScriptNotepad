namespace ScriptNotepad.DialogForms
{
    partial class FormDialogSelectFileTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogSelectFileTab));
            this.dgvOpenFiles = new System.Windows.Forms.DataGridView();
            this.lbFilterFiles = new System.Windows.Forms.Label();
            this.tbFilterFiles = new System.Windows.Forms.TextBox();
            this.colFileImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOpenFiles
            // 
            this.dgvOpenFiles.AllowUserToAddRows = false;
            this.dgvOpenFiles.AllowUserToDeleteRows = false;
            this.dgvOpenFiles.AllowUserToResizeColumns = false;
            this.dgvOpenFiles.AllowUserToResizeRows = false;
            this.dgvOpenFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOpenFiles.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOpenFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileImage,
            this.colFileName});
            this.dgvOpenFiles.Location = new System.Drawing.Point(12, 50);
            this.dgvOpenFiles.MultiSelect = false;
            this.dgvOpenFiles.Name = "dgvOpenFiles";
            this.dgvOpenFiles.RowHeadersVisible = false;
            this.dgvOpenFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOpenFiles.Size = new System.Drawing.Size(588, 287);
            this.dgvOpenFiles.TabIndex = 0;
            this.dgvOpenFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvOpenFiles_CellClick);
            this.dgvOpenFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDialogSelectFileTab_KeyDown);
            // 
            // lbFilterFiles
            // 
            this.lbFilterFiles.AutoSize = true;
            this.lbFilterFiles.Location = new System.Drawing.Point(12, 15);
            this.lbFilterFiles.Name = "lbFilterFiles";
            this.lbFilterFiles.Size = new System.Drawing.Size(32, 13);
            this.lbFilterFiles.TabIndex = 1;
            this.lbFilterFiles.Text = "Filter:";
            // 
            // tbFilterFiles
            // 
            this.tbFilterFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterFiles.HideSelection = false;
            this.tbFilterFiles.Location = new System.Drawing.Point(78, 12);
            this.tbFilterFiles.Name = "tbFilterFiles";
            this.tbFilterFiles.Size = new System.Drawing.Size(522, 20);
            this.tbFilterFiles.TabIndex = 2;
            this.tbFilterFiles.TextChanged += new System.EventHandler(this.TbFilterFiles_TextChanged);
            this.tbFilterFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbFilterFiles_KeyDown);
            // 
            // colFileImage
            // 
            this.colFileImage.HeaderText = "";
            this.colFileImage.Name = "colFileImage";
            this.colFileImage.ReadOnly = true;
            this.colFileImage.Width = 32;
            // 
            // colFileName
            // 
            this.colFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFileName.HeaderText = "File name";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            // 
            // FormDialogSelectFileTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 349);
            this.Controls.Add(this.tbFilterFiles);
            this.Controls.Add(this.lbFilterFiles);
            this.Controls.Add(this.dgvOpenFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDialogSelectFileTab";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find a tab";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDialogSelectFileTab_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOpenFiles;
        private System.Windows.Forms.Label lbFilterFiles;
        private System.Windows.Forms.TextBox tbFilterFiles;
        private System.Windows.Forms.DataGridViewImageColumn colFileImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
    }
}