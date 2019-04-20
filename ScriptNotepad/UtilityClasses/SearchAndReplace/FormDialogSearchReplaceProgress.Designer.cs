namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    partial class FormDialogSearchReplaceProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogSearchReplaceProgress));
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.lbProgressDesc = new System.Windows.Forms.Label();
            this.bwMain = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.Location = new System.Drawing.Point(12, 12);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(626, 17);
            this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbMain.TabIndex = 1;
            // 
            // lbProgressDesc
            // 
            this.lbProgressDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProgressDesc.AutoEllipsis = true;
            this.lbProgressDesc.Location = new System.Drawing.Point(12, 35);
            this.lbProgressDesc.Margin = new System.Windows.Forms.Padding(3);
            this.lbProgressDesc.Name = "lbProgressDesc";
            this.lbProgressDesc.Size = new System.Drawing.Size(626, 17);
            this.lbProgressDesc.TabIndex = 2;
            this.lbProgressDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bwMain
            // 
            this.bwMain.WorkerReportsProgress = true;
            this.bwMain.WorkerSupportsCancellation = true;
            this.bwMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwMain_DoWork);
            this.bwMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwMain_RunWorkerCompleted);
            // 
            // FormDialogSearchReplaceProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 70);
            this.Controls.Add(this.lbProgressDesc);
            this.Controls.Add(this.pbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDialogSearchReplaceProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress..";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDialogSearchReplaceProgress_FormClosing);
            this.Shown += new System.EventHandler(this.FormDialogCommonProgress_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.Label lbProgressDesc;
        private System.ComponentModel.BackgroundWorker bwMain;
    }
}