using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AnalyzerServiceApplication
{
    public partial class Main : Form
    {
        private int runtimeOfServiceInMinutes = 0;
        private DateTime serviceStartTime;
        private DateTime serviceEndTime;
        Analyzer.WebCrawler.WebScraperService webScraperService = new Analyzer.WebCrawler.WebScraperService();
        public Main()
        {
            InitializeComponent();
        }

        private void bStartService_Click(object sender, EventArgs e)
        {
            this.serviceStartTime = DateTime.Now;
            this.timer1.Start();
            runtimeOfServiceInMinutes = 0;
            this.lblRuntime.Text = runtimeOfServiceInMinutes.ToString();
            this.lblServiceStatus.Text = "Running";
            this.lblServiceStatus.ForeColor = Color.Green;

            this.webScraperService.StartScraping(this.serviceStartTime);
        }

        private void bStopService_Click(object sender, EventArgs e)
        {
            this.serviceEndTime = DateTime.Now;
            this.timer1.Stop();
            this.lblServiceStatus.Text = "Stopped";
            this.lblServiceStatus.ForeColor = Color.Red;
            this.webScraperService.StopScraping(this.serviceStartTime, this.serviceEndTime, this.runtimeOfServiceInMinutes);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            runtimeOfServiceInMinutes += 1;
            this.lblRuntime.Text = runtimeOfServiceInMinutes.ToString();
        }

        private void lblRuntime_TextChanged(object sender, EventArgs e)
        {
            this.lblItemsProcessed.Text = this.webScraperService.ItemsProcessedCounter.ToString();
            this.lblItemsFailed.Text = this.webScraperService.ItemsFailedCounter.ToString();
        }
    }
}
