using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analyzer.Common.Configuration.Crawler;

namespace AnalyzerConfiguratorApplication
{
    public partial class Main : Form
    {
        List<Analyzer.Common.Configuration.Crawler.Source> webSources = null;
        public Main()
        {
            InitializeComponent();
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.dgwWebSourcesConfigs.DataSource = this.webSources;
            //this.dgwWebSourcesConfigs.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            
        }

        private void Main_DoubleClick(object sender, EventArgs e)
        {
            
            //this.dgwWebSourcesConfigs.Rows.Add();
        }

        private void bLoadConfigurations_Click(object sender, EventArgs e)
        {
            this.webSources = Analyzer.Common.Configuration.ConfigurationManager.GetConfiguredSources<Analyzer.Common.Configuration.Crawler.Source>(this.tbConfigurationFileLocation.Text);
            if (this.webSources != null && this.webSources.Count > 0)
            {
                this.dgwWebSourcesConfigs.Rows.Clear();
                foreach (var source in this.webSources)
                {
                    int index = this.dgwWebSourcesConfigs.Rows.Add();
                    this.dgwWebSourcesConfigs.Rows[index].Cells["Title"].Value = source.Title;
                    this.dgwWebSourcesConfigs.Rows[index].Cells["URL"].Value = source.URL;
                    this.dgwWebSourcesConfigs.Rows[index].Cells["CrawlerType"].Value = source.CrawlerType.ToString();
                    this.dgwWebSourcesConfigs.Rows[index].Cells["CssSelector"].Value = source.CssSelector;
                    this.dgwWebSourcesConfigs.Rows[index].Cells["SourceType"].Value = source.SourceType.ToString();
                }
            }
        }

        private void bSaveConfigurations_Click(object sender, EventArgs e)
        {
            if (this.dgwWebSourcesConfigs.Rows.Count > 0)
            {
                if (this.webSources == null)
                    this.webSources = new List<Analyzer.Common.Configuration.Crawler.Source>();
                else if (this.webSources.Count > 0)
                    this.webSources.Clear();

                foreach (DataGridViewRow row in this.dgwWebSourcesConfigs.Rows)
                {
                    if (row.Cells["URL"].Value != null)
                    {
                        this.webSources.Add(new Analyzer.Common.Configuration.Crawler.Source()
                        {
                            Title = (String)row.Cells["Title"].Value,
                            URL = (String)row.Cells["URL"].Value,
                            CssSelector = (String)row.Cells["CssSelector"].Value,
                            CrawlerType = (CrawlerType)Enum.Parse(Analyzer.Common.Configuration.Crawler.CrawlerType.RSS.GetType(), (string)row.Cells["CrawlerType"].Value),
                            SourceType = (SourceType)Enum.Parse(Analyzer.Common.Configuration.Crawler.SourceType.Common.GetType(), (string)row.Cells["SourceType"].Value)
                        });
                    }
                }
                Analyzer.Common.Configuration.ConfigurationManager.WriteConfiguredSources<Source>(this.webSources, this.tbConfigurationFileLocation.Text);

            }
        }
    }
}
