namespace ScriptNotepad.PluginHandling
{
    partial class FormPluginManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPluginManage));
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPluginVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPluginDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvPlugins
            // 
            this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colPluginName,
            this.colPluginVersion,
            this.colPluginDescription});
            this.lvPlugins.Location = new System.Drawing.Point(12, 12);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.Size = new System.Drawing.Size(650, 204);
            this.lvPlugins.TabIndex = 0;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            // 
            // colFileName
            // 
            this.colFileName.Text = "File name";
            this.colFileName.Width = 154;
            // 
            // colPluginName
            // 
            this.colPluginName.Text = "Plug-in name";
            this.colPluginName.Width = 100;
            // 
            // colPluginVersion
            // 
            this.colPluginVersion.Text = "Version";
            this.colPluginVersion.Width = 58;
            // 
            // colPluginDescription
            // 
            this.colPluginDescription.Text = "Description";
            this.colPluginDescription.Width = 178;
            // 
            // FormPluginManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvPlugins);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPluginManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormPluginManage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPlugins;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colPluginName;
        private System.Windows.Forms.ColumnHeader colPluginVersion;
        private System.Windows.Forms.ColumnHeader colPluginDescription;
    }
}