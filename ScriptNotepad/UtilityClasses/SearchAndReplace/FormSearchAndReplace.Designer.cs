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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ssMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabFind.SuspendLayout();
            this.gpTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.gpSearchMode.SuspendLayout();
            this.pnLabelHolder01.SuspendLayout();
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
            this.tcMain.Controls.Add(this.tabPage2);
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(528, 272);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.TabPage tabPage2;
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
    }
}