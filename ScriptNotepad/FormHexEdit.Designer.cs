namespace ScriptNotepad
{
    partial class FormHexEdit
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHexEdit));
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.hexEditor = new WpfHexaEditor.HexEditor();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.elementHost.Location = new System.Drawing.Point(0, 41);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(868, 396);
            this.elementHost.TabIndex = 1;
            this.elementHost.Text = "elementHost";
            this.elementHost.Child = this.hexEditor;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Open TBL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // FormHexEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 437);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.elementHost);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHexEdit";
            this.Text = "WpfHexEditor Winform sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost;
        private WpfHexaEditor.HexEditor hexEditor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
