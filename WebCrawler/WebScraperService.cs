using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.WebCrawler.RSS;
using Analyzer.WebCrawler.Web;

namespace Analyzer.WebCrawler
{
    public class WebScraperService
    {
        private bool isServiceScraping = false;
        public void StartScraping(DateTime startTime)
        {
            this.isServiceScraping = true;
            Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWarning("Web Scraping service started. Start time: " + startTime);
        }

        public void StopScraping(DateTime startTime, DateTime endTime, int runtimeInMinutes)
        {
            this.isServiceScraping = false;
            Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWarning("Web Scraping service stop requested. Start time: " + startTime + " End Time: " + endTime + " Runtime in minutes: " + runtimeInMinutes);
        }

        /// <summary>
        /// This is the main function for the service which will operate the scraping
        /// </summary>
        private void ScraperOperator()
        {
            //var rssItems = RSSCrawler.ProcessRSSFeed(this.tbURL.Text, 60);
            //foreach (var rssItem in rssItems)
            //{
            //    this.tbResult.Text += "\n" + Analyzer.WebCrawler.Web.WebCrawler.ProcessWordpressArticle(rssItem.Url).Result;
            //}
        }
    }
}
