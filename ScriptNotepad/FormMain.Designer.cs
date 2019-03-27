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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAll = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAllWithUnsaved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.ssLbLineColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLineCol = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSpace1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbLinesColumnSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSpace2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbLDocLinesSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSpace3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbLineEnding = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSpace4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbEncoding = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSpace5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbInsertOverride = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSpace6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssLbSessionName = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttcMain = new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenWithEncoding = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSplit2 = new System.Windows.Forms.ToolStripSeparator();
            this.munSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAllWithUnsaved = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunScript = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCharSets = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageScriptSnippets = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.odAnyFile = new System.Windows.Forms.OpenFileDialog();
            this.sdAnyFile = new System.Windows.Forms.SaveFileDialog();
            this.tmGUI = new System.Windows.Forms.Timer(this.components);
            this.cmsFileTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFullFilePathToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNameToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFullFilePathAndNameToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenContainingFolderInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenContainingFolderInCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenContainingFolderInWindowsPowerShell = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenWithAssociatedApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllToTheLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllToTheRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.cmsFileTab.SuspendLayout();
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
            this.tlpMain.Controls.Add(this.ssMain, 0, 2);
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
            this.tsbSaveAllWithUnsaved,
            this.toolStripSeparator1,
            this.tsbUndo,
            this.tsbRedo});
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
            this.tsbSaveAs.Text = "Save as";
            this.tsbSaveAs.Click += new System.EventHandler(this.munSave_Click);
            // 
            // tsbSaveAll
            // 
            this.tsbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAll.Image = global::ScriptNotepad.Properties.Resources.save_all;
            this.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAll.Name = "tsbSaveAll";
            this.tsbSaveAll.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAll.Text = "Save all";
            this.tsbSaveAll.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // tsbSaveAllWithUnsaved
            // 
            this.tsbSaveAllWithUnsaved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAllWithUnsaved.Image = global::ScriptNotepad.Properties.Resources.save_all_plus;
            this.tsbSaveAllWithUnsaved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAllWithUnsaved.Name = "tsbSaveAllWithUnsaved";
            this.tsbSaveAllWithUnsaved.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAllWithUnsaved.Text = "Save all (+new)";
            this.tsbSaveAllWithUnsaved.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = global::ScriptNotepad.Properties.Resources.Undo;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = global::ScriptNotepad.Properties.Resources.Redo;
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbRedo.Text = "Redo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // ssMain
            // 
            this.tlpMain.SetColumnSpan(this.ssMain, 5);
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssLbLineColumn,
            this.tsslLineCol,
            this.ssLbSpace1,
            this.ssLbLinesColumnSelection,
            this.ssLbSpace2,
            this.ssLbLDocLinesSize,
            this.ssLbSpace3,
            this.ssLbLineEnding,
            this.ssLbSpace4,
            this.ssLbEncoding,
            this.ssLbSpace5,
            this.ssLbInsertOverride,
            this.ssLbSpace6,
            this.ssLbSessionName});
            this.ssMain.Location = new System.Drawing.Point(0, 391);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(776, 20);
            this.ssMain.TabIndex = 3;
            this.ssMain.Text = "statusStrip1";
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
            // ssLbSpace1
            // 
            this.ssLbSpace1.AutoSize = false;
            this.ssLbSpace1.Name = "ssLbSpace1";
            this.ssLbSpace1.Size = new System.Drawing.Size(30, 15);
            // 
            // ssLbLinesColumnSelection
            // 
            this.ssLbLinesColumnSelection.Name = "ssLbLinesColumnSelection";
            this.ssLbLinesColumnSelection.Size = new System.Drawing.Size(134, 15);
            this.ssLbLinesColumnSelection.Text = "Sel1: 1|1  Sel2: 1|1  Len: 0";
            // 
            // ssLbSpace2
            // 
            this.ssLbSpace2.AutoSize = false;
            this.ssLbSpace2.Name = "ssLbSpace2";
            this.ssLbSpace2.Size = new System.Drawing.Size(30, 15);
            // 
            // ssLbLDocLinesSize
            // 
            this.ssLbLDocLinesSize.Name = "ssLbLDocLinesSize";
            this.ssLbLDocLinesSize.Size = new System.Drawing.Size(92, 15);
            this.ssLbLDocLinesSize.Text = "length: 0 lines: 0";
            // 
            // ssLbSpace3
            // 
            this.ssLbSpace3.AutoSize = false;
            this.ssLbSpace3.Name = "ssLbSpace3";
            this.ssLbSpace3.Size = new System.Drawing.Size(30, 15);
            // 
            // ssLbLineEnding
            // 
            this.ssLbLineEnding.Name = "ssLbLineEnding";
            this.ssLbLineEnding.Size = new System.Drawing.Size(42, 15);
            this.ssLbLineEnding.Text = "CR+LF";
            // 
            // ssLbSpace4
            // 
            this.ssLbSpace4.AutoSize = false;
            this.ssLbSpace4.Name = "ssLbSpace4";
            this.ssLbSpace4.Size = new System.Drawing.Size(30, 15);
            // 
            // ssLbEncoding
            // 
            this.ssLbEncoding.Name = "ssLbEncoding";
            this.ssLbEncoding.Size = new System.Drawing.Size(34, 15);
            this.ssLbEncoding.Text = "UTF8";
            // 
            // ssLbSpace5
            // 
            this.ssLbSpace5.AutoSize = false;
            this.ssLbSpace5.Name = "ssLbSpace5";
            this.ssLbSpace5.Size = new System.Drawing.Size(30, 15);
            // 
            // ssLbInsertOverride
            // 
            this.ssLbInsertOverride.Name = "ssLbInsertOverride";
            this.ssLbInsertOverride.Size = new System.Drawing.Size(25, 15);
            this.ssLbInsertOverride.Text = "INS";
            // 
            // ssLbSpace6
            // 
            this.ssLbSpace6.AutoSize = false;
            this.ssLbSpace6.Name = "ssLbSpace6";
            this.ssLbSpace6.Size = new System.Drawing.Size(30, 15);
            // 
            // ssLbSessionName
            // 
            this.ssLbSessionName.Name = "ssLbSessionName";
            this.ssLbSessionName.Size = new System.Drawing.Size(90, 15);
            this.ssLbSessionName.Text = "Session: Default";
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
            this.sttcMain.RightButtonTabActivation = true;
            this.sttcMain.RightButtonTabDragging = false;
            this.sttcMain.SavedImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.SavedImage")));
            this.sttcMain.Size = new System.Drawing.Size(770, 360);
            this.sttcMain.SuspendTextChangedEvents = false;
            this.sttcMain.TabIndex = 4;
            this.sttcMain.TabActivated += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabActivated(this.sttcMain_TabActivated);
            this.sttcMain.TabClosing += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabClosing(this.sttcMain_TabClosing);
            this.sttcMain.CaretPositionChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnCaretPositionChanged(this.sttcMain_SelectionCaretChanged);
            this.sttcMain.DocumentMouseDown += new System.Windows.Forms.MouseEventHandler(this.sttcMain_DocumentMouseDown);
            this.sttcMain.DocumentMouseUp += new System.Windows.Forms.MouseEventHandler(this.sttcMain_DocumentMouseUp);
            this.sttcMain.SelectionChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnSelectionChanged(this.sttcMain_SelectionCaretChanged);
            this.sttcMain.DocumentTextChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnDocumentTextChanged(this.sttcMain_DocumentTextChanged);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuSearch,
            this.mnuTools,
            this.mnuPlugins,
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
            this.mnuOpenWithEncoding,
            this.mnuTest,
            this.mnuSplit1,
            this.mnuRecentFiles,
            this.mnuSplit2,
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
            this.mnuNew.Size = new System.Drawing.Size(191, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = global::ScriptNotepad.Properties.Resources.folder_page;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(191, 22);
            this.mnuOpen.Text = "Open...";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuOpenWithEncoding
            // 
            this.mnuOpenWithEncoding.Image = global::ScriptNotepad.Properties.Resources.open_unknown_encoding;
            this.mnuOpenWithEncoding.Name = "mnuOpenWithEncoding";
            this.mnuOpenWithEncoding.Size = new System.Drawing.Size(191, 22);
            this.mnuOpenWithEncoding.Text = "Open with encoding...";
            this.mnuOpenWithEncoding.Click += new System.EventHandler(this.mnuOpenWithEncoding_Click);
            // 
            // mnuTest
            // 
            this.mnuTest.Image = global::ScriptNotepad.Properties.Resources.astonished;
            this.mnuTest.Name = "mnuTest";
            this.mnuTest.Size = new System.Drawing.Size(191, 22);
            this.mnuTest.Text = "Test Form...";
            this.mnuTest.Visible = false;
            this.mnuTest.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // mnuSplit1
            // 
            this.mnuSplit1.Name = "mnuSplit1";
            this.mnuSplit1.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(191, 22);
            this.mnuRecentFiles.Text = "Recent...";
            // 
            // mnuSplit2
            // 
            this.mnuSplit2.Name = "mnuSplit2";
            this.mnuSplit2.Size = new System.Drawing.Size(188, 6);
            // 
            // munSave
            // 
            this.munSave.Image = global::ScriptNotepad.Properties.Resources.Save;
            this.munSave.Name = "munSave";
            this.munSave.Size = new System.Drawing.Size(191, 22);
            this.munSave.Text = "Save";
            this.munSave.Click += new System.EventHandler(this.munSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Image = global::ScriptNotepad.Properties.Resources.SaveAs;
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(191, 22);
            this.mnuSaveAs.Text = "Save as";
            this.mnuSaveAs.Click += new System.EventHandler(this.munSave_Click);
            // 
            // mnuSaveAll
            // 
            this.mnuSaveAll.Image = global::ScriptNotepad.Properties.Resources.save_all;
            this.mnuSaveAll.Name = "mnuSaveAll";
            this.mnuSaveAll.Size = new System.Drawing.Size(191, 22);
            this.mnuSaveAll.Text = "Save all";
            this.mnuSaveAll.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // mnuSaveAllWithUnsaved
            // 
            this.mnuSaveAllWithUnsaved.Image = global::ScriptNotepad.Properties.Resources.save_all_plus;
            this.mnuSaveAllWithUnsaved.Name = "mnuSaveAllWithUnsaved";
            this.mnuSaveAllWithUnsaved.Size = new System.Drawing.Size(191, 22);
            this.mnuSaveAllWithUnsaved.Text = "Save all (+new)";
            this.mnuSaveAllWithUnsaved.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunScript,
            this.mnuCharSets});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuRunScript
            // 
            this.mnuRunScript.Image = global::ScriptNotepad.Properties.Resources.CSharp;
            this.mnuRunScript.Name = "mnuRunScript";
            this.mnuRunScript.Size = new System.Drawing.Size(168, 22);
            this.mnuRunScript.Text = "Run script";
            this.mnuRunScript.Click += new System.EventHandler(this.mnuRunScript_Click);
            // 
            // mnuCharSets
            // 
            this.mnuCharSets.Image = global::ScriptNotepad.Properties.Resources.unicode;
            this.mnuCharSets.Name = "mnuCharSets";
            this.mnuCharSets.Size = new System.Drawing.Size(168, 22);
            this.mnuCharSets.Text = "Change encoding";
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
            this.mnuFind.Image = global::ScriptNotepad.Properties.Resources.Find;
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.Size = new System.Drawing.Size(103, 22);
            this.mnuFind.Text = "Find..";
            this.mnuFind.Click += new System.EventHandler(this.mnuFind_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuManageScriptSnippets,
            this.mnuSettings});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(47, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuManageScriptSnippets
            // 
            this.mnuManageScriptSnippets.Image = global::ScriptNotepad.Properties.Resources.Script;
            this.mnuManageScriptSnippets.Name = "mnuManageScriptSnippets";
            this.mnuManageScriptSnippets.Size = new System.Drawing.Size(196, 22);
            this.mnuManageScriptSnippets.Text = "Manage script snippets";
            this.mnuManageScriptSnippets.Click += new System.EventHandler(this.mnuManageScriptSnippets_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = global::ScriptNotepad.Properties.Resources.preferences;
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(196, 22);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // mnuPlugins
            // 
            this.mnuPlugins.Name = "mnuPlugins";
            this.mnuPlugins.Size = new System.Drawing.Size(63, 20);
            this.mnuPlugins.Text = "Plug-ins";
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
            // odAnyFile
            // 
            this.odAnyFile.FileOk += new System.ComponentModel.CancelEventHandler(this.odAnyFile_FileOk);
            // 
            // tmGUI
            // 
            this.tmGUI.Tick += new System.EventHandler(this.tmGUI_Tick);
            // 
            // cmsFileTab
            // 
            this.cmsFileTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFullFilePathToClipboard,
            this.mnuFileNameToClipboard,
            this.mnuFullFilePathAndNameToClipboard,
            this.toolStripMenuItem3,
            this.mnuOpenContainingFolderInExplorer,
            this.mnuOpenContainingFolderInCmd,
            this.mnuOpenContainingFolderInWindowsPowerShell,
            this.mnuOpenWithAssociatedApplication,
            this.toolStripMenuItem4,
            this.mnuCloseTab,
            this.mnuCloseAllButThis,
            this.mnuCloseAllToTheLeft,
            this.mnuCloseAllToTheRight});
            this.cmsFileTab.Name = "contextMenuStrip1";
            this.cmsFileTab.Size = new System.Drawing.Size(324, 258);
            this.cmsFileTab.Opening += new System.ComponentModel.CancelEventHandler(this.cmsFileTab_Opening);
            // 
            // mnuFullFilePathToClipboard
            // 
            this.mnuFullFilePathToClipboard.Name = "mnuFullFilePathToClipboard";
            this.mnuFullFilePathToClipboard.Size = new System.Drawing.Size(323, 22);
            this.mnuFullFilePathToClipboard.Text = "Full file path to clipboard";
            this.mnuFullFilePathToClipboard.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // mnuFileNameToClipboard
            // 
            this.mnuFileNameToClipboard.Name = "mnuFileNameToClipboard";
            this.mnuFileNameToClipboard.Size = new System.Drawing.Size(323, 22);
            this.mnuFileNameToClipboard.Text = "File name to clipboard";
            this.mnuFileNameToClipboard.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // mnuFullFilePathAndNameToClipboard
            // 
            this.mnuFullFilePathAndNameToClipboard.Name = "mnuFullFilePathAndNameToClipboard";
            this.mnuFullFilePathAndNameToClipboard.Size = new System.Drawing.Size(323, 22);
            this.mnuFullFilePathAndNameToClipboard.Text = "Full file path and name to clipboard";
            this.mnuFullFilePathAndNameToClipboard.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuOpenContainingFolderInExplorer
            // 
            this.mnuOpenContainingFolderInExplorer.Name = "mnuOpenContainingFolderInExplorer";
            this.mnuOpenContainingFolderInExplorer.Size = new System.Drawing.Size(323, 22);
            this.mnuOpenContainingFolderInExplorer.Text = "Open containing folder in explorer";
            this.mnuOpenContainingFolderInExplorer.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // mnuOpenContainingFolderInCmd
            // 
            this.mnuOpenContainingFolderInCmd.Name = "mnuOpenContainingFolderInCmd";
            this.mnuOpenContainingFolderInCmd.Size = new System.Drawing.Size(323, 22);
            this.mnuOpenContainingFolderInCmd.Text = "Open containing folder in cmd";
            this.mnuOpenContainingFolderInCmd.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // mnuOpenContainingFolderInWindowsPowerShell
            // 
            this.mnuOpenContainingFolderInWindowsPowerShell.Name = "mnuOpenContainingFolderInWindowsPowerShell";
            this.mnuOpenContainingFolderInWindowsPowerShell.Size = new System.Drawing.Size(323, 22);
            this.mnuOpenContainingFolderInWindowsPowerShell.Text = "Open containing folder in Windows PowerShell";
            this.mnuOpenContainingFolderInWindowsPowerShell.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // mnuOpenWithAssociatedApplication
            // 
            this.mnuOpenWithAssociatedApplication.Name = "mnuOpenWithAssociatedApplication";
            this.mnuOpenWithAssociatedApplication.Size = new System.Drawing.Size(323, 22);
            this.mnuOpenWithAssociatedApplication.Text = "Open with associated application";
            this.mnuOpenWithAssociatedApplication.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuCloseTab
            // 
            this.mnuCloseTab.Name = "mnuCloseTab";
            this.mnuCloseTab.Size = new System.Drawing.Size(323, 22);
            this.mnuCloseTab.Text = "Close";
            this.mnuCloseTab.Click += new System.EventHandler(this.CommonContextMenu_FileInteractionClick);
            // 
            // mnuCloseAllButThis
            // 
            this.mnuCloseAllButThis.Name = "mnuCloseAllButThis";
            this.mnuCloseAllButThis.Size = new System.Drawing.Size(323, 22);
            this.mnuCloseAllButThis.Text = "Close all but this";
            this.mnuCloseAllButThis.Click += new System.EventHandler(this.commonCloseManyDocuments);
            // 
            // mnuCloseAllToTheLeft
            // 
            this.mnuCloseAllToTheLeft.Name = "mnuCloseAllToTheLeft";
            this.mnuCloseAllToTheLeft.Size = new System.Drawing.Size(323, 22);
            this.mnuCloseAllToTheLeft.Text = "Close all to the left";
            this.mnuCloseAllToTheLeft.Click += new System.EventHandler(this.commonCloseManyDocuments);
            // 
            // mnuCloseAllToTheRight
            // 
            this.mnuCloseAllToTheRight.Name = "mnuCloseAllToTheRight";
            this.mnuCloseAllToTheRight.Size = new System.Drawing.Size(323, 22);
            this.mnuCloseAllToTheRight.Text = "Close all to the right";
            this.mnuCloseAllToTheRight.Click += new System.EventHandler(this.commonCloseManyDocuments);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.Text = "ScriptNotepad";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.cmsFileTab.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem mnuTest;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.OpenFileDialog odAnyFile;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.StatusStrip ssMain;
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
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuManageScriptSnippets;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace3;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLineEnding;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace4;
        private System.Windows.Forms.ToolStripStatusLabel ssLbEncoding;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace1;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace2;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace5;
        private System.Windows.Forms.ToolStripStatusLabel ssLbInsertOverride;
        private System.Windows.Forms.ToolStripMenuItem mnuCharSets;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenWithEncoding;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripSeparator mnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentFiles;
        private System.Windows.Forms.ToolStripSeparator mnuSplit2;
        private System.Windows.Forms.Timer tmGUI;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace6;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSessionName;
        private System.Windows.Forms.ContextMenuStrip cmsFileTab;
        private System.Windows.Forms.ToolStripMenuItem mnuFullFilePathToClipboard;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNameToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenContainingFolderInExplorer;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenContainingFolderInCmd;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenContainingFolderInWindowsPowerShell;
        private System.Windows.Forms.ToolStripMenuItem mnuFullFilePathAndNameToClipboard;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenWithAssociatedApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAllButThis;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAllToTheLeft;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAllToTheRight;
        private System.Windows.Forms.ToolStripMenuItem mnuPlugins;
    }
}

