namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    partial class FormSearchResultTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchResultTree));
            this.tvMain = new System.Windows.Forms.TreeView();
            this.ilTreeView = new System.Windows.Forms.ImageList(this.components);
            this.tlpToolStrip = new System.Windows.Forms.TableLayoutPanel();
            this.pnNextResult = new System.Windows.Forms.Panel();
            this.pnPreviousResult = new System.Windows.Forms.Panel();
            this.pnClose = new System.Windows.Forms.Panel();
            this.lbSearchResultDesc = new System.Windows.Forms.Label();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.tlpToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvMain
            // 
            this.tvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvMain.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvMain.FullRowSelect = true;
            this.tvMain.ImageIndex = 0;
            this.tvMain.ImageList = this.ilTreeView;
            this.tvMain.Location = new System.Drawing.Point(0, 15);
            this.tvMain.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tvMain.Name = "tvMain";
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.Size = new System.Drawing.Size(838, 131);
            this.tvMain.TabIndex = 0;
            this.tvMain.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TvMain_DrawNode);
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvMain_AfterSelect);
            // 
            // ilTreeView
            // 
            this.ilTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeView.ImageStream")));
            this.ilTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTreeView.Images.SetKeyName(0, "List.png");
            this.ilTreeView.Images.SetKeyName(1, "go-jump-definition.png");
            // 
            // tlpToolStrip
            // 
            this.tlpToolStrip.ColumnCount = 6;
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpToolStrip.Controls.Add(this.pnNextResult, 2, 0);
            this.tlpToolStrip.Controls.Add(this.pnPreviousResult, 0, 0);
            this.tlpToolStrip.Controls.Add(this.pnClose, 5, 0);
            this.tlpToolStrip.Controls.Add(this.lbSearchResultDesc, 4, 0);
            this.tlpToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpToolStrip.Location = new System.Drawing.Point(0, 0);
            this.tlpToolStrip.Margin = new System.Windows.Forms.Padding(0);
            this.tlpToolStrip.Name = "tlpToolStrip";
            this.tlpToolStrip.RowCount = 1;
            this.tlpToolStrip.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToolStrip.Size = new System.Drawing.Size(838, 15);
            this.tlpToolStrip.TabIndex = 2;
            // 
            // pnNextResult
            // 
            this.pnNextResult.BackgroundImage = global::ScriptNotepad.Properties.Resources.arrow_right;
            this.pnNextResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnNextResult.Location = new System.Drawing.Point(26, 3);
            this.pnNextResult.Name = "pnNextResult";
            this.pnNextResult.Size = new System.Drawing.Size(8, 8);
            this.pnNextResult.TabIndex = 3;
            this.ttMain.SetToolTip(this.pnNextResult, "Advance result");
            this.pnNextResult.Click += new System.EventHandler(this.TinyButton_Click);
            // 
            // pnPreviousResult
            // 
            this.pnPreviousResult.BackgroundImage = global::ScriptNotepad.Properties.Resources.arrow_left;
            this.pnPreviousResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnPreviousResult.Location = new System.Drawing.Point(3, 3);
            this.pnPreviousResult.Name = "pnPreviousResult";
            this.pnPreviousResult.Size = new System.Drawing.Size(8, 8);
            this.pnPreviousResult.TabIndex = 0;
            this.ttMain.SetToolTip(this.pnPreviousResult, "Previous result");
            this.pnPreviousResult.Click += new System.EventHandler(this.TinyButton_Click);
            // 
            // pnClose
            // 
            this.pnClose.BackgroundImage = global::ScriptNotepad.Properties.Resources.close_small;
            this.pnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnClose.Location = new System.Drawing.Point(826, 3);
            this.pnClose.Name = "pnClose";
            this.pnClose.Size = new System.Drawing.Size(8, 8);
            this.pnClose.TabIndex = 4;
            this.ttMain.SetToolTip(this.pnClose, "Close");
            this.pnClose.Click += new System.EventHandler(this.TinyButton_Click);
            // 
            // lbSearchResultDesc
            // 
            this.lbSearchResultDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSearchResultDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSearchResultDesc.Location = new System.Drawing.Point(58, 0);
            this.lbSearchResultDesc.Margin = new System.Windows.Forms.Padding(0);
            this.lbSearchResultDesc.Name = "lbSearchResultDesc";
            this.lbSearchResultDesc.Size = new System.Drawing.Size(765, 15);
            this.lbSearchResultDesc.TabIndex = 5;
            this.lbSearchResultDesc.Text = "Search results";
            // 
            // FormSearchResultTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 146);
            this.Controls.Add(this.tlpToolStrip);
            this.Controls.Add(this.tvMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormSearchResultTree";
            this.Text = "Search results";
            this.Shown += new System.EventHandler(this.FormSearchResultTree_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSearchResultTree_KeyDown);
            this.tlpToolStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.ImageList ilTreeView;
        private System.Windows.Forms.TableLayoutPanel tlpToolStrip;
        private System.Windows.Forms.Panel pnPreviousResult;
        private System.Windows.Forms.Panel pnNextResult;
        private System.Windows.Forms.Panel pnClose;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.Label lbSearchResultDesc;
    }
}