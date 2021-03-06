﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using System.Net;
using AngleSharp.Parser.Html;
using Analyzer.Common;

namespace Analyzer.WebCrawler.Web
{
    public class WebCrawler
    {
        /// <summary>
        /// Use this to get the whole page content
        /// </summary>
        /// <param name="pageURL"></param>
        /// <returns></returns>
        public static PageItem ProcessWebPage(String pageURL)
        {
            PageItem pageItem = null;
            try
            {
                // Setup the configuration to support document loading
                var config = Configuration.Default.WithDefaultLoader();
                // Load the names of all The Big Bang Theory episodes from Wikipedia
                using (GZipWebClient client = new GZipWebClient())
                {
                    client.Headers.Add("user-agent", Analyzer.Common.Configuration.ConfigurationManager.AppSettings.UserAgentOptions.GetRandom());
                    pageItem = new PageItem();
                    string webLocationContent = client.DownloadString(pageURL);
                    pageItem.Content = Analyzer.Common.HtmlRemoval.StripTagsCharArray(webLocationContent);
                    pageItem.Url = pageURL;
                }
            }
            catch (Exception ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in processing given wen URL: " + pageURL, ex);
                pageItem = null;
            }


            return pageItem;
        }

        public static PageItem ProcessWebPage(String pageURL, String cssSelector)
        {
            PageItem pageItem = null;
            try
            {
                // Setup the configuration to support document loading
                var config = Configuration.Default.WithDefaultLoader();
                // Load the names of all The Big Bang Theory episodes from Wikipedia
                using (GZipWebClient client = new GZipWebClient())
                {
                    client.Headers.Add("user-agent", Analyzer.Common.Configuration.ConfigurationManager.AppSettings.UserAgentOptions.GetRandom());
                    string webLocationContent = client.DownloadString(pageURL);
                    var parser = new HtmlParser();
                    var document = parser.Parse(webLocationContent);
                    // This CSS selector gets the desired content
                    var cellSelector = cssSelector;
                    // Perform the query to get all cells with the content
                    var cells = document.QuerySelectorAll(cellSelector);
                    // We are only interested in the text - select it with LINQ
                    var pageContent = cells.Select(m => m.TextContent);
                    if (pageContent != null && pageContent.Count() > 0)
                    {
                        pageItem = new PageItem();
                        StringBuilder sb = new StringBuilder();
                        foreach (var pageContentElement in pageContent)
                            sb.AppendLine(pageContentElement);

                        pageItem.Content = Analyzer.Common.HtmlRemoval.StripTagsCharArray(sb.ToString());
                        pageItem.Url = pageURL;
                    }
                }
            }
            catch (Exception ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in processing given wen URL: " + pageURL, ex);
                pageItem = null;
            }


            return pageItem;
        }

        public static PageItem ProcessWordpressArticle(String pageURL)
        {
            PageItem pageItem = null;

            try
            {


                // Setup the configuration to support document loading
                var config = Configuration.Default.WithDefaultLoader();
                // Load the names of all The Big Bang Theory episodes from Wikipedia
                var address = pageURL;

                using (GZipWebClient client = new GZipWebClient())
                {
                    client.Headers.Add("user-agent", Analyzer.Common.Configuration.ConfigurationManager.AppSettings.UserAgentOptions.GetRandom());
                    string webLocationContent = client.DownloadString(pageURL);
                    var parser = new HtmlParser();
                    var document = parser.Parse(webLocationContent);
                    // Asynchronously get the document in a new context using the configuration
                    //var document = await BrowsingContext.New(config).OpenAsync(address);
                    var article = document.All.SingleOrDefault(o => o.LocalName == "article");
                    var title = article.QuerySelectorAll("h1.entry-title");
                    var content = article.QuerySelectorAll("div.entry-content");



                    {
                        pageItem = new PageItem();


                        pageItem.Title = title.First().TextContent;
                        pageItem.Content = Analyzer.Common.HtmlRemoval.StripTagsCharArray(content.First().TextContent);
                        pageItem.Url = pageURL;
                    }
                }
            } catch(Exception ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in processing given wen URL: " + pageURL, ex);
                pageItem = null;
            }
            

            return pageItem;
        }
    }
}
