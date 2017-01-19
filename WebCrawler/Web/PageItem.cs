using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Common.Database.DataItems;

namespace Analyzer.WebCrawler.Web
{
    [DataContract]
    public class PageItem : Analyzer.WebCrawler.DataItemBase
    {
        [DataMember]
        public String Content { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public WebData ToWebData()
        {
            WebData webData = new WebData();

            webData.Content = this.Content;
            webData.URL = this.Url;
            webData.Published = this.Published;
            webData.Title = this.Title;
            webData.Category = this.Category;

            return webData;
        }
    }
}
