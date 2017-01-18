namespace AnalyzerConfiguratorApplication
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.tbConfigurationFileLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgwWebSourcesConfigs = new System.Windows.Forms.DataGridView();
            this.bLoadConfigurations = new System.Windows.Forms.Button();
            this.bSaveConfigurations = new System.Windows.Forms.Button();
            this.sourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CssSelector = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CrawlerType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwWebSourcesConfigs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tbConfigurationFileLocation
            // 
            this.tbConfigurationFileLocation.Location = new System.Drawing.Point(15, 25);
            this.tbConfigurationFileLocation.Name = "tbConfigurationFileLocation";
            this.tbConfigurationFileLocation.Size = new System.Drawing.Size(450, 20);
            this.tbConfigurationFileLocation.TabIndex = 1;
            this.tbConfigurationFileLocation.Text = "WebSources.conf";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Configuration file location:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bSaveConfigurations);
            this.panel1.Controls.Add(this.bLoadConfigurations);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbConfigurationFileLocation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 54);
            this.panel1.TabIndex = 3;
            // 
            // dgwWebSourcesConfigs
            // 
            this.dgwWebSourcesConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwWebSourcesConfigs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.URL,
            this.CssSelector,
            this.SourceType,
            this.CrawlerType});
            this.dgwWebSourcesConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwWebSourcesConfigs.Location = new System.Drawing.Point(0, 54);
            this.dgwWebSourcesConfigs.Name = "dgwWebSourcesConfigs";
            this.dgwWebSourcesConfigs.Size = new System.Drawing.Size(639, 502);
            this.dgwWebSourcesConfigs.TabIndex = 4;
            // 
            // bLoadConfigurations
            // 
            this.bLoadConfigurations.Location = new System.Drawing.Point(471, 25);
            this.bLoadConfigurations.Name = "bLoadConfigurations";
            this.bLoadConfigurations.Size = new System.Drawing.Size(75, 23);
            this.bLoadConfigurations.TabIndex = 3;
            this.bLoadConfigurations.Text = "Load";
            this.bLoadConfigurations.UseVisualStyleBackColor = true;
            this.bLoadConfigurations.Click += new System.EventHandler(this.bLoadConfigurations_Click);
            // 
            // bSaveConfigurations
            // 
            this.bSaveConfigurations.Location = new System.Drawing.Point(552, 25);
            this.bSaveConfigurations.Name = "bSaveConfigurations";
            this.bSaveConfigurations.Size = new System.Drawing.Size(75, 23);
            this.bSaveConfigurations.TabIndex = 4;
            this.bSaveConfigurations.Text = "Save";
            this.bSaveConfigurations.UseVisualStyleBackColor = true;
            this.bSaveConfigurations.Click += new System.EventHandler(this.bSaveConfigurations_Click);
            // 
            // sourceBindingSource
            // 
            this.sourceBindingSource.DataSource = typeof(Analyzer.Common.Configuration.Crawler.Source);
            // 
            // URL
            // 
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            // 
            // CssSelector
            // 
            this.CssSelector.HeaderText = "CssSelector";
            this.CssSelector.Name = "CssSelector";
            // 
            // SourceType
            // 
            this.SourceType.HeaderText = "SourceType";
            this.SourceType.Items.AddRange(new object[] {
            "Common",
            "WordPress",
            "Facebook",
            "Twitter",
            "Yahoo"});
            this.SourceType.Name = "SourceType";
            // 
            // CrawlerType
            // 
            this.CrawlerType.HeaderText = "CrawlerType";
            this.CrawlerType.Items.AddRange(new object[] {
            "RSS",
            "Web"});
            this.CrawlerType.Name = "CrawlerType";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 556);
            this.Controls.Add(this.dgwWebSourcesConfigs);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Analyzer Scraper Configurator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.DoubleClick += new System.EventHandler(this.Main_DoubleClick);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwWebSourcesConfigs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource sourceBindingSource;
        private System.Windows.Forms.TextBox tbConfigurationFileLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgwWebSourcesConfigs;
        private System.Windows.Forms.Button bSaveConfigurations;
        private System.Windows.Forms.Button bLoadConfigurations;
        private System.Windows.Forms.DataGridViewTextBoxColumn URL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CssSelector;
        private System.Windows.Forms.DataGridViewComboBoxColumn SourceType;
        private System.Windows.Forms.DataGridViewComboBoxColumn CrawlerType;
    }
}

