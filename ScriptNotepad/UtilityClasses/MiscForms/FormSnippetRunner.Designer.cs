
using CustomControls;

namespace ScriptNotepad.UtilityClasses.MiscForms
{
    partial class FormSnippetRunner
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
            this.cmbCommands = new CustomControls.ComboBoxCustomSearch();
            this.ibRunCommand = new CustomControls.ImageButton();
            this.tlpToolStrip = new System.Windows.Forms.TableLayoutPanel();
            this.pnClose = new System.Windows.Forms.Panel();
            this.tlpToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCommands
            // 
            this.cmbCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCommands.FormattingEnabled = true;
            this.cmbCommands.Location = new System.Drawing.Point(204, 18);
            this.cmbCommands.Name = "cmbCommands";
            this.cmbCommands.Size = new System.Drawing.Size(446, 23);
            this.cmbCommands.TabIndex = 1;
            // 
            // ibRunCommand
            // 
            this.ibRunCommand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ibRunCommand.ButtonImage = global::ScriptNotepad.Properties.Resources.media_playback_start;
            this.ibRunCommand.ButtonImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ibRunCommand.Location = new System.Drawing.Point(0, 18);
            this.ibRunCommand.Name = "ibRunCommand";
            this.ibRunCommand.Size = new System.Drawing.Size(198, 23);
            this.ibRunCommand.TabIndex = 2;
            this.ibRunCommand.Text = "Run (CTRL+Enter)";
            this.ibRunCommand.Click += new System.EventHandler(this.ibRunCommand_Click);
            // 
            // tlpToolStrip
            // 
            this.tlpToolStrip.ColumnCount = 6;
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToolStrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpToolStrip.Controls.Add(this.pnClose, 5, 0);
            this.tlpToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpToolStrip.Location = new System.Drawing.Point(0, 0);
            this.tlpToolStrip.Margin = new System.Windows.Forms.Padding(0);
            this.tlpToolStrip.Name = "tlpToolStrip";
            this.tlpToolStrip.RowCount = 1;
            this.tlpToolStrip.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToolStrip.Size = new System.Drawing.Size(650, 17);
            this.tlpToolStrip.TabIndex = 3;
            // 
            // pnClose
            // 
            this.pnClose.BackgroundImage = global::ScriptNotepad.Properties.Resources.close_small;
            this.pnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnClose.Location = new System.Drawing.Point(636, 3);
            this.pnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnClose.Name = "pnClose";
            this.pnClose.Size = new System.Drawing.Size(9, 9);
            this.pnClose.TabIndex = 4;
            this.pnClose.Click += new System.EventHandler(this.pnClose_Click);
            // 
            // FormSnippetRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 42);
            this.Controls.Add(this.tlpToolStrip);
            this.Controls.Add(this.ibRunCommand);
            this.Controls.Add(this.cmbCommands);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "FormSnippetRunner";
            this.ShowInTaskbar = false;
            this.Text = "Run command...";
            this.Shown += new System.EventHandler(this.FormSnippetRunner_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSnippetRunner_KeyDown);
            this.tlpToolStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBoxCustomSearch cmbCommands;
        private ImageButton ibRunCommand;
        private System.Windows.Forms.TableLayoutPanel tlpToolStrip;
        private System.Windows.Forms.Panel pnClose;
    }
}