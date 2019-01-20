namespace ScriptNotepad
{
    /// <summary>
    /// The main form of the application.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAll = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAllWithUnsaved = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ssLbLineColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLineCol = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbLinesColumnSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbLDocLinesSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttcMain = new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.munSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAllWithUnsaved = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunScript = new System.Windows.Forms.ToolStripMenuItem();
            this.odAnyFile = new System.Windows.Forms.OpenFileDialog();
            this.sdAnyFile = new System.Windows.Forms.SaveFileDialog();
            this.tlpMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 5;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.Controls.Add(this.tsMain, 0, 0);
            this.tlpMain.Controls.Add(this.statusStrip1, 0, 2);
            this.tlpMain.Controls.Add(this.sttcMain, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(12, 27);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(776, 411);
            this.tlpMain.TabIndex = 3;
            // 
            // tsMain
            // 
            this.tlpMain.SetColumnSpan(this.tsMain, 6);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbSaveAs,
            this.tsbSaveAll,
            this.tsbSaveAllWithUnsaved});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(776, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::ScriptNotepad.Properties.Resources.New_document;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::ScriptNotepad.Properties.Resources.folder_page;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open...";
            this.tsbOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::ScriptNotepad.Properties.Resources.Save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.munSave_Click);
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAs.Image = global::ScriptNotepad.Properties.Resources.SaveAs;
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAs.Text = "Save As";
            this.tsbSaveAs.Click += new System.EventHandler(this.munSave_Click);
            // 
            // tsbSaveAll
            // 
            this.tsbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAll.Image = global::ScriptNotepad.Properties.Resources.save_all;
            this.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAll.Name = "tsbSaveAll";
            this.tsbSaveAll.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAll.Text = "Save All";
            this.tsbSaveAll.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // tsbSaveAllWithUnsaved
            // 
            this.tsbSaveAllWithUnsaved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAllWithUnsaved.Image = global::ScriptNotepad.Properties.Resources.save_all_plus;
            this.tsbSaveAllWithUnsaved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAllWithUnsaved.Name = "tsbSaveAllWithUnsaved";
            this.tsbSaveAllWithUnsaved.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAllWithUnsaved.Text = "Save All (+new)";
            this.tsbSaveAllWithUnsaved.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // statusStrip1
            // 
            this.tlpMain.SetColumnSpan(this.statusStrip1, 5);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssLbLineColumn,
            this.tsslLineCol,
            this.ssLbLinesColumnSelection,
            this.ssLbLDocLinesSize});
            this.statusStrip1.Location = new System.Drawing.Point(0, 391);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(776, 20);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ssLbLineColumn
            // 
            this.ssLbLineColumn.Name = "ssLbLineColumn";
            this.ssLbLineColumn.Size = new System.Drawing.Size(77, 15);
            this.ssLbLineColumn.Text = "Line: 1  Col: 1";
            // 
            // tsslLineCol
            // 
            this.tsslLineCol.Name = "tsslLineCol";
            this.tsslLineCol.Size = new System.Drawing.Size(0, 15);
            // 
            // ssLbLinesColumnSelection
            // 
            this.ssLbLinesColumnSelection.Name = "ssLbLinesColumnSelection";
            this.ssLbLinesColumnSelection.Size = new System.Drawing.Size(134, 15);
            this.ssLbLinesColumnSelection.Text = "Sel1: 1|1  Sel2: 1|1  Len: 0";
            // 
            // ssLbLDocLinesSize
            // 
            this.ssLbLDocLinesSize.Name = "ssLbLDocLinesSize";
            this.ssLbLDocLinesSize.Size = new System.Drawing.Size(92, 15);
            this.ssLbLDocLinesSize.Text = "length: 0 lines: 0";
            // 
            // sttcMain
            // 
            this.sttcMain.ChangedImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.ChangedImage")));
            this.sttcMain.CloseButtonImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.CloseButtonImage")));
            this.tlpMain.SetColumnSpan(this.sttcMain, 5);
            this.sttcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sttcMain.LeftFileIndex = 0;
            this.sttcMain.Location = new System.Drawing.Point(3, 28);
            this.sttcMain.Name = "sttcMain";
            this.sttcMain.NewFilenameStart = "new ";
            this.sttcMain.SavedImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.SavedImage")));
            this.sttcMain.Size = new System.Drawing.Size(770, 360);
            this.sttcMain.SuspendTextChangedEvents = false;
            this.sttcMain.TabIndex = 4;
            this.sttcMain.TabActivated += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabActivated(this.sttcMain_TabActivated);
            this.sttcMain.TabClosing += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabClosing(this.sttcMain_TabClosing);
            this.sttcMain.CaretPositionChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnCaretPositionChanged(this.sttcMain_SelectionCaretChanged);
            this.sttcMain.SelectionChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnSelectionChanged(this.sttcMain_SelectionCaretChanged);
            this.sttcMain.DocumentTextChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnDocumentTextChanged(this.sttcMain_DocumentTextChanged);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuSearch,
            this.mnuHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 4;
            this.menuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.testToolStripMenuItem,
            this.munSave,
            this.mnuSaveAs,
            this.mnuSaveAll,
            this.mnuSaveAllWithUnsaved});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuNew
            // 
            this.mnuNew.Image = global::ScriptNotepad.Properties.Resources.New_document;
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(156, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = global::ScriptNotepad.Properties.Resources.folder_page;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(156, 22);
            this.mnuOpen.Text = "Open...";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // munSave
            // 
            this.munSave.Image = global::ScriptNotepad.Properties.Resources.Save;
            this.munSave.Name = "munSave";
            this.munSave.Size = new System.Drawing.Size(156, 22);
            this.munSave.Text = "Save";
            this.munSave.Click += new System.EventHandler(this.munSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Image = global::ScriptNotepad.Properties.Resources.SaveAs;
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(156, 22);
            this.mnuSaveAs.Text = "Save As";
            this.mnuSaveAs.Click += new System.EventHandler(this.munSave_Click);
            // 
            // mnuSaveAll
            // 
            this.mnuSaveAll.Image = global::ScriptNotepad.Properties.Resources.save_all;
            this.mnuSaveAll.Name = "mnuSaveAll";
            this.mnuSaveAll.Size = new System.Drawing.Size(156, 22);
            this.mnuSaveAll.Text = "Save All";
            this.mnuSaveAll.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // mnuSaveAllWithUnsaved
            // 
            this.mnuSaveAllWithUnsaved.Image = global::ScriptNotepad.Properties.Resources.save_all_plus;
            this.mnuSaveAllWithUnsaved.Name = "mnuSaveAllWithUnsaved";
            this.mnuSaveAllWithUnsaved.Size = new System.Drawing.Size(156, 22);
            this.mnuSaveAllWithUnsaved.Text = "Save All (+new)";
            this.mnuSaveAllWithUnsaved.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // mnuSearch
            // 
            this.mnuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFind});
            this.mnuSearch.Name = "mnuSearch";
            this.mnuSearch.Size = new System.Drawing.Size(54, 20);
            this.mnuSearch.Text = "Search";
            // 
            // mnuFind
            // 
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.Size = new System.Drawing.Size(103, 22);
            this.mnuFind.Text = "Find..";
            this.mnuFind.Click += new System.EventHandler(this.mnuFind_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunScript});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuRunScript
            // 
            this.mnuRunScript.Name = "mnuRunScript";
            this.mnuRunScript.Size = new System.Drawing.Size(180, 22);
            this.mnuRunScript.Text = "Run Script";
            this.mnuRunScript.Click += new System.EventHandler(this.mnuRunScript_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.Text = "ScriptNotepad";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuFind;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.OpenFileDialog odAnyFile;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLineColumn;
        private VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl sttcMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslLineCol;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLinesColumnSelection;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLDocLinesSize;
        private System.Windows.Forms.ToolStripMenuItem munSave;
        private System.Windows.Forms.SaveFileDialog sdAnyFile;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAll;
        private System.Windows.Forms.ToolStripButton tsbSaveAll;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAllWithUnsaved;
        private System.Windows.Forms.ToolStripButton tsbSaveAllWithUnsaved;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuRunScript;
    }
}

