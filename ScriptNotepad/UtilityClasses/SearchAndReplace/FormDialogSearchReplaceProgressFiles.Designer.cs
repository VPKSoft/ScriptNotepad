namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    partial class FormDialogSearchReplaceProgressFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogSearchReplaceProgressFiles));
            this.lbStatus = new System.Windows.Forms.Label();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.lbStatus2 = new System.Windows.Forms.Label();
            this.bwMain = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatus.AutoEllipsis = true;
            this.lbStatus.Location = new System.Drawing.Point(12, 9);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(626, 17);
            this.lbStatus.TabIndex = 0;
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.Location = new System.Drawing.Point(12, 63);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(626, 17);
            this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbMain.TabIndex = 2;
            // 
            // lbStatus2
            // 
            this.lbStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatus2.AutoEllipsis = true;
            this.lbStatus2.Location = new System.Drawing.Point(12, 36);
            this.lbStatus2.Name = "lbStatus2";
            this.lbStatus2.Size = new System.Drawing.Size(626, 17);
            this.lbStatus2.TabIndex = 3;
            // 
            // bwMain
            // 
            this.bwMain.WorkerReportsProgress = true;
            this.bwMain.WorkerSupportsCancellation = true;
            this.bwMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwMain_DoWork);
            this.bwMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwMain_RunWorkerCompleted);
            // 
            // FormDialogSearchReplaceProgressFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 92);
            this.Controls.Add(this.lbStatus2);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.lbStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDialogSearchReplaceProgressFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDialogSearchReplaceProgressFiles_FormClosing);
            this.Shown += new System.EventHandler(this.FormDialogSearchReplaceProgressFiles_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.Label lbStatus2;
        private System.ComponentModel.BackgroundWorker bwMain;
    }
}