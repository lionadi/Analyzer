using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace WebCrawler.Web
{
    public class WebCrawler
    {
        public static async Task<PageItem> ProcessWebPage(String pageURL, String cssSelector)
        {
            PageItem pageItem = null;

            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = pageURL;
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(address);
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

                pageItem.Content = sb.ToString();
                pageItem.Url = pageURL;
            }

            return pageItem;
        }
    }
}
