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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchResultTree));
            this.tvMain = new System.Windows.Forms.TreeView();
            this.ilTreeView = new System.Windows.Forms.ImageList(this.components);
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // tvMain
            // 
            this.tvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvMain.ImageIndex = 0;
            this.tvMain.ImageList = this.ilTreeView;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Name = "tvMain";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "Node0";
            treeNode2.Text = "Node0";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.Size = new System.Drawing.Size(838, 121);
            this.tvMain.TabIndex = 0;
            this.tvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvMain_NodeMouseClick);
            // 
            // ilTreeView
            // 
            this.ilTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeView.ImageStream")));
            this.ilTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTreeView.Images.SetKeyName(0, "List.png");
            this.ilTreeView.Images.SetKeyName(1, "go-jump-definition.png");
            // 
            // ssMain
            // 
            this.ssMain.Location = new System.Drawing.Point(0, 124);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(838, 22);
            this.ssMain.TabIndex = 1;
            this.ssMain.Text = "statusStrip1";
            // 
            // FormSearchResultTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 146);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tvMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSearchResultTree";
            this.Text = "Search results";
            this.Shown += new System.EventHandler(this.FormSearchResultTree_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.ImageList ilTreeView;
        private System.Windows.Forms.StatusStrip ssMain;
    }
}