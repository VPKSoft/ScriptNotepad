namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    partial class FormSearchAndReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchAndReplace));
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.ssLbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbSearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabFind = new System.Windows.Forms.TabPage();
            this.cbTransparency = new System.Windows.Forms.CheckBox();
            this.gpTransparency = new System.Windows.Forms.GroupBox();
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.rbTransparencyAlways = new System.Windows.Forms.RadioButton();
            this.rbTransparencyOnLosingFocus = new System.Windows.Forms.RadioButton();
            this.gpSearchMode = new System.Windows.Forms.GroupBox();
            this.rbSimpleExtended = new System.Windows.Forms.RadioButton();
            this.rbRegEx = new System.Windows.Forms.RadioButton();
            this.rbExtented = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.cbWrapAround = new System.Windows.Forms.CheckBox();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.cbMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btFindAllCurrent = new System.Windows.Forms.Button();
            this.btFindAllInAll = new System.Windows.Forms.Button();
            this.btCount = new System.Windows.Forms.Button();
            this.btFindNext = new System.Windows.Forms.Button();
            this.btFindPrevious = new System.Windows.Forms.Button();
            this.pnLabelHolder01 = new System.Windows.Forms.Panel();
            this.lbFind = new System.Windows.Forms.Label();
            this.cmbFind = new System.Windows.Forms.ComboBox();
            this.tabReplace = new System.Windows.Forms.TabPage();
            this.gpInSelection = new System.Windows.Forms.GroupBox();
            this.cbInSelection = new System.Windows.Forms.CheckBox();
            this.btReplaceAll = new System.Windows.Forms.Button();
            this.cbTransparency2 = new System.Windows.Forms.CheckBox();
            this.gpTransparency2 = new System.Windows.Forms.GroupBox();
            this.tbOpacity2 = new System.Windows.Forms.TrackBar();
            this.rbTransparencyAlways2 = new System.Windows.Forms.RadioButton();
            this.rbTransparencyOnLosingFocus2 = new System.Windows.Forms.RadioButton();
            this.gpSearchMode2 = new System.Windows.Forms.GroupBox();
            this.rbSimpleExtended2 = new System.Windows.Forms.RadioButton();
            this.rbRegEx2 = new System.Windows.Forms.RadioButton();
            this.rbExtented2 = new System.Windows.Forms.RadioButton();
            this.rbNormal2 = new System.Windows.Forms.RadioButton();
            this.cbWrapAround2 = new System.Windows.Forms.CheckBox();
            this.cbMatchCase2 = new System.Windows.Forms.CheckBox();
            this.cbMatchWholeWord2 = new System.Windows.Forms.CheckBox();
            this.btClose2 = new System.Windows.Forms.Button();
            this.btReplaceAllInAll = new System.Windows.Forms.Button();
            this.btReplace = new System.Windows.Forms.Button();
            this.btFindNext2 = new System.Windows.Forms.Button();
            this.btFindPrevious2 = new System.Windows.Forms.Button();
            this.pnLabelHolder03 = new System.Windows.Forms.Panel();
            this.lbReplace = new System.Windows.Forms.Label();
            this.cmbReplace = new System.Windows.Forms.ComboBox();
            this.pnLabelHolder02 = new System.Windows.Forms.Panel();
            this.lbFind2 = new System.Windows.Forms.Label();
            this.cmbFind2 = new System.Windows.Forms.ComboBox();
            this.tabFindInFiles = new System.Windows.Forms.TabPage();
            this.cbInHiddenFiles = new System.Windows.Forms.CheckBox();
            this.cbHiddenFolders = new System.Windows.Forms.CheckBox();
            this.cbInSubFolders = new System.Windows.Forms.CheckBox();
            this.cbTransparency3 = new System.Windows.Forms.CheckBox();
            this.gpTransparency3 = new System.Windows.Forms.GroupBox();
            this.tbOpacity3 = new System.Windows.Forms.TrackBar();
            this.rbTransparencyAlways3 = new System.Windows.Forms.RadioButton();
            this.rbTransparencyOnLosingFocus3 = new System.Windows.Forms.RadioButton();
            this.btClose3 = new System.Windows.Forms.Button();
            this.btReplaceAllInFiles = new System.Windows.Forms.Button();
            this.gpSearchMode3 = new System.Windows.Forms.GroupBox();
            this.rbSimpleExtended3 = new System.Windows.Forms.RadioButton();
            this.rbRegEx3 = new System.Windows.Forms.RadioButton();
            this.rbExtented3 = new System.Windows.Forms.RadioButton();
            this.rbNormal3 = new System.Windows.Forms.RadioButton();
            this.btFindAllInFiles = new System.Windows.Forms.Button();
            this.cbMatchCase3 = new System.Windows.Forms.CheckBox();
            this.cbMatchWholeWord3 = new System.Windows.Forms.CheckBox();
            this.btSelectFolder = new System.Windows.Forms.Button();
            this.pnLabelHolder07 = new System.Windows.Forms.Panel();
            this.lbDirectory3 = new System.Windows.Forms.Label();
            this.cmbDirectory3 = new System.Windows.Forms.ComboBox();
            this.pnLabelHolder06 = new System.Windows.Forms.Panel();
            this.lbFilters3 = new System.Windows.Forms.Label();
            this.cmbFilters3 = new System.Windows.Forms.ComboBox();
            this.pnLabelHolder05 = new System.Windows.Forms.Panel();
            this.lbReplace3 = new System.Windows.Forms.Label();
            this.cmbReplace3 = new System.Windows.Forms.ComboBox();
            this.pnLabelHolder04 = new System.Windows.Forms.Panel();
            this.lbFind3 = new System.Windows.Forms.Label();
            this.cmbFind3 = new System.Windows.Forms.ComboBox();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.ssMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabFind.SuspendLayout();
            this.gpTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.gpSearchMode.SuspendLayout();
            this.pnLabelHolder01.SuspendLayout();
            this.tabReplace.SuspendLayout();
            this.gpInSelection.SuspendLayout();
            this.gpTransparency2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity2)).BeginInit();
            this.gpSearchMode2.SuspendLayout();
            this.pnLabelHolder03.SuspendLayout();
            this.pnLabelHolder02.SuspendLayout();
            this.tabFindInFiles.SuspendLayout();
            this.gpTransparency3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity3)).BeginInit();
            this.gpSearchMode3.SuspendLayout();
            this.pnLabelHolder07.SuspendLayout();
            this.pnLabelHolder06.SuspendLayout();
            this.pnLabelHolder05.SuspendLayout();
            this.pnLabelHolder04.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssLbStatus,
            this.pbSearchProgress});
            this.ssMain.Location = new System.Drawing.Point(0, 298);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(536, 22);
            this.ssMain.TabIndex = 0;
            // 
            // ssLbStatus
            // 
            this.ssLbStatus.AutoSize = false;
            this.ssLbStatus.Name = "ssLbStatus";
            this.ssLbStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.ssLbStatus.Size = new System.Drawing.Size(400, 17);
            // 
            // pbSearchProgress
            // 
            this.pbSearchProgress.Name = "pbSearchProgress";
            this.pbSearchProgress.Size = new System.Drawing.Size(100, 16);
            this.pbSearchProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabFind);
            this.tcMain.Controls.Add(this.tabReplace);
            this.tcMain.Controls.Add(this.tabFindInFiles);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(536, 298);
            this.tcMain.TabIndex = 1;
            this.tcMain.TabIndexChanged += new System.EventHandler(this.TcMain_TabIndexChanged);
            // 
            // tabFind
            // 
            this.tabFind.Controls.Add(this.cbTransparency);
            this.tabFind.Controls.Add(this.gpTransparency);
            this.tabFind.Controls.Add(this.gpSearchMode);
            this.tabFind.Controls.Add(this.cbWrapAround);
            this.tabFind.Controls.Add(this.cbMatchCase);
            this.tabFind.Controls.Add(this.cbMatchWholeWord);
            this.tabFind.Controls.Add(this.btClose);
            this.tabFind.Controls.Add(this.btFindAllCurrent);
            this.tabFind.Controls.Add(this.btFindAllInAll);
            this.tabFind.Controls.Add(this.btCount);
            this.tabFind.Controls.Add(this.btFindNext);
            this.tabFind.Controls.Add(this.btFindPrevious);
            this.tabFind.Controls.Add(this.pnLabelHolder01);
            this.tabFind.Controls.Add(this.cmbFind);
            this.tabFind.Location = new System.Drawing.Point(4, 22);
            this.tabFind.Name = "tabFind";
            this.tabFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabFind.Size = new System.Drawing.Size(528, 272);
            this.tabFind.TabIndex = 0;
            this.tabFind.Text = "Find";
            this.tabFind.UseVisualStyleBackColor = true;
            // 
            // cbTransparency
            // 
            this.cbTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTransparency.AutoSize = true;
            this.cbTransparency.Checked = true;
            this.cbTransparency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTransparency.Location = new System.Drawing.Point(333, 174);
            this.cbTransparency.Name = "cbTransparency";
            this.cbTransparency.Size = new System.Drawing.Size(91, 17);
            this.cbTransparency.TabIndex = 14;
            this.cbTransparency.Text = "Transparency";
            this.cbTransparency.UseVisualStyleBackColor = true;
            this.cbTransparency.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // gpTransparency
            // 
            this.gpTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gpTransparency.Controls.Add(this.tbOpacity);
            this.gpTransparency.Controls.Add(this.rbTransparencyAlways);
            this.gpTransparency.Controls.Add(this.rbTransparencyOnLosingFocus);
            this.gpTransparency.Location = new System.Drawing.Point(339, 175);
            this.gpTransparency.Name = "gpTransparency";
            this.gpTransparency.Size = new System.Drawing.Size(181, 91);
            this.gpTransparency.TabIndex = 13;
            this.gpTransparency.TabStop = false;
            // 
            // tbOpacity
            // 
            this.tbOpacity.AutoSize = false;
            this.tbOpacity.Location = new System.Drawing.Point(6, 65);
            this.tbOpacity.Maximum = 100;
            this.tbOpacity.Minimum = 1;
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Size = new System.Drawing.Size(169, 17);
            this.tbOpacity.TabIndex = 15;
            this.tbOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbOpacity.Value = 80;
            this.tbOpacity.ValueChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // rbTransparencyAlways
            // 
            this.rbTransparencyAlways.AutoSize = true;
            this.rbTransparencyAlways.Location = new System.Drawing.Point(6, 42);
            this.rbTransparencyAlways.Name = "rbTransparencyAlways";
            this.rbTransparencyAlways.Size = new System.Drawing.Size(58, 17);
            this.rbTransparencyAlways.TabIndex = 1;
            this.rbTransparencyAlways.Text = "Always";
            this.rbTransparencyAlways.UseVisualStyleBackColor = true;
            this.rbTransparencyAlways.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // rbTransparencyOnLosingFocus
            // 
            this.rbTransparencyOnLosingFocus.AutoSize = true;
            this.rbTransparencyOnLosingFocus.Checked = true;
            this.rbTransparencyOnLosingFocus.Location = new System.Drawing.Point(6, 19);
            this.rbTransparencyOnLosingFocus.Name = "rbTransparencyOnLosingFocus";
            this.rbTransparencyOnLosingFocus.Size = new System.Drawing.Size(98, 17);
            this.rbTransparencyOnLosingFocus.TabIndex = 0;
            this.rbTransparencyOnLosingFocus.TabStop = true;
            this.rbTransparencyOnLosingFocus.Text = "On losing focus";
            this.rbTransparencyOnLosingFocus.UseVisualStyleBackColor = true;
            this.rbTransparencyOnLosingFocus.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // gpSearchMode
            // 
            this.gpSearchMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpSearchMode.Controls.Add(this.rbSimpleExtended);
            this.gpSearchMode.Controls.Add(this.rbRegEx);
            this.gpSearchMode.Controls.Add(this.rbExtented);
            this.gpSearchMode.Controls.Add(this.rbNormal);
            this.gpSearchMode.Location = new System.Drawing.Point(8, 160);
            this.gpSearchMode.Name = "gpSearchMode";
            this.gpSearchMode.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.gpSearchMode.Size = new System.Drawing.Size(258, 106);
            this.gpSearchMode.TabIndex = 12;
            this.gpSearchMode.TabStop = false;
            this.gpSearchMode.Text = "Search mode";
            // 
            // rbSimpleExtended
            // 
            this.rbSimpleExtended.AutoSize = true;
            this.rbSimpleExtended.Location = new System.Drawing.Point(6, 85);
            this.rbSimpleExtended.Name = "rbSimpleExtended";
            this.rbSimpleExtended.Size = new System.Drawing.Size(155, 17);
            this.rbSimpleExtended.TabIndex = 3;
            this.rbSimpleExtended.Text = "Simple extended (#, %, *, ?)";
            this.ttMain.SetToolTip(this.rbSimpleExtended, "? = one character, * = multiple characters, # = digit, % = single digit");
            this.rbSimpleExtended.UseVisualStyleBackColor = true;
            this.rbSimpleExtended.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbRegEx
            // 
            this.rbRegEx.AutoSize = true;
            this.rbRegEx.Location = new System.Drawing.Point(6, 62);
            this.rbRegEx.Name = "rbRegEx";
            this.rbRegEx.Size = new System.Drawing.Size(115, 17);
            this.rbRegEx.TabIndex = 2;
            this.rbRegEx.Text = "Regular expression";
            this.rbRegEx.UseVisualStyleBackColor = true;
            this.rbRegEx.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbExtented
            // 
            this.rbExtented.AutoSize = true;
            this.rbExtented.Location = new System.Drawing.Point(6, 39);
            this.rbExtented.Name = "rbExtented";
            this.rbExtented.Size = new System.Drawing.Size(154, 17);
            this.rbExtented.TabIndex = 1;
            this.rbExtented.Text = "Extented (\\n, \\r, \\t, \\0.\\x...)";
            this.rbExtented.UseVisualStyleBackColor = true;
            this.rbExtented.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Checked = true;
            this.rbNormal.Location = new System.Drawing.Point(6, 16);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(58, 17);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            this.rbNormal.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbWrapAround
            // 
            this.cbWrapAround.AutoSize = true;
            this.cbWrapAround.Location = new System.Drawing.Point(8, 127);
            this.cbWrapAround.Name = "cbWrapAround";
            this.cbWrapAround.Size = new System.Drawing.Size(88, 17);
            this.cbWrapAround.TabIndex = 11;
            this.cbWrapAround.Text = "Wrap around";
            this.cbWrapAround.UseVisualStyleBackColor = true;
            this.cbWrapAround.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(8, 104);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase.TabIndex = 10;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            this.cbMatchCase.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbMatchWholeWord
            // 
            this.cbMatchWholeWord.AutoSize = true;
            this.cbMatchWholeWord.Location = new System.Drawing.Point(8, 81);
            this.cbMatchWholeWord.Name = "cbMatchWholeWord";
            this.cbMatchWholeWord.Size = new System.Drawing.Size(135, 17);
            this.cbMatchWholeWord.TabIndex = 9;
            this.cbMatchWholeWord.Text = "Match whole word only";
            this.cbMatchWholeWord.UseVisualStyleBackColor = true;
            this.cbMatchWholeWord.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(366, 148);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(156, 21);
            this.btClose.TabIndex = 8;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // btFindAllCurrent
            // 
            this.btFindAllCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindAllCurrent.Location = new System.Drawing.Point(364, 104);
            this.btFindAllCurrent.Name = "btFindAllCurrent";
            this.btFindAllCurrent.Size = new System.Drawing.Size(156, 38);
            this.btFindAllCurrent.TabIndex = 7;
            this.btFindAllCurrent.Text = "Find all in current document";
            this.btFindAllCurrent.UseVisualStyleBackColor = true;
            this.btFindAllCurrent.Click += new System.EventHandler(this.BtFindAllCurrent_Click);
            // 
            // btFindAllInAll
            // 
            this.btFindAllInAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindAllInAll.Location = new System.Drawing.Point(364, 60);
            this.btFindAllInAll.Name = "btFindAllInAll";
            this.btFindAllInAll.Size = new System.Drawing.Size(156, 38);
            this.btFindAllInAll.TabIndex = 6;
            this.btFindAllInAll.Text = "Find all in all opened documents";
            this.btFindAllInAll.UseVisualStyleBackColor = true;
            this.btFindAllInAll.Click += new System.EventHandler(this.BtFindAllInAll_Click);
            // 
            // btCount
            // 
            this.btCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCount.Location = new System.Drawing.Point(364, 33);
            this.btCount.Name = "btCount";
            this.btCount.Size = new System.Drawing.Size(156, 21);
            this.btCount.TabIndex = 5;
            this.btCount.Text = "Count";
            this.btCount.UseVisualStyleBackColor = true;
            this.btCount.Click += new System.EventHandler(this.BtCount_Click);
            // 
            // btFindNext
            // 
            this.btFindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindNext.Location = new System.Drawing.Point(445, 6);
            this.btFindNext.Name = "btFindNext";
            this.btFindNext.Size = new System.Drawing.Size(75, 21);
            this.btFindNext.TabIndex = 4;
            this.btFindNext.Text = "Find >>";
            this.btFindNext.UseVisualStyleBackColor = true;
            this.btFindNext.Click += new System.EventHandler(this.btFindNext_Click);
            // 
            // btFindPrevious
            // 
            this.btFindPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindPrevious.Location = new System.Drawing.Point(364, 6);
            this.btFindPrevious.Name = "btFindPrevious";
            this.btFindPrevious.Size = new System.Drawing.Size(75, 21);
            this.btFindPrevious.TabIndex = 3;
            this.btFindPrevious.Text = "<< Find";
            this.btFindPrevious.UseVisualStyleBackColor = true;
            this.btFindPrevious.Click += new System.EventHandler(this.btFindPrevious_Click);
            // 
            // pnLabelHolder01
            // 
            this.pnLabelHolder01.Controls.Add(this.lbFind);
            this.pnLabelHolder01.Location = new System.Drawing.Point(5, 6);
            this.pnLabelHolder01.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder01.Name = "pnLabelHolder01";
            this.pnLabelHolder01.Size = new System.Drawing.Size(99, 21);
            this.pnLabelHolder01.TabIndex = 2;
            // 
            // lbFind
            // 
            this.lbFind.AutoEllipsis = true;
            this.lbFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFind.Location = new System.Drawing.Point(0, 0);
            this.lbFind.Name = "lbFind";
            this.lbFind.Size = new System.Drawing.Size(99, 21);
            this.lbFind.TabIndex = 0;
            this.lbFind.Text = "Find:";
            this.lbFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFind
            // 
            this.cmbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFind.FormattingEnabled = true;
            this.cmbFind.Location = new System.Drawing.Point(107, 6);
            this.cmbFind.Name = "cmbFind";
            this.cmbFind.Size = new System.Drawing.Size(251, 21);
            this.cmbFind.TabIndex = 1;
            this.cmbFind.TextChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // tabReplace
            // 
            this.tabReplace.Controls.Add(this.gpInSelection);
            this.tabReplace.Controls.Add(this.cbTransparency2);
            this.tabReplace.Controls.Add(this.gpTransparency2);
            this.tabReplace.Controls.Add(this.gpSearchMode2);
            this.tabReplace.Controls.Add(this.cbWrapAround2);
            this.tabReplace.Controls.Add(this.cbMatchCase2);
            this.tabReplace.Controls.Add(this.cbMatchWholeWord2);
            this.tabReplace.Controls.Add(this.btClose2);
            this.tabReplace.Controls.Add(this.btReplaceAllInAll);
            this.tabReplace.Controls.Add(this.btReplace);
            this.tabReplace.Controls.Add(this.btFindNext2);
            this.tabReplace.Controls.Add(this.btFindPrevious2);
            this.tabReplace.Controls.Add(this.pnLabelHolder03);
            this.tabReplace.Controls.Add(this.cmbReplace);
            this.tabReplace.Controls.Add(this.pnLabelHolder02);
            this.tabReplace.Controls.Add(this.cmbFind2);
            this.tabReplace.Location = new System.Drawing.Point(4, 22);
            this.tabReplace.Name = "tabReplace";
            this.tabReplace.Padding = new System.Windows.Forms.Padding(3);
            this.tabReplace.Size = new System.Drawing.Size(528, 272);
            this.tabReplace.TabIndex = 1;
            this.tabReplace.Text = "Replace";
            this.tabReplace.UseVisualStyleBackColor = true;
            // 
            // gpInSelection
            // 
            this.gpInSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpInSelection.Controls.Add(this.cbInSelection);
            this.gpInSelection.Controls.Add(this.btReplaceAll);
            this.gpInSelection.Location = new System.Drawing.Point(219, 57);
            this.gpInSelection.Margin = new System.Windows.Forms.Padding(0);
            this.gpInSelection.Name = "gpInSelection";
            this.gpInSelection.Padding = new System.Windows.Forms.Padding(0);
            this.gpInSelection.Size = new System.Drawing.Size(306, 34);
            this.gpInSelection.TabIndex = 27;
            this.gpInSelection.TabStop = false;
            // 
            // cbInSelection
            // 
            this.cbInSelection.AutoSize = true;
            this.cbInSelection.Location = new System.Drawing.Point(6, 13);
            this.cbInSelection.Name = "cbInSelection";
            this.cbInSelection.Size = new System.Drawing.Size(80, 17);
            this.cbInSelection.TabIndex = 24;
            this.cbInSelection.Text = "In selection";
            this.cbInSelection.UseVisualStyleBackColor = true;
            this.cbInSelection.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // btReplaceAll
            // 
            this.btReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReplaceAll.Location = new System.Drawing.Point(144, 10);
            this.btReplaceAll.Name = "btReplaceAll";
            this.btReplaceAll.Size = new System.Drawing.Size(156, 21);
            this.btReplaceAll.TabIndex = 18;
            this.btReplaceAll.Text = "Replace all";
            this.btReplaceAll.UseVisualStyleBackColor = true;
            this.btReplaceAll.Click += new System.EventHandler(this.BtReplaceAll_Click);
            // 
            // cbTransparency2
            // 
            this.cbTransparency2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTransparency2.AutoSize = true;
            this.cbTransparency2.Checked = true;
            this.cbTransparency2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTransparency2.Location = new System.Drawing.Point(332, 174);
            this.cbTransparency2.Name = "cbTransparency2";
            this.cbTransparency2.Size = new System.Drawing.Size(91, 17);
            this.cbTransparency2.TabIndex = 26;
            this.cbTransparency2.Text = "Transparency";
            this.cbTransparency2.UseVisualStyleBackColor = true;
            this.cbTransparency2.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // gpTransparency2
            // 
            this.gpTransparency2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gpTransparency2.Controls.Add(this.tbOpacity2);
            this.gpTransparency2.Controls.Add(this.rbTransparencyAlways2);
            this.gpTransparency2.Controls.Add(this.rbTransparencyOnLosingFocus2);
            this.gpTransparency2.Location = new System.Drawing.Point(338, 175);
            this.gpTransparency2.Name = "gpTransparency2";
            this.gpTransparency2.Size = new System.Drawing.Size(181, 91);
            this.gpTransparency2.TabIndex = 25;
            this.gpTransparency2.TabStop = false;
            // 
            // tbOpacity2
            // 
            this.tbOpacity2.AutoSize = false;
            this.tbOpacity2.Location = new System.Drawing.Point(6, 65);
            this.tbOpacity2.Maximum = 100;
            this.tbOpacity2.Minimum = 1;
            this.tbOpacity2.Name = "tbOpacity2";
            this.tbOpacity2.Size = new System.Drawing.Size(169, 17);
            this.tbOpacity2.TabIndex = 15;
            this.tbOpacity2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbOpacity2.Value = 80;
            this.tbOpacity2.ValueChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // rbTransparencyAlways2
            // 
            this.rbTransparencyAlways2.AutoSize = true;
            this.rbTransparencyAlways2.Location = new System.Drawing.Point(6, 42);
            this.rbTransparencyAlways2.Name = "rbTransparencyAlways2";
            this.rbTransparencyAlways2.Size = new System.Drawing.Size(58, 17);
            this.rbTransparencyAlways2.TabIndex = 1;
            this.rbTransparencyAlways2.Text = "Always";
            this.rbTransparencyAlways2.UseVisualStyleBackColor = true;
            this.rbTransparencyAlways2.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // rbTransparencyOnLosingFocus2
            // 
            this.rbTransparencyOnLosingFocus2.AutoSize = true;
            this.rbTransparencyOnLosingFocus2.Checked = true;
            this.rbTransparencyOnLosingFocus2.Location = new System.Drawing.Point(6, 19);
            this.rbTransparencyOnLosingFocus2.Name = "rbTransparencyOnLosingFocus2";
            this.rbTransparencyOnLosingFocus2.Size = new System.Drawing.Size(98, 17);
            this.rbTransparencyOnLosingFocus2.TabIndex = 0;
            this.rbTransparencyOnLosingFocus2.TabStop = true;
            this.rbTransparencyOnLosingFocus2.Text = "On losing focus";
            this.rbTransparencyOnLosingFocus2.UseVisualStyleBackColor = true;
            this.rbTransparencyOnLosingFocus2.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // gpSearchMode2
            // 
            this.gpSearchMode2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpSearchMode2.Controls.Add(this.rbSimpleExtended2);
            this.gpSearchMode2.Controls.Add(this.rbRegEx2);
            this.gpSearchMode2.Controls.Add(this.rbExtented2);
            this.gpSearchMode2.Controls.Add(this.rbNormal2);
            this.gpSearchMode2.Location = new System.Drawing.Point(8, 160);
            this.gpSearchMode2.Name = "gpSearchMode2";
            this.gpSearchMode2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.gpSearchMode2.Size = new System.Drawing.Size(258, 106);
            this.gpSearchMode2.TabIndex = 24;
            this.gpSearchMode2.TabStop = false;
            this.gpSearchMode2.Text = "Search mode";
            // 
            // rbSimpleExtended2
            // 
            this.rbSimpleExtended2.AutoSize = true;
            this.rbSimpleExtended2.Location = new System.Drawing.Point(6, 85);
            this.rbSimpleExtended2.Name = "rbSimpleExtended2";
            this.rbSimpleExtended2.Size = new System.Drawing.Size(155, 17);
            this.rbSimpleExtended2.TabIndex = 4;
            this.rbSimpleExtended2.Text = "Simple extended (#, %, *, ?)";
            this.rbSimpleExtended2.UseVisualStyleBackColor = true;
            this.rbSimpleExtended2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbRegEx2
            // 
            this.rbRegEx2.AutoSize = true;
            this.rbRegEx2.Location = new System.Drawing.Point(6, 62);
            this.rbRegEx2.Name = "rbRegEx2";
            this.rbRegEx2.Size = new System.Drawing.Size(115, 17);
            this.rbRegEx2.TabIndex = 2;
            this.rbRegEx2.Text = "Regular expression";
            this.rbRegEx2.UseVisualStyleBackColor = true;
            this.rbRegEx2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbExtented2
            // 
            this.rbExtented2.AutoSize = true;
            this.rbExtented2.Location = new System.Drawing.Point(6, 39);
            this.rbExtented2.Name = "rbExtented2";
            this.rbExtented2.Size = new System.Drawing.Size(154, 17);
            this.rbExtented2.TabIndex = 1;
            this.rbExtented2.Text = "Extented (\\n, \\r, \\t, \\0.\\x...)";
            this.rbExtented2.UseVisualStyleBackColor = true;
            this.rbExtented2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbNormal2
            // 
            this.rbNormal2.AutoSize = true;
            this.rbNormal2.Checked = true;
            this.rbNormal2.Location = new System.Drawing.Point(6, 16);
            this.rbNormal2.Name = "rbNormal2";
            this.rbNormal2.Size = new System.Drawing.Size(58, 17);
            this.rbNormal2.TabIndex = 0;
            this.rbNormal2.TabStop = true;
            this.rbNormal2.Text = "Normal";
            this.rbNormal2.UseVisualStyleBackColor = true;
            this.rbNormal2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbWrapAround2
            // 
            this.cbWrapAround2.AutoSize = true;
            this.cbWrapAround2.Location = new System.Drawing.Point(8, 127);
            this.cbWrapAround2.Name = "cbWrapAround2";
            this.cbWrapAround2.Size = new System.Drawing.Size(88, 17);
            this.cbWrapAround2.TabIndex = 23;
            this.cbWrapAround2.Text = "Wrap around";
            this.cbWrapAround2.UseVisualStyleBackColor = true;
            this.cbWrapAround2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbMatchCase2
            // 
            this.cbMatchCase2.AutoSize = true;
            this.cbMatchCase2.Location = new System.Drawing.Point(8, 104);
            this.cbMatchCase2.Name = "cbMatchCase2";
            this.cbMatchCase2.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase2.TabIndex = 22;
            this.cbMatchCase2.Text = "Match case";
            this.cbMatchCase2.UseVisualStyleBackColor = true;
            this.cbMatchCase2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbMatchWholeWord2
            // 
            this.cbMatchWholeWord2.AutoSize = true;
            this.cbMatchWholeWord2.Location = new System.Drawing.Point(8, 81);
            this.cbMatchWholeWord2.Name = "cbMatchWholeWord2";
            this.cbMatchWholeWord2.Size = new System.Drawing.Size(135, 17);
            this.cbMatchWholeWord2.TabIndex = 21;
            this.cbMatchWholeWord2.Text = "Match whole word only";
            this.cbMatchWholeWord2.UseVisualStyleBackColor = true;
            this.cbMatchWholeWord2.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // btClose2
            // 
            this.btClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose2.Location = new System.Drawing.Point(363, 138);
            this.btClose2.Name = "btClose2";
            this.btClose2.Size = new System.Drawing.Size(156, 21);
            this.btClose2.TabIndex = 20;
            this.btClose2.Text = "Close";
            this.btClose2.UseVisualStyleBackColor = true;
            this.btClose2.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // btReplaceAllInAll
            // 
            this.btReplaceAllInAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReplaceAllInAll.Location = new System.Drawing.Point(363, 94);
            this.btReplaceAllInAll.Name = "btReplaceAllInAll";
            this.btReplaceAllInAll.Size = new System.Drawing.Size(156, 38);
            this.btReplaceAllInAll.TabIndex = 19;
            this.btReplaceAllInAll.Text = "Replace all in all opended documents";
            this.btReplaceAllInAll.UseVisualStyleBackColor = true;
            this.btReplaceAllInAll.Click += new System.EventHandler(this.BtReplaceAllInAll_Click);
            // 
            // btReplace
            // 
            this.btReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReplace.Location = new System.Drawing.Point(363, 33);
            this.btReplace.Name = "btReplace";
            this.btReplace.Size = new System.Drawing.Size(156, 21);
            this.btReplace.TabIndex = 17;
            this.btReplace.Text = "Replace";
            this.btReplace.UseVisualStyleBackColor = true;
            this.btReplace.Click += new System.EventHandler(this.BtReplace_Click);
            // 
            // btFindNext2
            // 
            this.btFindNext2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindNext2.Location = new System.Drawing.Point(444, 6);
            this.btFindNext2.Name = "btFindNext2";
            this.btFindNext2.Size = new System.Drawing.Size(75, 21);
            this.btFindNext2.TabIndex = 16;
            this.btFindNext2.Text = "Find >>";
            this.btFindNext2.UseVisualStyleBackColor = true;
            this.btFindNext2.Click += new System.EventHandler(this.btFindNext_Click);
            // 
            // btFindPrevious2
            // 
            this.btFindPrevious2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindPrevious2.Location = new System.Drawing.Point(363, 6);
            this.btFindPrevious2.Name = "btFindPrevious2";
            this.btFindPrevious2.Size = new System.Drawing.Size(75, 21);
            this.btFindPrevious2.TabIndex = 15;
            this.btFindPrevious2.Text = "<< Find";
            this.btFindPrevious2.UseVisualStyleBackColor = true;
            this.btFindPrevious2.Click += new System.EventHandler(this.btFindPrevious_Click);
            // 
            // pnLabelHolder03
            // 
            this.pnLabelHolder03.Controls.Add(this.lbReplace);
            this.pnLabelHolder03.Location = new System.Drawing.Point(5, 33);
            this.pnLabelHolder03.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder03.Name = "pnLabelHolder03";
            this.pnLabelHolder03.Size = new System.Drawing.Size(99, 21);
            this.pnLabelHolder03.TabIndex = 6;
            // 
            // lbReplace
            // 
            this.lbReplace.AutoEllipsis = true;
            this.lbReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReplace.Location = new System.Drawing.Point(0, 0);
            this.lbReplace.Name = "lbReplace";
            this.lbReplace.Size = new System.Drawing.Size(99, 21);
            this.lbReplace.TabIndex = 0;
            this.lbReplace.Text = "Replace:";
            this.lbReplace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReplace
            // 
            this.cmbReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReplace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbReplace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbReplace.FormattingEnabled = true;
            this.cmbReplace.Location = new System.Drawing.Point(107, 33);
            this.cmbReplace.Name = "cmbReplace";
            this.cmbReplace.Size = new System.Drawing.Size(251, 21);
            this.cmbReplace.TabIndex = 5;
            this.cmbReplace.TextChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // pnLabelHolder02
            // 
            this.pnLabelHolder02.Controls.Add(this.lbFind2);
            this.pnLabelHolder02.Location = new System.Drawing.Point(5, 6);
            this.pnLabelHolder02.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder02.Name = "pnLabelHolder02";
            this.pnLabelHolder02.Size = new System.Drawing.Size(99, 21);
            this.pnLabelHolder02.TabIndex = 4;
            // 
            // lbFind2
            // 
            this.lbFind2.AutoEllipsis = true;
            this.lbFind2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFind2.Location = new System.Drawing.Point(0, 0);
            this.lbFind2.Name = "lbFind2";
            this.lbFind2.Size = new System.Drawing.Size(99, 21);
            this.lbFind2.TabIndex = 0;
            this.lbFind2.Text = "Find:";
            this.lbFind2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFind2
            // 
            this.cmbFind2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFind2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFind2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFind2.FormattingEnabled = true;
            this.cmbFind2.Location = new System.Drawing.Point(107, 6);
            this.cmbFind2.Name = "cmbFind2";
            this.cmbFind2.Size = new System.Drawing.Size(251, 21);
            this.cmbFind2.TabIndex = 3;
            this.cmbFind2.TextChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // tabFindInFiles
            // 
            this.tabFindInFiles.Controls.Add(this.cbInHiddenFiles);
            this.tabFindInFiles.Controls.Add(this.cbHiddenFolders);
            this.tabFindInFiles.Controls.Add(this.cbInSubFolders);
            this.tabFindInFiles.Controls.Add(this.cbTransparency3);
            this.tabFindInFiles.Controls.Add(this.gpTransparency3);
            this.tabFindInFiles.Controls.Add(this.btClose3);
            this.tabFindInFiles.Controls.Add(this.btReplaceAllInFiles);
            this.tabFindInFiles.Controls.Add(this.gpSearchMode3);
            this.tabFindInFiles.Controls.Add(this.btFindAllInFiles);
            this.tabFindInFiles.Controls.Add(this.cbMatchCase3);
            this.tabFindInFiles.Controls.Add(this.cbMatchWholeWord3);
            this.tabFindInFiles.Controls.Add(this.btSelectFolder);
            this.tabFindInFiles.Controls.Add(this.pnLabelHolder07);
            this.tabFindInFiles.Controls.Add(this.cmbDirectory3);
            this.tabFindInFiles.Controls.Add(this.pnLabelHolder06);
            this.tabFindInFiles.Controls.Add(this.cmbFilters3);
            this.tabFindInFiles.Controls.Add(this.pnLabelHolder05);
            this.tabFindInFiles.Controls.Add(this.cmbReplace3);
            this.tabFindInFiles.Controls.Add(this.pnLabelHolder04);
            this.tabFindInFiles.Controls.Add(this.cmbFind3);
            this.tabFindInFiles.Location = new System.Drawing.Point(4, 22);
            this.tabFindInFiles.Name = "tabFindInFiles";
            this.tabFindInFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFindInFiles.Size = new System.Drawing.Size(528, 272);
            this.tabFindInFiles.TabIndex = 2;
            this.tabFindInFiles.Text = "Find in files";
            this.tabFindInFiles.UseVisualStyleBackColor = true;
            // 
            // cbInHiddenFiles
            // 
            this.cbInHiddenFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInHiddenFiles.Location = new System.Drawing.Point(364, 91);
            this.cbInHiddenFiles.Name = "cbInHiddenFiles";
            this.cbInHiddenFiles.Size = new System.Drawing.Size(156, 17);
            this.cbInHiddenFiles.TabIndex = 42;
            this.cbInHiddenFiles.Text = "In hidden files";
            this.cbInHiddenFiles.UseVisualStyleBackColor = true;
            // 
            // cbHiddenFolders
            // 
            this.cbHiddenFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbHiddenFolders.Location = new System.Drawing.Point(364, 137);
            this.cbHiddenFolders.Name = "cbHiddenFolders";
            this.cbHiddenFolders.Size = new System.Drawing.Size(156, 17);
            this.cbHiddenFolders.TabIndex = 41;
            this.cbHiddenFolders.Text = "In hidden folders";
            this.cbHiddenFolders.UseVisualStyleBackColor = true;
            // 
            // cbInSubFolders
            // 
            this.cbInSubFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInSubFolders.Location = new System.Drawing.Point(364, 114);
            this.cbInSubFolders.Name = "cbInSubFolders";
            this.cbInSubFolders.Size = new System.Drawing.Size(156, 17);
            this.cbInSubFolders.TabIndex = 40;
            this.cbInSubFolders.Text = "In all sub-folders";
            this.cbInSubFolders.UseVisualStyleBackColor = true;
            // 
            // cbTransparency3
            // 
            this.cbTransparency3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTransparency3.AutoSize = true;
            this.cbTransparency3.Checked = true;
            this.cbTransparency3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTransparency3.Location = new System.Drawing.Point(332, 174);
            this.cbTransparency3.Name = "cbTransparency3";
            this.cbTransparency3.Size = new System.Drawing.Size(91, 17);
            this.cbTransparency3.TabIndex = 39;
            this.cbTransparency3.Text = "Transparency";
            this.cbTransparency3.UseVisualStyleBackColor = true;
            this.cbTransparency3.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // gpTransparency3
            // 
            this.gpTransparency3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gpTransparency3.Controls.Add(this.tbOpacity3);
            this.gpTransparency3.Controls.Add(this.rbTransparencyAlways3);
            this.gpTransparency3.Controls.Add(this.rbTransparencyOnLosingFocus3);
            this.gpTransparency3.Location = new System.Drawing.Point(338, 175);
            this.gpTransparency3.Name = "gpTransparency3";
            this.gpTransparency3.Size = new System.Drawing.Size(181, 91);
            this.gpTransparency3.TabIndex = 38;
            this.gpTransparency3.TabStop = false;
            // 
            // tbOpacity3
            // 
            this.tbOpacity3.AutoSize = false;
            this.tbOpacity3.Location = new System.Drawing.Point(6, 65);
            this.tbOpacity3.Maximum = 100;
            this.tbOpacity3.Minimum = 1;
            this.tbOpacity3.Name = "tbOpacity3";
            this.tbOpacity3.Size = new System.Drawing.Size(169, 17);
            this.tbOpacity3.TabIndex = 15;
            this.tbOpacity3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbOpacity3.Value = 80;
            this.tbOpacity3.ValueChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // rbTransparencyAlways3
            // 
            this.rbTransparencyAlways3.AutoSize = true;
            this.rbTransparencyAlways3.Location = new System.Drawing.Point(6, 42);
            this.rbTransparencyAlways3.Name = "rbTransparencyAlways3";
            this.rbTransparencyAlways3.Size = new System.Drawing.Size(58, 17);
            this.rbTransparencyAlways3.TabIndex = 1;
            this.rbTransparencyAlways3.Text = "Always";
            this.rbTransparencyAlways3.UseVisualStyleBackColor = true;
            this.rbTransparencyAlways3.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // rbTransparencyOnLosingFocus3
            // 
            this.rbTransparencyOnLosingFocus3.AutoSize = true;
            this.rbTransparencyOnLosingFocus3.Checked = true;
            this.rbTransparencyOnLosingFocus3.Location = new System.Drawing.Point(6, 19);
            this.rbTransparencyOnLosingFocus3.Name = "rbTransparencyOnLosingFocus3";
            this.rbTransparencyOnLosingFocus3.Size = new System.Drawing.Size(98, 17);
            this.rbTransparencyOnLosingFocus3.TabIndex = 0;
            this.rbTransparencyOnLosingFocus3.TabStop = true;
            this.rbTransparencyOnLosingFocus3.Text = "On losing focus";
            this.rbTransparencyOnLosingFocus3.UseVisualStyleBackColor = true;
            this.rbTransparencyOnLosingFocus3.CheckedChanged += new System.EventHandler(this.TransparencySettings_Changed);
            // 
            // btClose3
            // 
            this.btClose3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose3.Location = new System.Drawing.Point(364, 60);
            this.btClose3.Name = "btClose3";
            this.btClose3.Size = new System.Drawing.Size(156, 21);
            this.btClose3.TabIndex = 37;
            this.btClose3.Text = "Close";
            this.btClose3.UseVisualStyleBackColor = true;
            this.btClose3.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // btReplaceAllInFiles
            // 
            this.btReplaceAllInFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReplaceAllInFiles.Location = new System.Drawing.Point(364, 33);
            this.btReplaceAllInFiles.Name = "btReplaceAllInFiles";
            this.btReplaceAllInFiles.Size = new System.Drawing.Size(156, 21);
            this.btReplaceAllInFiles.TabIndex = 36;
            this.btReplaceAllInFiles.Text = "Replace all in files";
            this.btReplaceAllInFiles.UseVisualStyleBackColor = true;
            this.btReplaceAllInFiles.Click += new System.EventHandler(this.BtReplaceAllInFiles_Click);
            // 
            // gpSearchMode3
            // 
            this.gpSearchMode3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpSearchMode3.Controls.Add(this.rbSimpleExtended3);
            this.gpSearchMode3.Controls.Add(this.rbRegEx3);
            this.gpSearchMode3.Controls.Add(this.rbExtented3);
            this.gpSearchMode3.Controls.Add(this.rbNormal3);
            this.gpSearchMode3.Location = new System.Drawing.Point(8, 160);
            this.gpSearchMode3.Name = "gpSearchMode3";
            this.gpSearchMode3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.gpSearchMode3.Size = new System.Drawing.Size(258, 106);
            this.gpSearchMode3.TabIndex = 35;
            this.gpSearchMode3.TabStop = false;
            this.gpSearchMode3.Text = "Search mode";
            // 
            // rbSimpleExtended3
            // 
            this.rbSimpleExtended3.AutoSize = true;
            this.rbSimpleExtended3.Location = new System.Drawing.Point(6, 85);
            this.rbSimpleExtended3.Name = "rbSimpleExtended3";
            this.rbSimpleExtended3.Size = new System.Drawing.Size(155, 17);
            this.rbSimpleExtended3.TabIndex = 4;
            this.rbSimpleExtended3.Text = "Simple extended (#, %, *, ?)";
            this.rbSimpleExtended3.UseVisualStyleBackColor = true;
            this.rbSimpleExtended3.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbRegEx3
            // 
            this.rbRegEx3.AutoSize = true;
            this.rbRegEx3.Location = new System.Drawing.Point(6, 62);
            this.rbRegEx3.Name = "rbRegEx3";
            this.rbRegEx3.Size = new System.Drawing.Size(115, 17);
            this.rbRegEx3.TabIndex = 2;
            this.rbRegEx3.Text = "Regular expression";
            this.rbRegEx3.UseVisualStyleBackColor = true;
            this.rbRegEx3.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbExtented3
            // 
            this.rbExtented3.AutoSize = true;
            this.rbExtented3.Location = new System.Drawing.Point(6, 39);
            this.rbExtented3.Name = "rbExtented3";
            this.rbExtented3.Size = new System.Drawing.Size(154, 17);
            this.rbExtented3.TabIndex = 1;
            this.rbExtented3.Text = "Extented (\\n, \\r, \\t, \\0.\\x...)";
            this.rbExtented3.UseVisualStyleBackColor = true;
            this.rbExtented3.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // rbNormal3
            // 
            this.rbNormal3.AutoSize = true;
            this.rbNormal3.Checked = true;
            this.rbNormal3.Location = new System.Drawing.Point(6, 16);
            this.rbNormal3.Name = "rbNormal3";
            this.rbNormal3.Size = new System.Drawing.Size(58, 17);
            this.rbNormal3.TabIndex = 0;
            this.rbNormal3.TabStop = true;
            this.rbNormal3.Text = "Normal";
            this.rbNormal3.UseVisualStyleBackColor = true;
            this.rbNormal3.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // btFindAllInFiles
            // 
            this.btFindAllInFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindAllInFiles.Location = new System.Drawing.Point(364, 6);
            this.btFindAllInFiles.Name = "btFindAllInFiles";
            this.btFindAllInFiles.Size = new System.Drawing.Size(156, 21);
            this.btFindAllInFiles.TabIndex = 34;
            this.btFindAllInFiles.Text = "Find all";
            this.btFindAllInFiles.UseVisualStyleBackColor = true;
            this.btFindAllInFiles.Click += new System.EventHandler(this.BtFindAllInFiles_Click);
            // 
            // cbMatchCase3
            // 
            this.cbMatchCase3.AutoSize = true;
            this.cbMatchCase3.Location = new System.Drawing.Point(6, 137);
            this.cbMatchCase3.Name = "cbMatchCase3";
            this.cbMatchCase3.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase3.TabIndex = 33;
            this.cbMatchCase3.Text = "Match case";
            this.cbMatchCase3.UseVisualStyleBackColor = true;
            this.cbMatchCase3.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // cbMatchWholeWord3
            // 
            this.cbMatchWholeWord3.AutoSize = true;
            this.cbMatchWholeWord3.Location = new System.Drawing.Point(6, 114);
            this.cbMatchWholeWord3.Name = "cbMatchWholeWord3";
            this.cbMatchWholeWord3.Size = new System.Drawing.Size(135, 17);
            this.cbMatchWholeWord3.TabIndex = 32;
            this.cbMatchWholeWord3.Text = "Match whole word only";
            this.cbMatchWholeWord3.UseVisualStyleBackColor = true;
            this.cbMatchWholeWord3.CheckedChanged += new System.EventHandler(this.SearchAndReplaceCondition_Changed);
            // 
            // btSelectFolder
            // 
            this.btSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectFolder.Location = new System.Drawing.Point(327, 87);
            this.btSelectFolder.Name = "btSelectFolder";
            this.btSelectFolder.Size = new System.Drawing.Size(31, 21);
            this.btSelectFolder.TabIndex = 31;
            this.btSelectFolder.Text = "...";
            this.btSelectFolder.UseVisualStyleBackColor = true;
            // 
            // pnLabelHolder07
            // 
            this.pnLabelHolder07.Controls.Add(this.lbDirectory3);
            this.pnLabelHolder07.Location = new System.Drawing.Point(5, 87);
            this.pnLabelHolder07.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder07.Name = "pnLabelHolder07";
            this.pnLabelHolder07.Size = new System.Drawing.Size(68, 21);
            this.pnLabelHolder07.TabIndex = 14;
            // 
            // lbDirectory3
            // 
            this.lbDirectory3.AutoEllipsis = true;
            this.lbDirectory3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDirectory3.Location = new System.Drawing.Point(0, 0);
            this.lbDirectory3.Name = "lbDirectory3";
            this.lbDirectory3.Size = new System.Drawing.Size(68, 21);
            this.lbDirectory3.TabIndex = 0;
            this.lbDirectory3.Text = "Directory:";
            this.lbDirectory3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDirectory3
            // 
            this.cmbDirectory3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDirectory3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDirectory3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDirectory3.FormattingEnabled = true;
            this.cmbDirectory3.Location = new System.Drawing.Point(76, 87);
            this.cmbDirectory3.Name = "cmbDirectory3";
            this.cmbDirectory3.Size = new System.Drawing.Size(245, 21);
            this.cmbDirectory3.TabIndex = 13;
            // 
            // pnLabelHolder06
            // 
            this.pnLabelHolder06.Controls.Add(this.lbFilters3);
            this.pnLabelHolder06.Location = new System.Drawing.Point(5, 60);
            this.pnLabelHolder06.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder06.Name = "pnLabelHolder06";
            this.pnLabelHolder06.Size = new System.Drawing.Size(99, 21);
            this.pnLabelHolder06.TabIndex = 12;
            // 
            // lbFilters3
            // 
            this.lbFilters3.AutoEllipsis = true;
            this.lbFilters3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFilters3.Location = new System.Drawing.Point(0, 0);
            this.lbFilters3.Name = "lbFilters3";
            this.lbFilters3.Size = new System.Drawing.Size(99, 21);
            this.lbFilters3.TabIndex = 0;
            this.lbFilters3.Text = "Filters:";
            this.lbFilters3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFilters3
            // 
            this.cmbFilters3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilters3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFilters3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFilters3.FormattingEnabled = true;
            this.cmbFilters3.Location = new System.Drawing.Point(107, 60);
            this.cmbFilters3.Name = "cmbFilters3";
            this.cmbFilters3.Size = new System.Drawing.Size(251, 21);
            this.cmbFilters3.TabIndex = 11;
            // 
            // pnLabelHolder05
            // 
            this.pnLabelHolder05.Controls.Add(this.lbReplace3);
            this.pnLabelHolder05.Location = new System.Drawing.Point(5, 33);
            this.pnLabelHolder05.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder05.Name = "pnLabelHolder05";
            this.pnLabelHolder05.Size = new System.Drawing.Size(99, 21);
            this.pnLabelHolder05.TabIndex = 10;
            // 
            // lbReplace3
            // 
            this.lbReplace3.AutoEllipsis = true;
            this.lbReplace3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReplace3.Location = new System.Drawing.Point(0, 0);
            this.lbReplace3.Name = "lbReplace3";
            this.lbReplace3.Size = new System.Drawing.Size(99, 21);
            this.lbReplace3.TabIndex = 0;
            this.lbReplace3.Text = "Replace:";
            this.lbReplace3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReplace3
            // 
            this.cmbReplace3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReplace3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbReplace3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbReplace3.FormattingEnabled = true;
            this.cmbReplace3.Location = new System.Drawing.Point(107, 33);
            this.cmbReplace3.Name = "cmbReplace3";
            this.cmbReplace3.Size = new System.Drawing.Size(251, 21);
            this.cmbReplace3.TabIndex = 9;
            // 
            // pnLabelHolder04
            // 
            this.pnLabelHolder04.Controls.Add(this.lbFind3);
            this.pnLabelHolder04.Location = new System.Drawing.Point(5, 6);
            this.pnLabelHolder04.Margin = new System.Windows.Forms.Padding(0);
            this.pnLabelHolder04.Name = "pnLabelHolder04";
            this.pnLabelHolder04.Size = new System.Drawing.Size(99, 21);
            this.pnLabelHolder04.TabIndex = 8;
            // 
            // lbFind3
            // 
            this.lbFind3.AutoEllipsis = true;
            this.lbFind3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFind3.Location = new System.Drawing.Point(0, 0);
            this.lbFind3.Name = "lbFind3";
            this.lbFind3.Size = new System.Drawing.Size(99, 21);
            this.lbFind3.TabIndex = 0;
            this.lbFind3.Text = "Find:";
            this.lbFind3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFind3
            // 
            this.cmbFind3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFind3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFind3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFind3.FormattingEnabled = true;
            this.cmbFind3.Location = new System.Drawing.Point(107, 6);
            this.cmbFind3.Name = "cmbFind3";
            this.cmbFind3.Size = new System.Drawing.Size(251, 21);
            this.cmbFind3.TabIndex = 7;
            // 
            // FormSearchAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 320);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.ssMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSearchAndReplace";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FormSearchAndReplace_NeedReloadDocuments);
            this.Deactivate += new System.EventHandler(this.FormSearchAndReplace_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSearchAndReplace_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSearchAndReplace_FormClosed);
            this.Shown += new System.EventHandler(this.FormSearchAndReplace_NeedReloadDocuments);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tabFind.ResumeLayout(false);
            this.tabFind.PerformLayout();
            this.gpTransparency.ResumeLayout(false);
            this.gpTransparency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
            this.gpSearchMode.ResumeLayout(false);
            this.gpSearchMode.PerformLayout();
            this.pnLabelHolder01.ResumeLayout(false);
            this.tabReplace.ResumeLayout(false);
            this.tabReplace.PerformLayout();
            this.gpInSelection.ResumeLayout(false);
            this.gpInSelection.PerformLayout();
            this.gpTransparency2.ResumeLayout(false);
            this.gpTransparency2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity2)).EndInit();
            this.gpSearchMode2.ResumeLayout(false);
            this.gpSearchMode2.PerformLayout();
            this.pnLabelHolder03.ResumeLayout(false);
            this.pnLabelHolder02.ResumeLayout(false);
            this.tabFindInFiles.ResumeLayout(false);
            this.tabFindInFiles.PerformLayout();
            this.gpTransparency3.ResumeLayout(false);
            this.gpTransparency3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity3)).EndInit();
            this.gpSearchMode3.ResumeLayout(false);
            this.gpSearchMode3.PerformLayout();
            this.pnLabelHolder07.ResumeLayout(false);
            this.pnLabelHolder06.ResumeLayout(false);
            this.pnLabelHolder05.ResumeLayout(false);
            this.pnLabelHolder04.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel ssLbStatus;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabFind;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btFindAllCurrent;
        private System.Windows.Forms.Button btFindAllInAll;
        private System.Windows.Forms.Button btCount;
        private System.Windows.Forms.Button btFindNext;
        private System.Windows.Forms.Button btFindPrevious;
        private System.Windows.Forms.Panel pnLabelHolder01;
        private System.Windows.Forms.Label lbFind;
        private System.Windows.Forms.ComboBox cmbFind;
        private System.Windows.Forms.TabPage tabReplace;
        private System.Windows.Forms.GroupBox gpSearchMode;
        private System.Windows.Forms.RadioButton rbRegEx;
        private System.Windows.Forms.RadioButton rbExtented;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.CheckBox cbWrapAround;
        private System.Windows.Forms.CheckBox cbMatchCase;
        private System.Windows.Forms.CheckBox cbMatchWholeWord;
        private System.Windows.Forms.CheckBox cbTransparency;
        private System.Windows.Forms.GroupBox gpTransparency;
        private System.Windows.Forms.TrackBar tbOpacity;
        private System.Windows.Forms.RadioButton rbTransparencyAlways;
        private System.Windows.Forms.RadioButton rbTransparencyOnLosingFocus;
        private System.Windows.Forms.GroupBox gpInSelection;
        private System.Windows.Forms.CheckBox cbInSelection;
        private System.Windows.Forms.Button btReplaceAll;
        private System.Windows.Forms.CheckBox cbTransparency2;
        private System.Windows.Forms.GroupBox gpTransparency2;
        private System.Windows.Forms.TrackBar tbOpacity2;
        private System.Windows.Forms.RadioButton rbTransparencyAlways2;
        private System.Windows.Forms.RadioButton rbTransparencyOnLosingFocus2;
        private System.Windows.Forms.GroupBox gpSearchMode2;
        private System.Windows.Forms.RadioButton rbRegEx2;
        private System.Windows.Forms.RadioButton rbExtented2;
        private System.Windows.Forms.RadioButton rbNormal2;
        private System.Windows.Forms.CheckBox cbWrapAround2;
        private System.Windows.Forms.CheckBox cbMatchCase2;
        private System.Windows.Forms.CheckBox cbMatchWholeWord2;
        private System.Windows.Forms.Button btClose2;
        private System.Windows.Forms.Button btReplaceAllInAll;
        private System.Windows.Forms.Button btReplace;
        private System.Windows.Forms.Button btFindNext2;
        private System.Windows.Forms.Button btFindPrevious2;
        private System.Windows.Forms.Panel pnLabelHolder03;
        private System.Windows.Forms.Label lbReplace;
        private System.Windows.Forms.ComboBox cmbReplace;
        private System.Windows.Forms.Panel pnLabelHolder02;
        private System.Windows.Forms.Label lbFind2;
        private System.Windows.Forms.ComboBox cmbFind2;
        private System.Windows.Forms.ToolStripProgressBar pbSearchProgress;
        private System.Windows.Forms.RadioButton rbSimpleExtended;
        private System.Windows.Forms.RadioButton rbSimpleExtended2;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.TabPage tabFindInFiles;
        private System.Windows.Forms.Panel pnLabelHolder07;
        private System.Windows.Forms.Label lbDirectory3;
        private System.Windows.Forms.ComboBox cmbDirectory3;
        private System.Windows.Forms.Panel pnLabelHolder06;
        private System.Windows.Forms.Label lbFilters3;
        private System.Windows.Forms.ComboBox cmbFilters3;
        private System.Windows.Forms.Panel pnLabelHolder05;
        private System.Windows.Forms.Label lbReplace3;
        private System.Windows.Forms.ComboBox cmbReplace3;
        private System.Windows.Forms.Panel pnLabelHolder04;
        private System.Windows.Forms.Label lbFind3;
        private System.Windows.Forms.ComboBox cmbFind3;
        private System.Windows.Forms.CheckBox cbHiddenFolders;
        private System.Windows.Forms.CheckBox cbInSubFolders;
        private System.Windows.Forms.CheckBox cbTransparency3;
        private System.Windows.Forms.GroupBox gpTransparency3;
        private System.Windows.Forms.TrackBar tbOpacity3;
        private System.Windows.Forms.RadioButton rbTransparencyAlways3;
        private System.Windows.Forms.RadioButton rbTransparencyOnLosingFocus3;
        private System.Windows.Forms.Button btClose3;
        private System.Windows.Forms.Button btReplaceAllInFiles;
        private System.Windows.Forms.GroupBox gpSearchMode3;
        private System.Windows.Forms.RadioButton rbSimpleExtended3;
        private System.Windows.Forms.RadioButton rbRegEx3;
        private System.Windows.Forms.RadioButton rbExtented3;
        private System.Windows.Forms.RadioButton rbNormal3;
        private System.Windows.Forms.Button btFindAllInFiles;
        private System.Windows.Forms.CheckBox cbMatchCase3;
        private System.Windows.Forms.CheckBox cbMatchWholeWord3;
        private System.Windows.Forms.Button btSelectFolder;
        private System.Windows.Forms.CheckBox cbInHiddenFiles;
    }
}