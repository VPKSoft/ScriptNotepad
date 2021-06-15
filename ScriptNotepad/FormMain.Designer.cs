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
            this.pnDockRunSnippet = new System.Windows.Forms.Panel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReloadFromDisk = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAll = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAllWithUnsaved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSpellCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
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
            this.ssLbSpace7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbZoomPercentage = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbSpace8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbTabs = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbTabsValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttcMain = new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl();
            this.pnDock = new System.Windows.Forms.Panel();
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
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDiffRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDiffLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRenameNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tmSpellCheck = new System.Windows.Forms.Timer(this.components);
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenWithEncoding = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenNoBOM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuReloadFromDisk = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSplit2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSession = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.munSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAllWithUnsaved = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunScript = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunScriptOrCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCharSets = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearAllStyles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStyle3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStyle4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStyle5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClearStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearStyle3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearStyle4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearStyle5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportAsHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportAsHTMLToNewDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTMLToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTMLToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTMLToFileExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDateAndTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateAndTime6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGoto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRenameNewFileMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFoldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUnfoldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindInFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMarkText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowSymbol = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowWhiteSpaceAndTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowEndOfLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowIndentGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowWrapSymbol = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProgrammingLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageScriptSnippets = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManagePlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageSessions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocalization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDumpLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDiffFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCaseSensitive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSortLines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortAscending = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSortDescending = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomizedSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveDuplicateLines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWrapDocumentTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuJsonPrettify = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuJsonUglify = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNextTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPreviousTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFirstTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLastTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFindTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tmAutoSave = new System.Windows.Forms.Timer(this.components);
            this.sdHTML = new System.Windows.Forms.SaveFileDialog();
            this.pdPrint = new System.Windows.Forms.PrintDialog();
            this.tlpMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.cmsFileTab.SuspendLayout();
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
            this.tlpMain.Controls.Add(this.pnDockRunSnippet, 0, 2);
            this.tlpMain.Controls.Add(this.tsMain, 0, 0);
            this.tlpMain.Controls.Add(this.ssMain, 0, 3);
            this.tlpMain.Controls.Add(this.sttcMain, 0, 1);
            this.tlpMain.Controls.Add(this.pnDock, 0, 4);
            this.tlpMain.Location = new System.Drawing.Point(14, 31);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(1102, 636);
            this.tlpMain.TabIndex = 3;
            // 
            // pnDockRunSnippet
            // 
            this.pnDockRunSnippet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnDockRunSnippet.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.pnDockRunSnippet, 5);
            this.pnDockRunSnippet.Location = new System.Drawing.Point(0, 613);
            this.pnDockRunSnippet.Margin = new System.Windows.Forms.Padding(0);
            this.pnDockRunSnippet.Name = "pnDockRunSnippet";
            this.pnDockRunSnippet.Size = new System.Drawing.Size(1102, 1);
            this.pnDockRunSnippet.TabIndex = 6;
            // 
            // tsMain
            // 
            this.tlpMain.SetColumnSpan(this.tsMain, 6);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.toolStripSeparator2,
            this.tsbReloadFromDisk,
            this.toolStripSeparator3,
            this.tsbSave,
            this.tsbSaveAs,
            this.tsbSaveAll,
            this.tsbSaveAllWithUnsaved,
            this.toolStripSeparator1,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator4,
            this.tsbSpellCheck,
            this.toolStripSeparator5,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.toolStripSeparator6,
            this.tsbPrint,
            this.tsbPrintPreview,
            this.toolStripSeparator7,
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1102, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open...";
            this.tsbOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbReloadFromDisk
            // 
            this.tsbReloadFromDisk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReloadFromDisk.Image = global::ScriptNotepad.Properties.Resources.reload_disk;
            this.tsbReloadFromDisk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReloadFromDisk.Name = "tsbReloadFromDisk";
            this.tsbReloadFromDisk.Size = new System.Drawing.Size(23, 22);
            this.tsbReloadFromDisk.Text = "Reload from disk";
            this.tsbReloadFromDisk.Click += new System.EventHandler(this.MnuReloadFromDisk_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.munSave_Click);
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAs.Image")));
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAs.Text = "Save as";
            this.tsbSaveAs.Click += new System.EventHandler(this.munSave_Click);
            // 
            // tsbSaveAll
            // 
            this.tsbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAll.Image")));
            this.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAll.Name = "tsbSaveAll";
            this.tsbSaveAll.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAll.Text = "Save all";
            this.tsbSaveAll.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // tsbSaveAllWithUnsaved
            // 
            this.tsbSaveAllWithUnsaved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAllWithUnsaved.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAllWithUnsaved.Image")));
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
            this.tsbUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndo.Image")));
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbRedo.Image")));
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbRedo.Text = "Redo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSpellCheck
            // 
            this.tsbSpellCheck.CheckOnClick = true;
            this.tsbSpellCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSpellCheck.Image = global::ScriptNotepad.Properties.Resources.spell_check;
            this.tsbSpellCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSpellCheck.Name = "tsbSpellCheck";
            this.tsbSpellCheck.Size = new System.Drawing.Size(23, 22);
            this.tsbSpellCheck.Text = "Spell checking enabled";
            this.tsbSpellCheck.Click += new System.EventHandler(this.TsbSpellCheck_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = global::ScriptNotepad.Properties.Resources.zoom_in;
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomIn.Text = "toolStripButton1";
            this.tsbZoomIn.Click += new System.EventHandler(this.ZoomInOut_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = global::ScriptNotepad.Properties.Resources.zoom_out;
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomOut.Text = "toolStripButton1";
            this.tsbZoomOut.Click += new System.EventHandler(this.ZoomInOut_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::ScriptNotepad.Properties.Resources.printer;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "Print...";
            this.tsbPrint.Click += new System.EventHandler(this.TsbPrint_Click);
            // 
            // tsbPrintPreview
            // 
            this.tsbPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrintPreview.Image = global::ScriptNotepad.Properties.Resources.print_preview;
            this.tsbPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrintPreview.Name = "tsbPrintPreview";
            this.tsbPrintPreview.Size = new System.Drawing.Size(23, 22);
            this.tsbPrintPreview.Text = "Print preview";
            this.tsbPrintPreview.Click += new System.EventHandler(this.TsbPrintPreview_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = global::ScriptNotepad.Properties.Resources.edit_cut_red;
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "Cut";
            this.tsbCut.Click += new System.EventHandler(this.TsbCopyPasteCut_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = global::ScriptNotepad.Properties.Resources.Copy;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "Copy";
            this.tsbCopy.Click += new System.EventHandler(this.TsbCopyPasteCut_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = global::ScriptNotepad.Properties.Resources.Paste;
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "Paste";
            this.tsbPaste.Click += new System.EventHandler(this.TsbCopyPasteCut_Click);
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
            this.ssLbSessionName,
            this.ssLbSpace7,
            this.sslbZoom,
            this.sslbZoomPercentage,
            this.sslbSpace8,
            this.sslbTabs,
            this.sslbTabsValue});
            this.ssMain.Location = new System.Drawing.Point(0, 614);
            this.ssMain.Name = "ssMain";
            this.ssMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.ssMain.Size = new System.Drawing.Size(1102, 22);
            this.ssMain.TabIndex = 3;
            this.ssMain.Text = "statusStrip1";
            // 
            // ssLbLineColumn
            // 
            this.ssLbLineColumn.Name = "ssLbLineColumn";
            this.ssLbLineColumn.Size = new System.Drawing.Size(77, 17);
            this.ssLbLineColumn.Text = "Line: 1  Col: 1";
            // 
            // tsslLineCol
            // 
            this.tsslLineCol.Name = "tsslLineCol";
            this.tsslLineCol.Size = new System.Drawing.Size(0, 17);
            // 
            // ssLbSpace1
            // 
            this.ssLbSpace1.AutoSize = false;
            this.ssLbSpace1.Name = "ssLbSpace1";
            this.ssLbSpace1.Size = new System.Drawing.Size(30, 17);
            // 
            // ssLbLinesColumnSelection
            // 
            this.ssLbLinesColumnSelection.Name = "ssLbLinesColumnSelection";
            this.ssLbLinesColumnSelection.Size = new System.Drawing.Size(134, 17);
            this.ssLbLinesColumnSelection.Text = "Sel1: 1|1  Sel2: 1|1  Len: 0";
            // 
            // ssLbSpace2
            // 
            this.ssLbSpace2.AutoSize = false;
            this.ssLbSpace2.Name = "ssLbSpace2";
            this.ssLbSpace2.Size = new System.Drawing.Size(30, 17);
            // 
            // ssLbLDocLinesSize
            // 
            this.ssLbLDocLinesSize.Name = "ssLbLDocLinesSize";
            this.ssLbLDocLinesSize.Size = new System.Drawing.Size(92, 17);
            this.ssLbLDocLinesSize.Text = "length: 0 lines: 0";
            // 
            // ssLbSpace3
            // 
            this.ssLbSpace3.AutoSize = false;
            this.ssLbSpace3.Name = "ssLbSpace3";
            this.ssLbSpace3.Size = new System.Drawing.Size(30, 17);
            // 
            // ssLbLineEnding
            // 
            this.ssLbLineEnding.Name = "ssLbLineEnding";
            this.ssLbLineEnding.Size = new System.Drawing.Size(42, 17);
            this.ssLbLineEnding.Text = "CR+LF";
            // 
            // ssLbSpace4
            // 
            this.ssLbSpace4.AutoSize = false;
            this.ssLbSpace4.Name = "ssLbSpace4";
            this.ssLbSpace4.Size = new System.Drawing.Size(30, 17);
            // 
            // ssLbEncoding
            // 
            this.ssLbEncoding.Name = "ssLbEncoding";
            this.ssLbEncoding.Size = new System.Drawing.Size(33, 17);
            this.ssLbEncoding.Text = "UTF8";
            // 
            // ssLbSpace5
            // 
            this.ssLbSpace5.AutoSize = false;
            this.ssLbSpace5.Name = "ssLbSpace5";
            this.ssLbSpace5.Size = new System.Drawing.Size(30, 17);
            // 
            // ssLbInsertOverride
            // 
            this.ssLbInsertOverride.Name = "ssLbInsertOverride";
            this.ssLbInsertOverride.Size = new System.Drawing.Size(25, 17);
            this.ssLbInsertOverride.Text = "INS";
            // 
            // ssLbSpace6
            // 
            this.ssLbSpace6.AutoSize = false;
            this.ssLbSpace6.Name = "ssLbSpace6";
            this.ssLbSpace6.Size = new System.Drawing.Size(30, 17);
            // 
            // ssLbSessionName
            // 
            this.ssLbSessionName.Name = "ssLbSessionName";
            this.ssLbSessionName.Size = new System.Drawing.Size(90, 17);
            this.ssLbSessionName.Text = "Session: Default";
            // 
            // ssLbSpace7
            // 
            this.ssLbSpace7.AutoSize = false;
            this.ssLbSpace7.Name = "ssLbSpace7";
            this.ssLbSpace7.Size = new System.Drawing.Size(30, 17);
            // 
            // sslbZoom
            // 
            this.sslbZoom.Name = "sslbZoom";
            this.sslbZoom.Size = new System.Drawing.Size(42, 17);
            this.sslbZoom.Text = "Zoom:";
            this.sslbZoom.Click += new System.EventHandler(this.ResetZoom_Click);
            // 
            // sslbZoomPercentage
            // 
            this.sslbZoomPercentage.Name = "sslbZoomPercentage";
            this.sslbZoomPercentage.Size = new System.Drawing.Size(35, 17);
            this.sslbZoomPercentage.Text = "100%";
            this.sslbZoomPercentage.Click += new System.EventHandler(this.ResetZoom_Click);
            // 
            // sslbSpace8
            // 
            this.sslbSpace8.AutoSize = false;
            this.sslbSpace8.Name = "sslbSpace8";
            this.sslbSpace8.Size = new System.Drawing.Size(30, 17);
            // 
            // sslbTabs
            // 
            this.sslbTabs.Name = "sslbTabs";
            this.sslbTabs.Size = new System.Drawing.Size(28, 17);
            this.sslbTabs.Text = "Tab:";
            // 
            // sslbTabsValue
            // 
            this.sslbTabsValue.Name = "sslbTabsValue";
            this.sslbTabsValue.Size = new System.Drawing.Size(24, 17);
            this.sslbTabsValue.Text = "0/0";
            // 
            // sttcMain
            // 
            this.sttcMain.AllowDrop = true;
            this.sttcMain.ChangedImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.ChangedImage")));
            this.sttcMain.CloseButtonImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.CloseButtonImage")));
            this.sttcMain.ColorBraceHighlightBackground = System.Drawing.Color.LightGray;
            this.sttcMain.ColorBraceHighlightBad = System.Drawing.Color.Red;
            this.sttcMain.ColorBraceHighlightForeground = System.Drawing.Color.BlueViolet;
            this.tlpMain.SetColumnSpan(this.sttcMain, 5);
            this.sttcMain.CurrentZoomPercentage = 100;
            this.sttcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sttcMain.LeftFileIndex = 0;
            this.sttcMain.Location = new System.Drawing.Point(5, 28);
            this.sttcMain.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.sttcMain.Name = "sttcMain";
            this.sttcMain.NewFilenameStart = "new ";
            this.sttcMain.RightButtonTabActivation = true;
            this.sttcMain.RightButtonTabDragging = false;
            this.sttcMain.SavedImage = ((System.Drawing.Image)(resources.GetObject("sttcMain.SavedImage")));
            this.sttcMain.SeamlessScroll = true;
            this.sttcMain.Size = new System.Drawing.Size(1092, 582);
            this.sttcMain.SuspendTextChangedEvents = false;
            this.sttcMain.TabIndex = 4;
            this.sttcMain.TabWidth = 4;
            this.sttcMain.UseBraceHighlight = false;
            this.sttcMain.UseCodeIndenting = false;
            this.sttcMain.WheelScroll = VPKSoft.ScintillaTabbedTextControl.WheelScrollButtons.Control;
            this.sttcMain.ZoomPercentageAll = 100;
            this.sttcMain.ZoomSynchronization = false;
            this.sttcMain.TabActivated += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabActivated(this.sttcMain_TabActivated);
            this.sttcMain.DocumentZoomChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnDocumentZoomChanged(this.SttcMain_DocumentZoomChanged);
            this.sttcMain.TabClosing += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabClosing(this.sttcMain_TabClosing);
            this.sttcMain.TabClosed += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnTabClosed(this.SttcMain_TabClosed);
            this.sttcMain.CaretPositionChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnCaretPositionChanged(this.sttcMain_SelectionCaretChanged);
            this.sttcMain.DocumentMouseDown += new System.Windows.Forms.MouseEventHandler(this.sttcMain_DocumentMouseDown);
            this.sttcMain.DocumentMouseUp += new System.Windows.Forms.MouseEventHandler(this.sttcMain_DocumentMouseUp);
            this.sttcMain.DocumentMouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SttcMain_DocumentMouseDoubleClick);
            this.sttcMain.SelectionChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnSelectionChanged(this.sttcMain_SelectionCaretChanged);
            this.sttcMain.DocumentTextChanged += new VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl.OnDocumentTextChanged(this.sttcMain_DocumentTextChanged);
            this.sttcMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.SttcMain_DragDrop);
            this.sttcMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.SttcMain_DragEnterOrOver);
            this.sttcMain.DragOver += new System.Windows.Forms.DragEventHandler(this.SttcMain_DragEnterOrOver);
            // 
            // pnDock
            // 
            this.pnDock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnDock.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.pnDock, 5);
            this.pnDock.Location = new System.Drawing.Point(0, 636);
            this.pnDock.Margin = new System.Windows.Forms.Padding(0);
            this.pnDock.Name = "pnDock";
            this.pnDock.Size = new System.Drawing.Size(1102, 1);
            this.pnDock.TabIndex = 5;
            // 
            // odAnyFile
            // 
            this.odAnyFile.FileOk += new System.ComponentModel.CancelEventHandler(this.odAnyFile_FileOk);
            // 
            // tmGUI
            // 
            this.tmGUI.Interval = 1;
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
            this.mnuCloseAllToTheRight,
            this.toolStripMenuItem10,
            this.mnuDiffRight,
            this.mnuDiffLeft,
            this.toolStripMenuItem17,
            this.mnuRenameNewFile});
            this.cmsFileTab.Name = "contextMenuStrip1";
            this.cmsFileTab.Size = new System.Drawing.Size(324, 336);
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
            this.mnuCloseAllButThis.Click += new System.EventHandler(this.CommonCloseManyDocuments);
            // 
            // mnuCloseAllToTheLeft
            // 
            this.mnuCloseAllToTheLeft.Name = "mnuCloseAllToTheLeft";
            this.mnuCloseAllToTheLeft.Size = new System.Drawing.Size(323, 22);
            this.mnuCloseAllToTheLeft.Text = "Close all to the left";
            this.mnuCloseAllToTheLeft.Click += new System.EventHandler(this.CommonCloseManyDocuments);
            // 
            // mnuCloseAllToTheRight
            // 
            this.mnuCloseAllToTheRight.Name = "mnuCloseAllToTheRight";
            this.mnuCloseAllToTheRight.Size = new System.Drawing.Size(323, 22);
            this.mnuCloseAllToTheRight.Text = "Close all to the right";
            this.mnuCloseAllToTheRight.Click += new System.EventHandler(this.CommonCloseManyDocuments);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuDiffRight
            // 
            this.mnuDiffRight.Name = "mnuDiffRight";
            this.mnuDiffRight.Size = new System.Drawing.Size(323, 22);
            this.mnuDiffRight.Text = "Diff to the right";
            this.mnuDiffRight.Click += new System.EventHandler(this.MnuDiffRight_Click);
            // 
            // mnuDiffLeft
            // 
            this.mnuDiffLeft.Name = "mnuDiffLeft";
            this.mnuDiffLeft.Size = new System.Drawing.Size(323, 22);
            this.mnuDiffLeft.Text = "Diff to the left";
            this.mnuDiffLeft.Click += new System.EventHandler(this.MnuDiffLeft_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuRenameNewFile
            // 
            this.mnuRenameNewFile.Name = "mnuRenameNewFile";
            this.mnuRenameNewFile.Size = new System.Drawing.Size(323, 22);
            this.mnuRenameNewFile.Text = "Rename new file";
            this.mnuRenameNewFile.Click += new System.EventHandler(this.MnuRenameNewFile_Click);
            // 
            // tmSpellCheck
            // 
            this.tmSpellCheck.Tick += new System.EventHandler(this.TmSpellCheck_Tick);
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuOpenWithEncoding,
            this.mnuOpenNoBOM,
            this.toolStripMenuItem7,
            this.mnuReloadFromDisk,
            this.mnuTest,
            this.mnuSplit1,
            this.mnuRecentFiles,
            this.mnuSplit2,
            this.mnuSession,
            this.toolStripMenuItem1,
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
            this.mnuNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuNew.Image")));
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNew.Size = new System.Drawing.Size(249, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpen.Image")));
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpen.Size = new System.Drawing.Size(249, 22);
            this.mnuOpen.Text = "Open...";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuOpenWithEncoding
            // 
            this.mnuOpenWithEncoding.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenWithEncoding.Image")));
            this.mnuOpenWithEncoding.Name = "mnuOpenWithEncoding";
            this.mnuOpenWithEncoding.Size = new System.Drawing.Size(249, 22);
            this.mnuOpenWithEncoding.Text = "Open with encoding...";
            this.mnuOpenWithEncoding.Click += new System.EventHandler(this.mnuOpenWithEncoding_Click);
            // 
            // mnuOpenNoBOM
            // 
            this.mnuOpenNoBOM.Image = global::ScriptNotepad.Properties.Resources.open_no_bom;
            this.mnuOpenNoBOM.Name = "mnuOpenNoBOM";
            this.mnuOpenNoBOM.Size = new System.Drawing.Size(249, 22);
            this.mnuOpenNoBOM.Text = "Open no-BOM Unicode file...";
            this.mnuOpenNoBOM.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(246, 6);
            // 
            // mnuReloadFromDisk
            // 
            this.mnuReloadFromDisk.Image = global::ScriptNotepad.Properties.Resources.reload_disk;
            this.mnuReloadFromDisk.Name = "mnuReloadFromDisk";
            this.mnuReloadFromDisk.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuReloadFromDisk.Size = new System.Drawing.Size(249, 22);
            this.mnuReloadFromDisk.Text = "Reload from disk";
            this.mnuReloadFromDisk.Click += new System.EventHandler(this.MnuReloadFromDisk_Click);
            // 
            // mnuTest
            // 
            this.mnuTest.Image = ((System.Drawing.Image)(resources.GetObject("mnuTest.Image")));
            this.mnuTest.Name = "mnuTest";
            this.mnuTest.Size = new System.Drawing.Size(249, 22);
            this.mnuTest.Text = "Test Form...";
            this.mnuTest.Visible = false;
            this.mnuTest.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // mnuSplit1
            // 
            this.mnuSplit1.Name = "mnuSplit1";
            this.mnuSplit1.Size = new System.Drawing.Size(246, 6);
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Image = global::ScriptNotepad.Properties.Resources.History;
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(249, 22);
            this.mnuRecentFiles.Text = "Recent...";
            // 
            // mnuSplit2
            // 
            this.mnuSplit2.Name = "mnuSplit2";
            this.mnuSplit2.Size = new System.Drawing.Size(246, 6);
            // 
            // mnuSession
            // 
            this.mnuSession.Image = ((System.Drawing.Image)(resources.GetObject("mnuSession.Image")));
            this.mnuSession.Name = "mnuSession";
            this.mnuSession.Size = new System.Drawing.Size(249, 22);
            this.mnuSession.Text = "Session";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(246, 6);
            // 
            // munSave
            // 
            this.munSave.Image = ((System.Drawing.Image)(resources.GetObject("munSave.Image")));
            this.munSave.Name = "munSave";
            this.munSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.munSave.Size = new System.Drawing.Size(249, 22);
            this.munSave.Text = "Save";
            this.munSave.Click += new System.EventHandler(this.munSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveAs.Image")));
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.mnuSaveAs.Size = new System.Drawing.Size(249, 22);
            this.mnuSaveAs.Text = "Save as";
            this.mnuSaveAs.Click += new System.EventHandler(this.munSave_Click);
            // 
            // mnuSaveAll
            // 
            this.mnuSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveAll.Image")));
            this.mnuSaveAll.Name = "mnuSaveAll";
            this.mnuSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuSaveAll.Size = new System.Drawing.Size(249, 22);
            this.mnuSaveAll.Text = "Save all";
            this.mnuSaveAll.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // mnuSaveAllWithUnsaved
            // 
            this.mnuSaveAllWithUnsaved.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveAllWithUnsaved.Image")));
            this.mnuSaveAllWithUnsaved.Name = "mnuSaveAllWithUnsaved";
            this.mnuSaveAllWithUnsaved.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuSaveAllWithUnsaved.Size = new System.Drawing.Size(249, 22);
            this.mnuSaveAllWithUnsaved.Text = "Save all (+new)";
            this.mnuSaveAllWithUnsaved.Click += new System.EventHandler(this.mnuSaveAll_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUndo,
            this.mnuRedo,
            this.toolStripMenuItem13,
            this.mnuCut,
            this.mnuCopy,
            this.mnuPaste,
            this.toolStripMenuItem14,
            this.mnuRunScript,
            this.mnuRunScriptOrCommand,
            this.mnuCharSets,
            this.toolStripMenuItem6,
            this.mnuStyle,
            this.toolStripMenuItem8,
            this.mnuExportAsHTML,
            this.mnuDateAndTime,
            this.toolStripMenuItem15,
            this.mnuGoto,
            this.toolStripMenuItem18,
            this.mnuRenameNewFileMainMenu,
            this.mnuFoldAll,
            this.mnuUnfoldAll});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.DropDownOpening += new System.EventHandler(this.MenuCommon_DropDownOpening);
            // 
            // mnuUndo
            // 
            this.mnuUndo.Image = global::ScriptNotepad.Properties.Resources.Undo;
            this.mnuUndo.Name = "mnuUndo";
            this.mnuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuUndo.Size = new System.Drawing.Size(236, 22);
            this.mnuUndo.Text = "Undo";
            this.mnuUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // mnuRedo
            // 
            this.mnuRedo.Image = global::ScriptNotepad.Properties.Resources.Redo;
            this.mnuRedo.Name = "mnuRedo";
            this.mnuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuRedo.Size = new System.Drawing.Size(236, 22);
            this.mnuRedo.Text = "Redo";
            this.mnuRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuCut
            // 
            this.mnuCut.Image = global::ScriptNotepad.Properties.Resources.edit_cut_red;
            this.mnuCut.Name = "mnuCut";
            this.mnuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuCut.Size = new System.Drawing.Size(236, 22);
            this.mnuCut.Text = "Cut";
            this.mnuCut.Click += new System.EventHandler(this.TsbCopyPasteCut_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Image = global::ScriptNotepad.Properties.Resources.Copy;
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuCopy.Size = new System.Drawing.Size(236, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.TsbCopyPasteCut_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Image = global::ScriptNotepad.Properties.Resources.Paste;
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuPaste.Size = new System.Drawing.Size(236, 22);
            this.mnuPaste.Text = "Paste";
            this.mnuPaste.Click += new System.EventHandler(this.TsbCopyPasteCut_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuRunScript
            // 
            this.mnuRunScript.Image = ((System.Drawing.Image)(resources.GetObject("mnuRunScript.Image")));
            this.mnuRunScript.Name = "mnuRunScript";
            this.mnuRunScript.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D3)));
            this.mnuRunScript.Size = new System.Drawing.Size(236, 22);
            this.mnuRunScript.Text = "Run script";
            this.mnuRunScript.Click += new System.EventHandler(this.mnuRunScript_Click);
            // 
            // mnuRunScriptOrCommand
            // 
            this.mnuRunScriptOrCommand.Name = "mnuRunScriptOrCommand";
            this.mnuRunScriptOrCommand.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuRunScriptOrCommand.Size = new System.Drawing.Size(236, 22);
            this.mnuRunScriptOrCommand.Text = "Run script or command...";
            this.mnuRunScriptOrCommand.Click += new System.EventHandler(this.mnuRunScriptOrCommand_Click);
            // 
            // mnuCharSets
            // 
            this.mnuCharSets.Image = ((System.Drawing.Image)(resources.GetObject("mnuCharSets.Image")));
            this.mnuCharSets.Name = "mnuCharSets";
            this.mnuCharSets.Size = new System.Drawing.Size(236, 22);
            this.mnuCharSets.Text = "Change encoding";
            this.mnuCharSets.Click += new System.EventHandler(this.mnuCharSets_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuStyle
            // 
            this.mnuStyle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearAllStyles,
            this.toolStripMenuItem5,
            this.mnuStyle1,
            this.mnuStyle2,
            this.mnuStyle3,
            this.mnuStyle4,
            this.mnuStyle5,
            this.toolStripMenuItem2,
            this.mnuClearStyle1,
            this.mnuClearStyle2,
            this.mnuClearStyle3,
            this.mnuClearStyle4,
            this.mnuClearStyle5});
            this.mnuStyle.Image = global::ScriptNotepad.Properties.Resources.style;
            this.mnuStyle.Name = "mnuStyle";
            this.mnuStyle.Size = new System.Drawing.Size(236, 22);
            this.mnuStyle.Text = "Style";
            // 
            // mnuClearAllStyles
            // 
            this.mnuClearAllStyles.Name = "mnuClearAllStyles";
            this.mnuClearAllStyles.Size = new System.Drawing.Size(266, 22);
            this.mnuClearAllStyles.Text = "Clear all style marks";
            this.mnuClearAllStyles.Click += new System.EventHandler(this.ClearAllStyles_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(263, 6);
            // 
            // mnuStyle1
            // 
            this.mnuStyle1.Name = "mnuStyle1";
            this.mnuStyle1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.mnuStyle1.Size = new System.Drawing.Size(266, 22);
            this.mnuStyle1.Tag = "9";
            this.mnuStyle1.Text = "Mark using the first style";
            this.mnuStyle1.Click += new System.EventHandler(this.StyleSelectOf_Click);
            // 
            // mnuStyle2
            // 
            this.mnuStyle2.Name = "mnuStyle2";
            this.mnuStyle2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.mnuStyle2.Size = new System.Drawing.Size(266, 22);
            this.mnuStyle2.Tag = "10";
            this.mnuStyle2.Text = "Mark using the seconds style";
            this.mnuStyle2.Click += new System.EventHandler(this.StyleSelectOf_Click);
            // 
            // mnuStyle3
            // 
            this.mnuStyle3.Name = "mnuStyle3";
            this.mnuStyle3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.mnuStyle3.Size = new System.Drawing.Size(266, 22);
            this.mnuStyle3.Tag = "11";
            this.mnuStyle3.Text = "Mark using the third style";
            this.mnuStyle3.Click += new System.EventHandler(this.StyleSelectOf_Click);
            // 
            // mnuStyle4
            // 
            this.mnuStyle4.Name = "mnuStyle4";
            this.mnuStyle4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.mnuStyle4.Size = new System.Drawing.Size(266, 22);
            this.mnuStyle4.Tag = "12";
            this.mnuStyle4.Text = "Mark using the fourth style";
            this.mnuStyle4.Click += new System.EventHandler(this.StyleSelectOf_Click);
            // 
            // mnuStyle5
            // 
            this.mnuStyle5.Name = "mnuStyle5";
            this.mnuStyle5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.mnuStyle5.Size = new System.Drawing.Size(266, 22);
            this.mnuStyle5.Tag = "13";
            this.mnuStyle5.Text = "Mark using the fifth style";
            this.mnuStyle5.Click += new System.EventHandler(this.StyleSelectOf_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(263, 6);
            // 
            // mnuClearStyle1
            // 
            this.mnuClearStyle1.Name = "mnuClearStyle1";
            this.mnuClearStyle1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D1)));
            this.mnuClearStyle1.Size = new System.Drawing.Size(266, 22);
            this.mnuClearStyle1.Tag = "9";
            this.mnuClearStyle1.Text = "Clear style one";
            this.mnuClearStyle1.Click += new System.EventHandler(this.ClearStyleOf_Click);
            // 
            // mnuClearStyle2
            // 
            this.mnuClearStyle2.Name = "mnuClearStyle2";
            this.mnuClearStyle2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D2)));
            this.mnuClearStyle2.Size = new System.Drawing.Size(266, 22);
            this.mnuClearStyle2.Tag = "10";
            this.mnuClearStyle2.Text = "Clear style two";
            this.mnuClearStyle2.Click += new System.EventHandler(this.ClearStyleOf_Click);
            // 
            // mnuClearStyle3
            // 
            this.mnuClearStyle3.Name = "mnuClearStyle3";
            this.mnuClearStyle3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D3)));
            this.mnuClearStyle3.Size = new System.Drawing.Size(266, 22);
            this.mnuClearStyle3.Tag = "11";
            this.mnuClearStyle3.Text = "Clear style three";
            this.mnuClearStyle3.Click += new System.EventHandler(this.ClearStyleOf_Click);
            // 
            // mnuClearStyle4
            // 
            this.mnuClearStyle4.Name = "mnuClearStyle4";
            this.mnuClearStyle4.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D4)));
            this.mnuClearStyle4.Size = new System.Drawing.Size(266, 22);
            this.mnuClearStyle4.Tag = "12";
            this.mnuClearStyle4.Text = "Clear style four";
            this.mnuClearStyle4.Click += new System.EventHandler(this.ClearStyleOf_Click);
            // 
            // mnuClearStyle5
            // 
            this.mnuClearStyle5.Name = "mnuClearStyle5";
            this.mnuClearStyle5.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D5)));
            this.mnuClearStyle5.Size = new System.Drawing.Size(266, 22);
            this.mnuClearStyle5.Tag = "13";
            this.mnuClearStyle5.Text = "Clear style five";
            this.mnuClearStyle5.Click += new System.EventHandler(this.ClearStyleOf_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuExportAsHTML
            // 
            this.mnuExportAsHTML.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportAsHTMLToNewDocument,
            this.mnuHTMLToClipboard,
            this.mnuHTMLToFile,
            this.mnuHTMLToFileExecute});
            this.mnuExportAsHTML.Name = "mnuExportAsHTML";
            this.mnuExportAsHTML.Size = new System.Drawing.Size(236, 22);
            this.mnuExportAsHTML.Text = "Export text formatted as HTML";
            // 
            // mnuExportAsHTMLToNewDocument
            // 
            this.mnuExportAsHTMLToNewDocument.Name = "mnuExportAsHTMLToNewDocument";
            this.mnuExportAsHTMLToNewDocument.Size = new System.Drawing.Size(263, 22);
            this.mnuExportAsHTMLToNewDocument.Text = "To a new document";
            this.mnuExportAsHTMLToNewDocument.Click += new System.EventHandler(this.MnuExportAsHTMLToNewDocument_Click);
            // 
            // mnuHTMLToClipboard
            // 
            this.mnuHTMLToClipboard.Name = "mnuHTMLToClipboard";
            this.mnuHTMLToClipboard.Size = new System.Drawing.Size(263, 22);
            this.mnuHTMLToClipboard.Text = "To clipboard";
            this.mnuHTMLToClipboard.Click += new System.EventHandler(this.MnuExportAsHTMLToNewDocument_Click);
            // 
            // mnuHTMLToFile
            // 
            this.mnuHTMLToFile.Name = "mnuHTMLToFile";
            this.mnuHTMLToFile.Size = new System.Drawing.Size(263, 22);
            this.mnuHTMLToFile.Text = "To file";
            this.mnuHTMLToFile.Click += new System.EventHandler(this.MnuExportAsHTMLToNewDocument_Click);
            // 
            // mnuHTMLToFileExecute
            // 
            this.mnuHTMLToFileExecute.Name = "mnuHTMLToFileExecute";
            this.mnuHTMLToFileExecute.Size = new System.Drawing.Size(263, 22);
            this.mnuHTMLToFileExecute.Text = "To file and open with a web browser";
            this.mnuHTMLToFileExecute.Click += new System.EventHandler(this.MnuExportAsHTMLToNewDocument_Click);
            // 
            // mnuDateAndTime
            // 
            this.mnuDateAndTime.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsertDateAndTime});
            this.mnuDateAndTime.Name = "mnuDateAndTime";
            this.mnuDateAndTime.Size = new System.Drawing.Size(236, 22);
            this.mnuDateAndTime.Text = "Date and time";
            // 
            // mnuInsertDateAndTime
            // 
            this.mnuInsertDateAndTime.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsertDateAndTime1,
            this.mnuInsertDateAndTime2,
            this.mnuInsertDateAndTime3,
            this.mnuInsertDateAndTime4,
            this.mnuInsertDateAndTime5,
            this.mnuInsertDateAndTime6});
            this.mnuInsertDateAndTime.Name = "mnuInsertDateAndTime";
            this.mnuInsertDateAndTime.Size = new System.Drawing.Size(148, 22);
            this.mnuInsertDateAndTime.Text = "Date and time";
            this.mnuInsertDateAndTime.DropDownOpening += new System.EventHandler(this.MnuInsertDateAndTime_DropDownOpening);
            // 
            // mnuInsertDateAndTime1
            // 
            this.mnuInsertDateAndTime1.Name = "mnuInsertDateAndTime1";
            this.mnuInsertDateAndTime1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D1)));
            this.mnuInsertDateAndTime1.Size = new System.Drawing.Size(286, 22);
            this.mnuInsertDateAndTime1.Tag = "0";
            this.mnuInsertDateAndTime1.Text = "Insert date and time type 1";
            this.mnuInsertDateAndTime1.Click += new System.EventHandler(this.MnuDate1_Click);
            // 
            // mnuInsertDateAndTime2
            // 
            this.mnuInsertDateAndTime2.Name = "mnuInsertDateAndTime2";
            this.mnuInsertDateAndTime2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D2)));
            this.mnuInsertDateAndTime2.Size = new System.Drawing.Size(286, 22);
            this.mnuInsertDateAndTime2.Tag = "1";
            this.mnuInsertDateAndTime2.Text = "Insert date and time type 2";
            this.mnuInsertDateAndTime2.Click += new System.EventHandler(this.MnuDate1_Click);
            // 
            // mnuInsertDateAndTime3
            // 
            this.mnuInsertDateAndTime3.Name = "mnuInsertDateAndTime3";
            this.mnuInsertDateAndTime3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D3)));
            this.mnuInsertDateAndTime3.Size = new System.Drawing.Size(286, 22);
            this.mnuInsertDateAndTime3.Tag = "2";
            this.mnuInsertDateAndTime3.Text = "Insert date and time type 3";
            this.mnuInsertDateAndTime3.Click += new System.EventHandler(this.MnuDate1_Click);
            // 
            // mnuInsertDateAndTime4
            // 
            this.mnuInsertDateAndTime4.Name = "mnuInsertDateAndTime4";
            this.mnuInsertDateAndTime4.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D4)));
            this.mnuInsertDateAndTime4.Size = new System.Drawing.Size(286, 22);
            this.mnuInsertDateAndTime4.Tag = "3";
            this.mnuInsertDateAndTime4.Text = "Insert date and time type 4";
            this.mnuInsertDateAndTime4.Click += new System.EventHandler(this.MnuDate1_Click);
            // 
            // mnuInsertDateAndTime5
            // 
            this.mnuInsertDateAndTime5.Name = "mnuInsertDateAndTime5";
            this.mnuInsertDateAndTime5.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D5)));
            this.mnuInsertDateAndTime5.Size = new System.Drawing.Size(286, 22);
            this.mnuInsertDateAndTime5.Tag = "4";
            this.mnuInsertDateAndTime5.Text = "Insert date and time type 5";
            this.mnuInsertDateAndTime5.Click += new System.EventHandler(this.MnuDate1_Click);
            // 
            // mnuInsertDateAndTime6
            // 
            this.mnuInsertDateAndTime6.Name = "mnuInsertDateAndTime6";
            this.mnuInsertDateAndTime6.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D6)));
            this.mnuInsertDateAndTime6.Size = new System.Drawing.Size(286, 22);
            this.mnuInsertDateAndTime6.Tag = "5";
            this.mnuInsertDateAndTime6.Text = "Insert date and time type 6";
            this.mnuInsertDateAndTime6.Click += new System.EventHandler(this.MnuDate1_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuGoto
            // 
            this.mnuGoto.Name = "mnuGoto";
            this.mnuGoto.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.mnuGoto.Size = new System.Drawing.Size(236, 22);
            this.mnuGoto.Text = "Goto...";
            this.mnuGoto.Click += new System.EventHandler(this.GotoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuRenameNewFileMainMenu
            // 
            this.mnuRenameNewFileMainMenu.Image = global::ScriptNotepad.Properties.Resources.krename;
            this.mnuRenameNewFileMainMenu.Name = "mnuRenameNewFileMainMenu";
            this.mnuRenameNewFileMainMenu.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuRenameNewFileMainMenu.Size = new System.Drawing.Size(236, 22);
            this.mnuRenameNewFileMainMenu.Text = "Rename new file...";
            this.mnuRenameNewFileMainMenu.Click += new System.EventHandler(this.MnuRenameNewFile_Click);
            // 
            // mnuFoldAll
            // 
            this.mnuFoldAll.Name = "mnuFoldAll";
            this.mnuFoldAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D0)));
            this.mnuFoldAll.Size = new System.Drawing.Size(236, 22);
            this.mnuFoldAll.Text = "Fold all";
            this.mnuFoldAll.Click += new System.EventHandler(this.foldAllLinesToolStripMenuItem_Click);
            // 
            // mnuUnfoldAll
            // 
            this.mnuUnfoldAll.Name = "mnuUnfoldAll";
            this.mnuUnfoldAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D0)));
            this.mnuUnfoldAll.Size = new System.Drawing.Size(236, 22);
            this.mnuUnfoldAll.Text = "Unfold all";
            this.mnuUnfoldAll.Click += new System.EventHandler(this.unfoldAllToolStripMenuItem_Click);
            // 
            // mnuSearch
            // 
            this.mnuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFind,
            this.mnuReplace,
            this.mnuFindInFiles,
            this.mnuMarkText});
            this.mnuSearch.Name = "mnuSearch";
            this.mnuSearch.Size = new System.Drawing.Size(54, 20);
            this.mnuSearch.Text = "Search";
            // 
            // mnuFind
            // 
            this.mnuFind.Image = ((System.Drawing.Image)(resources.GetObject("mnuFind.Image")));
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuFind.Size = new System.Drawing.Size(215, 22);
            this.mnuFind.Text = "Find...";
            this.mnuFind.Click += new System.EventHandler(this.mnuFind_Click);
            // 
            // mnuReplace
            // 
            this.mnuReplace.Image = ((System.Drawing.Image)(resources.GetObject("mnuReplace.Image")));
            this.mnuReplace.Name = "mnuReplace";
            this.mnuReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuReplace.Size = new System.Drawing.Size(215, 22);
            this.mnuReplace.Text = "Replace...";
            this.mnuReplace.Click += new System.EventHandler(this.MnuReplace_Click);
            // 
            // mnuFindInFiles
            // 
            this.mnuFindInFiles.Image = ((System.Drawing.Image)(resources.GetObject("mnuFindInFiles.Image")));
            this.mnuFindInFiles.Name = "mnuFindInFiles";
            this.mnuFindInFiles.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.mnuFindInFiles.Size = new System.Drawing.Size(215, 22);
            this.mnuFindInFiles.Text = "Find in files...";
            this.mnuFindInFiles.Click += new System.EventHandler(this.MnuFindInFiles_Click);
            // 
            // mnuMarkText
            // 
            this.mnuMarkText.Image = global::ScriptNotepad.Properties.Resources.mark_words;
            this.mnuMarkText.Name = "mnuMarkText";
            this.mnuMarkText.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mnuMarkText.Size = new System.Drawing.Size(215, 22);
            this.mnuMarkText.Text = "Mark...";
            this.mnuMarkText.Click += new System.EventHandler(this.MnuMarkText_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowSymbol,
            this.mnuWordWrap});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            // 
            // mnuShowSymbol
            // 
            this.mnuShowSymbol.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowWhiteSpaceAndTab,
            this.mnuShowEndOfLine,
            this.mnuShowIndentGuide,
            this.mnuShowWrapSymbol});
            this.mnuShowSymbol.Image = global::ScriptNotepad.Properties.Resources.paragraph;
            this.mnuShowSymbol.Name = "mnuShowSymbol";
            this.mnuShowSymbol.Size = new System.Drawing.Size(145, 22);
            this.mnuShowSymbol.Text = "Show symbol";
            this.mnuShowSymbol.DropDownOpening += new System.EventHandler(this.MnuShowSymbol_DropDownOpening);
            // 
            // mnuShowWhiteSpaceAndTab
            // 
            this.mnuShowWhiteSpaceAndTab.CheckOnClick = true;
            this.mnuShowWhiteSpaceAndTab.Name = "mnuShowWhiteSpaceAndTab";
            this.mnuShowWhiteSpaceAndTab.Size = new System.Drawing.Size(211, 22);
            this.mnuShowWhiteSpaceAndTab.Text = "Show white space and tab";
            this.mnuShowWhiteSpaceAndTab.Click += new System.EventHandler(this.MnuShowWhiteSpaceAndTab_Click);
            // 
            // mnuShowEndOfLine
            // 
            this.mnuShowEndOfLine.CheckOnClick = true;
            this.mnuShowEndOfLine.Name = "mnuShowEndOfLine";
            this.mnuShowEndOfLine.Size = new System.Drawing.Size(211, 22);
            this.mnuShowEndOfLine.Text = "Show end of line";
            this.mnuShowEndOfLine.Click += new System.EventHandler(this.MnuShowEndOfLine_Click);
            // 
            // mnuShowIndentGuide
            // 
            this.mnuShowIndentGuide.CheckOnClick = true;
            this.mnuShowIndentGuide.Name = "mnuShowIndentGuide";
            this.mnuShowIndentGuide.Size = new System.Drawing.Size(211, 22);
            this.mnuShowIndentGuide.Text = "Show indent guide";
            this.mnuShowIndentGuide.Click += new System.EventHandler(this.MnuShowIndentGuide_Click);
            // 
            // mnuShowWrapSymbol
            // 
            this.mnuShowWrapSymbol.CheckOnClick = true;
            this.mnuShowWrapSymbol.Name = "mnuShowWrapSymbol";
            this.mnuShowWrapSymbol.Size = new System.Drawing.Size(211, 22);
            this.mnuShowWrapSymbol.Text = "Show wrap symbol";
            this.mnuShowWrapSymbol.Click += new System.EventHandler(this.MnuShowWrapSymbol_Click);
            // 
            // mnuWordWrap
            // 
            this.mnuWordWrap.CheckOnClick = true;
            this.mnuWordWrap.Image = global::ScriptNotepad.Properties.Resources.word_wrap2;
            this.mnuWordWrap.Name = "mnuWordWrap";
            this.mnuWordWrap.Size = new System.Drawing.Size(145, 22);
            this.mnuWordWrap.Text = "Word wrap";
            this.mnuWordWrap.Click += new System.EventHandler(this.MnuWordWrap_Click);
            // 
            // mnuProgrammingLanguage
            // 
            this.mnuProgrammingLanguage.Name = "mnuProgrammingLanguage";
            this.mnuProgrammingLanguage.Size = new System.Drawing.Size(71, 20);
            this.mnuProgrammingLanguage.Text = "Language";
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuManageScriptSnippets,
            this.mnuSettings,
            this.mnuManagePlugins,
            this.mnuManageSessions,
            this.mnuLocalization,
            this.toolStripMenuItem11,
            this.mnuDiffFiles});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(46, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuManageScriptSnippets
            // 
            this.mnuManageScriptSnippets.Image = ((System.Drawing.Image)(resources.GetObject("mnuManageScriptSnippets.Image")));
            this.mnuManageScriptSnippets.Name = "mnuManageScriptSnippets";
            this.mnuManageScriptSnippets.Size = new System.Drawing.Size(196, 22);
            this.mnuManageScriptSnippets.Text = "Manage script snippets";
            this.mnuManageScriptSnippets.Click += new System.EventHandler(this.mnuManageScriptSnippets_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuSettings.Image")));
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(196, 22);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // mnuManagePlugins
            // 
            this.mnuManagePlugins.Image = ((System.Drawing.Image)(resources.GetObject("mnuManagePlugins.Image")));
            this.mnuManagePlugins.Name = "mnuManagePlugins";
            this.mnuManagePlugins.Size = new System.Drawing.Size(196, 22);
            this.mnuManagePlugins.Text = "Manage plug-ins";
            this.mnuManagePlugins.Click += new System.EventHandler(this.mnuManagePlugins_Click);
            // 
            // mnuManageSessions
            // 
            this.mnuManageSessions.Image = ((System.Drawing.Image)(resources.GetObject("mnuManageSessions.Image")));
            this.mnuManageSessions.Name = "mnuManageSessions";
            this.mnuManageSessions.Size = new System.Drawing.Size(196, 22);
            this.mnuManageSessions.Text = "Manage sessions";
            this.mnuManageSessions.Click += new System.EventHandler(this.mnuManageSessions_Click);
            // 
            // mnuLocalization
            // 
            this.mnuLocalization.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDumpLanguage});
            this.mnuLocalization.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalization.Image")));
            this.mnuLocalization.Name = "mnuLocalization";
            this.mnuLocalization.Size = new System.Drawing.Size(196, 22);
            this.mnuLocalization.Text = "Localization";
            this.mnuLocalization.Click += new System.EventHandler(this.MnuLocalization_Click);
            // 
            // mnuDumpLanguage
            // 
            this.mnuDumpLanguage.Image = global::ScriptNotepad.Properties.Resources.database_go;
            this.mnuDumpLanguage.Name = "mnuDumpLanguage";
            this.mnuDumpLanguage.Size = new System.Drawing.Size(159, 22);
            this.mnuDumpLanguage.Text = "Dump language";
            this.mnuDumpLanguage.Click += new System.EventHandler(this.MnuDumpLanguage_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(193, 6);
            // 
            // mnuDiffFiles
            // 
            this.mnuDiffFiles.Image = global::ScriptNotepad.Properties.Resources.diff_icon;
            this.mnuDiffFiles.Name = "mnuDiffFiles";
            this.mnuDiffFiles.Size = new System.Drawing.Size(196, 22);
            this.mnuDiffFiles.Text = "Diff files...";
            this.mnuDiffFiles.Click += new System.EventHandler(this.MnuDiffFiles_Click);
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
            this.mnuAbout.Image = global::ScriptNotepad.Properties.Resources.About;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuText,
            this.mnuSearch,
            this.mnuView,
            this.mnuProgrammingLanguage,
            this.mnuTools,
            this.mnuPlugins,
            this.mnuWindow,
            this.mnuTab,
            this.mnuNavigation,
            this.mnuHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuMain.Size = new System.Drawing.Size(1130, 24);
            this.menuMain.TabIndex = 4;
            this.menuMain.Text = "menuStrip1";
            // 
            // mnuText
            // 
            this.mnuText.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCaseSensitive,
            this.toolStripMenuItem9,
            this.mnuSortLines,
            this.mnuRemoveDuplicateLines,
            this.mnuWrapDocumentTo,
            this.toolStripSeparator8,
            this.mnuJsonPrettify,
            this.mnuJsonUglify});
            this.mnuText.Name = "mnuText";
            this.mnuText.Size = new System.Drawing.Size(40, 20);
            this.mnuText.Text = "Text";
            // 
            // mnuCaseSensitive
            // 
            this.mnuCaseSensitive.Image = global::ScriptNotepad.Properties.Resources.format_text_uppercase;
            this.mnuCaseSensitive.Name = "mnuCaseSensitive";
            this.mnuCaseSensitive.Size = new System.Drawing.Size(205, 22);
            this.mnuCaseSensitive.Text = "Case sensitive";
            this.mnuCaseSensitive.Click += new System.EventHandler(this.mnuCaseSensitive_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(202, 6);
            // 
            // mnuSortLines
            // 
            this.mnuSortLines.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSortAscending,
            this.mnuSortDescending,
            this.mnuCustomizedSort});
            this.mnuSortLines.Image = global::ScriptNotepad.Properties.Resources.sort_alphabet;
            this.mnuSortLines.Name = "mnuSortLines";
            this.mnuSortLines.Size = new System.Drawing.Size(205, 22);
            this.mnuSortLines.Text = "Sort lines...";
            this.mnuSortLines.Click += new System.EventHandler(this.MnuSortLines_Click);
            // 
            // mnuSortAscending
            // 
            this.mnuSortAscending.Name = "mnuSortAscending";
            this.mnuSortAscending.Size = new System.Drawing.Size(169, 22);
            this.mnuSortAscending.Text = "Sort ascending";
            this.mnuSortAscending.Click += new System.EventHandler(this.MnuSortLines_Click);
            // 
            // mnuSortDescending
            // 
            this.mnuSortDescending.Name = "mnuSortDescending";
            this.mnuSortDescending.Size = new System.Drawing.Size(169, 22);
            this.mnuSortDescending.Text = "Sort descending";
            this.mnuSortDescending.Click += new System.EventHandler(this.MnuSortLines_Click);
            // 
            // mnuCustomizedSort
            // 
            this.mnuCustomizedSort.Name = "mnuCustomizedSort";
            this.mnuCustomizedSort.Size = new System.Drawing.Size(169, 22);
            this.mnuCustomizedSort.Text = "Customized sort...";
            this.mnuCustomizedSort.Click += new System.EventHandler(this.mnuCustomizedSort_Click);
            // 
            // mnuRemoveDuplicateLines
            // 
            this.mnuRemoveDuplicateLines.Image = global::ScriptNotepad.Properties.Resources.duplicate_lines;
            this.mnuRemoveDuplicateLines.Name = "mnuRemoveDuplicateLines";
            this.mnuRemoveDuplicateLines.Size = new System.Drawing.Size(205, 22);
            this.mnuRemoveDuplicateLines.Text = "Remove duplicate lines...";
            this.mnuRemoveDuplicateLines.Click += new System.EventHandler(this.mnuRemoveDuplicateLines_Click);
            // 
            // mnuWrapDocumentTo
            // 
            this.mnuWrapDocumentTo.Image = global::ScriptNotepad.Properties.Resources.word_wrapped;
            this.mnuWrapDocumentTo.Name = "mnuWrapDocumentTo";
            this.mnuWrapDocumentTo.Size = new System.Drawing.Size(205, 22);
            this.mnuWrapDocumentTo.Text = "Wrap document to...";
            this.mnuWrapDocumentTo.Click += new System.EventHandler(this.MnuWrapDocumentTo_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(202, 6);
            // 
            // mnuJsonPrettify
            // 
            this.mnuJsonPrettify.Name = "mnuJsonPrettify";
            this.mnuJsonPrettify.Size = new System.Drawing.Size(205, 22);
            this.mnuJsonPrettify.Text = "Prettify Json";
            this.mnuJsonPrettify.Click += new System.EventHandler(this.mnuJsonPrettify_Click);
            // 
            // mnuJsonUglify
            // 
            this.mnuJsonUglify.Name = "mnuJsonUglify";
            this.mnuJsonUglify.Size = new System.Drawing.Size(205, 22);
            this.mnuJsonUglify.Text = "Json to one line";
            this.mnuJsonUglify.Click += new System.EventHandler(this.mnuJsonPrettify_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new System.Drawing.Size(63, 20);
            this.mnuWindow.Text = "Window";
            // 
            // mnuTab
            // 
            this.mnuTab.Name = "mnuTab";
            this.mnuTab.Size = new System.Drawing.Size(37, 20);
            this.mnuTab.Text = "Tab";
            // 
            // mnuNavigation
            // 
            this.mnuNavigation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNextTab,
            this.mnuPreviousTab,
            this.toolStripMenuItem16,
            this.mnuFirstTab,
            this.mnuLastTab,
            this.toolStripMenuItem12,
            this.mnuFindTab});
            this.mnuNavigation.Name = "mnuNavigation";
            this.mnuNavigation.Size = new System.Drawing.Size(77, 20);
            this.mnuNavigation.Text = "Navigation";
            this.mnuNavigation.DropDownOpening += new System.EventHandler(this.MnuNavigation_DropDownOpening);
            // 
            // mnuNextTab
            // 
            this.mnuNextTab.Image = global::ScriptNotepad.Properties.Resources.Play;
            this.mnuNextTab.Name = "mnuNextTab";
            this.mnuNextTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuNextTab.Size = new System.Drawing.Size(211, 22);
            this.mnuNextTab.Text = "Next tab";
            this.mnuNextTab.Click += new System.EventHandler(this.MnuNextPrevious_Click);
            // 
            // mnuPreviousTab
            // 
            this.mnuPreviousTab.Image = global::ScriptNotepad.Properties.Resources.Playback;
            this.mnuPreviousTab.Name = "mnuPreviousTab";
            this.mnuPreviousTab.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.mnuPreviousTab.Size = new System.Drawing.Size(211, 22);
            this.mnuPreviousTab.Text = "Previous tab";
            this.mnuPreviousTab.Click += new System.EventHandler(this.MnuNextPrevious_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(208, 6);
            this.toolStripMenuItem16.Visible = false;
            // 
            // mnuFirstTab
            // 
            this.mnuFirstTab.Image = global::ScriptNotepad.Properties.Resources.jump_first;
            this.mnuFirstTab.Name = "mnuFirstTab";
            this.mnuFirstTab.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
            this.mnuFirstTab.Size = new System.Drawing.Size(211, 22);
            this.mnuFirstTab.Text = "To first tab";
            this.mnuFirstTab.Click += new System.EventHandler(this.mnuFirstLastTab_Click);
            // 
            // mnuLastTab
            // 
            this.mnuLastTab.Image = ((System.Drawing.Image)(resources.GetObject("mnuLastTab.Image")));
            this.mnuLastTab.Name = "mnuLastTab";
            this.mnuLastTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.mnuLastTab.Size = new System.Drawing.Size(211, 22);
            this.mnuLastTab.Text = "To last tab";
            this.mnuLastTab.Click += new System.EventHandler(this.mnuFirstLastTab_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(208, 6);
            // 
            // mnuFindTab
            // 
            this.mnuFindTab.Image = global::ScriptNotepad.Properties.Resources.find_tab;
            this.mnuFindTab.Name = "mnuFindTab";
            this.mnuFindTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.mnuFindTab.Size = new System.Drawing.Size(211, 22);
            this.mnuFindTab.Text = "Find tab";
            this.mnuFindTab.Click += new System.EventHandler(this.MnuFindTab_Click);
            // 
            // tmAutoSave
            // 
            this.tmAutoSave.Interval = 300000;
            this.tmAutoSave.Tick += new System.EventHandler(this.TmAutoSave_Tick);
            // 
            // sdHTML
            // 
            this.sdHTML.Filter = "Hyper Text Markup Language file|*.html";
            // 
            // pdPrint
            // 
            this.pdPrint.UseEXDialog = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 681);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMain";
            this.Text = "ScriptNotepad";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.LocationChanged += new System.EventHandler(this.FormMain_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.FormMain_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.cmsFileTab.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.OpenFileDialog odAnyFile;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLineColumn;
        private VPKSoft.ScintillaTabbedTextControl.ScintillaTabbedTextControl sttcMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslLineCol;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLinesColumnSelection;
        private System.Windows.Forms.ToolStripStatusLabel ssLbLDocLinesSize;
        private System.Windows.Forms.SaveFileDialog sdAnyFile;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripButton tsbSaveAll;
        private System.Windows.Forms.ToolStripButton tsbSaveAllWithUnsaved;
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
        private System.Windows.Forms.Panel pnDock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbReloadFromDisk;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Timer tmSpellCheck;
        private System.Windows.Forms.ToolStripButton tsbSpellCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenWithEncoding;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem mnuReloadFromDisk;
        private System.Windows.Forms.ToolStripMenuItem mnuTest;
        private System.Windows.Forms.ToolStripSeparator mnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentFiles;
        private System.Windows.Forms.ToolStripSeparator mnuSplit2;
        private System.Windows.Forms.ToolStripMenuItem mnuSession;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem munSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAll;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAllWithUnsaved;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuRunScript;
        private System.Windows.Forms.ToolStripMenuItem mnuCharSets;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuStyle;
        private System.Windows.Forms.ToolStripMenuItem mnuClearAllStyles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuStyle1;
        private System.Windows.Forms.ToolStripMenuItem mnuStyle2;
        private System.Windows.Forms.ToolStripMenuItem mnuStyle3;
        private System.Windows.Forms.ToolStripMenuItem mnuStyle4;
        private System.Windows.Forms.ToolStripMenuItem mnuStyle5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuClearStyle1;
        private System.Windows.Forms.ToolStripMenuItem mnuClearStyle2;
        private System.Windows.Forms.ToolStripMenuItem mnuClearStyle3;
        private System.Windows.Forms.ToolStripMenuItem mnuClearStyle4;
        private System.Windows.Forms.ToolStripMenuItem mnuClearStyle5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuFind;
        private System.Windows.Forms.ToolStripMenuItem mnuReplace;
        private System.Windows.Forms.ToolStripMenuItem mnuFindInFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuShowSymbol;
        private System.Windows.Forms.ToolStripMenuItem mnuShowWhiteSpaceAndTab;
        private System.Windows.Forms.ToolStripMenuItem mnuShowEndOfLine;
        private System.Windows.Forms.ToolStripMenuItem mnuShowIndentGuide;
        private System.Windows.Forms.ToolStripMenuItem mnuShowWrapSymbol;
        private System.Windows.Forms.ToolStripMenuItem mnuWordWrap;
        private System.Windows.Forms.ToolStripMenuItem mnuProgrammingLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuManageScriptSnippets;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuManagePlugins;
        private System.Windows.Forms.ToolStripMenuItem mnuManageSessions;
        private System.Windows.Forms.ToolStripMenuItem mnuLocalization;
        private System.Windows.Forms.ToolStripMenuItem mnuDumpLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuPlugins;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuDiffRight;
        private System.Windows.Forms.ToolStripMenuItem mnuDiffLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem mnuDiffFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripStatusLabel ssLbSpace7;
        private System.Windows.Forms.ToolStripStatusLabel sslbZoom;
        private System.Windows.Forms.ToolStripStatusLabel sslbZoomPercentage;
        private System.Windows.Forms.Timer tmAutoSave;
        private System.Windows.Forms.ToolStripMenuItem mnuTab;
        private System.Windows.Forms.ToolStripMenuItem mnuMarkText;
        private System.Windows.Forms.ToolStripMenuItem mnuExportAsHTML;
        private System.Windows.Forms.ToolStripMenuItem mnuExportAsHTMLToNewDocument;
        private System.Windows.Forms.SaveFileDialog sdHTML;
        private System.Windows.Forms.ToolStripMenuItem mnuHTMLToClipboard;
        private System.Windows.Forms.ToolStripMenuItem mnuHTMLToFile;
        private System.Windows.Forms.ToolStripMenuItem mnuHTMLToFileExecute;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPrintPreview;
        private System.Windows.Forms.PrintDialog pdPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuUndo;
        private System.Windows.Forms.ToolStripMenuItem mnuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem mnuCut;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem mnuDateAndTime;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime1;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime2;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime3;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime4;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime5;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateAndTime6;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem mnuGoto;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenNoBOM;
        private System.Windows.Forms.ToolStripMenuItem mnuNavigation;
        private System.Windows.Forms.ToolStripMenuItem mnuNextTab;
        private System.Windows.Forms.ToolStripMenuItem mnuPreviousTab;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem mnuFindTab;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem mnuRenameNewFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem mnuRenameNewFileMainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuText;
        private System.Windows.Forms.ToolStripMenuItem mnuSortLines;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveDuplicateLines;
        private System.Windows.Forms.ToolStripMenuItem mnuWrapDocumentTo;
        private System.Windows.Forms.ToolStripMenuItem mnuCaseSensitive;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem mnuFirstTab;
        private System.Windows.Forms.ToolStripMenuItem mnuLastTab;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem mnuSortAscending;
        private System.Windows.Forms.ToolStripMenuItem mnuSortDescending;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomizedSort;
        private System.Windows.Forms.ToolStripStatusLabel sslbSpace8;
        private System.Windows.Forms.ToolStripStatusLabel sslbTabs;
        private System.Windows.Forms.ToolStripStatusLabel sslbTabsValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mnuJsonPrettify;
        private System.Windows.Forms.ToolStripMenuItem mnuJsonUglify;
        private System.Windows.Forms.ToolStripMenuItem mnuRunScriptOrCommand;
        private System.Windows.Forms.Panel pnDockRunSnippet;
        private System.Windows.Forms.ToolStripMenuItem mnuFoldAll;
        private System.Windows.Forms.ToolStripMenuItem mnuUnfoldAll;
    }
}

