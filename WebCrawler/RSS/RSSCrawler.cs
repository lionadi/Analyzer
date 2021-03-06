﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using Analyzer.Common;

namespace Analyzer.WebCrawler.RSS
{
    public class RSSCrawler
    {
        public static List<FeedItem> ProcessRSSFeed(String rssFeedURL, int daysOldFilter)
        {
            List<FeedItem> itemsList = null;
            try
            {


                XDocument rssFeed = XDocument.Load(rssFeedURL);

                var posts = from rssItem in rssFeed.Descendants("item")

                            select new

                            {
                                Title = rssItem.Element("title").Value,

                                Published = rssItem.Element("pubDate").Value.ParseDateTime(),

                                Url = rssItem.Element("link").Value,

                                //Content = rssItem.Element("content:encoded").Value,

                                Description = rssItem.Element("description").Value

                            };


                var newPosts = from post in posts where (DateTime.Now - post.Published).Days < daysOldFilter select post;


                itemsList = new List<FeedItem>();

                foreach (var item in newPosts)
                {
                    itemsList.Add(new FeedItem { Title = item.Title, Published = item.Published, Url = item.Url, Content = item.Description });
                }

                

                
            } catch(Exception ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in processing given RSS Feed: " + rssFeedURL, ex);
                itemsList = null;
            }

            return itemsList;
        }

        public static List<FeedItem> ProcessRSSFeed(String rssFeedURL, DateTime startDate)
        {
            List<FeedItem> itemsList = null;
            try
            {


                XDocument rssFeed = XDocument.Load(rssFeedURL);

                var posts = from rssItem in rssFeed.Descendants("item")

                            select new

                            {

                                Title = rssItem.Element("title").Value,

                                Published = rssItem.Element("pubDate").Value.ParseDateTime(),

                                Url = rssItem.Element("link").Value,
                                Description = rssItem.Element("description").Value

                            };


                var newPosts = from post in posts where post.Published > startDate select post;


                itemsList = new List<FeedItem>();

                foreach (var item in newPosts)
                {
                    itemsList.Add(new FeedItem { Title = item.Title, Published = item.Published, Url = item.Url, Content = item.Description });
                }




            }
            catch (Exception ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in processing given RSS Feed: " + rssFeedURL, ex);
                itemsList = null;
            }

            return itemsList;
        }

        public static List<FeedItem> ProcessRSSFeed(String rssFeedURL, DateTime startDate, DateTime endDate)
        {
            List<FeedItem> itemsList = null;
            try
            {


                XDocument rssFeed = XDocument.Load(rssFeedURL);

                var posts = from rssItem in rssFeed.Descendants("item")

                            select new

                            {

                                Title = rssItem.Element("title").Value,

                                Published = rssItem.Element("pubDate").Value.ParseDateTime(),

                                Url = rssItem.Element("link").Value,
                                Description = rssItem.Element("description").Value

                            };


                var newPosts = from post in posts where post.Published > startDate && post.Published < endDate select post;


                itemsList = new List<FeedItem>();

                foreach (var item in newPosts)
                {
                    itemsList.Add(new FeedItem { Title = item.Title, Published = item.Published, Url = item.Url, Content = item.Description });
                }




            }
            catch (Exception ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in processing given RSS Feed: " + rssFeedURL, ex);
                itemsList = null;
            }

            return itemsList;
        }
    }
}
