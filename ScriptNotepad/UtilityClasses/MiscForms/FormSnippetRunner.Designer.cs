
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
            this.lbRunSnippet = new System.Windows.Forms.Label();
            this.pnRunSnippet = new System.Windows.Forms.TableLayoutPanel();
            this.pnRunCommandButton = new System.Windows.Forms.Panel();
            this.pnRunSnippet.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCommands
            // 
            this.cmbCommands.FormattingEnabled = true;
            this.cmbCommands.Location = new System.Drawing.Point(12, 12);
            this.cmbCommands.Name = "cmbCommands";
            this.cmbCommands.Size = new System.Drawing.Size(386, 23);
            this.cmbCommands.TabIndex = 1;
            // 
            // lbRunSnippet
            // 
            this.lbRunSnippet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRunSnippet.Location = new System.Drawing.Point(3, 0);
            this.lbRunSnippet.Name = "lbRunSnippet";
            this.lbRunSnippet.Size = new System.Drawing.Size(171, 23);
            this.lbRunSnippet.TabIndex = 0;
            this.lbRunSnippet.Text = "Run command (F5)";
            this.lbRunSnippet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbRunSnippet.Click += new System.EventHandler(this.lbRunSnippet_Click);
            // 
            // pnRunSnippet
            // 
            this.pnRunSnippet.ColumnCount = 2;
            this.pnRunSnippet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnRunSnippet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.pnRunSnippet.Controls.Add(this.lbRunSnippet, 0, 0);
            this.pnRunSnippet.Controls.Add(this.pnRunCommandButton, 1, 0);
            this.pnRunSnippet.Location = new System.Drawing.Point(198, 41);
            this.pnRunSnippet.Name = "pnRunSnippet";
            this.pnRunSnippet.RowCount = 1;
            this.pnRunSnippet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnRunSnippet.Size = new System.Drawing.Size(200, 23);
            this.pnRunSnippet.TabIndex = 3;
            // 
            // pnRunCommandButton
            // 
            this.pnRunCommandButton.BackgroundImage = global::ScriptNotepad.Properties.Resources.media_playback_start;
            this.pnRunCommandButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnRunCommandButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRunCommandButton.Location = new System.Drawing.Point(177, 0);
            this.pnRunCommandButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnRunCommandButton.Name = "pnRunCommandButton";
            this.pnRunCommandButton.Size = new System.Drawing.Size(23, 23);
            this.pnRunCommandButton.TabIndex = 1;
            this.pnRunCommandButton.Click += new System.EventHandler(this.lbRunSnippet_Click);
            // 
            // FormSnippetRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 76);
            this.Controls.Add(this.pnRunSnippet);
            this.Controls.Add(this.cmbCommands);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSnippetRunner";
            this.ShowInTaskbar = false;
            this.Text = "Run command...";
            this.Activated += new System.EventHandler(this.FormSnippetRunner_Activated);
            this.Shown += new System.EventHandler(this.FormSnippetRunner_Shown);
            this.pnRunSnippet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBoxCustomSearch cmbCommands;
        private System.Windows.Forms.Label lbRunSnippet;
        private System.Windows.Forms.TableLayoutPanel pnRunSnippet;
        private System.Windows.Forms.Panel pnRunCommandButton;
    }
}