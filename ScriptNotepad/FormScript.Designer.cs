﻿namespace ScriptNotepad
{
    partial class FormScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScript));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbDiscardChanges = new System.Windows.Forms.ToolStripButton();
            this.tsbComboScriptType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tstScriptName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRunScript = new System.Windows.Forms.ToolStripButton();
            this.scintilla = new ScintillaNET.Scintilla();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tbCompilerResults = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbSave,
            this.tsbSaveAs,
            this.tsbDiscardChanges,
            this.tsbComboScriptType,
            this.toolStripSeparator1,
            this.tstScriptName,
            this.toolStripSeparator2,
            this.tsbRunScript});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(944, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSaveButtons_Click);
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAs.Image = global::ScriptNotepad.Properties.Resources.SaveAs;
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAs.Text = "Save as";
            this.tsbSaveAs.Click += new System.EventHandler(this.tsbSaveButtons_Click);
            // 
            // tsbDiscardChanges
            // 
            this.tsbDiscardChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDiscardChanges.Image = global::ScriptNotepad.Properties.Resources.discard_changes;
            this.tsbDiscardChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDiscardChanges.Name = "tsbDiscardChanges";
            this.tsbDiscardChanges.Size = new System.Drawing.Size(23, 22);
            this.tsbDiscardChanges.Text = "Discard changes";
            this.tsbDiscardChanges.Click += new System.EventHandler(this.tsbDiscardChanges_Click);
            // 
            // tsbComboScriptType
            // 
            this.tsbComboScriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbComboScriptType.Name = "tsbComboScriptType";
            this.tsbComboScriptType.Size = new System.Drawing.Size(121, 25);
            this.tsbComboScriptType.ToolTipText = "The script type";
            this.tsbComboScriptType.SelectedIndexChanged += new System.EventHandler(this.common_Changed);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tstScriptName
            // 
            this.tstScriptName.AutoSize = false;
            this.tstScriptName.Name = "tstScriptName";
            this.tstScriptName.Size = new System.Drawing.Size(200, 22);
            this.tstScriptName.Text = "A name for the script";
            this.tstScriptName.ToolTipText = "A name for the script";
            this.tstScriptName.TextChanged += new System.EventHandler(this.common_Changed);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRunScript
            // 
            this.tsbRunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRunScript.Image = ((System.Drawing.Image)(resources.GetObject("tsbRunScript.Image")));
            this.tsbRunScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunScript.Name = "tsbRunScript";
            this.tsbRunScript.Size = new System.Drawing.Size(23, 22);
            this.tsbRunScript.Text = "Run script to the active document";
            this.tsbRunScript.Click += new System.EventHandler(this.tsbRunScript_Click);
            // 
            // scintilla
            // 
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.Location = new System.Drawing.Point(3, 3);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(938, 461);
            this.scintilla.TabIndex = 0;
            this.scintilla.TextChanged += new System.EventHandler(this.common_Changed);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.scintilla, 0, 0);
            this.tlpMain.Controls.Add(this.tbCompilerResults, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(0, 28);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain.Size = new System.Drawing.Size(944, 519);
            this.tlpMain.TabIndex = 2;
            // 
            // tbCompilerResults
            // 
            this.tbCompilerResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCompilerResults.Location = new System.Drawing.Point(3, 470);
            this.tbCompilerResults.Multiline = true;
            this.tbCompilerResults.Name = "tbCompilerResults";
            this.tbCompilerResults.ReadOnly = true;
            this.tbCompilerResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCompilerResults.Size = new System.Drawing.Size(938, 46);
            this.tbCompilerResults.TabIndex = 1;
            this.tbCompilerResults.WordWrap = false;
            // 
            // FormScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 572);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormScript";
            this.Text = "C# Script";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormScript_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripComboBox tsbComboScriptType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton tsbRunScript;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TextBox tbCompilerResults;
        private System.Windows.Forms.ToolStripButton tsbDiscardChanges;
        private System.Windows.Forms.ToolStripTextBox tstScriptName;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
    }
}