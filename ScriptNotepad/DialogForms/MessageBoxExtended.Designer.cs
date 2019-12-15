namespace ScriptNotepad.DialogForms
{
    partial class MessageBoxExtended
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
            this.pbMessageBoxIcon = new System.Windows.Forms.PictureBox();
            this.lbText = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cbRememberAnswer = new System.Windows.Forms.CheckBox();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessageBoxIcon)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMessageBoxIcon
            // 
            this.pbMessageBoxIcon.Location = new System.Drawing.Point(3, 3);
            this.pbMessageBoxIcon.Name = "pbMessageBoxIcon";
            this.pbMessageBoxIcon.Size = new System.Drawing.Size(32, 32);
            this.pbMessageBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMessageBoxIcon.TabIndex = 0;
            this.pbMessageBoxIcon.TabStop = false;
            // 
            // lbText
            // 
            this.lbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbText.Location = new System.Drawing.Point(41, 3);
            this.lbText.Margin = new System.Windows.Forms.Padding(3);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(437, 85);
            this.lbText.TabIndex = 1;
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.cbRememberAnswer, 0, 1);
            this.tlpMain.Controls.Add(this.pbMessageBoxIcon, 0, 0);
            this.tlpMain.Controls.Add(this.lbText, 1, 0);
            this.tlpMain.Location = new System.Drawing.Point(12, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(481, 114);
            this.tlpMain.TabIndex = 2;
            // 
            // cbRememberAnswer
            // 
            this.cbRememberAnswer.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.cbRememberAnswer, 2);
            this.cbRememberAnswer.Location = new System.Drawing.Point(3, 94);
            this.cbRememberAnswer.Name = "cbRememberAnswer";
            this.cbRememberAnswer.Size = new System.Drawing.Size(114, 17);
            this.cbRememberAnswer.TabIndex = 4;
            this.cbRememberAnswer.Text = "Remember answer";
            this.cbRememberAnswer.UseVisualStyleBackColor = true;
            // 
            // flpButtons
            // 
            this.flpButtons.BackColor = System.Drawing.SystemColors.Control;
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpButtons.Location = new System.Drawing.Point(0, 135);
            this.flpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Padding = new System.Windows.Forms.Padding(10);
            this.flpButtons.Size = new System.Drawing.Size(505, 46);
            this.flpButtons.TabIndex = 3;
            // 
            // MessageBoxExtended
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(505, 181);
            this.Controls.Add(this.flpButtons);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxExtended";
            this.ShowInTaskbar = false;
            this.Text = "MessageBoxExtented";
            this.Shown += new System.EventHandler(this.MessageBoxExtended_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbMessageBoxIcon)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMessageBoxIcon;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.CheckBox cbRememberAnswer;
    }
}