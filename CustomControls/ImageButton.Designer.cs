
namespace CustomControls
{
    partial class ImageButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbButtonText = new System.Windows.Forms.Label();
            this.pnImage = new System.Windows.Forms.Panel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpMain.Controls.Add(this.lbButtonText, 0, 0);
            this.tlpMain.Controls.Add(this.pnImage, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(100, 23);
            this.tlpMain.TabIndex = 0;
            this.tlpMain.Click += new System.EventHandler(this.DelegateClick);
            // 
            // lbButtonText
            // 
            this.lbButtonText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbButtonText.Location = new System.Drawing.Point(3, 0);
            this.lbButtonText.Name = "lbButtonText";
            this.lbButtonText.Size = new System.Drawing.Size(71, 23);
            this.lbButtonText.TabIndex = 0;
            this.lbButtonText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbButtonText.Click += new System.EventHandler(this.DelegateClick);
            // 
            // pnImage
            // 
            this.pnImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnImage.Location = new System.Drawing.Point(77, 0);
            this.pnImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnImage.Name = "pnImage";
            this.pnImage.Size = new System.Drawing.Size(23, 23);
            this.pnImage.TabIndex = 1;
            this.pnImage.Click += new System.EventHandler(this.DelegateClick);
            // 
            // ImageButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tlpMain);
            this.Name = "ImageButton";
            this.Size = new System.Drawing.Size(100, 23);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbButtonText;
        private System.Windows.Forms.Panel pnImage;
    }
}
