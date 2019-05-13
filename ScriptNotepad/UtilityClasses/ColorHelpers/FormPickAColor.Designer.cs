namespace ScriptNotepad.UtilityClasses.ColorHelpers
{
    partial class FormPickAColor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPickAColor));
            this.lbColorFromArgb = new System.Windows.Forms.Label();
            this.tbColorFromArgb = new System.Windows.Forms.TextBox();
            this.tbHexRGB = new System.Windows.Forms.TextBox();
            this.lbHexRGB = new System.Windows.Forms.Label();
            this.tbHexARGB = new System.Windows.Forms.TextBox();
            this.lbHexARGB = new System.Windows.Forms.Label();
            this.tbHSB = new System.Windows.Forms.TextBox();
            this.lbHSB = new System.Windows.Forms.Label();
            this.tbHSV = new System.Windows.Forms.TextBox();
            this.lbHSV = new System.Windows.Forms.Label();
            this.pnColor = new System.Windows.Forms.Panel();
            this.tbHSL = new System.Windows.Forms.TextBox();
            this.lbHSL = new System.Windows.Forms.Label();
            this.tbCMYK = new System.Windows.Forms.TextBox();
            this.lbCMYK = new System.Windows.Forms.Label();
            this.cdColors = new System.Windows.Forms.ColorDialog();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbColorFromArgb
            // 
            this.lbColorFromArgb.AutoSize = true;
            this.lbColorFromArgb.Location = new System.Drawing.Point(12, 22);
            this.lbColorFromArgb.Name = "lbColorFromArgb";
            this.lbColorFromArgb.Size = new System.Drawing.Size(40, 13);
            this.lbColorFromArgb.TabIndex = 0;
            this.lbColorFromArgb.Text = "ARGB:";
            // 
            // tbColorFromArgb
            // 
            this.tbColorFromArgb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbColorFromArgb.Location = new System.Drawing.Point(12, 38);
            this.tbColorFromArgb.Name = "tbColorFromArgb";
            this.tbColorFromArgb.Size = new System.Drawing.Size(240, 20);
            this.tbColorFromArgb.TabIndex = 2;
            // 
            // tbHexRGB
            // 
            this.tbHexRGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHexRGB.Location = new System.Drawing.Point(12, 84);
            this.tbHexRGB.Name = "tbHexRGB";
            this.tbHexRGB.Size = new System.Drawing.Size(240, 20);
            this.tbHexRGB.TabIndex = 4;
            // 
            // lbHexRGB
            // 
            this.lbHexRGB.AutoSize = true;
            this.lbHexRGB.Location = new System.Drawing.Point(12, 68);
            this.lbHexRGB.Name = "lbHexRGB";
            this.lbHexRGB.Size = new System.Drawing.Size(61, 13);
            this.lbHexRGB.TabIndex = 3;
            this.lbHexRGB.Text = "Hex (RGB):";
            // 
            // tbHexARGB
            // 
            this.tbHexARGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHexARGB.Location = new System.Drawing.Point(12, 130);
            this.tbHexARGB.Name = "tbHexARGB";
            this.tbHexARGB.Size = new System.Drawing.Size(240, 20);
            this.tbHexARGB.TabIndex = 6;
            // 
            // lbHexARGB
            // 
            this.lbHexARGB.AutoSize = true;
            this.lbHexARGB.Location = new System.Drawing.Point(12, 114);
            this.lbHexARGB.Name = "lbHexARGB";
            this.lbHexARGB.Size = new System.Drawing.Size(68, 13);
            this.lbHexARGB.TabIndex = 5;
            this.lbHexARGB.Text = "Hex (ARGB):";
            // 
            // tbHSB
            // 
            this.tbHSB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHSB.Location = new System.Drawing.Point(12, 176);
            this.tbHSB.Name = "tbHSB";
            this.tbHSB.Size = new System.Drawing.Size(240, 20);
            this.tbHSB.TabIndex = 8;
            // 
            // lbHSB
            // 
            this.lbHSB.AutoSize = true;
            this.lbHSB.Location = new System.Drawing.Point(12, 160);
            this.lbHSB.Name = "lbHSB";
            this.lbHSB.Size = new System.Drawing.Size(32, 13);
            this.lbHSB.TabIndex = 7;
            this.lbHSB.Text = "HSB:";
            // 
            // tbHSV
            // 
            this.tbHSV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHSV.Location = new System.Drawing.Point(12, 222);
            this.tbHSV.Name = "tbHSV";
            this.tbHSV.Size = new System.Drawing.Size(240, 20);
            this.tbHSV.TabIndex = 10;
            // 
            // lbHSV
            // 
            this.lbHSV.AutoSize = true;
            this.lbHSV.Location = new System.Drawing.Point(12, 206);
            this.lbHSV.Name = "lbHSV";
            this.lbHSV.Size = new System.Drawing.Size(32, 13);
            this.lbHSV.TabIndex = 9;
            this.lbHSV.Text = "HSV:";
            // 
            // pnColor
            // 
            this.pnColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnColor.Location = new System.Drawing.Point(258, 12);
            this.pnColor.Name = "pnColor";
            this.pnColor.Size = new System.Drawing.Size(153, 322);
            this.pnColor.TabIndex = 11;
            this.pnColor.Click += new System.EventHandler(this.PnColor_Click);
            // 
            // tbHSL
            // 
            this.tbHSL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHSL.Location = new System.Drawing.Point(12, 268);
            this.tbHSL.Name = "tbHSL";
            this.tbHSL.Size = new System.Drawing.Size(240, 20);
            this.tbHSL.TabIndex = 13;
            // 
            // lbHSL
            // 
            this.lbHSL.AutoSize = true;
            this.lbHSL.Location = new System.Drawing.Point(12, 252);
            this.lbHSL.Name = "lbHSL";
            this.lbHSL.Size = new System.Drawing.Size(31, 13);
            this.lbHSL.TabIndex = 12;
            this.lbHSL.Text = "HSL:";
            // 
            // tbCMYK
            // 
            this.tbCMYK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCMYK.Location = new System.Drawing.Point(12, 314);
            this.tbCMYK.Name = "tbCMYK";
            this.tbCMYK.Size = new System.Drawing.Size(237, 20);
            this.tbCMYK.TabIndex = 15;
            // 
            // lbCMYK
            // 
            this.lbCMYK.AutoSize = true;
            this.lbCMYK.Location = new System.Drawing.Point(12, 298);
            this.lbCMYK.Name = "lbCMYK";
            this.lbCMYK.Size = new System.Drawing.Size(40, 13);
            this.lbCMYK.TabIndex = 14;
            this.lbCMYK.Text = "CMYK:";
            // 
            // cdColors
            // 
            this.cdColors.AnyColor = true;
            this.cdColors.FullOpen = true;
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(336, 340);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 16;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // FormPickAColor
            // 
            this.AcceptButton = this.btClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(423, 375);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tbCMYK);
            this.Controls.Add(this.lbCMYK);
            this.Controls.Add(this.tbHSL);
            this.Controls.Add(this.lbHSL);
            this.Controls.Add(this.pnColor);
            this.Controls.Add(this.tbHSV);
            this.Controls.Add(this.lbHSV);
            this.Controls.Add(this.tbHSB);
            this.Controls.Add(this.lbHSB);
            this.Controls.Add(this.tbHexARGB);
            this.Controls.Add(this.lbHexARGB);
            this.Controls.Add(this.tbHexRGB);
            this.Controls.Add(this.lbHexRGB);
            this.Controls.Add(this.tbColorFromArgb);
            this.Controls.Add(this.lbColorFromArgb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPickAColor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pick a color";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbColorFromArgb;
        private System.Windows.Forms.TextBox tbColorFromArgb;
        private System.Windows.Forms.TextBox tbHexRGB;
        private System.Windows.Forms.Label lbHexRGB;
        private System.Windows.Forms.TextBox tbHexARGB;
        private System.Windows.Forms.Label lbHexARGB;
        private System.Windows.Forms.TextBox tbHSB;
        private System.Windows.Forms.Label lbHSB;
        private System.Windows.Forms.TextBox tbHSV;
        private System.Windows.Forms.Label lbHSV;
        private System.Windows.Forms.Panel pnColor;
        private System.Windows.Forms.TextBox tbHSL;
        private System.Windows.Forms.Label lbHSL;
        private System.Windows.Forms.TextBox tbCMYK;
        private System.Windows.Forms.Label lbCMYK;
        private System.Windows.Forms.ColorDialog cdColors;
        private System.Windows.Forms.Button btClose;
    }
}