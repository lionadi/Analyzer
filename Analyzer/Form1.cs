using AngleSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCrawler.RSS;


namespace Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bCrawlURL_Click(object sender, EventArgs e)
        {
            

            //foreach(var title in titles)
            //    this.tbResult.Text += title;

            var rssItems = RSSCrawler.ProcessRSSFeed(this.tbURL.Text, 60);
        }
    }
}
