using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Analyzer.Common.Configuration.Crawler;

namespace Analyzer.WebCrawler.RSS
{
    [DataContract]

    public class FeedItem : Analyzer.WebCrawler.DataItemBase

    {
        [DataMember]
        public int NumComments { get; set; }

        [DataMember]
        public SourceType SourceType { get; set; }

        /// <summary>
        /// Use this value to determine if the feed item has been processed. If it is a new date than this then it must be processed.
        /// </summary>
        //[DataMember]
        //public DateTime ProcessingTimeLimit { get; set; }

    }
}
