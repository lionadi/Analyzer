using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.WebCrawler.RSS;
using Analyzer.WebCrawler.Web;
using Analyzer.Common.Configuration;
using Analyzer.Common.Configuration.Crawler;
using System.Timers;
using Analyzer.Common;

namespace Analyzer.WebCrawler
{
    public class WebScraperService
    {
        public WebScraperService()
        {
            this.scrapingIntervals = new Timer(ConfigurationManager.AppSettings.WebScrapingTimeIntervalsInMinutes);
            this.scrapingIntervals.Elapsed += ScrapingIntervals_Elapsed;
            this.scrapingIntervals.AutoReset = true;
        }

        private void ScrapingIntervals_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Run this as long as the timer intervals hit and as long as the hasn't been a request to stop the scraping
            this.ScraperOperator();
        }

        /// <summary>
        /// If this value is set to false the scraping process will stop.
        /// </summary>
        private bool isServiceScraping = false;
        List<Source> webSources = new List<Source>();
        Timer scrapingIntervals = null;
        public void StartScraping(DateTime startTime)
        {
            this.isServiceScraping = true;
            Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("Web Scraping service started. Start time: " + startTime);

            this.webSources = Analyzer.Common.Configuration.ConfigurationManager.GetConfiguredSources<Source>(ConfigurationManager.AppSettings.WebScrapingConfigurationFileLocation);
            // Start the timer
            this.scrapingIntervals.Enabled = true;
        }
    

        public void StopScraping(DateTime startTime, DateTime endTime, int runtimeInMinutes)
        {
            this.isServiceScraping = false;
            Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("Web Scraping service stop requested. Start time: " + startTime + " End Time: " + endTime + " Runtime in minutes: " + runtimeInMinutes);
            this.scrapingIntervals.Enabled = false;
        }
        public bool IisServiceScraping
        {
            get
            {
                return this.isServiceScraping;
            }
        }

        /// <summary>
        /// This is the main function for the service which will operate the scraping
        /// </summary>
        private void ScraperOperator()
        {
            try
            {


                // Processing web pages based on RSS feeds
                //-------------------------------------------------------

                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("START: web scraping iteration started at: " + DateTime.Now);

                // Stop the timer until the current scraping iteration has finnished. No need to overlapp
                this.scrapingIntervals.Enabled = false;

                this.RSSFeedScraperOperator();


                // Start the timer to wait for the next iteration
                this.scrapingIntervals.Enabled = true;

                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("END: web scraping iteration ended at: " + DateTime.Now);

                //-------------------------------------------------------
            } catch(Exception ex)
            {
                this.scrapingIntervals.Enabled = false;
                this.isServiceScraping = false;
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("ERROR: Scraping high level failure. Stopping scraping at: " + DateTime.Now, ex);
            }
        }

        private async void RSSFeedScraperOperator()
        {
            Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("START: RSS Feed scraping iteration started at: " + DateTime.Now);
            // this is a web location scraping queue. Use this to randomly select locations from the scraping queue to avoid being pinned as a denial of service attack.
            // The idea is to process other sites while there is going to be a time interval untill the same site is processed
            // We don't want to cause problems for the site and do not want our function to be interupted.
            // NOTICE: This might not work if you are using a web service, this is presently designed for web URLs
            List<RSS.FeedItem> rssFeedsWebLocationsToProcess = new List<RSS.FeedItem>();

            var rssFeeds = this.webSources.Where(o => o.CrawlerType == CrawlerType.RSS);

            foreach (var rssFeed in rssFeeds)
            {
                // Check to see if the scraping is to be stopped
                if (!this.isServiceScraping)
                    break;

                var rssItems = RSSCrawler.ProcessRSSFeed(rssFeed.URL, ConfigurationManager.AppSettings.RSSFeedHistoryRangeForScrapingInDays);
                if (rssItems != null)
                {
                    foreach (var rssItem in rssItems)
                    {
                        // Check to see if the scraping is to be stopped
                        if (!this.isServiceScraping)
                            break;

                        rssItem.SourceType = rssFeed.SourceType;
                        rssFeedsWebLocationsToProcess.Add(rssItem);
                    }
                }
                else
                {
                    Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("WARNING: Unable to process rss feed: " + rssFeed.URL);
                }
            }

            // Shuftle the location to get a randomized processing queue, helps making a more clear and reliable code
            rssFeedsWebLocationsToProcess.Shuffle();
            // Process the web location queue
            foreach (var rssItem in rssFeedsWebLocationsToProcess)
            {
                // Check to see if the scraping is to be stopped
                if (!this.isServiceScraping)
                    break;

                PageItem webLocationData = null;

                switch(rssItem.SourceType)
                {
                    case SourceType.WordPress:
                        {
                            webLocationData = Analyzer.WebCrawler.Web.WebCrawler.ProcessWordpressArticle(rssItem.Url);
                        } break;

                    case SourceType.Facebook:
                        {
                            
                        } break;

                    case SourceType.Twitter:
                        {

                        }
                        break;

                    case SourceType.Common:
                        {
                            webLocationData = Analyzer.WebCrawler.Web.WebCrawler.ProcessWebPage(rssItem.Url);
                        }
                        break;

                    default:
                        {
                            webLocationData = Analyzer.WebCrawler.Web.WebCrawler.ProcessWebPage(rssItem.Url);
                        } break;
                }
                
                if (webLocationData != null)
                {
                    var result = await Analyzer.Common.Database.DatabaseService.GetInstance().AddtoWriteQueueAsync<Analyzer.Common.Database.DataItems.WebData>(rssItem.SourceType.ToString(), webLocationData.ToWebData());
                    if (!result)
                    {
                        Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("WARNING: Unable to add the web location to the database queue: " + rssItem.Url);
                    }
                }
                else
                {
                    Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("WARNING: Unable to process web location: " + rssItem.Url);
                }
            }
            Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteWebScrapingInformation("ENDED: RSS Feed scraping iteration started at: " + DateTime.Now);
        }
    }
}
