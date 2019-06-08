using ScintillaDiff;

namespace ScriptNotepad.UtilityClasses.MiscForms
{
    partial class FormFileDiffView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFileDiffView));
            this.diffControl = new ScintillaDiff.ScintillaDiffControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPreviousDiff = new System.Windows.Forms.ToolStripButton();
            this.tsbNextDiff = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSplitView = new System.Windows.Forms.ToolStripButton();
            this.tsbSwapContents = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // diffControl
            // 
            this.diffControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diffControl.DiffColorAdded = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(255)))), ((int)(((byte)(135)))));
            this.diffControl.DiffColorChangeBackground = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
            this.diffControl.DiffColorDeleted = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.diffControl.DiffStyle = ScintillaDiff.ScintillaDiffStyles.DiffStyle.DiffSideBySide;
            this.diffControl.ImageRowAdded = ((System.Drawing.Bitmap)(resources.GetObject("diffControl.ImageRowAdded")));
            this.diffControl.ImageRowAddedScintillaIndex = 28;
            this.diffControl.ImageRowDeleted = ((System.Drawing.Bitmap)(resources.GetObject("diffControl.ImageRowDeleted")));
            this.diffControl.ImageRowDeletedScintillaIndex = 29;
            this.diffControl.ImageRowDiff = ((System.Drawing.Bitmap)(resources.GetObject("diffControl.ImageRowDiff")));
            this.diffControl.ImageRowDiffScintillaIndex = 31;
            this.diffControl.ImageRowOk = ((System.Drawing.Bitmap)(resources.GetObject("diffControl.ImageRowOk")));
            this.diffControl.ImageRowOkScintillaIndex = 30;
            this.diffControl.Location = new System.Drawing.Point(12, 45);
            this.diffControl.MarkColorIndexModifiedBackground = 31;
            this.diffControl.MarkColorIndexRemovedOrAdded = 30;
            this.diffControl.Name = "diffControl";
            this.diffControl.Size = new System.Drawing.Size(776, 393);
            this.diffControl.TabIndex = 0;
            this.diffControl.TextLeft = "";
            this.diffControl.TextRight = "";
            this.diffControl.UseRowOkSign = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPreviousDiff,
            this.tsbNextDiff,
            this.toolStripSeparator1,
            this.tsbSplitView,
            this.toolStripSeparator2,
            this.tsbSwapContents});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPreviousDiff
            // 
            this.tsbPreviousDiff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPreviousDiff.Image = global::ScriptNotepad.Properties.Resources.book_previous;
            this.tsbPreviousDiff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreviousDiff.Name = "tsbPreviousDiff";
            this.tsbPreviousDiff.Size = new System.Drawing.Size(23, 22);
            this.tsbPreviousDiff.Text = "Previous difference";
            this.tsbPreviousDiff.Click += new System.EventHandler(this.TsbPreviousDiff_Click);
            // 
            // tsbNextDiff
            // 
            this.tsbNextDiff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextDiff.Image = global::ScriptNotepad.Properties.Resources.book_next;
            this.tsbNextDiff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextDiff.Name = "tsbNextDiff";
            this.tsbNextDiff.Size = new System.Drawing.Size(23, 22);
            this.tsbNextDiff.Text = "Advance difference";
            this.tsbNextDiff.Click += new System.EventHandler(this.TsbNextDiff_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSplitView
            // 
            this.tsbSplitView.Checked = true;
            this.tsbSplitView.CheckOnClick = true;
            this.tsbSplitView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSplitView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSplitView.Image = global::ScriptNotepad.Properties.Resources.split_view;
            this.tsbSplitView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSplitView.Name = "tsbSplitView";
            this.tsbSplitView.Size = new System.Drawing.Size(23, 22);
            this.tsbSplitView.Text = "Split view";
            this.tsbSplitView.Click += new System.EventHandler(this.TsbSplitView_Click);
            // 
            // tsbSwapContents
            // 
            this.tsbSwapContents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSwapContents.Image = global::ScriptNotepad.Properties.Resources.swap_view;
            this.tsbSwapContents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwapContents.Name = "tsbSwapContents";
            this.tsbSwapContents.Size = new System.Drawing.Size(23, 22);
            this.tsbSwapContents.Text = "Swap contents";
            this.tsbSwapContents.Click += new System.EventHandler(this.TsbSwapContents_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FormFileDiffView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.diffControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFileDiffView";
            this.Text = "Diff files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFileDiffView_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScintillaDiffControl diffControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSplitView;
        private System.Windows.Forms.ToolStripButton tsbPreviousDiff;
        private System.Windows.Forms.ToolStripButton tsbNextDiff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSwapContents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}