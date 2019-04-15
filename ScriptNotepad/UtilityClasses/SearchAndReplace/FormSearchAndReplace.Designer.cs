﻿namespace ScriptNotepad.UtilityClasses.SearchAndReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchAndReplace));
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.ssLbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabFind = new System.Windows.Forms.TabPage();
            this.cbTransparency = new System.Windows.Forms.CheckBox();
            this.gpTransparency = new System.Windows.Forms.GroupBox();
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.rbTransparencyAlways = new System.Windows.Forms.RadioButton();
            this.rbTransparencyOnLosingFocus = new System.Windows.Forms.RadioButton();
            this.gpSearchMode = new System.Windows.Forms.GroupBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbOpacity2 = new System.Windows.Forms.TrackBar();
            this.rbTransparencyAlways2 = new System.Windows.Forms.RadioButton();
            this.rbTransparencyOnLosingFocus2 = new System.Windows.Forms.RadioButton();
            this.gpSearchMode2 = new System.Windows.Forms.GroupBox();
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
            this.ssMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabFind.SuspendLayout();
            this.gpTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.gpSearchMode.SuspendLayout();
            this.pnLabelHolder01.SuspendLayout();
            this.tabReplace.SuspendLayout();
            this.gpInSelection.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity2)).BeginInit();
            this.gpSearchMode2.SuspendLayout();
            this.pnLabelHolder03.SuspendLayout();
            this.pnLabelHolder02.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssLbStatus});
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
            this.ssLbStatus.Size = new System.Drawing.Size(490, 17);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabFind);
            this.tcMain.Controls.Add(this.tabReplace);
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
            // 
            // gpSearchMode
            // 
            this.gpSearchMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpSearchMode.Controls.Add(this.rbRegEx);
            this.gpSearchMode.Controls.Add(this.rbExtented);
            this.gpSearchMode.Controls.Add(this.rbNormal);
            this.gpSearchMode.Location = new System.Drawing.Point(8, 174);
            this.gpSearchMode.Name = "gpSearchMode";
            this.gpSearchMode.Size = new System.Drawing.Size(198, 92);
            this.gpSearchMode.TabIndex = 12;
            this.gpSearchMode.TabStop = false;
            this.gpSearchMode.Text = "Search mode";
            // 
            // rbRegEx
            // 
            this.rbRegEx.AutoSize = true;
            this.rbRegEx.Location = new System.Drawing.Point(6, 65);
            this.rbRegEx.Name = "rbRegEx";
            this.rbRegEx.Size = new System.Drawing.Size(115, 17);
            this.rbRegEx.TabIndex = 2;
            this.rbRegEx.Text = "Regular expression";
            this.rbRegEx.UseVisualStyleBackColor = true;
            this.rbRegEx.CheckedChanged += new System.EventHandler(this.SearchCondition_Changed);
            // 
            // rbExtented
            // 
            this.rbExtented.AutoSize = true;
            this.rbExtented.Location = new System.Drawing.Point(6, 42);
            this.rbExtented.Name = "rbExtented";
            this.rbExtented.Size = new System.Drawing.Size(154, 17);
            this.rbExtented.TabIndex = 1;
            this.rbExtented.Text = "Extented (\\n, \\r, \\t, \\0.\\x...)";
            this.rbExtented.UseVisualStyleBackColor = true;
            this.rbExtented.CheckedChanged += new System.EventHandler(this.SearchCondition_Changed);
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Checked = true;
            this.rbNormal.Location = new System.Drawing.Point(6, 19);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(58, 17);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            this.rbNormal.CheckedChanged += new System.EventHandler(this.SearchCondition_Changed);
            // 
            // cbWrapAround
            // 
            this.cbWrapAround.AutoSize = true;
            this.cbWrapAround.Location = new System.Drawing.Point(8, 138);
            this.cbWrapAround.Name = "cbWrapAround";
            this.cbWrapAround.Size = new System.Drawing.Size(88, 17);
            this.cbWrapAround.TabIndex = 11;
            this.cbWrapAround.Text = "Wrap around";
            this.cbWrapAround.UseVisualStyleBackColor = true;
            this.cbWrapAround.CheckedChanged += new System.EventHandler(this.SearchCondition_Changed);
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(8, 115);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase.TabIndex = 10;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            this.cbMatchCase.CheckedChanged += new System.EventHandler(this.SearchCondition_Changed);
            // 
            // cbMatchWholeWord
            // 
            this.cbMatchWholeWord.AutoSize = true;
            this.cbMatchWholeWord.Location = new System.Drawing.Point(8, 92);
            this.cbMatchWholeWord.Name = "cbMatchWholeWord";
            this.cbMatchWholeWord.Size = new System.Drawing.Size(135, 17);
            this.cbMatchWholeWord.TabIndex = 9;
            this.cbMatchWholeWord.Text = "Match whole word only";
            this.cbMatchWholeWord.UseVisualStyleBackColor = true;
            this.cbMatchWholeWord.CheckedChanged += new System.EventHandler(this.SearchCondition_Changed);
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
            this.cmbFind.FormattingEnabled = true;
            this.cmbFind.Location = new System.Drawing.Point(107, 6);
            this.cmbFind.Name = "cmbFind";
            this.cmbFind.Size = new System.Drawing.Size(251, 21);
            this.cmbFind.TabIndex = 1;
            this.cmbFind.TextChanged += new System.EventHandler(this.SearchCondition_Changed);
            // 
            // tabReplace
            // 
            this.tabReplace.Controls.Add(this.gpInSelection);
            this.tabReplace.Controls.Add(this.cbTransparency2);
            this.tabReplace.Controls.Add(this.groupBox1);
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
            this.cbInSelection.Enabled = false;
            this.cbInSelection.Location = new System.Drawing.Point(6, 13);
            this.cbInSelection.Name = "cbInSelection";
            this.cbInSelection.Size = new System.Drawing.Size(80, 17);
            this.cbInSelection.TabIndex = 24;
            this.cbInSelection.Text = "In selection";
            this.cbInSelection.UseVisualStyleBackColor = true;
            this.cbInSelection.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
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
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbOpacity2);
            this.groupBox1.Controls.Add(this.rbTransparencyAlways2);
            this.groupBox1.Controls.Add(this.rbTransparencyOnLosingFocus2);
            this.groupBox1.Location = new System.Drawing.Point(338, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 91);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
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
            // 
            // gpSearchMode2
            // 
            this.gpSearchMode2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpSearchMode2.Controls.Add(this.rbRegEx2);
            this.gpSearchMode2.Controls.Add(this.rbExtented2);
            this.gpSearchMode2.Controls.Add(this.rbNormal2);
            this.gpSearchMode2.Location = new System.Drawing.Point(8, 174);
            this.gpSearchMode2.Name = "gpSearchMode2";
            this.gpSearchMode2.Size = new System.Drawing.Size(198, 92);
            this.gpSearchMode2.TabIndex = 24;
            this.gpSearchMode2.TabStop = false;
            this.gpSearchMode2.Text = "Search mode";
            // 
            // rbRegEx2
            // 
            this.rbRegEx2.AutoSize = true;
            this.rbRegEx2.Location = new System.Drawing.Point(6, 65);
            this.rbRegEx2.Name = "rbRegEx2";
            this.rbRegEx2.Size = new System.Drawing.Size(115, 17);
            this.rbRegEx2.TabIndex = 2;
            this.rbRegEx2.Text = "Regular expression";
            this.rbRegEx2.UseVisualStyleBackColor = true;
            this.rbRegEx2.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
            // 
            // rbExtented2
            // 
            this.rbExtented2.AutoSize = true;
            this.rbExtented2.Location = new System.Drawing.Point(6, 42);
            this.rbExtented2.Name = "rbExtented2";
            this.rbExtented2.Size = new System.Drawing.Size(154, 17);
            this.rbExtented2.TabIndex = 1;
            this.rbExtented2.Text = "Extented (\\n, \\r, \\t, \\0.\\x...)";
            this.rbExtented2.UseVisualStyleBackColor = true;
            this.rbExtented2.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
            // 
            // rbNormal2
            // 
            this.rbNormal2.AutoSize = true;
            this.rbNormal2.Checked = true;
            this.rbNormal2.Location = new System.Drawing.Point(6, 19);
            this.rbNormal2.Name = "rbNormal2";
            this.rbNormal2.Size = new System.Drawing.Size(58, 17);
            this.rbNormal2.TabIndex = 0;
            this.rbNormal2.TabStop = true;
            this.rbNormal2.Text = "Normal";
            this.rbNormal2.UseVisualStyleBackColor = true;
            this.rbNormal2.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
            // 
            // cbWrapAround2
            // 
            this.cbWrapAround2.AutoSize = true;
            this.cbWrapAround2.Location = new System.Drawing.Point(7, 138);
            this.cbWrapAround2.Name = "cbWrapAround2";
            this.cbWrapAround2.Size = new System.Drawing.Size(88, 17);
            this.cbWrapAround2.TabIndex = 23;
            this.cbWrapAround2.Text = "Wrap around";
            this.cbWrapAround2.UseVisualStyleBackColor = true;
            this.cbWrapAround2.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
            // 
            // cbMatchCase2
            // 
            this.cbMatchCase2.AutoSize = true;
            this.cbMatchCase2.Location = new System.Drawing.Point(7, 115);
            this.cbMatchCase2.Name = "cbMatchCase2";
            this.cbMatchCase2.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase2.TabIndex = 22;
            this.cbMatchCase2.Text = "Match case";
            this.cbMatchCase2.UseVisualStyleBackColor = true;
            this.cbMatchCase2.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
            // 
            // cbMatchWholeWord2
            // 
            this.cbMatchWholeWord2.AutoSize = true;
            this.cbMatchWholeWord2.Location = new System.Drawing.Point(7, 92);
            this.cbMatchWholeWord2.Name = "cbMatchWholeWord2";
            this.cbMatchWholeWord2.Size = new System.Drawing.Size(135, 17);
            this.cbMatchWholeWord2.TabIndex = 21;
            this.cbMatchWholeWord2.Text = "Match whole word only";
            this.cbMatchWholeWord2.UseVisualStyleBackColor = true;
            this.cbMatchWholeWord2.CheckedChanged += new System.EventHandler(this.ReplaceCondition_Changed);
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
            this.cmbReplace.FormattingEnabled = true;
            this.cmbReplace.Location = new System.Drawing.Point(107, 33);
            this.cmbReplace.Name = "cmbReplace";
            this.cmbReplace.Size = new System.Drawing.Size(251, 21);
            this.cmbReplace.TabIndex = 5;
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
            this.cmbFind2.FormattingEnabled = true;
            this.cmbFind2.Location = new System.Drawing.Point(107, 6);
            this.cmbFind2.Name = "cmbFind2";
            this.cmbFind2.Size = new System.Drawing.Size(251, 21);
            this.cmbFind2.TabIndex = 3;
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity2)).EndInit();
            this.gpSearchMode2.ResumeLayout(false);
            this.gpSearchMode2.PerformLayout();
            this.pnLabelHolder03.ResumeLayout(false);
            this.pnLabelHolder02.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
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
    }
}